using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CloudService.Database;

namespace CloudService
{
    public class Service : IService
    {
        public bool Login(Users user)
        {
            DatabaseEntities db = new DatabaseEntities();
            var eu = db.Users.Find(user.Email);

            if (eu != null && eu.Password == user.Password)
                return true;
            else
                return false;
        }

        public bool Register(Users user)
        {
            throw new NotImplementedException();
        }

        public bool SaveData(Users user)
        {
            throw new NotImplementedException();
        }

        public bool LoadData(Users user)
        {
            throw new NotImplementedException();
        }
    }
}
