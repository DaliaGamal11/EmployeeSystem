using System;
using System.Data;
using System.Transactions;
using System.Xml.Linq;

namespace ConsoleApp7
{
    // Enum for Gender
    public enum GenderType
    {
        Female = 'F',
        Male = 'M'
    }

    public enum SecurityLevel
    {
        Guest,          // Level 1
        Developer,      // Level 2
        Secretary,      // Level 3
        DBA,            // Level 4
    }

    public struct Employee
    {
        private int id;
        private SecurityLevel securityLevel;
        private int salary;
        private HireDate hireDate;
        private GenderType gender;

        public Employee(int id, SecurityLevel securityLevel, int salary, GenderType gender, HireDate hireDate)
        {
            this.id = id;
            this.securityLevel = securityLevel;
            this.salary = salary;
            this.gender = gender;
            this.hireDate = hireDate;
        }

        // Properties 
        public int Id { get => id; set => id = value; }
        public SecurityLevel SecurityLevel { get => securityLevel; set => securityLevel = value; }
        public int Salary { get => salary; set => salary = value; }
        public GenderType Gender { get => gender; set => gender = value; }
        public HireDate HireDate { get => hireDate; set => hireDate = value; }

        public void SetGender(char g)
        {
            // Convert the char to upper case and check if it matches 'M' or 'F'
            if (g == 'F' || g == 'f')
            {
                gender = GenderType.Female;
            }
            else if (g == 'M' || g == 'm')
            {
                gender = GenderType.Male;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'F' for Female or 'M' for Male.");
            }
        }

        public void DisplayEmployeeInfo()
        {
            Console.WriteLine($"ID: {id}");
            Console.WriteLine($"Security Level: {securityLevel}");
            Console.WriteLine(string.Format("Salary: {0:C}", salary));
            hireDate.Display();  // Display the hire date
            Console.WriteLine($"Gender: {gender}");
        }
    }

    // Hire Date Structure
    public struct HireDate
    {
        private int day;
        private int month;
        private int year;

        public HireDate(int day, int month, int year)
        {
            this.day = day;
            this.month = month;
            this.year = year;
        }

        // Properties 
        public int Day { get => day; set => day = value; }
        public int Month { get => month; set => month = value; }
        public int Year { get => year; set => year = value; }

        public void SetDay(int d)
        {
            while (d < 1 || d > 31)
            {
                Console.WriteLine("Please enter a value within 1 to 31 for the Day.");
                if (!int.TryParse(Console.ReadLine(), out d))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
            day = d;
        }

        public void SetMonth(int m)
        {
            while (m < 1 || m > 12)
            {
                Console.WriteLine("Please enter a value within 1 to 12 for the Month.");
                if (!int.TryParse(Console.ReadLine(), out m))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
            month = m;
        }

        public void SetYear(int y)
        {
            int currentYear = DateTime.Now.Year;  // Get the current year

            while (y < 1900 || y > currentYear)
            {
                Console.WriteLine($"Please enter a valid year between 1900 and {currentYear}.");
                if (!int.TryParse(Console.ReadLine(), out y))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
            year = y;
        }

        public void Display()
        {
            Console.WriteLine($"Hire Date: {day}/{month}/{year}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Employee[] employees = new Employee[3];

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Enter the info for employee {i + 1}:");

                
                Console.Write("Enter Employee ID: ");
                int id = 0;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Invalid input. Please enter a valid ID.");
                }

                Console.Write("Enter Employee Gender (F for Female, M for Male): ");
                char genderInput = char.MinValue;
                while (genderInput != 'F' && genderInput != 'M' && genderInput != 'f' && genderInput != 'm')
                {
                    try
                    {
                        genderInput = char.Parse(Console.ReadLine().ToUpper());  // Convert to upper case to simplify the check
                        if (genderInput == 'F' || genderInput == 'M' || genderInput == 'f' || genderInput == 'm')
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid gender input. Please enter 'F' for Female or 'M' for Male.");
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid input. Please enter 'F' for Female or 'M' for Male.");
                    }
                }

                
                employees[i].SetGender(genderInput);


                Console.Write("Enter Employee Security Level (Guest, Developer, Secratery , DBA): ");
                string securityLevelInput = Console.ReadLine();

                SecurityLevel securityLevelEnum;
                if (!Enum.TryParse(securityLevelInput, true, out securityLevelEnum))  
                {
                    Console.WriteLine("Invalid security level. Defaulting to Basic.");
                    securityLevelEnum = SecurityLevel.Guest;  // Set default if input is invalid
                }

                Console.Write("Enter Salary: ");
                int salary = 0;
                while (!int.TryParse(Console.ReadLine(), out salary) || salary < 0)
                {
                    Console.WriteLine("Invalid salary. Please enter a valid salary.");
                }

                
                Console.WriteLine("Enter Hiring Date:");
                Console.Write("Day: ");
                int day = 0;
                while (!int.TryParse(Console.ReadLine(), out day))
                {
                    Console.WriteLine("Invalid input. Please enter a valid day.");
                }

                Console.Write("Month: ");
                int month = 0;
                while (!int.TryParse(Console.ReadLine(), out month))
                {
                    Console.WriteLine("Invalid input. Please enter a valid month.");
                }

                Console.Write("Year: ");
                int year = 0;
                while (!int.TryParse(Console.ReadLine(), out year))
                {
                    Console.WriteLine("Invalid input. Please enter a valid year.");
                }

                HireDate hireDate = new HireDate(day, month, year);

                // Set Employee Data
                employees[i] = new Employee(id, securityLevelEnum, salary, genderInput == 'M' ? GenderType.Male : GenderType.Female, hireDate);
            }

            Console.WriteLine("Here are the data of the employees:");
            foreach (var employee in employees)
            {
                employee.DisplayEmployeeInfo();
            }
        }
    }
}
