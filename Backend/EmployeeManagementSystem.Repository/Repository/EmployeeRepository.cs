using Dapper;
using EmployeeManagementSystem.Entities.Model;
using EmployeeManagementSystem.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Repository.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDataAccessRepository _dataAccess;

        public EmployeeRepository(IDataAccessRepository dataAccess)
        {

            _dataAccess = dataAccess;
        }


        #region GetMethods 
       /* public async Task<(IEnumerable<Employee>, int)> GetAllVendorsDetail(DataTableFilter model)
        {
            using (var connection = _dataAccess.CreateConnection())
            {
                connection.Open();
                try
                {
                    var data = await connection.QueryMultipleAsync("sp_INVVendorsDetails_GetAllVendorsDetailsWithFilter", new { model.PageStart, model.PageLength, model.Search, model.SortBy, model.SortOrder }, commandType: CommandType.StoredProcedure);
                    var allVendorDetails = data.Read<Employee>();
                    var TotalVendors = data.ReadFirstOrDefault().TotalVendors;

                    return (allVendorDetails, TotalVendors);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error => ", ex.Message);
                    throw ex;

                }
                finally
                {
                    _dataAccess.CloseConnection(connection);
                }
            }
        }
*/
        public async Task<IEnumerable<Employee>> GetAllEmployee()
        {
            try
            {
                var employees = await _dataAccess.GetData<Employee, dynamic>("tblEmployee_GetAll", new { });
                return employees;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => ", ex.Message);
                return null;
            }

        }

        public async Task<IEnumerable<Employee>> GetEmployeeByEmail(string email)
        {
            try
            {
                var employee = await _dataAccess.GetData<Employee, dynamic>("tblEmployee_GetEmployeeById", new { @Email = email });
                return employee;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => ", ex.Message);
                return null;
            }

        }
        #endregion
        public async Task<bool> DeleteEmployee(long id)
        {
            try
            {   
                 var EmpId = id;
                 await _dataAccess.SaveData<dynamic>("tblEmployee_DeleteEmp", new { EmpId });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => ", ex.Message);
                return false;
            }

        }
        
        public async Task<bool> AddOrUpdate(Employee employee)
        {           
            try
            {
                if (employee != null)
                {
                    await _dataAccess.SaveData("tblEmployee_AddOrUpdateEmployee", new { employee.FirstName, employee.LastName, employee.Email, employee.Salary, employee.DepartmentId, employee.JoinDate,employee.Tax, employee.Id});
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
