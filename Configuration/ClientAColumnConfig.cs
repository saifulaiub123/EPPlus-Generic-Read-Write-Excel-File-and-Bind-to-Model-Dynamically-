using DataExchangeWorkerService.Interface;
using System.Collections.Generic;

namespace DataExchangeWorkerService.Configuration
{
    public class ClientAColumnConfig : IExcelConfig
    {
        //Extract
        public int ExtractStartRow { get; set; } = 2;
        public List<ExcelColumn> ExtractColumns()
        {
            var excelColumn = new List<ExcelColumn>();
            var index = 1;

            excelColumn.Add(new ExcelColumn(index++, "ClientId", false));
            excelColumn.Add(new ExcelColumn(index++, "ClientName", true));
            excelColumn.Add(new ExcelColumn(index++, "Email", true));

            excelColumn.Add(new ExcelColumn(index++, "Address", true));
            excelColumn.Add(new ExcelColumn(index++, "OrderNo", true));
            excelColumn.Add(new ExcelColumn(index, "Quantity", true));

            return excelColumn;
        }

        //Load
        public int LoadStartRow { get; set; } = 2;
        public List<ExcelColumn> LoadColumns()
        {
            var excelColumn = new List<ExcelColumn>();
            var index = 1;

            excelColumn.Add(new ExcelColumn(index++, "ClientId", false));
            excelColumn.Add(new ExcelColumn(index++, "ClientName", true));
            excelColumn.Add(new ExcelColumn(index++, "Email", true));
            excelColumn.Add(new ExcelColumn(index++, "Address", true));
            excelColumn.Add(new ExcelColumn(index++, "OrderNo", true));
            excelColumn.Add(new ExcelColumn(index, "Quantity", true));

            return excelColumn;
        }
    }
}
