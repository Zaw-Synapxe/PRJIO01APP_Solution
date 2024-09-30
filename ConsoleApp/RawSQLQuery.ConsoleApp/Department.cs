using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawSQLQuery.ConsoleApp
{
    public class Department : EntityBase
    {
        public Int64 Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
