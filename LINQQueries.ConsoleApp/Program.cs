namespace LINQQueries.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18, StandardID = 1 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 21, StandardID = 1 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 18, StandardID = 2 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20, StandardID = 2 } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 21 }
            };

            IList<Standard> standardList = new List<Standard>() {
                new Standard(){ StandardID = 1, StandardName="Standard 1"},
                new Standard(){ StandardID = 2, StandardName="Standard 2"},
                new Standard(){ StandardID = 3, StandardName="Standard 3"}
            };


            // Multiple Select and where operator
            var studentNames = studentList.Where(s => s.Age > 18)
                              .Select(s => s)
                              .Where(st => st.StandardID > 0)
                              .Select(s => s.StudentName);


            // returns Collection of Anonymous Objects
            var teenStudentsName = from s in studentList
                                   where s.Age > 12 && s.Age < 20
                                   select new { StudentName = s.StudentName };

            teenStudentsName.ToList().ForEach(s => Console.WriteLine(s.StudentName));


            // LINQ GroupBy Query
            var studentsGroupByStandard = from s in studentList
                                          group s by s.StandardID into sg
                                          orderby sg.Key
                                          select new { sg.Key, sg };
            var studentsGroupByStandard2 = from s in studentList
                                          where s.StandardID > 0
                                          group s by s.StandardID into sg
                                          orderby sg.Key
                                          select new { sg.Key, sg };


            foreach (var group in studentsGroupByStandard)
            {
                Console.WriteLine("StandardID {0}:", group.Key);

                group.sg.ToList().ForEach(st => Console.WriteLine(st.StudentName));
            }


            // Left Outer Join
            var studentsGroup = from stad in standardList
                                join s in studentList
                                on stad.StandardID equals s.StandardID
                                    into sg
                                select new
                                {
                                    StandardName = stad.StandardName,
                                    Students = sg
                                };

            foreach (var group in studentsGroup)
            {
                Console.WriteLine(group.StandardName);

                group.Students.ToList().ForEach(st => Console.WriteLine(st.StudentName));
            }

            // Left Outer Join
            var studentsWithStandard = from stad in standardList
                                       join s in studentList
                                       on stad.StandardID equals s.StandardID
                                       into sg
                                       from std_grp in sg
                                       orderby stad.StandardName, std_grp.StudentName
                                       select new
                                       {
                                           StudentName = std_grp.StudentName,
                                           StandardName = stad.StandardName
                                       };


            foreach (var group in studentsWithStandard)
            {
                Console.WriteLine("{0} is in {1}", group.StudentName, group.StandardName);
            }


            // Sorting
            var sortedStudents = from s in studentList
                                 orderby s.StandardID, s.Age
                                 select new
                                 {
                                     StudentName = s.StudentName,
                                     Age = s.Age,
                                     StandardID = s.StandardID
                                 };

            sortedStudents.ToList().ForEach(s => Console.WriteLine("Student Name: {0}, Age: {1}, StandardID: {2}", s.StudentName, s.Age, s.StandardID));


            // nner join
            var studentWithStandard = from s in studentList
                                      join stad in standardList
                                      on s.StandardID equals stad.StandardID
                                      select new
                                      {
                                          StudentName = s.StudentName,
                                          StandardName = stad.StandardName
                                      };

            studentWithStandard.ToList().ForEach(s => Console.WriteLine("{0} is in {1}", s.StudentName, s.StandardName));


            // Nested Query
            var nestedQueries = from s in studentList
                                where s.Age > 18 && s.StandardID ==
                                    (from std in standardList
                                     where std.StandardName == "Standard 1"
                                     select std.StandardID).FirstOrDefault()
                                select s;

            nestedQueries.ToList().ForEach(s => Console.WriteLine(s.StudentName));



            //
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            var evenNumbers = from num in numbers
                              where num % 2 == 0
                              select num;
            var evenNumberStrings = from num in numbers
                                    where num % 2 == 0
                                    select num.ToString();


            //
            List<Person> persons = new List<Person>
            {
                new Person { Name = "Alice", Age = 25, City = "New York" },
                new Person { Name = "Bob", Age = 30, City = "Los Angeles" },
                new Person { Name = "Charlie", Age = 20, City = "San Francisco" },
                new Person { Name = "David", Age = 35, City = "New York" },
            };

            var newYorkers = from person in persons
                             where person.City == "New York"
                             select person;

            foreach (var person in newYorkers)
            {
                Console.WriteLine("{0}, {1}", person.Name, person.Age);
            }


            //
            List<Order> orders = new List<Order>
            {
                new Order { OrderId = 1, CustomerId = 1, Total = 100.00m },
                new Order { OrderId = 2, CustomerId = 2, Total = 200.00m },
                new Order { OrderId = 3, CustomerId = 1, Total = 50.00m },
                new Order { OrderId = 4, CustomerId = 3, Total = 150.00m },
                new Order { OrderId = 5, CustomerId = 2, Total = 75.00m },
                new Order { OrderId = 6, CustomerId = 1, Total = 300.00m },
                new Order { OrderId = 7, CustomerId = 3, Total = 125.00m },
            };

            var customerAverages = orders
                .GroupBy(order => order.CustomerId)
                .Select(group => new
                {
                    CustomerId = group.Key,
                    AverageTotal = group.Average(order => order.Total)
                })
                .OrderByDescending(result => result.AverageTotal);

            foreach (var result in customerAverages)
            {
                Console.WriteLine($"Customer {result.CustomerId} has an average order total of {result.AverageTotal:C}");
            }


            // C# LINQ full join
            List<int> list1 = new List<int> { 1, 2, 3 };
            List<int> list2 = new List<int> { 2, 3, 4 };

            var fullOuterJoin = list1
                .SelectMany(x => list2.DefaultIfEmpty(),
                    (x, y) => new { Left = x, Right = y })
                .Where(xy => xy.Left == xy.Right || xy.Right == 0 || xy.Left == 0)
                .Select(xy => new { Value = xy.Left == 0 ? xy.Right : xy.Left });

            foreach (var item in fullOuterJoin)
            {
                Console.WriteLine(item.Value);
            }

            // C# LINQ distinct
            int[] distnumbers = { 1, 2, 3, 3, 4, 5, 5, 6 };
            var distinctNumbers = distnumbers.Distinct();
            foreach (var number in distinctNumbers)
            {
                Console.WriteLine(number);
            }

            // LINQ Aggregating Lists
            List<int> numbersA = new List<int> { 1, 2, 3, 4, 5 };
            int sum = numbersA.Aggregate((acc, x) => acc + x);
            //or
            int sum2 = numbersA.Sum();




            //
        }
    }

    //
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
        public int StandardID { get; set; }
    }

    public class Standard
    {
        public int StandardID { get; set; }
        public string StandardName { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal Total { get; set; }
    }


    //
}