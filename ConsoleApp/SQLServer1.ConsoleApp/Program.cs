using System.Data.SqlClient;
using System.Text;

namespace SQLServer1.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");

                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "(local)";   // update me
                //builder.DataSource = "ZAW-MAXIMUS-XI";   // update me
                builder.UserID = "sa";              // update me
                builder.Password = "$A12312312Zaw";      // update me
                builder.InitialCatalog = "Northwind";

                // Connect to SQL
                Console.Write("Connecting to SQL Server ... ");

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();
                    Console.WriteLine("Done. Connection Connected !");

                    String sql = "";
                    StringBuilder sb = new StringBuilder();

                    //////// Create a sample database
                    //////Console.Write("Dropping and creating database 'Northwind' ... ");
                    //////sql = "DROP DATABASE IF EXISTS [SampleDB]; CREATE DATABASE [Northwind]";
                    //////using (SqlCommand command = new SqlCommand(sql, connection))
                    //////{
                    //////    command.ExecuteNonQuery();
                    //////    Console.WriteLine("Done.");
                    //////}

                    //////// Create a Table and insert some sample data
                    //////Console.Write("Creating sample table with data, press any key to continue...");
                    //////Console.ReadKey(true);

                    //////sb.Append("USE Northwind; ");
                    //////sb.Append("CREATE TABLE Employees1 ( ");
                    //////sb.Append(" Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
                    //////sb.Append(" Name NVARCHAR(50), ");
                    //////sb.Append(" Location NVARCHAR(50) ");
                    //////sb.Append("); ");
                    //////sb.Append("INSERT INTO Employees1 (Name, Location) VALUES ");
                    //////sb.Append("(N'Jared', N'Australia'), ");
                    //////sb.Append("(N'Nikita', N'India'), ");
                    //////sb.Append("(N'Tom', N'Germany'); ");
                    //////sql = sb.ToString();
                    //////using (SqlCommand command = new SqlCommand(sql, connection))
                    //////{
                    //////    command.ExecuteNonQuery();
                    //////    Console.WriteLine("Done.");
                    //////}

                    //////// INSERT demo
                    //////Console.Write("Inserting a new row into table, press any key to continue...");
                    //////Console.ReadKey(true);
                    //////sb.Clear();
                    //////sb.Append("INSERT Employees1 (Name, Location) ");
                    //////sb.Append("VALUES (@name, @location);");
                    //////sql = sb.ToString();
                    //////using (SqlCommand command = new SqlCommand(sql, connection))
                    //////{
                    //////    command.Parameters.AddWithValue("@name", "Jake");
                    //////    command.Parameters.AddWithValue("@location", "United States");
                    //////    int rowsAffected = command.ExecuteNonQuery();
                    //////    Console.WriteLine(rowsAffected + " row(s) inserted");
                    //////}

                    //////// UPDATE demo
                    //////String userToUpdate = "Nikita";
                    //////Console.Write("Updating 'Location' for user '" + userToUpdate + "', press any key to continue...");
                    //////Console.ReadKey(true);
                    //////sb.Clear();
                    //////sb.Append("UPDATE Employees1 SET Location = N'United States' WHERE Name = @name");
                    //////sql = sb.ToString();
                    //////using (SqlCommand command = new SqlCommand(sql, connection))
                    //////{
                    //////    command.Parameters.AddWithValue("@name", userToUpdate);
                    //////    int rowsAffected = command.ExecuteNonQuery();
                    //////    Console.WriteLine(rowsAffected + " row(s) updated");
                    //////}

                    //////// DELETE demo
                    //////String userToDelete = "Jared";
                    //////Console.Write("Deleting user '" + userToDelete + "', press any key to continue...");
                    //////Console.ReadKey(true);
                    //////sb.Clear();
                    //////sb.Append("DELETE FROM Employees1 WHERE Name = @name;");
                    //////sql = sb.ToString();
                    //////using (SqlCommand command = new SqlCommand(sql, connection))
                    //////{
                    //////    command.Parameters.AddWithValue("@name", userToDelete);
                    //////    int rowsAffected = command.ExecuteNonQuery();
                    //////    Console.WriteLine(rowsAffected + " row(s) deleted");
                    //////}


                    // READ demo
                    Console.WriteLine("Reading data from table, press any key to continue...");
                    Console.ReadKey(true);
                    sql = "SELECT Id, Name, Location FROM Employees1;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                            }
                        }
                    }

                    //
                }


                //
            }
            catch (SqlException ex) {
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            Console.WriteLine("All done. Press any key to finish...");
            Console.ReadKey(true);
        }

        //
    }
}
