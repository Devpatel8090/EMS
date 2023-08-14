using EmployeeManagementSystem.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Repository.Interface
{
    public interface IPayRollRepository
    {
        public Task<(IEnumerable<PayRoll>, string)> GetPayRollData(int month, int year);
        public Task<bool> InsertDataIntoPayroll(int month, int year);
        public Task<int> GetMonthOfLastSalryPaid(int year);
    }
}
