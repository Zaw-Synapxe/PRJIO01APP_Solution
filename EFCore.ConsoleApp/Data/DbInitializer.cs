using EFCore.ConsoleApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.ConsoleApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(EFCoreDbContext context)
        {
            context.Database.EnsureCreated();
            // Check if there are any Country or State or City already in the database
            if (context.tbl_Country_MA.Any() || context.tbl_State_MA.Any() || context.tbl_City_MA.Any())
            {
                // Database has been seeded
                return;
            }
            else
            {
                //Here, you need to Implement the Custom Logic to Seed the Master data
                IList<Country> countries = new List<Country>();
                Country IND = new() { CountryName = "INDIA", CountryCode = "IND" };
                Country AUS = new() { CountryName = "Austrailla", CountryCode = "AUS" };
                Country SIG = new() { CountryName = "Singapore", CountryCode = "SIN" };
                Country MYA = new() { CountryName = "Myanmar", CountryCode = "MYA" };
                countries.Add(IND);
                countries.Add(AUS);
                countries.Add(SIG);
                countries.Add(MYA);
                context.tbl_Country_MA.AddRange(countries);

                IList<State> states = new List<State>();
                State Odisha = new() { StateName = "ODISHA", CountryId = IND.CountryId };
                State Delhi = new() { StateName = "DELHI", CountryId = IND.CountryId };
                State Shan = new() { StateName = "SHAN", CountryId = MYA.CountryId };
                states.Add(Odisha);
                states.Add(Delhi);
                states.Add(Shan);
                context.tbl_State_MA.AddRange(states);

                IList<City> cities = new List<City>();
                City BBSR = new() { CityName = "Bhubaneswar", StateId = Odisha.StateId };
                City CTC = new() { CityName = "Cuttack", StateId = Odisha.StateId };
                City TGYI = new() { CityName = "TaungGyi", StateId = Shan.StateId };
                cities.Add(BBSR);
                cities.Add(CTC);
                cities.Add(TGYI);
                context.tbl_City_MA.AddRange(cities);

            }
            context.SaveChanges();

        }

        //Using Transactions: For larger seeding operations or when data integrity is critical,
        //you might use database transactions to ensure all seed data is committed or rolled back as a single unit.
        public static void InitializeWithTransaction(EFCoreDbContext context)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Database.EnsureCreated();
                    // Check if there are any Country or State or City already in the database
                    if (context.tbl_Country_MA.Any() || context.tbl_State_MA.Any() || context.tbl_City_MA.Any())
                    {
                        // Database has been seeded
                        return;
                    }
                    else
                    {
                        //Here, you need to Implement the Custom Logic to Seed the Master data
                        IList<Country> countries = new List<Country>();
                        Country IND = new() { CountryName = "INDIA", CountryCode = "IND" };
                        Country AUS = new() { CountryName = "Austrailla", CountryCode = "AUS" };
                        Country SIG = new() { CountryName = "Singapore", CountryCode = "SIN" };
                        Country MYA = new() { CountryName = "Myanmar", CountryCode = "MYA" };
                        countries.Add(IND);
                        countries.Add(AUS);
                        countries.Add(SIG);
                        countries.Add(MYA);
                        context.tbl_Country_MA.AddRange(countries);

                        IList<State> states = new List<State>();
                        State Odisha = new() { StateName = "ODISHA", CountryId = IND.CountryId };
                        State Delhi = new() { StateName = "DELHI", CountryId = IND.CountryId };
                        State Shan = new() { StateName = "SHAN", CountryId = MYA.CountryId };
                        states.Add(Odisha);
                        states.Add(Delhi);
                        states.Add(Shan);
                        context.tbl_State_MA.AddRange(states);

                        IList<City> cities = new List<City>();
                        City BBSR = new() { CityName = "Bhubaneswar", StateId = Odisha.StateId };
                        City CTC = new() { CityName = "Cuttack", StateId = Odisha.StateId };
                        City TGYI = new() { CityName = "TaungGyi", StateId = Shan.StateId };
                        cities.Add(BBSR);
                        cities.Add(CTC);
                        cities.Add(TGYI);
                        context.tbl_City_MA.AddRange(cities);
                    }

                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Handle or log the exception
                    Console.WriteLine("Error : " + ex.Message);
                }
            }
        }

        //
    }
}
