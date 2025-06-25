using EmployeeManagementSystem.Entities.Model;
using EmployeeManagementSystem.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Repository.Repository
{
    public class LeaveRequestRepository: ILeaveRequestRepository
    {

        private readonly IDataAccessRepository _dataAccess;

        public LeaveRequestRepository(IDataAccessRepository dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<bool> AddOrUpdate(LeaveRequest data)
        {
            try
            {               
                if (data != null)
                {
                    await _dataAccess.SaveData("tblLeaveRequest_AddLeave", new { data.EmployeeNumber,data.ReasonForLeave,data.LeaveStartDate,data.LeaveEndDate,data.DurationOfLeave,data.PhoneNumber,data.IsAvailableOnCall,data.AlternatePhoneNumber,data.IsAvailableInCity });
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => ", ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<LeaveRequest>> GetEmployeeLeavesByEmail(string email)
        {
            try
            {
                var leaveRequests = await _dataAccess.GetData<LeaveRequest, dynamic>("tblLeaveRequest_GetEmployeeLeavesByEmail", new { Email = email});
                return leaveRequests;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => ", ex.Message);
                return null;
            }

        }
        public async Task<IEnumerable<LeaveRequest>> GetAllPendingLeaves()
        {
            try
            {
                var leaveRequests = await _dataAccess.GetData<LeaveRequest, dynamic>("tblLeaveRequest_GetAllPendingLeaves", new { });
                return leaveRequests;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => ", ex.Message);
                return null;
            }

        }

        public async Task<bool> ApproveLeave(long id)
        {
            try
            {
                if (id > 0)
                {
                    await _dataAccess.SaveData("tblLeaveRequest_ApproveLeave", new { id });
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => ", ex.Message);
                return false;
            }
        }

        public async Task<bool> DenyLeave(long id)
        {
            try
            {
                if (id > 0)
                {
                    await _dataAccess.SaveData("tblLeaveRequest_DenyLeave", new { id });
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => ", ex.Message);
                return false;
            }
        }



    }
}
