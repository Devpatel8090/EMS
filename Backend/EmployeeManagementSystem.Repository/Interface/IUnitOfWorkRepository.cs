using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Repository.Interface
{
  public interface IUnitOfWorkRepository
  {
        public IEmployeeRepository Employee { get; }
        public IDepartmentRepository Department { get; }
        public ICredentialRepository Credential { get; }
        public ILeaveRequestRepository LeaveRequest { get; }
        public IPayRollRepository PayRoll { get; }
        public IExcelTaskRepository ExcelTask { get; }
    }
}
