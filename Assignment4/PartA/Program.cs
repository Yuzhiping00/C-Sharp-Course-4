/**
 * Author: Zhiping Yu
 * Date:   November 15 2020
 * Purpose: This program stores the information in a list of Employee objects by reading the employee.txt data
 *          file. If reading file is failed, the program will be terminated. Otherwise, user will be displayed
 *          with a menu to choose how to display the employee's information in a table. If user choose to exit
 *          the program, he/she can enter number 6.
 *
 * I, Zhiping Yu, 000822513 certify that this material is my original work.  
 *  No other person's work has been used without due acknowledgement.
 * 
 */


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PartA
{
    /// <summary>
    /// This is the main tester class that the program starts at 
    /// </summary>
    class Program

    {
        /// <summary>
        /// The main method reads the data file, return a list of employees. If return null, the program should be terminated
        /// Otherwise, the program should display the employees information which is based on the seleciton of user.
        /// </summary>
        /// <param name="args">Command line arguments are not used in this program</param>
        static void Main(string[] args)

        {
            List<Employee> employees = Read();
            if (employees != null)
            {
                processUserSelection(employees);
            }
            else
            {
                Console.WriteLine("\nProgram is terminated due to exception, Click any key to close.");
                Console.ReadKey();
            }
        }
        /// <summary>
        /// Ask user to make a seletion at a specific range and respond to any requests.
        /// </summary>
        /// <param name="employees">The List of employees</param>
        private static void processUserSelection(List<Employee> employees)
        {
            DisplayEmployees(employees);
            Console.WriteLine("\n\nOperation starts from here:\n");
            DisplayMenu();
            while (true)
            {
                string choice = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("\nYou selected : " + choice);
                if (int.TryParse(choice, out int option) == false || option < 1 || option > 6)
                {
                    Console.WriteLine("\nPlease enter an integer beween 1 and 6");
                    Console.WriteLine("Enter choice:\n ");
                    continue;
                }
                if (option == 6)
                {
                    break;
                }
                else
                {
                    Sort(option, employees);
                }
                Console.WriteLine("\n**********************************************");
                DisplayMenu();
            }
        }
        /// <summary>
        /// Sort the employee's properties based on user's different selections
        /// </summary>
        /// <param name="option">the number user input</param>
        /// <param name="employees">the List of employees</param>
        private static void Sort(int option, List<Employee> employees)
        {
            if (option == 1)
            {
                SortName(employees); // sort employee's name in ascending order
            }
            else if (option == 2)
            {
                SortNumber(employees); //sort employee's number in ascending order
            }
            else if (option == 3) //sort employee's hourly rate in descending order
            {
                SortRate(employees);
            }
            else if (option == 4)
            {
                SortHours(employees); //sort employee's weekly hours in descending order
            }
            else
            {
                SortGrossPay(employees); //sort employee's gross pay in descending order
            }

        }
        /// <summary>
        /// Use Lambda expression to sort employee's gross pay in descending order
        /// </summary>
        /// <param name="employees">The List of employees</param>
        private static void SortGrossPay(List<Employee> employees)
        {
            employees.Sort((employee1, employee2) => employee1.Gross.CompareTo(employee2.Gross));
            employees.Reverse(); // reverse the entire elements in the List<Employee>

            /*Another two ways to sort list in descending order
            employees.Sort((employee1, employee2) => employee2.Gross.CompareTo(employee1.Gross));
            employees.Sort((employee1, employee2) => -employee1.Gross.CompareTo(employee2.Gross));*/


            Console.WriteLine("\nThis option is to descend employee's hours:");
            DisplayEmployees(employees);

        }
        /// <summary>
        /// User Lambda expression to sort employee's weekly hours in descending order
        /// </summary>
        /// <param name="employees">The List of employees</param>
        private static void SortHours(List<Employee> employees)
        {
            employees.Sort((employee1, employee2) => employee1.Hours.CompareTo(employee2.Hours));
            employees.Reverse(); // reverse the entire elements in the List<Employee>
            Console.WriteLine("\nThis option is to descend employee's hours:");
            DisplayEmployees(employees);
        }
        /// <summary>
        /// User Lambda expression to sort employee's hourly rate in descending order
        /// </summary>
        /// <param name="employees">The List of employees</param>
        private static void SortRate(List<Employee> employees)
        {
            employees.Sort((employee1, employee2) => employee1.Rate.CompareTo(employee2.Rate));
            employees.Reverse(); // reverse the entire elements in the List<Employee>
            Console.WriteLine("\nThis option is to descend employee's rate");
            DisplayEmployees(employees);
        }
        /// <summary>
        /// User Lambda expression to sort employee's id in ascending order
        /// </summary>
        /// <param name="employees">The List of employees</param>
        private static void SortNumber(List<Employee> employees)
        {
            employees.Sort((employee1, employee2) => employee1.Number.CompareTo(employee2.Number));
            Console.WriteLine("\nThis option is to ascend employee's number:");
            DisplayEmployees(employees);
        }
        /// <summary>
        /// User Lambda expression to sort employee's name in ascending order
        /// </summary>
        /// <param name="employees">The List of employees</param>
        private static void SortName(List<Employee> employees)
        {

            employees.Sort((employee1, employee2) => employee1.Name.CompareTo(employee2.Name));
            Console.WriteLine("\nThis option is to ascend employee's name:");
            DisplayEmployees(employees);
        }
        /// <summary>
        /// Display the options menu for user to choose 
        /// </summary>
        private static void DisplayMenu()
        {

            Console.WriteLine("1. Sort by Employee Name (ascending) ");
            Console.WriteLine("2. Sort by Employee Number (ascending) ");
            Console.WriteLine("3. Sort by Employee Pay Rate (descending) ");
            Console.WriteLine("4. Sort by Employee Hours (descending) ");
            Console.WriteLine("5. Sort by Employee Gross Pay (descending) ");
            Console.WriteLine("6. Exit");
            Console.WriteLine("Enter choice:");
        }
        /// <summary>
        /// Print out each employee information with a title
        /// </summary>
        /// <param name="employees">The List of employees</param>
        private static void DisplayEmployees(List<Employee> employees)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Employee              Number  Rate    Hours  Gross Pay               ZhiPing's Company");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("====================  ======  ======  =====  =========           ============================");
            foreach (Employee element in employees)
            {
                Console.WriteLine(element);
            }
        }
        /// <summary>
        /// Create an employees list based on reading a text file. If file cannot be found
        /// or data inside is not valid, an exception will handle those problems. 
        /// </summary>
        /// <returns>The List of employees</returns>

        private static List<Employee> Read()
        {
            List<Employee> employees = new List<Employee>();
            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                //Open the file for reading purposes
                fs = new FileStream("employees.txt", FileMode.Open);
                sr = new StreamReader(fs);

                // As long as there is data in the file, keep processing 
                // Each employee record is comma separated, so create new employees and add them 
                // into the list of employee

                while (!sr.EndOfStream)
                {
                    string input = sr.ReadLine();
                    string[] elements = input.Split(',');
                    string name = elements[0];
                    if (int.TryParse(elements[1], out int number) == false)
                    {
                        Console.WriteLine("Failed to parse the number from the file!");

                    }
                    if (decimal.TryParse(elements[2], out decimal rate) == false)
                    {
                        Console.WriteLine("Failed to parse the rate from the file!");
                    }
                    if (double.TryParse(elements[3], out double hours) == false)
                    {
                        Console.WriteLine("Failed to parse the hours from the file!");
                    }
                    employees.Add(new Employee(name, number, rate, hours));
                }
            } // Just in case the file can't be found
            catch (Exception e)
            {
                Console.WriteLine("\nException loading employees from the file due to " +
                 e.Message);
                return null;
            } 
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
                if (sr != null)
                {
                    sr.Close();
                }

            }
            return employees;

        }
    }
}


