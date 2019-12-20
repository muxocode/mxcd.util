using System;

namespace mxcd.util.exception
{
    /// <summary>
    /// Excepción genérica
    /// </summary>
    public class UtilException : System.Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Mensaje"></param>
        /// <param name="Excepcion"></param>
        public UtilException(string message, Exception exception = null) : base(message, exception)
        {
        }
    }
}
