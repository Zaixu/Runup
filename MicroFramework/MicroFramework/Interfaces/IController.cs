using System;
using Microsoft.SPOT;

namespace MicroFramework
{
    /// <summary>
    /// Interface for controllers, that are using in the webserver
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// Handler function for webserver to call
        /// </summary>
        /// <param name="request">String of request data</param>
        /// <returns>Return string with response data</returns>
        string Handler(string request);
    }
}
