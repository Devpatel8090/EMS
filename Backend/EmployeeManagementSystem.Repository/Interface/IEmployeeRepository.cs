using EmployeeManagementSystem.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Repository.Interface
{
    public interface IEmployeeRepository
    {
        /*  Task<(IEnumerable<VendorsDetails>, int)> GetAllVendorsDetail(DataTableFilter model);*/
        Task<IEnumerable<Employee>> GetAllEmployee();
        Task<IEnumerable<Employee>> GetEmployeeByEmail(string email);
        Task<bool> DeleteEmployee(long id);
        Task<bool> AddOrUpdate(Employee employee);
    }
}
