﻿/// \file RoutePoint.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Implementations
{
    public class RoutePoint : IRoutePoint
    {
        // Properties
        // :RoutePoint
        int IRoutePoint.X
        {
            get;
            set;
        }

        int IRoutePoint.Y
        {
            get;
            set;
        }
    }
}
