using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Repository.Interface
{
    public interface IExcelTaskRepository
    {
        public Task<string> AddColorFilter();
    }
}
