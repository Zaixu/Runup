using System;
using Microsoft.SPOT;

namespace MicroFramework
{
    /// <summary>
    /// Driver interface, giving the function to be called to get data
    /// </summary>
    interface IDriver
    {
        /// <summary>
        /// Get data from driver
        /// </summary>
        /// <returns>Returns object of the data</returns>
        object GetData();
    }
}
