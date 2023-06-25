using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityLibraries.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("Resource Not Found!")
        {

        }
    }
}
