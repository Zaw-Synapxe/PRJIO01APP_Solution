using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQQueries.ConsoleApp.Model
{
    public static class JSONReader<T> where T : class
    {
        public static List<T> GetAll(string dataFileName)
        {
            string json = File.ReadAllText("Model/ " + dataFileName + " .json");
            var personalInfoList = JsonConvert.DeserializeObject<List<T>>(json);
            return personalInfoList;
        }
    }
}
