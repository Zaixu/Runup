﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CloudService;

namespace Domain.Interfaces
{
    /// <summary>
    /// For creation of DB object from domain objects.
    /// </summary>
    interface IDBFactory
    {
        /// <summary>
        /// Creates DB route from domain route.
        /// 
        /// All underlying types are converted too.
        /// </summary>
        /// <param name="exercise">The route to be converted.</param>
        /// <returns></returns>
        Routes CreateRoute(IRoute route);
    }
}
