using System;
using System.Collections.Generic;
using System.Text;

namespace Vives.DOMAIN.Helpers
{
    public enum ExceptionTypes
    {
        Warning,
        Fatal,
    }
    public class VivesException : Exception
    {
        public ExceptionTypes EType { get; set; } = ExceptionTypes.Fatal;
        public VivesException()
        {

        }
        public VivesException(string message, ExceptionTypes eType = ExceptionTypes.Fatal) : base(message)
        {

        }
        public VivesException(string message, Exception inner, ExceptionTypes eType = ExceptionTypes.Fatal) : base(message, inner)
        {

        }
    }
}
