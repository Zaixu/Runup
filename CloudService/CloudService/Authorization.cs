using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CloudService.Database;

namespace CloudService
{
    public class Authorization : IAuthorization
    {
        public bool Login(User user)
        {
            runupEntities ent = new runupEntities();
            return false;
        }

    }
}
