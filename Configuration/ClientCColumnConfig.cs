using System.Collections.Generic;
using DataExchangeWorkerService.Interface;

namespace DataExchangeWorkerService.Configuration
{
    public class ClientCColumnConfig : IExcelConfig
    {
        //Extract
        public int ExtractStartRow { get; set; } = 2;
        public List<ExcelColumn> ExtractColumns()
        {
            var excelColumn = new List<ExcelColumn>();
            var index = 1;

            excelColumn.Add(new ExcelColumn(index++, "DepoCode", false));
            excelColumn.Add(new ExcelColumn(index, "DepoName", true));

            return excelColumn;
        }

        //Load
        public int LoadStartRow { get; set; } = 2;
        public List<ExcelColumn> LoadColumns()
        {
            var excelColumn = new List<ExcelColumn>();
            var index = 1;

            excelColumn.Add(new ExcelColumn(index++, "DepoCode", false));
            excelColumn.Add(new ExcelColumn(index, "DepoName", true));

            return excelColumn;
        }
    }
}
