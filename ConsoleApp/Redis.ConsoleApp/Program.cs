using Newtonsoft.Json;
using Redis.ConsoleApp;
using StackExchange.Redis;
using System.Data;

class Program
{
    static void Main()
    {
        var config = new ConfigurationOptions
        {
            EndPoints = { "192.168.123.150:6379" },
            AbortOnConnectFail = false
        };

        // Connect to Redis server 
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(config);
        IDatabase db = redis.GetDatabase();

        Console.WriteLine("Connected to Redis ...");

        // Set a value in Redis
        db.StringSet("myKey", "Hello, Redis!");

        // Retrieve the value from Redis
        string? value = db.StringGet("myKey"); // Allow nullable value
        Console.WriteLine("Stored value: " + (value ?? "null")); // Handle null case


        // Create a Redis Hash
        HashEntry[] hashEntries = new HashEntry[]
        {
            new HashEntry("field1", "value1"),
            new HashEntry("field2", "value2")
        };
        db.HashSet("myhash", hashEntries);
        // Retrieve a specific field from the Hash
        string? fieldValue = db.HashGet("myhash", "field1");
        Console.WriteLine("Stored value: from Hash " + fieldValue);


        // Add items to a Redis List
        db.ListLeftPush("mylist", "item1");
        db.ListLeftPush("mylist", "item2");
        // Retrieve all items from the List
        RedisValue[] listItems = db.ListRange("mylist");
        Console.WriteLine("List item Count: " + listItems.Length); // Use Length property instead of Count method group
        foreach (var item in listItems)
        {
            Console.WriteLine("List item: " + item);
        }
        Console.WriteLine("✅ Item retrieved from cache!");


        // ------------------------------------------------------
        // Create a sample DataTable
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Rows.Add(1, "Alice");
        dt.Rows.Add(2, "Bob");

        // Serialize DataTable to JSON
        string jsonData = JsonConvert.SerializeObject(dt);

        // Store JSON data in Redis
        db.StringSet("myDataTable", jsonData);

        Console.WriteLine("DataTable stored in Redis!");
        // ------------------------------------------------------

        Task.Delay(4000).Wait(); // Wait for 2 seconds

        // ------------------------------------------------------
        // Retrieve JSON string from Redis
        string? jsonData2 = db.StringGet("myDataTable"); // Allow nullable value

        if (!string.IsNullOrEmpty(jsonData2)) // Check for null or empty string
        {
            // Deserialize JSON to DataTable
            DataTable? dt2 = JsonConvert.DeserializeObject<DataTable>(jsonData2);

            if (dt2 != null) // Check for null after deserialization
            {
                // Display extracted data
                foreach (DataRow row in dt2.Rows)
                {
                    Console.WriteLine($"ID: {row["ID"]}, Name: {row["Name"]}");
                }
            }
            else
            {
                Console.WriteLine("Deserialization returned null.");
            }
        }
        else
        {
            Console.WriteLine("No data found in Redis for 'myDataTable'.");
        }
        // ------------------------------------------------------


        // Close connection
        redis.Close();



        //var cache = RedisHelper.Database;
        //await cache.StringSetAsync("OURKEY", "JORDAN");

        //var value = await cache.StringGetAsync("OURKEY");

        //for (int a = 0; a < 100; a++)
        //{
        //    await cache.StringSetAsync($"OURKEY_{a}", $"JORDAN_{a}");
        //}
        //for (int a = 0; a < 100; a++)
        //{
        //    Console.WriteLine(await cache.StringGetAsync($"OURKEY_{a}"));
        //}

    }
}

