using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Entities.Model
{
    public class PayRoll
    {
        public long Id { get; set; }
        public string? EmployeeNumber { get; set; }

        public long? Salary { get; set; }
        public long? TaxDeducted { get; set; }
        public long? OnHandSalary { get; set; }
        public int? Tax { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int? TotalLeaves { get; set; }
        public long? AdditionalDeduction { get; set; }

       /* public string? TableName { get; set; }*/

    }
}
