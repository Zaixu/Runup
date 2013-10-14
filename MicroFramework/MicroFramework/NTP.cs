using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Time;
using System.Net;
using System.Net.Sockets;

namespace MicroFramework
{
    /// <summary>
    /// Network Time Protocol Class - Syncronizes time through internet in its own thread
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

        /// <summary>
        /// Constructor - Saves primary/secondary host and subscribes to TimeService events
        /// </summary>
        /// <param name="primary">Primary Host String</param>
        /// <param name="secondary">Secondary Host String</param>
        public NTP(string primary, string secondary)
        {
            //Save hosts to class
            primaryHost = primary;
            secondaryHost = secondary;
            //Setup event to handle change of network
            Microsoft.SPOT.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
            //Subscribe to TimeService events to get info about changes and failures.
            TimeService.SystemTimeChanged += TimeService_SystemTimeChanged;
            TimeService.TimeSyncFailed += TimeService_TimeSyncFailed;
            //Start NTP Thread
            Start();
        }

        /// <summary>
        /// Network Availability Event from system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Start NTP Thread
        /// </summary>
        public void Start()
        {
            try
            {
                Debug.Print("Starting NTP");
                //Stop thread before starting it
                Stop();
                //Setup NTP addresses
                Setup();
                //Start TimeService
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

        /// <summary>
        /// Stop NTP Thread
        /// </summary>
        public void Stop()
        {
            //If its running, stop timeservice
            if (running)
            {
                Debug.Print("Stopping NTP");
                TimeService.Stop();
                running = false;
            }
        }

        /// <summary>
        /// Setup NTP Hosts and settings
        /// </summary>
        private void Setup()
        {
                //Get ip of primary host
                ipHostPrimary = Dns.GetHostEntry(primaryHost);
                primaryAddress = ipHostPrimary.AddressList[0];
                //Get ip of secondary host
                ipHostSecondary = Dns.GetHostEntry(secondaryHost);
                secondaryAddress = ipHostSecondary.AddressList[0];
                
                //Create TimeService settings with primary host and secondary host, refresh rate.
                TimeServiceSettings settings = new TimeServiceSettings();
                settings.PrimaryServer = primaryAddress.GetAddressBytes();
                settings.AlternateServer = secondaryAddress.GetAddressBytes();
                settings.AutoDayLightSavings = true; // Does nothing
                settings.ForceSyncAtWakeUp = true;
                settings.RefreshTime = 3600;
                //Set settings
                TimeService.Settings = settings;
        }

        /// <summary>
        /// TimeService TimeSyncFailed Event, incase theres trouble with TimeService, event gets called
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">TimeSyncFailedEvent Arguments</param>
        void TimeService_TimeSyncFailed(object sender, TimeSyncFailedEventArgs e)
        {
            //Write out error
            Debug.Print(e.EventTime + " NTP Error: Error Syncronizing - " + e.ErrorCode);
        }

        /// <summary>
        /// TimeService SystemTimeChanged Event, when the system time changes, we need to set offset depending on daylight savings
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">SystemTimeChangedEvent Arguments</param>
        void TimeService_SystemTimeChanged(object sender, SystemTimeChangedEventArgs e)
        {
            //Daylight savings are the last sunday of march and october
            DateTime march = new DateTime(DateTime.Now.Year, 3, 1);
            DateTime october = new DateTime(DateTime.Now.Year, 10, 1);
            //Get last sunday in both months
            DateTime daylightstart = LastDayInMonth(march, DayOfWeek.Sunday);
            DateTime daylightstop = LastDayInMonth(october, DayOfWeek.Sunday);
            //If we are within thoose dates, add daylight saving time
            if (daylightstart.Ticks < DateTime.Now.Ticks && daylightstop.Ticks > DateTime.Now.Ticks)
                TimeService.SetTimeZoneOffset(TimeZoneMinutes + DaylightSavingTime);
            else
                TimeService.SetTimeZoneOffset(TimeZoneMinutes);
        }

        /// <summary>
        /// Get datetime of the last day in a month
        /// </summary>
        /// <param name="time">DateTime object of the first of the month</param>
        /// <param name="day">Which last day in month to find</param>
        /// <returns>Returns DateTime object of the last day of the month</returns>
        private DateTime LastDayInMonth(DateTime time, DayOfWeek day)
        {
            //Create datetime of the actual last day of the month
            DateTime lastDay = new DateTime(time.Year, time.Month+1, 1).AddDays(-1);
            //Int value of day enum
            int wDay = (int)day;
            //Int value of last day in month
            int lDay = (int)lastDay.DayOfWeek;
            //Example: Monday=0,Tuesday=1,Wednesday=2,Thursday=3,Friday=4,Saturday=5,Sunday=6
            // Friday(4) >= Wednesday(2) ? Wednesday(2)-Friday(4) : Wednesday(2) - Friday(4) - 7
            return lastDay.AddDays(lDay >= wDay ? wDay - lDay : wDay - lDay - 7);
        }
    }
}
