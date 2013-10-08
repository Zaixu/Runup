using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CloudService
{
    public class Service : IService
    {
        public bool Login(Database.Users user)
        {
            throw new NotImplementedException();
        }

        public bool Register(Database.Users user)
        {
            throw new NotImplementedException();
        }

        public bool SaveData(Database.Users user)
        {
            throw new NotImplementedException();
        }

        public bool LoadData(Database.Users user)
        {
            throw new NotImplementedException();
        }
    }
}
