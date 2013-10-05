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

        int TimeZoneMinutes = 60;
        int DaylightSavingTime = 60;

        public NTP(string primary, string secondary)
        {
            Setup(primary, secondary);
            Start();
        }

        public void Start()
        {
            TimeService.Start();
        }

        public void Setup(string primary, string secondary)
        {
            ipHostPrimary = Dns.GetHostEntry(primary);
            primaryAddress = ipHostPrimary.AddressList[0];

            ipHostSecondary = Dns.GetHostEntry(secondary);
            secondaryAddress = ipHostSecondary.AddressList[0];

            TimeServiceSettings settings = new TimeServiceSettings();
            settings.PrimaryServer = primaryAddress.GetAddressBytes();
            settings.AlternateServer = secondaryAddress.GetAddressBytes();
            settings.AutoDayLightSavings = true; // Does nothing
            settings.ForceSyncAtWakeUp = true;
            settings.RefreshTime = 3600;

            TimeService.Settings = settings;

            TimeService.SystemTimeChanged += TimeService_SystemTimeChanged;
            TimeService.TimeSyncFailed += TimeService_TimeSyncFailed;
        }

        public void Stop()
        {
            TimeService.Stop();
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
