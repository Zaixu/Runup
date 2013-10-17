using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CloudService;

namespace Domain.Interfaces
{
    /// <summary>
    /// For synchronization of objects to server.
    /// </summary>
    public interface ISyncService
    {
        /// <summary>
        /// Saves a new exercise for the user.
        /// </summary>
        /// <param name="user">The user to save the exercise for.</param>
        /// <param name="exercise">The exercise to save.</param>
        void SaveExercise(Users user, IExercise exercise);

        /// <summary>
        /// Get all exercises the user has.
        /// 
        /// Only 'lightweight' so doesn't return information about the route run.
        /// A call to 'GetFullExercise' have to be made for that.
        /// </summary>
        /// <param name="user">The user to receive for.</param>
        /// <returns></returns>
        ICollection<IExercise> GetExercisesLight(Users user);

        /// <summary>
        /// Gets all information about an exercise with the supplied id.
        /// </summary>
        /// <param name="user">The user with the exercise.</param>
        /// <param name="exerciseID">ID of the exercise</param>
        /// <returns></returns>
        IExercise GetFullExercise(Users user, int exerciseID);
    }
}
