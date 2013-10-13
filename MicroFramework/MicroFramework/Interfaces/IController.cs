using System;
using Microsoft.SPOT;

namespace MicroFramework
{
    /// <summary>
    /// 
    /// </summary>
    interface IController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string Handler(string request);
    }
}
