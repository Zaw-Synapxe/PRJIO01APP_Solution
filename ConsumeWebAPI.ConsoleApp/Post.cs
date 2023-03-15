using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeWebAPI.ConsoleApp
{
    public class Post
    {
        public int userId { get; set; }

        public int id { get; set; }

        public string? title { get; set; }

        public string? body { get; set; }

        public override string ToString()
        {
            return $"Post Id:{this.id} \\n UserId:{this.userId} \\n Title: {this.title} \\n Body: {this.body}";
        }
    }
}
