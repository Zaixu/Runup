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

        /// <summary>
        /// Saves a new exercise for a user.
        /// </summary>
        /// <param name="user">The user that has done the exercise.</param>
        /// <param name="exercise">The exercise to be saved.</param>
        /// <returns></returns>
        [OperationContract]
        string SaveExercise(Users user, Exercises exercise);

        /// <summary>
        /// Returns all exercises for the user.
        /// 
        /// 'Lightweight' so doesn't returns RoutePoints.
        /// </summary>
        /// <param name="user">The user the receive exercises from.</param>
        /// <returns></returns>
        [OperationContract]
        ICollection<Exercises> GetExercisesLight(Users user);

        /// <summary>
        /// Gets information about the exercise with the provided ID.
        /// </summary>
        /// <param name="user">The user with the exercise.</param>
        /// <param name="exerciseID">ID of the exercise.</param>
        /// <returns></returns>
        [OperationContract]
        Exercises GetFullExercise(Users user, int exerciseID);
    }
}
