﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Entities.Model
{
    public class DataTable<T1>
        where T1 : class
    {
        public DataTable(List<T1> TotalDataItems, int totalRecordsCount)
        {
            RecordsTotal = totalRecordsCount;
            RecordsFiltered = totalRecordsCount;
            Data = TotalDataItems;
        }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public List<T1> Data { get; set; }
    }
}