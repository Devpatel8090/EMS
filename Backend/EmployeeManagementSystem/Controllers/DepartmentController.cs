using EmployeeManagementSystem.Entities.Model;
using EmployeeManagementSystem.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public DepartmentController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [HttpGet]
        [Route("GetAllDepartment")]
        public async Task<IEnumerable<Department>> GetAllDepartment()
        {
            var allDepartments = await _unitOfWork.Department.GetAllDepartments();
            return allDepartments;
        }
    }
}
