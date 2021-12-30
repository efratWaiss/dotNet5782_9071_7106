using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IBL.BO
{
   public class Exceptions
    {
        [Serializable]
        class idException : Exception
        {
            public idException() : base() { }
            public idException(string message) : base(message) { }
            public idException(string message, System.Exception inner)
                        : base(message, inner) { }
            protected idException(SerializationInfo info, StreamingContext context)
                        : base(info, context) { }

            override public string ToString()
            { return Message; }
        }
        class notExistException :Exception
        {
            public notExistException() : base() { }
            public notExistException(string message) : base(message) { }
            public notExistException(string message, System.Exception inner)
                        : base(message, inner) { }
            protected notExistException(SerializationInfo info, StreamingContext context)
                        : base(info, context) { }

            override public string ToString()
            { return Message; }
        }
    }
}
