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
    /// <summary>
    /// Class to function as service outwards to application
    /// </summary>
    public class Service : IService
    {
        /// <summary>
        /// Checks if a user exists
        /// </summary>
        /// <param name="user">User log in details</param>
        /// <returns>String with text according to status</returns>
        public string Login(Users user)
        {
            if (user != null)
            {
                //Check the object given
                string response = Validate(user);
                //If errors return
                if (response.Length > 0)
                    return response;

                //Find user email in database
                DatabaseEntities db = new DatabaseEntities();
                var existUser = db.Users.Find(user.Email);

                //If it exists and password is correct, success else return error
                if (existUser != null && existUser.Password == user.Password)
                    return "Success";
                else
                    return "User does not exist or wrong info";
            }
            return "No user given";
        }

        /// <summary>
        /// Validation of user fields
        /// </summary>
        /// <param name="user">User class to be checked</param>
        /// <returns>String with checks that dint go through</returns>
        private string Validate(Users user)
        {
            string response = "";
            //Check that email isnt empty
            if (user.Email.Length == 0)
                response = response + "Invalid email length\n\r";
            //Check that password isnt empty
            if (user.Password.Length == 0)
                response = response + "Invalid password length\n\r";
            //check there is atleast a @ and a dot
            if ((!user.Email.Contains('@')) && user.Email.Contains('.'))
                response = response + "Invalid email address\n\r";
            //Return errors
            return response;
        }

        /// <summary>
        /// Registers a user
        /// </summary>
        /// <param name="user">User parameter to register</param>
        /// <returns>Returns a string with error or if succeeded</returns>
        public string Register(Users user)
        {
            if (user != null)
            {
                //Check the object given for errors
                string response = Validate(user);
                //If errors return
                if (response.Length > 0)
                    return response;

                //Find user in database from given user data
                DatabaseEntities db = new DatabaseEntities();
                var existUser = db.Users.Find(user.Email);

                //If he doesnt exist, add new user to database, else return error string
                if (existUser == null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return "Success";
                }
                else
                {
                    return "Error already exists";
                }
            }
            else
                return "No user given";
        }

        /// <summary>
        /// Saves a new exercise for a user.
        /// </summary>
        /// <param name="user">The user that has done the exercise.</param>
        /// <param name="exercise">The exercise to be saved.</param>
        /// <returns></returns>
        public string SaveExercise(Users user, Exercises exercise)
        {
            if (user != null && exercise != null)
            {
                if(Login(user) == "Success")
                {
                    using (var dbC = new DatabaseEntities())
                    {
                    
                            Users dbuser = dbC.Users.Find(user.Email);
                            dbuser.Exercises.Add(exercise);

                            dbC.SaveChanges();

                            return ("Success");
                    
                    }
                }
                else
                    return ("User not found");
            }

            return ("Invalid values");
        }

        public ICollection<Exercises> GetExercisesLight(Users user)
        {
            if (user != null)
            {
                if (Login(user) == "Success")
                {
                    using (var dbC = new DatabaseEntities())
                    {
                        ICollection<Exercises> exercises = dbC.Exercises.ToList();

                        return exercises;
                    }
                }
                else
                    return null;
            }
            else
                return null;
        }

        public Exercises GetFullExercise(Users user, int exerciseID)
        {
            if (user != null && exerciseID != null)
            {
                if (Login(user) == "Success")
                {
                    using (var dbC = new DatabaseEntities())
                    {
                        Exercises exercise = dbC.Exercises.Include("RoutePoints").Where(x => x.idExercises == exerciseID).FirstOrDefault();

                        return exercise;
                    }
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}
