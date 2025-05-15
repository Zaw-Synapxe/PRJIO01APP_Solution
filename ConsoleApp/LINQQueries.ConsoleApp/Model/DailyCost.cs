using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQQueries.ConsoleApp.Model
{
    public class DailyCost
    {
        public long Id { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
