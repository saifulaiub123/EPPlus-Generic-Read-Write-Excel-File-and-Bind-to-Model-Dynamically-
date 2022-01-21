using System.Collections.Generic;
using DataExchangeWorkerService.Interface;

namespace DataExchangeWorkerService.Configuration
{
    public class ClientBColumnConfig : IExcelConfig
    {
        //Extract
        public int ExtractStartRow { get; set; } = 2;
        public List<ExcelColumn> ExtractColumns()
        {
            var excelColumn = new List<ExcelColumn>();
            var index = 1;

            excelColumn.Add(new ExcelColumn(index++, "ClaimId", false));
            excelColumn.Add(new ExcelColumn(index++, "BaName", true));
            excelColumn.Add(new ExcelColumn(index++, "MobileNumber", true));

            excelColumn.Add(new ExcelColumn(index++, "ClaimDate", true));
            excelColumn.Add(new ExcelColumn(index++, "Amount", true));
            excelColumn.Add(new ExcelColumn(index++, "PaymentDate", true));
            excelColumn.Add(new ExcelColumn(index, "TransactionId", true));

            return excelColumn;
        }

        //Load
        public int LoadStartRow { get; set; } = 2;
        public List<ExcelColumn> LoadColumns()
        {
            var excelColumn = new List<ExcelColumn>();
            var index = 1;

            excelColumn.Add(new ExcelColumn(index++, "ClaimId", false));
            excelColumn.Add(new ExcelColumn(index++, "BaName", true));
            excelColumn.Add(new ExcelColumn(index++, "MobileNumber", true));

            excelColumn.Add(new ExcelColumn(index++, "ClaimDate", true));
            excelColumn.Add(new ExcelColumn(index++, "Amount", true));
            excelColumn.Add(new ExcelColumn(index++, "PaymentDate", true));
            excelColumn.Add(new ExcelColumn(index, "TransactionId", true));

            return excelColumn;
        }
    }
}
