using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Entities.Model
{
    public class Employee
    {
        public long? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public long? Salary { get; set; }
        public int? Tax { get; set; }
        public long? DepartmentId { get; set; }
        public string? EmployeeNumber { get; set; }
        public DateTime? JoinDate { get; set; }
        public string? departmentName { get; set; }
        public string? Password { get; set; }

    }
}
