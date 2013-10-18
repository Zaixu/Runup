using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using RunupApp.Resources;

namespace RunupApp.ViewModels
{
    /// <summary>
    /// For conversion of datetime to 'friendly' string.
    /// </summary>
    public class DateTimeConverter : IValueConverter
    {
        // Functions
        // :IValueConverter
        /// <summary>
        /// Converts from DateTime to string.
        /// </summary>
        /// <param name="value">Datetime value.</param>
        /// <param name="targetType">Targettype which is a string.</param>
        /// <param name="parameter">Any parameters.</param>
        /// <param name="cultureinfo">Cultureinf.(Not used.)</param>
        /// <returns>The 'friendly' string.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureinfo)
        {
            DateTime date = (DateTime)value;
            string formatString = AppResources.DateTimeFriendlyFormat;
            return date.ToString(formatString);
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="value">Datetime value.</param>
        /// <param name="targetType">Targettype which is a string.</param>
        /// <param name="parameter">Any parameters.</param>
        /// <param name="cultureinfo">Cultureinf.(Not used.)</param>
        /// <returns>null</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureinfo)
        {
            return null;
        }
    }
}
