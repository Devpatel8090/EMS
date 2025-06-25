using EmployeeManagementSystem.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Repository.Repository
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly IDataAccessRepository _dataAccess;

        public UnitOfWorkRepository(/*IConfiguration configuration*/ IDataAccessRepository dataAccess)
        {
            /*_configuration = configuration;*/
            _dataAccess = dataAccess;
            Employee = new EmployeeRepository(_dataAccess);
            Department = new DepartmentRepository(_dataAccess);
            Credential = new CredentialRepository(_dataAccess);
            LeaveRequest = new LeaveRequestRepository(_dataAccess);
            PayRoll = new PayRollRepository(_dataAccess);
            ExcelTask = new ExcelTaskRepository(_dataAccess);
        }
        /*public IDataAccessRepository DataAccess { get; private set; }*/
        public IEmployeeRepository Employee { get; private set; }
        public IDepartmentRepository Department { get; private set; }
        public ICredentialRepository Credential { get; private set; }
        public ILeaveRequestRepository LeaveRequest { get; private set; }
        public IPayRollRepository PayRoll { get; private set; }
        public IExcelTaskRepository ExcelTask { get; private set; }

    }
}
