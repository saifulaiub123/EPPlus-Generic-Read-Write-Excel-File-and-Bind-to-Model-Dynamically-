using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace DataExchangeWorkerService.Helpers
{
    public static class ExcelHelper
    {
        public static List<TModel> ReadFile<TModel, TConfig>(string filePath)
        {
            var rowCol = "";
            try
            {
                TConfig configObj = (TConfig)Activator.CreateInstance(typeof(TConfig));
                var methodInfo = configObj?.GetType().GetMethod("ExtractColumns");
                var cols = (List<Configuration.ExcelColumn>)methodInfo?.Invoke(configObj, null);

                List<TModel> clientModelList = new List<TModel>();
                using var pck = new ExcelPackage();
                using (var stream = File.OpenRead(filePath))
                {
                    pck.Load(stream);
                }
                var worksheet = pck.Workbook.Worksheets.First();

                for (var row = Convert.ToInt32(configObj?.GetType().GetProperty("ExtractStartRow")?.GetValue(configObj, null)); row <= worksheet.Dimension.End.Row; row++)
                {
                    TModel clientModel = (TModel)Activator.CreateInstance(typeof(TModel));
                    for (var j = worksheet.Dimension.Start.Column; j <= worksheet.Dimension.End.Column; j++)
                    {
                        var col = cols?.First(x => x.Index == j);
                        if (clientModel != null)
                        {
                            rowCol = $"{row} - {j}";
                            var cellRange = worksheet.Cells[row, 1, row, worksheet.Cells.End.Column];
                            var isRowEmpty = cellRange.All(c => c.Value == null);
                            if (!isRowEmpty)
                            {
                                var type = clientModel.GetType().GetProperty(col.Model)?.PropertyType;
                                var val = ConvertColumnValue(type, worksheet.Cells[row, j].Value);
                                clientModel.GetType().GetProperty(col.Model)?.SetValue(clientModel, val);
                            }
                        }
                    }
                    clientModelList.Add(clientModel);
                }
                return clientModelList;
            }
            catch(Exception ex)
            {
                var p = rowCol;
                throw ex;
            }
            
        }

        public static void WriteFile<TModel, TConfig>(string filePath, List<TModel> data, string outputFilePath)
        {
            FileInfo file = new FileInfo(filePath);
            using ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
            int colCount = worksheet.Dimension.End.Column;


            TConfig configObj = (TConfig)Activator.CreateInstance(typeof(TConfig));
            var methodInfo = configObj?.GetType().GetMethod("LoadColumns");
            var cols = (List<Configuration.ExcelColumn>)methodInfo?.Invoke(configObj, null);

            var rowIndex = Convert.ToInt32(configObj?.GetType().GetProperty("LoadStartRow")?.GetValue(configObj, null));
            foreach (var row in data)
            {
                for (int col = 1; col <= colCount; col++)
                {
                    var val = row.GetType().GetProperty(cols?.FirstOrDefault(x => x.Index == col)?.Model!)!
                        .GetValue(row);

                    worksheet.Cells[rowIndex, col].Value = val != null ? val.ToString() : null as object;
                }
                rowIndex++;
            }
            FileInfo fi = new FileInfo(outputFilePath);
            package.SaveAs(fi);
        }

        public static object ConvertColumnValue(Type type, object val)
        {
            if (type == typeof(int))
            {
                val = val != null ? Convert.ToInt32(val) : null;
            }
            else if (type == typeof(string))
            {
                val = val != null ? Convert.ToString(val) : (object)null;
            }
            else if (type == typeof(double))
            {
                val = val != null ? Convert.ToDouble(val) : null;
            }
            else if (type == typeof(decimal))
            {
                val = val != null ? Convert.ToDecimal(val) : null;
            }
            else if (type.UnderlyingSystemType == typeof(DateTime))
            {
                val = val != null ? Convert.ToDateTime(val) : null;
            }
            else if (type.UnderlyingSystemType == typeof(DateTime?))
            {
                val = val != null ? Convert.ToDateTime(val) : null;
            }
            else
            {
                val = val != null ? Convert.ToString(val) : (object)null;
            }

            return val;
        }
    }
}
