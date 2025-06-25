using EmployeeManagementSystem.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spire.Xls;

using Spire.Xls.Core.Spreadsheet.AutoFilter;
using System.Drawing;
using System.Reflection;
using Spire.Xls.Collections;

namespace EmployeeManagementSystem.Repository.Repository
{
    public class ExcelTaskRepository : IExcelTaskRepository
    {
        private readonly IDataAccessRepository _dataAccess;

        public ExcelTaskRepository(IDataAccessRepository dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<string> AddColorFilter(/*string loadFrom,string storedto,string rangeStart,string rangeEnd*/)
        {

            try
            {

                //Instantiate a Workbook object

                Workbook workbook = new Workbook();

                //Load the Excel file

                var getCurrentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                Console.WriteLine(getCurrentPath);
                workbook.LoadFromFile("C:\\Users\\pca39\\Desktop\\StudyExcel.xlsx");
                /*workbook.LoadFromFile(loadFrom);*/


                //Get the first worksheet

                Worksheet sheet = workbook.Worksheets[0];


                //Create an auto filter in the sheet and specify the range to be filterd
                /* Cel l c = sheet.Cells.GetCell(0, 1);*/

                sheet.AutoFilters.Range = sheet.Range["A1:A4143"];

                /*    sheet.AutoFilters.Range = sheet.Range[:A4143"];*/

                //Get the coloumn to be filterd

                FilterColumn filtercolumn = (FilterColumn)sheet.AutoFilters[0];

                //Add a color filter to filter the column based on cell color
                //worksheet.Range["E13:G13"].Style.Font.Color = Color.Purple;


                sheet.AutoFilters.AddFontColorFilter(
                   filtercolumn, Color.Purple);
                /*sheet.Range[addressName].Value.ToString();*/


                /*  var dev = sheet.Range["A1:A4143"].RichText.RtfText;
                  Console.WriteLine(dev);*/


                //Filter the data

                sheet.AutoFilters.Filter();
                /*AutoFiltersCollection afc = sheet.AutoFilters;
                afc.Filter();*/

                //Save the file
                Console.WriteLine(workbook);

                workbook.SaveToFile("ColorFilter.xlsx", ExcelVersion.Version2013);

                /*workbook.SaveToFile(storedto, ExcelVersion.Version2013);*/

               /* return "ColorFilter.xlsx";*/


                /* oWorksheet.Rows[i].Cells[j]*/

                /* for (var i = 1; i < this.workBook.Worksheets[k].Rows.Length; ++i)*/

                /* sheet.Rows[0].ColumnCount*/



                /* CellRange[] ranges = newSheet.FindAllString("M/S", false, false);

                 foreach (CellRange range in ranges)
                 {
                     //range.Text = "";
                     range.Text = range.Text.Replace("M/S", "");
                 }
 */

                Workbook newBook = new Workbook();

                Worksheet newSheet = newBook.Worksheets[0];

                Workbook oldBook = new Workbook();

                oldBook.LoadFromFile("ColorFilter.xlsx");

                Worksheet oldSheet = oldBook.Worksheets[0];

                int i = 1;

                int columnCount = sheet.Columns.Count();

                foreach (CellRange range in oldSheet.Columns[0])
                {
                    Color textColor = range.Style.Font.Color;
                    if (textColor.Name == "ff0000ff" || textColor.Name == "ff800080") { 


                        CellRange sourceRange = oldSheet.Range[range.Row, 1, range.Row, columnCount];

                        CellRange destRange = newSheet.Range[i, 1, i, columnCount];

                        oldSheet.Copy(sourceRange, destRange, true);

                        i++;
                    }
                    /*Name = ff000000, ARGB = (255, 0, 0, 0)}*/
                    /*          "{Name=ff0000ff, ARGB=(255, 0, 0, 255)}"*/
                  /*  "{Name=ff800080, ARGB=(255, 128, 0, 128)}"*/



                }
                newBook.SaveToFile("NewForm.xlsx", ExcelVersion.Version2010);

                return "NewForm.xlsx";


            }

            catch (Exception ex)
            {
                Console.WriteLine("Error => ", ex.Message);
                throw ex;
            }
        }
    }
}
