﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;




namespace DO
{
    [Serializable]
    public class IdException : Exception
    {
        public IdException() : base() { }
        public IdException(string message) : base(message) { }
        public IdException(string message, Exception inner)
                    : base(message, inner) { }
        protected IdException(SerializationInfo info, StreamingContext context)
                    : base(info, context) { }

        override public string ToString()
        { return Message; }
    }
    public class NotExistException : Exception
    {
        public NotExistException() : base() { }
        public NotExistException(string message) : base(message) { }
        public NotExistException(string message, Exception inner)
                    : base(message, inner) { }
        protected NotExistException(SerializationInfo info, StreamingContext context)
                    : base(info, context) { }

        override public string ToString()
        { return Message; }
    }
    public class NotFoundThisInstance : Exception
    {
        public NotFoundThisInstance() : base() { }
        public NotFoundThisInstance(string message) : base(message) { }
        public NotFoundThisInstance(string message, Exception inner)
                    : base(message, inner) { }
        protected NotFoundThisInstance(SerializationInfo info, StreamingContext context)
                    : base(info, context) { }

        override public string ToString()
        { return Message; }
    }
}


