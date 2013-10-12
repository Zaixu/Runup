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
        /// <param name="exercise">The exercise to save.</param>
        /// <param name="user">The user to save the exercise for.</param>
        void SaveExercise(IRoute route, Users user);
    }
}
