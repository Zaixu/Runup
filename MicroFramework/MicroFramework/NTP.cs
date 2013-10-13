using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Time;
using System.Net;
using System.Net.Sockets;

namespace MicroFramework
{
    /// <summary>
    /// Network Time Protocol Class - Syncronizes time through internet
    /// </summary>
    public class NTP
    {
        private IPHostEntry ipHostPrimary = null;
        private IPHostEntry ipHostSecondary = null;
        private IPAddress primaryAddress = null;
        private IPAddress secondaryAddress = null;
        private string primaryHost;
        private string secondaryHost;
        private bool running = false;
        private int TimeZoneMinutes = 60;
        private int DaylightSavingTime = 60;

        public NTP(string primary, string secondary)
        {
            primaryHost = primary;
            secondaryHost = secondary;
            // Setup events to handle system
            Microsoft.SPOT.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
            TimeService.SystemTimeChanged += TimeService_SystemTimeChanged;
            TimeService.TimeSyncFailed += TimeService_TimeSyncFailed;

            Start();
        }

        void NetworkChange_NetworkAvailabilityChanged(object sender, Microsoft.SPOT.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
            {
                // Must have lost connection, reset everything incase of startup without ethernet
                Start();
            }
            else
            {
                // No point in running without ethernet
                Stop();
            }
        }

        public void Start()
        {
            try
            {
                Debug.Print("Starting NTP");
                Stop();
                Setup();
                running = true;
                TimeService.Start();
            }
            catch
            {
                // Catch Connection Problem
                // Do nothing, SocketException, bad ethernet, event will reinitiate when its ready - Counting on Exception so TimeService.Start wont run if theres no ethernet
                Debug.Print("Stopping NTP - Unable to start up");
            }
        }

        public void Stop()
        {
            if (running)
            {
                Debug.Print("Stopping NTP");
                TimeService.Stop();
                running = false;
            }
        }

        private void Setup()
        {
                
                ipHostPrimary = Dns.GetHostEntry(primaryHost);
                primaryAddress = ipHostPrimary.AddressList[0];

                ipHostSecondary = Dns.GetHostEntry(secondaryHost);
                secondaryAddress = ipHostSecondary.AddressList[0];

                TimeServiceSettings settings = new TimeServiceSettings();
                settings.PrimaryServer = primaryAddress.GetAddressBytes();
                settings.AlternateServer = secondaryAddress.GetAddressBytes();
                settings.AutoDayLightSavings = true; // Does nothing
                settings.ForceSyncAtWakeUp = true;
                settings.RefreshTime = 3600;

                TimeService.Settings = settings;
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

        private DateTime LastSundayInMonth(DateTime time, DayOfWeek day)
        {
            DateTime lastDay = new DateTime(time.Year, time.Month+1, 1).AddDays(-1);
            int wDay = (int)day;
            int lDay = (int)lastDay.DayOfWeek;
            return lastDay.AddDays(lDay >= wDay ? wDay - lDay : wDay - lDay - 7);
        }
    }
}
