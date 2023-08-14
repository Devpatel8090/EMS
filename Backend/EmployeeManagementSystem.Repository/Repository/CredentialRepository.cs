using EmployeeManagementSystem.Entities.Model;
using EmployeeManagementSystem.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Repository.Repository
{
    public class CredentialRepository : ICredentialRepository
    {
        private readonly IDataAccessRepository _dataAccess;
        public CredentialRepository(IDataAccessRepository dataAccess)
        {

            _dataAccess = dataAccess;
        }

        public async Task<string> SignIn(Credentials data)
        {
            try
            {
                var message = await _dataAccess.GetSingleData<string, dynamic>("tblCredentials_SignIn", new { data.Email, data.Password });
                return message;


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => ", ex.Message);
                return null;
            }

        }

    }
}
