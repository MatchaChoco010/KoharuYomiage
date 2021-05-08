using System;

namespace CeVIOAI.Exceptions
{
    public class DllNotFound : Exception
    {
        public DllNotFound(Exception ex) : base("Dll Not Found", ex)
        {
        }
    }
}
