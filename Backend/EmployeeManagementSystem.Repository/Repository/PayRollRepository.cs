using Dapper;
using EmployeeManagementSystem.Entities.Model;
using EmployeeManagementSystem.Repository.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Repository.Repository
{
    public class PayRollRepository: IPayRollRepository
    {
        private readonly IDataAccessRepository _dataAccess;

        public PayRollRepository(IDataAccessRepository dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<(IEnumerable<PayRoll>, string)> GetPayRollData(int month,int year)
        {
            using (var connection = _dataAccess.CreateConnection())
            {
                connection.Open();
                try
                {
                    DynamicParameters dynamicParameter = new DynamicParameters();
                    dynamicParameter.Add("Year", year, dbType: DbType.Int32);
                    dynamicParameter.Add("Month", month, dbType: DbType.Int32);

                    var data = await connection.QueryMultipleAsync("tblPayroll_GetDataIntoPayroll", dynamicParameter, commandType: CommandType.StoredProcedure);
                    var payRollData = data.Read<PayRoll>();
                    var tableName = data.ReadFirstOrDefault().TableName;

                    return (payRollData, tableName);
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


        public async Task<bool> InsertDataIntoPayroll(int month,int year)
        {
            try
            {
                DynamicParameters dynamicParameter = new DynamicParameters();
                dynamicParameter.Add("Year", year, dbType: DbType.Int32);
                dynamicParameter.Add("Month", month, dbType: DbType.Int32);
                await _dataAccess.SaveData<dynamic>("tblPayroll_InsertDataIntoPayroll", dynamicParameter);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => ", ex.Message);
                return false;
            }

        }

        public async Task<int> GetMonthOfLastSalryPaid(int year)
        {
            try
            {
                DynamicParameters dynamicParameter = new DynamicParameters();
                dynamicParameter.Add("Year", year, dbType: DbType.Int32);
                var lastMonth = await _dataAccess.GetSingleData<int,dynamic>("tblPayroll_GetMonthOfLastSalryPaid", dynamicParameter);
                return lastMonth;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error => ", ex.Message);
                return 0;
            }
        }
    }
}
