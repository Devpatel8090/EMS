using EmployeeManagementSystem.Entities.Model;
using EmployeeManagementSystem.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public EmployeeController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<JsonResult> GetAllEmployees()
        {
            var allEmployees = await _unitOfWork.Employee.GetAllEmployee();
            return new JsonResult(new { allEmployees = allEmployees, count = allEmployees.Count() });
        }
        [HttpDelete]
        [Route("DeleteEmployee")]
        public async Task<bool> DeleteEmployee(long id)
        {
            var isDeleted = await _unitOfWork.Employee.DeleteEmployee(id);
            if (isDeleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        [Route("UpdateEmployee")]
        public async Task<bool> UpdateEmployee(Employee data)
        {
            if (data != null)
            {
                var isUpdated = await _unitOfWork.Employee.AddOrUpdate(data);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
