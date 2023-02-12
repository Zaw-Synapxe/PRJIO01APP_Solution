using System.Globalization;

namespace GenericsBasics.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Salaries salaries = new Salaries();

            // ArrayList salaryList = salaries.GetSalaries();
            List<float> salaryList = salaries.GetSalaries();

            float salary = salaryList[1];

            salary = salary + (salary * 0.02f);

            Console.WriteLine(salary);

            Console.ReadKey();

            //--------------------------------------------------
            //ValueTypeDemo
            decimal loanAmount = 0;
            decimal loanAmountCopy = 0;

            Console.WriteLine("Please enter a loan amount");

            loanAmount = Decimal.Parse(Console.ReadLine(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

            loanAmountCopy = loanAmount;

            Console.WriteLine($"loanAmount: {loanAmount}, loanAmountCopy {loanAmountCopy}");

            Console.ReadKey();

            loanAmount = loanAmount + 600;

            Console.WriteLine();
            Console.WriteLine($"After adding 600 pounds loanAmount: {loanAmount}, loanAmountCopy {loanAmountCopy}");

            Console.ReadKey();


            //-----------------------------------------------------
            //Employee Application
            int employeeId = 0;
            string firstName = String.Empty;
            string lastName = String.Empty;
            decimal annualSalary = 0;
            char gender = '\0';
            bool isManager = false;

            Console.WriteLine("Please enter a unique Id for this employee");

            employeeId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter the employee's first name");

            firstName = Console.ReadLine();

            Console.WriteLine("Please enter the employee's last name");

            lastName = Console.ReadLine();

            Console.WriteLine("Please enter the employee's annual salary");

            annualSalary = Decimal.Parse(Console.ReadLine(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

            Console.WriteLine("Please enter the employee's gender ('f' = female, 'm' = male)");

            gender = Convert.ToChar(Console.ReadLine());

            Console.WriteLine("The employee is a manager (true/false)");

            isManager = Convert.ToBoolean(Console.ReadLine());

            string genderTerm = (gender == 'f') ? "female" : "male";

            string managerNarrative = (isManager) ? "part of the management team" : "currently not part of the management team";


            string narrative = $"Employee with an Id of {employeeId} ";
            narrative += $"who's full name is {firstName} {lastName}{Environment.NewLine}";
            narrative += $"is a {genderTerm} employee who is {managerNarrative}.{Environment.NewLine}";
            narrative += $"This employee earns an annual salary of {annualSalary} pounds.";

            Console.Clear();

            Console.WriteLine(narrative);

            Console.ReadKey();


            //------------------------------------------------------
            // Student Application
            int stuId = 0;
            string stufirstName = string.Empty;
            string stulastName = string.Empty;
            decimal stuloanAmount = 0;
            char stugender = '\0';
            bool isNew = false;

            Console.WriteLine("Please enter the student Id");

            stuId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter the student's first name");

            stufirstName = Console.ReadLine();

            Console.WriteLine("Please enter the student's last name");

            stulastName = Console.ReadLine();

            Console.WriteLine("Please enter the student's loan amount");

            stuloanAmount = Decimal.Parse(Console.ReadLine(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

            Console.WriteLine("Please enter the student's gender ('f' = female, 'm' = male)");

            stugender = Convert.ToChar(Console.ReadLine());

            Console.WriteLine("The student is new (true/false)");

            isNew = Convert.ToBoolean(Console.ReadLine());

            Student student = new Student(stuId, stufirstName, stulastName, stuloanAmount, stugender, isNew);

            Console.Clear();

            Student studentCopy = student;

            Console.WriteLine("Student data  " + student.StudentData());

            Console.WriteLine();

            Console.WriteLine("Student copy data  " + studentCopy.StudentData());

            Console.WriteLine();

            Console.WriteLine("Please update the student's loan amount");

            student.UpdateLoanAmount(Convert.ToDecimal(Console.ReadLine()));

            string dividerText = "After loan update";

            Console.WriteLine(new String('-', dividerText.Length));
            Console.WriteLine(dividerText);
            Console.WriteLine(new String('-', dividerText.Length));

            Console.WriteLine("Student data  " + student.StudentData());

            Console.WriteLine();

            Console.WriteLine("Student copy data  " + studentCopy.StudentData());

            Console.ReadKey();




            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--> Print Red");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--> Print Green");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--> Print Yellow");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("--> Print Blue");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("--> Print Gray");
            Console.ResetColor();
        }
    }

    public class Salaries
    {
        //ArrayList _salaryList = new ArrayList();
        List<float> _salaryList = new List<float>();

        public Salaries()
        {
            /*_salaryList.Add(60000.34);
            _salaryList.Add(40000.51);
            _salaryList.Add(20000.23);*/

            _salaryList.Add(60000.34f);
            _salaryList.Add(40000.51f);
            _salaryList.Add(20000.23f);

        }

        // public ArrayList GetSalaries()
        public List<float> GetSalaries()
        {
            return _salaryList;
        }

    }


    public class Student
    {
        int _stuId = 0;
        string _firstName = String.Empty;
        string _lastName = String.Empty;
        decimal _loanAmount = 0;
        char _gender = '\0';
        bool _isNew = false;

        public Student(int stuId, string firstName, string lastName, decimal loanAmount, char gender, bool isNew)
        {
            _stuId = stuId;
            _firstName = firstName;
            _lastName = lastName;
            _loanAmount = loanAmount;
            _gender = gender;
            _isNew = isNew;

        }

        public void UpdateLoanAmount(decimal loanAmount)
        {
            _loanAmount = loanAmount;

        }

        public string StudentData()
        {
            string studentData = $"stuId: {_stuId}, firstName: {_firstName}, loan Amount: {_loanAmount}";

            return studentData;

        }


    }


}