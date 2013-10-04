/// \file User.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Implementations
{
    public class User : IUser
    {
        // Properties
        // :IUser
        public int Id
        {
            set { throw new NotImplementedException(); }
        }

        public string Name
        {
            get;
            set;
        }

        public ICollection<IExercise> Exercises
        {
            get;
            set;
        }
    }
}
