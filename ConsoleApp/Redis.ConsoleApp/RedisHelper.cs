using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.ConsoleApp
{
    internal class RedisHelper
    {
        private static IDatabase redisDatabase;
        public static IDatabase Database
        {
            get
            {
                return redisDatabase;
            }
        }
        static RedisHelper()
        {
            var connection = ConnectionMultiplexer.Connect("localhost:6379");
            redisDatabase = connection.GetDatabase();
        }
    }
}
