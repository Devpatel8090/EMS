using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Entities.Model
{
    public class LeaveRequest
    {
        public long? Id { get; set; }

        public string? EmployeeNumber { get; set; }
        public string? ReasonForLeave { get; set; }
        public DateTime? LeaveStartDate { get; set; }
        public DateTime? LeaveEndDate { get; set; }
        public int? DurationOfLeave { get; set; }
        public long? PhoneNumber { get; set; }
        public bool IsAvailableOnCall { get; set; } = false;
        public long? AlternatePhoneNumber { get; set; }
        public bool IsAvailableInCity { get; set; } = false ;

        public string? Status { get; set; } 
    }
}
