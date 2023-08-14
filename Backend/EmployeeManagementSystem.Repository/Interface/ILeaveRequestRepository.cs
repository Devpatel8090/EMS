using EmployeeManagementSystem.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Repository.Interface
{
    public interface ILeaveRequestRepository
    {
        public  Task<bool> AddOrUpdate(LeaveRequest data);
        public Task<IEnumerable<LeaveRequest>> GetEmployeeLeavesByEmail(string email);
        public Task<IEnumerable<LeaveRequest>> GetAllPendingLeaves();
        public Task<bool> DenyLeave(long id);
        public Task<bool> ApproveLeave(long id);
    }
}
