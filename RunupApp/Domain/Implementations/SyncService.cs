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
        private ServiceClient _client;
        private SyncCallbackSaveExercise _callBackSaveExercise = null;
        private SyncCallbackGetExercisesLight _callBackGetExercisesLight = null;
        private SyncCallbackGetFullExercise _callBackGetFullExercise = null;
        private IExerciseFactory _factory;

        // Functions
        // :Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SyncService()
        {
            // Setup
            _client = new ServiceClient();
            _client.SaveExerciseCompleted += new EventHandler<SaveExerciseCompletedEventArgs>(CloudService_SaveExerciseCompleted);
            _client.GetExercisesLightCompleted += new EventHandler<GetExercisesLightCompletedEventArgs>(CloudService_GetExercisesLightCompleted);
            _client.GetFullExerciseCompleted += new EventHandler<GetFullExerciseCompletedEventArgs>(CloudService_GetFullExercise);
            _factory = new ExerciseFactory();
        }

        // :ISyncService
        public void SaveExercise(Users user, IExercise exercise, SyncCallbackSaveExercise callback)
        {
            // Setup
            Exercises dbExercise = _factory.CreateDBExercise(exercise);
            _callBackSaveExercise = callback;
            
            // Call
            _client.SaveExerciseAsync(user, dbExercise);
        }

        public void GetExercisesLight(Users user, SyncCallbackGetExercisesLight callback)
        {
            // Setup
            _callBackGetExercisesLight = callback;

            // Call
            _client.GetExercisesLightAsync(user);
        }

        public void GetFullExercise(Users user, int exerciseID, SyncCallbackGetFullExercise callback)
        {
            // Setup
            _callBackGetFullExercise = callback;

            // Call
            _client.GetFullExerciseAsync(user, exerciseID);
        }

        // :Helper functions
        public void CloudService_SaveExerciseCompleted(object sender, Domain.CloudService.SaveExerciseCompletedEventArgs e)
        {
            _callBackSaveExercise(e.Result);
        }

        public void CloudService_GetExercisesLightCompleted(object sender, Domain.CloudService.GetExercisesLightCompletedEventArgs e)
        {
            // Convert
            ICollection<IExercise> exercises = new List<IExercise>();
            foreach (var exercise in e.Result)
            {
                exercises.Add(_factory.CreateDomainExercise(exercise));
            }

            // Call
            _callBackGetExercisesLight(exercises);
        }

        public void CloudService_GetFullExercise(object sender, Domain.CloudService.GetFullExerciseCompletedEventArgs e)
        {
            // Convert
            IExercise exercise = _factory.CreateDomainExercise(e.Result);

            // Call
            _callBackGetFullExercise(exercise);
        }
    }
}
