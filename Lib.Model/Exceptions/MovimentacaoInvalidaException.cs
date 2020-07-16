using System;
using System.Collections.Generic;
using System.Text;

namespace Lib.Model.Exceptions
{
    public class MovimentacaoInvalidaException : Exception
    {
        public MovimentacaoInvalidaException(){}

        public MovimentacaoInvalidaException(string message):base(message){}

        public MovimentacaoInvalidaException(string message, Exception inner):base(message, inner){}
    }
}
