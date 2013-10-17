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
        /// <param name="callback">Callback for when server returns answer.</param>
        void SaveExercise(Users user, IExercise exercise, SyncCallbackSaveExercise callback);

        /// <summary>
        /// Get all exercises the user has.
        /// 
        /// Only 'lightweight' so doesn't return information about the route run.
        /// A call to 'GetFullExercise' have to be made for that.
        /// </summary>
        /// <param name="user">The user to receive for.</param>
        /// <param name="callback">Callback for when server returns answer.</param>
        void GetExercisesLight(Users user, SyncCallbackGetExercisesLight callback);

        /// <summary>
        /// Gets all information about an exercise with the supplied id.
        /// </summary>
        /// <param name="user">The user with the exercise.</param>
        /// <param name="exerciseID">ID of the exercise</param>
        /// <param name="callback">Callback for when server returns answer.</param>
        void GetFullExercise(Users user, int exerciseID, SyncCallbackGetFullExercise callback);
    }

    // Helper types
    /// <summary>
    /// For server callback when saving exercise.
    /// </summary>
    /// <param name="status">Success or not.</param>
    public delegate void SyncCallbackSaveExercise(string status);

    /// <summary>
    /// For server callback when want to get list of exercises.
    /// </summary>
    /// <param name="exercises">List of exercises.</param>
    public delegate void SyncCallbackGetExercisesLight(ICollection<IExercise> exercises);

    /// <summary>
    /// For server callback when want to an exercise.
    /// </summary>
    /// <param name="exercise">Exercise received.</param>
    public delegate void SyncCallbackGetFullExercise(IExercise exercise);
}
