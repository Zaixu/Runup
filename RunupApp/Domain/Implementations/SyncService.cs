using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.CloudService;

namespace Domain.Implementations
{
    public class SyncService : ISyncService
    {
        // Members
        private SyncCallback _callback;

        // Functions
        // :Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="callback">Called when feedback from server.</param>
        public SyncService(SyncCallback callback)
        {
            _callback = callback;
        }

        // :ISyncService
        public void SaveExercise(IExercise exercise, Users user)
        {
            IDBFactory factory = new DBFactory();
            Exercises dbExercise = factory.CreateExercise(exercise);

            CloudService.ServiceClient client = new ServiceClient();
            client.SaveExerciseCompleted += new EventHandler<SaveExerciseCompletedEventArgs>(CloudService_SaveExerciseCompleted);
            client.SaveExerciseAsync(user, dbExercise);
        }

        public void CloudService_SaveExerciseCompleted(object sender, Domain.CloudService.SaveExerciseCompletedEventArgs e)
        {
            _callback(e.Result);
        }
    }

    // Helper types
    /// <summary>
    /// For server callback.
    /// </summary>
    /// <param name="status">Success or not.</param>
    public delegate void SyncCallback(string status);
}
