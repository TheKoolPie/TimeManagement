using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagement.Api.Exceptions
{
    public class PersistencyException : Exception
    {
        public PersistencyException() : base("Could not save changes to context") { }
        public PersistencyException(Exception inner) : base("Could not save changes to context", inner) { }
    }
}
