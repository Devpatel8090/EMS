using EmployeeManagementSystem.Entities.Model;
using EmployeeManagementSystem.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public AuthenticationController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<JsonResult> SignIn(Credentials data)
        {
            if (data != null)
            {
                var message = await _unitOfWork.Credential.SignIn(data);
                if(message != "InvalidInformation" || message != "EmailDoesNotExist")
                {
                    return new JsonResult(new { message = message, status="Ok" });
                }
                else
                {
                    return new JsonResult(new { message = message, status = "Error" });
                }
                //return new JsonResult(message);
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("GetEmployeeByEmail")]
        public async Task<JsonResult> GetEmployeeByEmail(string email)
        {
            var employee = await _unitOfWork.Employee.GetEmployeeByEmail(email);
            return new JsonResult(employee);
        }

    }
}
