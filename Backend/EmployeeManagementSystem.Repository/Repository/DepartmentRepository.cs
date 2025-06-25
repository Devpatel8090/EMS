using EmployeeManagementSystem.Entities.Model;
using EmployeeManagementSystem.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Repository.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IDataAccessRepository _dataAccess;

        public DepartmentRepository(IDataAccessRepository dataAccess)
        {

            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            try
            {
                var departments = await _dataAccess.GetData<Department, dynamic>("tblDepartment_GetAlL", new { });
                return departments;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => ", ex.Message);
                return null;
            }

        }
    }
}
