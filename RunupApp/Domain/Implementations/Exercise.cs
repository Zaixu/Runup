/// \file Exercise.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Implementations
{
    public class Exercise : IExercise
    {
        // Properties
        // :IExercise
        public DateTime ExerciseStart
        {
            get;
            set;
        }

        public DateTime ExerciseEnd
        {
            get;
            set;
        }

        public IRoute RouteRun
        {
            get;
            set;
        }

        // Functions
        public Exercise()
        {
            // Setup
            RouteRun = new Route();
            ExerciseStart = DateTime.Now;
            ExerciseEnd = DateTime.Now;
        }
    }
}
