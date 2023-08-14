using EmployeeManagementSystem.Entities.Model;
using EmployeeManagementSystem.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayRollController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public PayRollController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [HttpGet]
        [Route("GetPayRollData")]
        public async Task<JsonResult> GetPayRollData(int month, int year)
        {
            var (payRollData, tableName) = await _unitOfWork.PayRoll.GetPayRollData(month, year);
            return new JsonResult(new { data = payRollData, tableName = tableName });
        }

        [HttpGet]
        [Route("GetMonthOfLastSalryPaid")]
        public async Task<JsonResult> GetMonthOfLastSalryPaid(int year)
        {
            var lastMonth = await _unitOfWork.PayRoll.GetMonthOfLastSalryPaid(year);
            return new JsonResult(new { lastMonth = lastMonth }); 
        }

        [HttpPost]
        [Route("InsertDataIntoPayroll")]
        public async Task<JsonResult> InsertDataIntoPayroll(int month, int year)
        {
            if (month > 0)
            {
                await _unitOfWork.PayRoll.InsertDataIntoPayroll(month,year);
                return new JsonResult(true);
            }
            else
            {
                return new JsonResult(false);
            }
        }

    }
}
