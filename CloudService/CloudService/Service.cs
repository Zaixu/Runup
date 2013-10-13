using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CloudService.Database;
using System.Collections.ObjectModel;

namespace CloudService
{
    public class Service : IService
    {
        public string Login(Users user)
        {
            DatabaseEntities db = new DatabaseEntities();
            var existUser = db.Users.Find(user.Email);

            if (existUser != null && existUser.Password == user.Password)
                return "Success";
            else
                return "Error logging in";
        }

        public string Register(Users user)
        {
            DatabaseEntities db = new DatabaseEntities();
            var existUser = db.Users.Find(user.Email);

            if(existUser == null)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return "Success";
            }
            else
            {
                return "Error registering";
            }
        }

        public bool SaveData(Users user)
        {
            throw new NotImplementedException();
        }

        public bool LoadData(Users user)
        {
            throw new NotImplementedException();
        }

        public bool SaveExercise(Users user, Routes route)
        {
            if (user != null && route != null)
            {
                using (var dbC = new DatabaseEntities())
                {
                    if (dbC.Users.Find(user.Email) != null)
                    {
                        Users dbuser = dbC.Users.Find(user.Email);
                        dbuser.Routes.Add(route);

                        dbC.SaveChanges();
                    }
                    else
                        return (false);
                }
            }

            return (false);
        }
    }
}
