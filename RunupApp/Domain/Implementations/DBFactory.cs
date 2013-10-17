using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.CloudService;
using System.Collections.ObjectModel;

namespace Domain.Implementations
{
    class DBFactory : IDBFactory
    {
        public Exercises CreateExercise(IExercise exercise)
        {
            // Setup
            Exercises createdExercise = new Exercises();

            // Convert
            // :Exercise
            createdExercise.RoutePoints = new ObservableCollection<RoutePoints>();
            createdExercise.ExerciseStart = exercise.ExerciseStart;
            createdExercise.ExerciseEnd = exercise.ExerciseEnd;

            // :Points
            foreach (var point in exercise.Points)
            {
                var createdPoint = new RoutePoints();
                createdPoint.Latitude = point.Latitude;
                createdPoint.Longitude = point.Longitude;
                createdPoint.Time = point.Time;

                createdExercise.RoutePoints.Add(createdPoint);
            }

            return (createdExercise);
        }
    }
}
