// See https://aka.ms/new-console-template for more information
using RawSQLQuery.ConsoleApp;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

using (SqlConnection connection = new(Helper.connectionString))
{
    try
    {
        connection.Open();
        Console.WriteLine("Connection successful!");
        var result = Helper.GetAllDepartment(connection);

        foreach (var item in result)
        {
            Console.WriteLine($"Branch Id{item.Id}, Branch Nme: {item.Name}");
        }

        connection.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}
