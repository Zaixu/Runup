using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CloudService.Database;


namespace CloudService
{
 
    /// <summary>
    /// Service interface for outwards communication to applications, defines service and operation contracts for accessing
    /// </summary>
    [ServiceContract]
    public interface IService
    {
        /// <summary>
        /// Handles login function from external apps
        /// </summary>
        /// <param name="user">User object to be logged in</param>
        /// <returns>Returns login status string</returns>
        [OperationContract]
        string Login(Users user);

        /// <summary>
        /// Handles registering function from external apps
        /// </summary>
        /// <param name="user">User object to be registered</param>
        /// <returns>Returns register status string</returns>
        [OperationContract]
        string Register(Users user);

        [OperationContract]
        bool SaveData(Users user);

        [OperationContract]
        bool LoadData(Users user);

        [OperationContract]
        string SaveExercise(Users user, Routes route);
    }
}
