using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BO
{
    [Serializable]
    class IdException : Exception
    {
        public IdException() : base() { }
        public IdException(string message) : base(message) { }
        public IdException(string message, System.Exception inner)
                    : base(message, inner) { }
        protected IdException(SerializationInfo info, StreamingContext context)
                    : base(info, context) { }

        override public string ToString()
        { return Message; }
    }
    class NotExistException : Exception
    {
        public NotExistException() : base() { }
        public NotExistException(string message) : base(message) { }
        public NotExistException(string message, System.Exception inner)
                    : base(message, inner) { }
        protected NotExistException(SerializationInfo info, StreamingContext context)
                    : base(info, context) { }

        override public string ToString()
        { return Message; }
    }
}
