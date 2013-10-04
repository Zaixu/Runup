/// \file IUser.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    /// <summary>
    /// For containing user 'anchor' for exercise and authorization information.
    /// </summary>
    public interface IUser
    {
        // Properties
        /// <summary>
        /// Unique identifier.
        /// </summary>
        int Id
        {
            set;
        }

        /// <summary>
        /// Name of the user.
        /// </summary>
        string Name
        {
            get;
            set;
        }

        /// <summary>
        /// List of exerices the user has done.
        /// </summary>
        ICollection<IExercise> Exercises
        {
            get;
            set;
        }
    }
}
