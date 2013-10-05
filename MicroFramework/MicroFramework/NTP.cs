using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Time;
using System.Net;
using System.Net.Sockets;

namespace MicroFramework
{
    class NTP
    {
        IPHostEntry ipHostPrimary = null;
        IPHostEntry ipHostSecondary = null;
        IPAddress primaryAddress = null;
        IPAddress secondaryAddress = null;
        string primaryHost;
        string secondaryHost;

        int TimeZoneMinutes = 60;
        int DaylightSavingTime = 60;

        public NTP(string primary, string secondary)
        {
            // Setup events to handle system
            Microsoft.SPOT.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
            TimeService.SystemTimeChanged += TimeService_SystemTimeChanged;
            TimeService.TimeSyncFailed += TimeService_TimeSyncFailed;

            // Setup class
            Setup(primary, secondary);
        }

        void NetworkChange_NetworkAvailabilityChanged(object sender, Microsoft.SPOT.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
            {
                // Must have lost connection, reset everything incase of startup without ethernet
                Setup(primaryHost, secondaryHost);
            }
            else
            {
                // No point in running without ethernet
                Stop();
            }
        }

        public void Start()
        {
            TimeService.Start();
        }

        public void Stop()
        {
            TimeService.Stop();
        }

        public void Setup(string primary, string secondary)
        {
            try
            {
                // Stopping, new setup
                Stop();

                primaryHost = primary;
                secondaryHost = secondary;

                ipHostPrimary = Dns.GetHostEntry(primary);
                primaryAddress = ipHostPrimary.AddressList[0];

                ipHostSecondary = Dns.GetHostEntry(secondary);
                secondaryAddress = ipHostSecondary.AddressList[0];

                TimeServiceSettings settings = new TimeServiceSettings();
                settings.PrimaryServer = primaryAddress.GetAddressBytes();
                settings.AlternateServer = secondaryAddress.GetAddressBytes();
                settings.AutoDayLightSavings = true; // Does nothing
                settings.ForceSyncAtWakeUp = true;
                settings.RefreshTime = 5;

                TimeService.Settings = settings;

                Start();
            }
            catch (SocketException e)
            {
                // Do nothing, SocketException, bad ethernet, event will reinitiate when its ready
            }
        }

        void TimeService_TimeSyncFailed(object sender, TimeSyncFailedEventArgs e)
        {
            Debug.Print(e.EventTime + " NTP Error: Error Syncronizing - " + e.ErrorCode);
        }

        void TimeService_SystemTimeChanged(object sender, SystemTimeChangedEventArgs e)
        {
            DateTime march = new DateTime(DateTime.Now.Year, 3, 1);
            DateTime october = new DateTime(DateTime.Now.Year, 10, 1);
            DateTime daylightstart = LastSundayInMonth(march, DayOfWeek.Sunday);
            DateTime daylightstop = LastSundayInMonth(october, DayOfWeek.Sunday);

            if (daylightstart.Ticks < DateTime.Now.Ticks && daylightstop.Ticks > DateTime.Now.Ticks)
                TimeService.SetTimeZoneOffset(TimeZoneMinutes + DaylightSavingTime);
            else
                TimeService.SetTimeZoneOffset(TimeZoneMinutes);
        }

        public DateTime LastSundayInMonth(DateTime time, DayOfWeek day)
        {
            DateTime lastDay = new DateTime(time.Year, time.Month+1, 1).AddDays(-1);
            int wDay = (int)day;
            int lDay = (int)lastDay.DayOfWeek;
            return lastDay.AddDays(lDay >= wDay ? wDay - lDay : wDay - lDay - 7);
        }
    }
}
