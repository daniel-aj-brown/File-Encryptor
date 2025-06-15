using System;

namespace Encryption
{
    public class FileExistsException : Exception
    {
        public FileExistsException(string message) : base(message)
        {
        }
    }
}
