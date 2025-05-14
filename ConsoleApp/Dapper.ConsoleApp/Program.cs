using Azure;
using Dapper;
using Dapper.ConsoleApp;
using log4net.Config;
using log4net;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;
using System.Data;
using System.Net;
using System.Reflection;
using static Dapper.SqlMapper;
using Log4Net.LogUtility;

class Program
{
    private static IConfiguration config2;
    static async Task Main(string[] args) //static async void Main(string[] args)
    {
        // 0
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo("log4netconfig.config"));

        var demo = new Logger();
        demo.Info("Starting the console application");
        demo.Debug($"Starting {MethodBase.GetCurrentMethod()?.DeclaringType}");


        // 1
        var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddEnvironmentVariables()
           .AddCommandLine(args);
        IConfiguration config = builder.Build();

        // 2
        config2 = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build(); // Make sure appsettings.json is set to include in bin directory. Set it as Copy always.


        //access configuration values
        //A connection was successfully established with the server, but then an error occurred during the login process.
        //(provider: SSL Provider, error: 0 - The certificate chain was issued by an authority that is not trusted.)
        string connectionString = config.GetConnectionString("DefaultConnection1");
        string logLevel = config["Logging:LogLevel:Default"];

        Console.WriteLine(logLevel);

        //
        var serverConnection1 = config.GetSection("ServerConnection");
        var url = serverConnection1.GetValue<string>("Url");
        var port = serverConnection1.GetValue<int>("Port");
        var isSsl = serverConnection1.GetValue<bool>("IsSSL");
        var password = serverConnection1.GetValue<string>("Password");
        Console.WriteLine("{0} - {1} - {2} - {3}", url, port, isSsl, password);

        //
        var serverConnection = new ServerConnection();
        config.GetSection("ServerConnection").Bind(serverConnection);
        Console.WriteLine(serverConnection);

        //
        var companyOptions = new CompanyOptions();
        config.GetSection("CompanyOptions").Bind(companyOptions);
        Console.WriteLine(companyOptions);

        //
        //
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            db.Open();
            var result = db.Query<string>("select 'Hellow world ...'").Single();

            Console.WriteLine(result);
        }

        ////
        //var query = "SELECT * FROM Companies";
        //using (IDbConnection db = new SqlConnection(connectionString))
        //{
        //    db.Open();
        //    var custList = (List<Company>)db.Query<Company>(query);
        //    foreach (Company c in custList)
        //    {
        //        Console.WriteLine(c.Id + " - " + c.Name);
        //    }

        //    var cp = db.Query<Company>("SELECT * FROM Companies").ToList();
        //    cp.ForEach(c => Console.WriteLine(c));


        //    //get data
        //    List<Company> custList2 = db.Query<Company>("select * from companies").ToList();
        //}

        //
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            db.Open();
            var scalar = db.Query<string>("SELECT GETDATE()").SingleOrDefault();
            //scalar.Dump("This is a string scalar result:");

            var results = db.Query<MyObject>(@"
		        SELECT * FROM (
		        VALUES (1,'one'),
			        (2,'two'),
			        (3,'three')
		        ) AS mytable(id,name)");
            //results.Dump("This is a table mapped to a class:");
        }


        // DI
        var serviceProvider = new ServiceCollection()
               // Add your services here..
               .AddSingleton<IConfiguration>(Program.config2)
               .AddSingleton<Context>()
               .AddSingleton<IWorker, Worker>()
               .AddScoped<ICompanyRepository, CompanyRepository>()
               .BuildServiceProvider();
        // Now get Your Services like this
        var worker = serviceProvider.GetRequiredService<IWorker>();
        var configx = serviceProvider.GetRequiredService<IConfiguration>();
        var crepo = serviceProvider.GetRequiredService<ICompanyRepository>();

        worker.DoSomeWork();

        Console.WriteLine($"Deault Connection String is: {config2.GetConnectionString("DefaultConnection1")}");
        Console.WriteLine($"Secret key value is: {config2.GetSection("secret_key").Value}");
        // For classes other than Program, we need to access it from service reference like this:
        Console.WriteLine($"Secret key value is: {configx.GetSection("secret_key").Value}");

        //crepo.DeleteCompany(1);


        // GetCompanies
        try
        {
            var companies = await crepo.GetCompanies();
            Console.WriteLine(companies);
        }
        catch (Exception ex)
        {
            //log error
            demo.Error(ex.Message, ex.InnerException);
            Console.WriteLine("Error :" + ex.Message);
        }

        // GetCompany
        try
        {
            var company = await crepo.GetCompany(1);
            if (company == null)
            {
                Console.WriteLine("Null Error: Not Found ...");
            }
            else
            {
                Console.WriteLine(company);
            }
        }
        catch (Exception ex)
        {
            //log error
            demo.Error(ex.Message, ex.InnerException);
            Console.WriteLine("Error :" + ex.Message);
        }

        // CreateCompany
        try
        {
            var company = new CompanyForCreationDto{
                Name = "Name 1",
                Address = "Address 1",
                Country = "Country 1"
            };

            var createdCompany = await crepo.CreateCompany(company);
            Console.WriteLine(createdCompany.Id);
        }
        catch (Exception ex)
        {
            //log error
            demo.Error(ex.Message, ex.InnerException);
            Console.WriteLine("Error :" + ex.Message);
        }

        // UpdateCompany
        try
        {
            var company = new CompanyForUpdateDto
            {
                Name = "Name 1",
                Address = "Address 111 111",
                Country = "Country 1"
            };

            var dbCompany = await crepo.GetCompany(1);
            if (dbCompany == null)
            {
                Console.WriteLine("Null Error: Not Found ...");
            }
            else
            {
                await crepo.UpdateCompany(1, company);
                Console.WriteLine("Updated Id : " + 1);
            }
                
        }
        catch (Exception ex)
        {
            //log error
            demo.Error(ex.Message, ex.InnerException);
            Console.WriteLine("Error :" + ex.Message);
        }

        // DeleteCompany
        try
        {
            var dbCompany = await crepo.GetCompany(1);
            if (dbCompany == null)
            {
                Console.WriteLine("Null Error: Not Found ...");
            }
            else
            {
                await crepo.DeleteCompany(1);
                Console.WriteLine("Deleted Id : " + 1);
            }

        }
        catch (Exception ex)
        {
            //log error
            demo.Error(ex.Message, ex.InnerException);
            Console.WriteLine("Error :" + ex.Message);
        }

        // GetCompanyByEmployeeId
        // used SP
        try
        {
            var company = await crepo.GetCompanyByEmployeeId(1);
            if (company == null)
            {
                Console.WriteLine("Null Error: Not Found ...");
            }
            else
            {
                string x = "";
                if (company.Employees != null)
                {
                    x = company.Employees[0].Id.ToString();
                }

                Console.WriteLine("Get Company by Employee Id : " + company.Id + " - " + x);
            }
        }
        catch (Exception ex)
        {
            //log error
            demo.Error(ex.Message, ex.InnerException);
            Console.WriteLine("Error :" + ex.Message);
        }






        // -----------------------------------------------------------------
        demo.Info("Ending application");
        Console.ReadLine();
        //
    }


    //
}

// Entities
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Position { get; set; }
    public int? CompanyId { get; set; }
}

// Entities
public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Country { get; set; }
    public List<Employee> Employees { get; set; } = new List<Employee>();
}

// DTO
public class CompanyForCreationDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Country { get; set; }
}

// DTO
public class CompanyForUpdateDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Country { get; set; }
}

public class ServerConnection
{
    public string Url { get; set; }
    public string Password { get; set; }
    public int Port { get; set; }
    public bool IsSSL { get; set; }

    public override string ToString()
    {
        return $"${this.Url}:${this.Port},password=${this.Password},ssl=${this.IsSSL}";
    }
}


public class CompanyOptions
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public int EmployeeCount { get; set; }
}
public class MyObject
{
    public int Id { get; set; }
    public string Name { get; set; }
}


//USE[DapperASPNetCore]
//GO
//SET ANSI_NULLS ON
//GO
//SET QUOTED_IDENTIFIER ON
//GO
//CREATE PROCEDURE [dbo].[ShowCompanyForProvidedEmployeeId] @Id int
//AS
//SELECT c.Id, c.Name, c.Address, c.Country
//FROM Companies c JOIN Employees e ON c.Id = e.CompanyId
//Where e.Id = @Id
//GO



//USE[DapperASPNetCore]
//GO
//ALTER TABLE [dbo].[Employees] DROP CONSTRAINT[FK_Employees_Companies]
//GO
///****** Object:  Table [dbo].[Employees]    Script Date: 5/10/2021 10:39:05 AM ******/
//DROP TABLE [dbo].[Employees]
//GO
///****** Object:  Table [dbo].[Companies]    Script Date: 5/10/2021 10:39:05 AM ******/
//DROP TABLE [dbo].[Companies]
//GO
///****** Object:  Table [dbo].[Companies]    Script Date: 5/10/2021 10:39:05 AM ******/
//SET ANSI_NULLS ON
//GO
//SET QUOTED_IDENTIFIER ON
//GO
//CREATE TABLE [dbo].[Companies] (

//    [Id][int] IDENTITY(1, 1) NOT NULL,

//    [Name] [nvarchar] (50) NOT NULL,

//    [Address] [nvarchar] (60) NOT NULL,

//    [Country] [nvarchar] (50) NOT NULL,
// CONSTRAINT[PK_Companies] PRIMARY KEY CLUSTERED 
//(

//    [Id] ASC
//)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
//) ON[PRIMARY]

//GO
///****** Object:  Table [dbo].[Employees]    Script Date: 5/10/2021 10:39:05 AM ******/
//SET ANSI_NULLS ON
//GO
//SET QUOTED_IDENTIFIER ON
//GO
//CREATE TABLE [dbo].[Employees] (

//    [Id][int] IDENTITY(1, 1) NOT NULL,

//    [Name] [nvarchar] (50) NOT NULL,

//    [Age] [int] NOT NULL,

//    [Position] [nvarchar] (50) NOT NULL,

//    [CompanyId] [int] NOT NULL,
// CONSTRAINT[PK_Employees] PRIMARY KEY CLUSTERED 
//(

//    [Id] ASC
//)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
//) ON[PRIMARY]

//GO
//SET IDENTITY_INSERT [dbo].[Companies] ON

//INSERT[dbo].[Companies] ([Id], [Name], [Address], [Country]) VALUES(1, N'IT_Solutions Ltd', N'583 Wall Dr. Gwynn Oak, MD 21207', N'USA')
//INSERT[dbo].[Companies]([Id], [Name], [Address], [Country]) VALUES(2, N'Admin_Solutions Ltd', N'312 Forest Avenue, BF 923', N'USA')
//SET IDENTITY_INSERT[dbo].[Companies] OFF
//SET IDENTITY_INSERT [dbo].[Employees] ON

//INSERT[dbo].[Employees] ([Id], [Name], [Age], [Position], [CompanyId]) VALUES(1, N'Sam Raiden', 26, N'Software developer', 1)
//INSERT[dbo].[Employees]([Id], [Name], [Age], [Position], [CompanyId]) VALUES(2, N'Kane Miller', 35, N'Administrator', 2)
//INSERT[dbo].[Employees]([Id], [Name], [Age], [Position], [CompanyId]) VALUES(3, N'Jana McLeaf', 30, N'Software developer', 1)
//SET IDENTITY_INSERT[dbo].[Employees] OFF
//ALTER TABLE [dbo].[Employees] WITH CHECK ADD  CONSTRAINT [FK_Employees_Companies] FOREIGN KEY([CompanyId])
//REFERENCES[dbo].[Companies]([Id])
//GO
//ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT[FK_Employees_Companies]
//GO