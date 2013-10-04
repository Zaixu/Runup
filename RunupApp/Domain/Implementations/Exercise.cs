﻿/// \file Exercise.cs
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
        DateTime IExercise.ExerciseStart
        {
            get;
            set;
        }

        DateTime IExercise.ExerciseEnd
        {
            get;
            set;
        }

        IRoute IExercise.RouteRun
        {
            get;
            set;
        }
    }
}
