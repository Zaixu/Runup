using System;
using Microsoft.SPOT;

namespace MicroFramework
{
    interface IController
    {
        string Handler(string request);
    }
}
