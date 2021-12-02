using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;



namespace IDAL
{
    namespace DO
    {
        [Serializable]
        class idException : Exception
        {
            public idException() : base() { }
            public idException(string message) : base(message) { }
            public idException(string message, Exception inner)
                        : base(message, inner) { }
            protected idException(SerializationInfo info, StreamingContext context)
                        : base(info, context) { }

            override public string ToString()
            { return Message; }
        }
        class notExistException : Exception
        {
            public notExistException() : base() { }
            public notExistException(string message) : base(message) { }
            public notExistException(string message, Exception inner)
                        : base(message, inner) { }
            protected notExistException(SerializationInfo info, StreamingContext context)
                        : base(info, context) { }

            override public string ToString()
            { return Message; }
        }
    }

}
