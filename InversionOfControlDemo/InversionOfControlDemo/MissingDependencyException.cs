using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace InversionOfControlDemo
{
    [Serializable]
    public class MissingDependencyException : Exception
    {
        public MissingDependencyException(Type missingType, Type dependentType,
       string[] resolutionStack)
        {

        }

        public MissingDependencyException(string message) : base(message)
        {

        }

        public MissingDependencyException(string message, Exception inner) :
       base(message, inner)
        {

        }

        protected MissingDependencyException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
        {

        }
    }
}
