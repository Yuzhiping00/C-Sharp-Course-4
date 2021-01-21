/**
 * Author: Zhiping Yu
 * Date:   November 15 2020
 * Purpose: This class is a concrete class and it can be used to create Employee objects in order to call its 
 *          methods to complete some tasks.
 *          
 * I, Zhiping Yu, 000822513 certify that this material is my original work.  
 *  No other person's work has been used without due acknowledgement.
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartA
{
    /// <summary>
    /// This class is a model class which is used to describe the employee
    /// </summary>
    class Employee
    {
        // Employee's Properties
        public  string Name { get; set; } // employee's name
        public  int Number  { get; set; } // employee's Number
        public  decimal Rate  { get; set; } // employee's hourly Rate
        public  double Hours { get; set; } // employee's weekly hours
        public  decimal Gross { get; } // employee's gross pay

        /// <summary>
        /// Four-argument constructor for Employee
        /// </summary>
        /// <param name="name">Employee name</param>
        /// <param name="number">Employee id</param>
        /// <param name="rate">Employee hourly rate</param>
        /// <param name="hours">weekly hours Employee worked</param>
        
        public Employee(string name, int number, decimal rate, double hours)
        {
            Name = name;
            Number = number;
            Rate = rate;
            Hours = hours;
            Gross = (hours < 40) ? (decimal)hours * rate : (40.0m * rate) + (((decimal)hours - 40.0m) * rate * 1.5m);

        }
        
       /// <summary>
       /// Display an employee's properties in a formated way
       /// </summary>
       /// <returns>The representation of an Employee</returns>
       public override String ToString() => $"{Name,-20}  {Number:D5}  {Rate,6:C}  {Hours:F2}  {Gross,9:C}";

    }

}

