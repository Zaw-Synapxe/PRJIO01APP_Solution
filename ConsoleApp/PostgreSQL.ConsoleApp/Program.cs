using Npgsql;

class Program
{
    static void Main()
    {
        var connectionString = "Host=192.168.123.150;Port=2665;Database=zaw_DB;Username=root;Password=passwordroot;";

        using (var connection = new NpgsqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection successful!");

                // Example query
                using (var command = new NpgsqlCommand("SELECT NOW()", connection))
                {
                    var result = command.ExecuteScalar();
                    Console.WriteLine($"Server Time: {result}");
                }


                Console.WriteLine("Reading Data From Table (tbl_user1)");
                // Define the query
                string query = "SELECT oid, username, createddate, status FROM tbl_user1";

                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"OID: {reader["oid"]}, UserName: {reader["username"]}, CreatedDate: {reader["createddate"]}, Status: {reader["status"]}");
                    }
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
