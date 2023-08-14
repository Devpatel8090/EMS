using EmployeeManagementSystem.Entities.Model;
using EmployeeManagementSystem.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public LeaveRequestController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [HttpPost]
        [Route("AddLeaveRequest")]
        public async Task<JsonResult> AddLeaveRequest(LeaveRequest data)
        {
            if (data != null)
            {
                var isUpdated = await _unitOfWork.LeaveRequest.AddOrUpdate(data);
                return new JsonResult(true);
            }
            else
            {
                return new JsonResult(false);
            }
        }
        [HttpGet]
        [Route("GetEmployeeLeaveByEmail")]
        public async Task<IEnumerable<LeaveRequest>> GetEmployeeLeavesByEmail(string email)
        {
            if (email != null)
            {
                var employeeLeaves = await _unitOfWork.LeaveRequest.GetEmployeeLeavesByEmail(email);
                return employeeLeaves;
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("GetAllPendingLeaves")]
        public async Task<JsonResult> GetAllPendingLeaves()
        {            
          var employeeLeaves = await _unitOfWork.LeaveRequest.GetAllPendingLeaves();
          return new JsonResult(new { allLeavesDetails = employeeLeaves, count = employeeLeaves.Count() });
        }


        [HttpPost]
        [Route("ApproveLeave")]
        public async Task<JsonResult> ApproveLeave(long id)
        {
            if (id > 0)
            {
                var isUpdated = await _unitOfWork.LeaveRequest.ApproveLeave(id);
                return new JsonResult(true);
            }
            else
            {
                return new JsonResult(false);
            }
        }

        [HttpPost]
        [Route("DenyLeave")]
        public async Task<JsonResult> DenyLeave(long id)
        {
            if (id > 0)
            {
                var isUpdated = await _unitOfWork.LeaveRequest.DenyLeave(id);
                return new JsonResult(true);
            }
            else
            {
                return new JsonResult(false);
            }
        }

    }
}
