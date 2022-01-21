using DataExchangeWorkerService.Configuration;
using System.Collections.Generic;

namespace DataExchangeWorkerService.Interface
{
    public interface IExcelConfig
    {
        public int ExtractStartRow { get; set; }
        List<ExcelColumn> ExtractColumns();

        public int LoadStartRow { get; set; }
        List<ExcelColumn> LoadColumns();
    }
}
