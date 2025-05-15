using Domain.Entities;

namespace XYZ.API.Data
{
    public class SeedData
    {
        public IEnumerable<PersonalInfo> GetPersonalInfoList()
        {
            List<PersonalInfo> listPersonalInfo = new List<PersonalInfo>();
            for (int i = 0; i < 1000; i++)
            {
                Random _Random = new();
                PersonalInfo _PersonalInfo = new()
                {
                    FirstName = "Tom-" + GenerateString(6),
                    LastName = GenerateString(5),
                    DateOfBirth = DateTime.Now.AddDays(-_Random.Next(52)),
                    City = GenerateString(4),
                    Country = GenerateString(4),
                    MobileNo = _Random.Next(1000, 100000).ToString(),
                    Email = "dev@" + GenerateString(6),
                    //PasportNo = _Random.Next(1000, 1000000).ToString(),
                    //NID = _Random.Next(1000, 1000000).ToString(),

                    CreatedDate = DateTime.Now.AddDays(-_Random.Next(30)),
                    UpdatedDate = DateTime.Now.AddDays(-_Random.Next(30))
                };
                listPersonalInfo.Add(_PersonalInfo);
            }
            return listPersonalInfo;
        }

        readonly Random _Random = new();
        private const string Alphabet = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private string GenerateString(int size)
        {
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = Alphabet[_Random.Next(Alphabet.Length)];
            }
            return new string(chars);
        }


        //
        public IEnumerable<Branch> GetBranchList()
        {
            return new List<Branch>
            {
                new Branch { Name = "Autumn", Description = "TBD", },
                new Branch { Name = "Spring", Description = "TBD", },
                new Branch { Name = "Winter", Description = "TBD", },
                new Branch { Name = "Test Branch", Description = "TBD", },
            };
        }
        public IEnumerable<Department> GetDepartmentList()
        {
            return new List<Department>
            {
                new Department { Name = "CSE", Description = "TBD", },
                new Department { Name = "EEE", Description = "TBD", },
                new Department { Name = "ECE", Description = "TBD", },
                new Department { Name = "Test Department", Description = "TBD", },
            };
        }

        //
        public IEnumerable<Category> GetCategoryList()
        {
            return new List<Category>
            {
                new Category { Name = "Item Category 01", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 02", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 03", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 04", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 05", Description = "Description of your category item: lorem ipsum" },

                new Category { Name = "Item Category 06", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 07", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 08", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 09", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 10", Description = "Description of your category item: lorem ipsum" },

                new Category { Name = "Item Category 11", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 12", Description = "Description of your category item: lorem ipsum" },
            };
        }

        //
    }
}
