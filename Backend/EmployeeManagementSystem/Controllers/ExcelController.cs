using EmployeeManagementSystem.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public ExcelController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [HttpGet]
        [Route("GetAllColorEntry")]
        public async Task<JsonResult> Getpath()
        {
            var fileName = _unitOfWork.ExcelTask.AddColorFilter();
            return new JsonResult(fileName);
        }
    }
}
