using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CloudService;

namespace Domain.Interfaces
{
    /// <summary>
    /// For creation back and forth between DB and domain objects.
    /// </summary>
    interface IExerciseFactory
    {
        /// <summary>
        /// Creates DB exercise from domain xercise.
        /// 
        /// All underlying types are converted too.
        /// </summary>
        /// <param name="exercise">The exercise to be converted.</param>
        /// <returns>The converted object.</returns>
        Exercises CreateDBExercise(IExercise exercise);

        /// <summary>
        /// Creates domain exercise from DB exercise.
        /// </summary>
        /// <param name="exercise">Exercise to convert.</param>
        /// <returns>The converted object.</returns>
        IExercise CreateDomainExercise(Exercises exercise);
    }
}
