using Bogus;
using EFCore.ConsoleApp.Data;
using EFCore.ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Xml;

namespace EFCore.ConsoleApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            //string? environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            //if (String.IsNullOrWhiteSpace(environment))
            //    throw new ArgumentNullException("Environment not found in DOTNET_ENVIRONMENT");
            //Console.WriteLine("Environment: {0}", environment);

            //if (environment == "Development")
            //{
            //    //builder
            //    //    .AddJsonFile(
            //    //        Path.Combine(AppContext.BaseDirectory, string.Format("..{0}..{0}..{0}", Path.DirectorySeparatorChar), $"appsettings.{environment}.json"),
            //    //        optional: true
            //    //    );
            //}
            //else
            //{
            //    //builder
            //    //    .AddJsonFile($"appsettings.{environment}.json", optional: false);
            //}

            #region Config
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"appsettings.{environment}.json", true, true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();



            var serverConnection1 = configuration.GetSection("ServerConnection");

            var connectionString = configuration["DefaultConnection"];
            Console.WriteLine($"Connection String is: {connectionString}");

            var emailHost = configuration["Smtp:Host"];
            Console.WriteLine($"Email Host is: {emailHost}");

            var settingTwo = configuration["MySecondClass:SettingOne"];
            Console.WriteLine(settingTwo);

            //
            var url = serverConnection1.GetValue<string>("Url");
            var port = serverConnection1.GetValue<int>("Port");
            var isSsl = serverConnection1.GetValue<bool>("IsSSL");
            var password = serverConnection1.GetValue<string>("Password");

            // $localhost:$8080,password=$helloP@ssw0rd,ssl=$False
            var serverConnection = new ServerConnection();
            configuration.GetSection("ServerConnection").Bind(serverConnection);

            Console.WriteLine(serverConnection);

            if (args.Length != 0)
            {
                Console.Error.WriteLine("Usage : Program.exe ");
            }

            //

            var aH = new AppSettingsHandler("appsettings.json");
            var aS = aH.GetAppSettings();
            var myPath = aS.DataBase?.PathToDatabases;
            #endregion

            // https://dotnettutorials.net/lesson/transactions-in-entity-framework-core/
            // --------------------
            try
            {
                using (EFCoreDbContext context = new EFCoreDbContext(configuration))
                {
                    //Call the Initialize Method to Seed the Data
                    //DbInitializer.Initialize(context);
                    //DbInitializer.InitializeWithTransaction(context);


                    //
                    Console.WriteLine("Country Master:");
                    var Countries = context.tbl_Country_MA.ToList();
                    foreach (var country in Countries)
                    {
                        Console.WriteLine($"\tCountry ID: {country.CountryId}, Name: {country.CountryName}, Code: {country.CountryCode}");
                    }
                    Console.WriteLine("State Master:");
                    var States = context.tbl_State_MA.ToList();
                    foreach (var state in States)
                    {
                        Console.WriteLine($"\tState ID: {state.StateId}, Name: {state.StateName}");
                    }
                    Console.WriteLine("City Master:");
                    var Cities = context.tbl_City_MA.ToList();
                    foreach (var city in Cities)
                    {
                        Console.WriteLine($"\tCity ID: {city.CityId}, Name: {city.CityName}");
                    }
                }
                await Task.Delay(500);

                //Console.Read();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); ;
            }
            // --------------------

            //// https://dotnettutorials.net/lesson/transactions-in-entity-framework-core/
            //// --------------------
            //try
            //{
            //    using (EFCoreDbContext context = new EFCoreDbContext(configuration))
            //    {
            //        #region notusingTransaction
            //        //Student std1 = new Student()
            //        //{
            //        //    FirstName = "Pranaya",
            //        //    LastName = "Rout"
            //        //};
            //        //context.tbl_Student.Add(std1);

            //        //// This will automatically use a transaction.
            //        //context.SaveChanges();

            //        //Student std2 = new Student()
            //        //{
            //        //    FirstName = "Tarun",
            //        //    LastName = "Kumar"
            //        //};
            //        //context.tbl_Student.Add(std2);

            //        //// This will automatically use a new transaction.

            //        //context.SaveChanges();
            //        //Console.WriteLine("Entities are Saved");
            //        #endregion

            //        // Begin a new transaction
            //        await using (var transaction = await context.Database.BeginTransactionAsync())
            //        {
            //            try
            //            {
            //                // Perform database operations within the transaction
            //                Student std1 = new Student()
            //                {
            //                    FirstName = "Pranaya",
            //                    LastName = "Rout"
            //                };
            //                context.tbl_Student.Add(std1);
            //                // SaveChanges() but do not commit yet, this will persist changes but hold the commit until
            //                // we are sure that all operations succeed
            //                await context.SaveChangesAsync();
            //                Student std2 = new Student()
            //                {
            //                    FirstName = "Tarun",
            //                    LastName = "Kumar"
            //                };
            //                context.tbl_Student.Add(std2);
            //                // SaveChanges() but do not commit yet, this will persist changes but hold the commit until
            //                // we are sure that all operations succeed
            //                await context.SaveChangesAsync();
            //                // If everything is fine until here, commit the transaction
            //                await transaction.CommitAsync();
            //                Console.WriteLine("Entities are Saved");
            //            }
            //            catch (Exception ex)
            //            {
            //                // If an exception is thrown, roll back the transaction
            //                await transaction.RollbackAsync();
            //                // Handle or throw the exception as needed
            //                Console.WriteLine(ex.Message);
            //                throw;
            //            }
            //            //
            //        }

            //    }
            //    await Task.Delay(500);

            //    Console.Read();

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error: {ex.Message}"); ;
            //}
            //// --------------------

            // https://dotnettutorials.net/lesson/entity-states-in-entity-framework-core/
            #region EF Core - 1
            //// 1 - add
            //try
            //{
            //    using (EFCoreDbContext context = new EFCoreDbContext(configuration))
            //    {
            //        //Create a New Student Object
            //        var student = new Student2()
            //        {
            //            FirstName = "Pranaya",
            //            LastName = "Rout",
            //            Height = 5.10M,
            //            Weight = 50
            //        };
                    
            //        //Add the Student into context object using DbSet Property and Add method
            //        context.tbl_Student2.Add(student);

            //        //Now the Entity State will be in Added State
            //        Console.WriteLine($"Before SaveChanges Entity State: {context.Entry(student).State}");

            //        //Call the SaveChanges method to make the changes Permanent into the Database
            //        context.SaveChanges();

            //        //Now the Entity State will change from Added State to Unchanged State
            //        Console.WriteLine($"After SaveChanges Entity State: {context.Entry(student).State}");

            //        //
            //        var student2 = new Student2()
            //        {
            //            FirstName = "Prateek",
            //            LastName = "Sahu",
            //            Height = 5.82M,
            //            Weight = 60
            //        };

            //        context.tbl_Student2.Add(student2);
            //        Console.WriteLine($"Before SaveChanges Entity State: {context.Entry(student2).State}");
            //        context.SaveChanges();
            //        Console.WriteLine($"After SaveChanges Entity State: {context.Entry(student2).State}");


            //        Console.WriteLine("Student Saved Successfully...");
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error: {ex.Message}");
            //}

            //// 2 - update
            //try
            //{
            //    using (EFCoreDbContext context = new EFCoreDbContext(configuration))
            //    {
            //        //Fetch the Student from database whose Id = 1
            //        var student = context.tbl_Student2.Find(1);
            //        if (student != null)
            //        {
            //            //At this point Entity State will be Unchanged
            //            Console.WriteLine($"Before Updating Entity State: {context.Entry(student).State}");
            //            //Update the first name and last name
            //            student.FirstName = "Pranaya";
            //            student.LastName = "Sahu Prateek";
            //            student.Height = 6.00M;
            //            //At this point Entity State will be Modified
            //            Console.WriteLine($"After Updating Entity State: {context.Entry(student).State}");

            //            //Call SaveChanges method to update student data into database
            //            context.SaveChanges();
            //            //Now the Entity State will change from Modified State to Unchanged State
            //            Console.WriteLine($"After SaveChanges Entity State: {context.Entry(student).State}");
            //            Console.WriteLine("Student Updated Successfully...");
            //        }
            //        else
            //        {
            //            Console.WriteLine("Invalid Student ID : 1");
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error: {ex.Message}");
            //}

            //// 3 - delete
            //try
            //{
            //    using (EFCoreDbContext context = new EFCoreDbContext(configuration))
            //    {
            //        //Find the Student to be deleted by Id
            //        var student = context.tbl_Student2.Find(2);
            //        if (student != null)
            //        {
            //            //At this point the Entity State will be Unchanged
            //            Console.WriteLine($"Entity State Before Removing: {context.Entry(student).State}");

            //            //The following statement mark the Entity State as Deleted
            //            context.tbl_Student2.Remove(student);
            //            //context.Remove<Student>(student);
            //            //context.Remove(student);
            //            //At this point, the Entity State will be in Deleted state
            //            Console.WriteLine($"Entity State After Removing: {context.Entry(student).State}");
            //            //SaveChanges method will delete the Entity from the database
            //            context.SaveChanges();

            //            //Once the SaveChanges Method executed successfully, 
            //            //the Entity State will be in Detached state
            //            Console.WriteLine($"Entity State After Removing: {context.Entry(student).State}");
            //        }
            //        else
            //        {
            //            Console.WriteLine("Invalid Student ID: 2");
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error: {ex.Message}");
            //}

            #endregion

            #region EF Core - 2
            //
            try
            {
                using (EFCoreDbContext context = new EFCoreDbContext(configuration))
                {
                    //Creating a Variable
                    string firstName = "Pranaya";

                    //FirstOrDefault method using Method Syntax
                    var student1 = context.tbl_Student2.FirstOrDefault(s => s.FirstName == firstName);
                    Console.WriteLine($"FirstName: {student1?.FirstName}, LastName: {student1?.LastName}");

                    //First method using Method Syntax
                    var student2 = context.tbl_Student2.First(s => s.FirstName == firstName);
                    Console.WriteLine($"FirstName: {student2?.FirstName}, LastName: {student2?.LastName}");
                    
                    //FirstOrDefault method using Query Syntax
                    var student3 = (from s in context.tbl_Student2
                                    where s.FirstName == firstName
                                    select s).FirstOrDefault();
                    Console.WriteLine($"FirstName: {student3?.FirstName}, LastName: {student3?.LastName}");
                    
                    //First method using Query Syntax
                    var student4 = (from s in context.tbl_Student2
                                    where s.FirstName == firstName
                                    select s).First();
                    Console.WriteLine($"FirstName: {student4?.FirstName}, LastName: {student4?.LastName}");

                    // ---------------------
                    //Fetching All the Students from Students table
                    var studentList = context.tbl_Student2.ToList();
                    //Displaying all the Student information
                    foreach (var student in studentList)
                    {
                        Console.WriteLine($"FirstName: {student?.FirstName}, LastName: {student?.LastName}, DOB: {student?.DateOfBirth}, Height: {student?.Height}");
                    }


                    // ---------------------
                    //Order By using Query syntax
                    var studentsQS = from s in context.tbl_Student2
                                     orderby s.FirstName ascending
                                     select s;
                    // Query Syntax to Project the Result to an Anonymous Type
                    var selectQueryS = (from std in context.tbl_Student2
                                            select new
                                            {
                                                firstName = std.FirstName,
                                                lastName = std.LastName,
                                                height = std.Height
                                            });

                    //Order By using Method syntax
                    var studentsMS = context.tbl_Student2.OrderBy(s => s.FirstName).ToList();
                    //Method Syntax to Project the Result to an Anonymous Type
                    var selectMethodS = context.tbl_Student2.
                                            Select(std => new
                                            {
                                                firstName = std.FirstName,
                                                lastName = std.LastName,
                                                height = std.Height
                                            }).ToList();

                    //Displaying the Records
                    foreach (var student in studentsQS) // selectQueryS // studentsMS // selectMethodS
                    {
                        Console.WriteLine($"\tFirstName: {student?.FirstName}, LastName: {student?.LastName}");
                    }
                    // ---------------------

                    // ---------------------
                    //Join using Method Syntax
                    var JoinUsingMS = context.tbl_Student2 //Outer Data Source
                                .Join(
                                context.tbl_Standards,  //Inner Data Source
                                st => st.Standard != null ? st.Standard.StandardId : 0, //Inner Key Selector
                                sd => sd.StandardId, //Outer Key selector
                                (st, sd) => new //Projecting the data into an anonymous type
                                {
                                    StudentName = st.FirstName + " " + st.LastName,
                                    StandrdId = sd.StandardId,
                                    StandardDescriptin = sd.Description,
                                    StudentHeight = st.Height
                                }).ToList();
                    foreach (var x in JoinUsingMS)
                    {
                        Console.WriteLine($"Name: {x?.StudentName}, StandrdId: {x?.StandrdId}, StandareDesc: {x?.StandardDescriptin}, Height: {x?.StudentHeight}");
                    }
                    //Join using Query Syntax
                    var JoinUsingQS = (from st in context.tbl_Student2
                                       join sd in context.tbl_Standards
                                       on st.Standard != null ? st.Standard.StandardId : 0 equals sd.StandardId
                                       select new
                                       {
                                           StudentName = st.FirstName + " " + st.LastName,
                                           StandrdId = sd.StandardId,
                                           StandardDescriptin = sd.Description,
                                           StudentHeight = st.Height
                                       }).ToList();
                    foreach (var x in JoinUsingQS)
                    {
                        Console.WriteLine($"Name: {x?.StudentName}, StandrdId: {x?.StandrdId}, StandareDesc: {x?.StandardDescriptin}, Height: {x?.StudentHeight}");
                    }
                    // ---------------------


                    // ---------------------
                    // Begin a new transaction
                    await using (var transaction = await context.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            Standard st = new Standard()
                            {
                                StandardName = "1st Ex",
                                Description = "First-Standard Ex"
                            };
                            context.tbl_Standards.Add(st);
                            await context.SaveChangesAsync();

                            //Student2 sd = new Student2()
                            //{
                            //    FirstName = "Tarun",
                            //    LastName = "Kumar",
                            //    DateOfBirth = Convert.ToDateTime("1988-02-29"),
                            //    Height = 5.16M,
                            //    Weight = 85,
                            //    StudentId = st.StandardId
                            //};
                            //context.tbl_Student2.Add(sd);
                            //await context.SaveChangesAsync();
                            
                            await transaction.CommitAsync();

                            Console.WriteLine("Entities are Saved");
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            Console.WriteLine(ex.Message);
                            throw;
                        }
                        //
                    }
                    // ---------------------


                    //
                }

                //
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            #endregion







            #region insert MillionRecords

            //public class DataContext : DbContext
            //{
            //    public DataContext(DbContextOptions<DataContext> options) : base(options)
            //    {
            //    }
            //    public DbSet<Product> Products { get; set; }
            //}

            // initialize data context
            //var connectionString = "Data Source=localhost; Initial Catalog=Product; Integrated Security=True";
            //var contextOptionsBuilder = new DbContextOptionsBuilder<DataContext>();
            //contextOptionsBuilder.UseSqlServer(connectionString);
            //var context = new DataContext(contextOptionsBuilder.Options);

            try
            {
                using (EFCoreDbContext context = new EFCoreDbContext(configuration))
                {
                    // create database
                    await context.Database.EnsureDeletedAsync();
                    await context.Database.EnsureCreatedAsync();

                    // setup bogus faker
                    var faker = new Faker<Product>();
                    faker.RuleFor(p => p.Code, f => f.Commerce.Ean13());
                    faker.RuleFor(p => p.Description, f => f.Commerce.ProductName());
                    faker.RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0]);
                    faker.RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000));

                    // generate 1 million products
                    var products = faker.Generate(1_000_000);

                    var batches = products
                        .Select((p, i) => (Product: p, Index: i))
                        .GroupBy(x => x.Index / 100_000)
                        .Select(g => g.Select(x => x.Product).ToList())
                        .ToList();

                    ////// insert batches
                    ////var stopwatch = new Stopwatch();
                    ////stopwatch.Start();

                    ////var count = 0;
                    ////foreach (var batch in batches)
                    ////{
                    ////    count++;
                    ////    Console.WriteLine($"Inserting batch {count} of {batches.Count}...");

                    ////    await context.tbl_Products.AddRangeAsync(batch);
                    ////    await context.SaveChangesAsync();
                    ////}

                    ////stopwatch.Stop();

                    ////Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
                    Console.WriteLine("Press any key to exit...");


                }
                await Task.Delay(500);

                //Console.Read();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); ;
            }

            #endregion

            //
            ////https://github.com/jindeveloper/.NET-Core-Weather-API-Wrapper/blob/master/Weather.ApiWrapper/WeatherApi.cs
            //var weatherWrapper = new WeatherApi();

            //var result = await weatherWrapper.GetResult();

            //var parsed = JToken.Parse(result);

            //Console.WriteLine(parsed.ToString(Formatting.Indented));



            //
            Console.Read();
        }



    }


}