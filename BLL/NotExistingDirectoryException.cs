using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class NotExistingDirectoryException:Exception
    {
        public string Directory { get; }

        public NotExistingDirectoryException() { }

        public NotExistingDirectoryException(string message)
            : base(message) { }

        public NotExistingDirectoryException(string message, Exception inner)
            : base(message, inner) { }

        public NotExistingDirectoryException(string message, string directory)
            : this(message)
        {
            Directory = directory;
        }
    }
}
