using ExcelDataReader;
using System.Data;
using System.IO;
using System.Text;

namespace Shared.Support.Utility
{
    public static class ExcelSupport
    {
        //public static DataSet ReadExcel(Stream fileStream)
        //{
        //    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        //    using var reader = ExcelReaderFactory.CreateReader(fileStream);

        //    return reader.AsDataSet();
        //}

        //public static DataSet ReadExcel(byte[] content)
        //{
        //    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        //    using var memory = new MemoryStream(content);
        //    using var reader = ExcelReaderFactory.CreateReader(memory);

        //    return reader.AsDataSet(new ExcelDataSetConfiguration()
        //    {
        //        ConfigureDataTable = _ => new ExcelDataTableConfiguration()
        //        {
        //            FilterRow = rowReader =>
        //            {
        //                var hasData = false;
        //                for (var i = 0; i < rowReader.FieldCount; i++)
        //                {
        //                    if (rowReader[i] == null || string.IsNullOrEmpty(rowReader[i].ToString()))
        //                    {
        //                        continue;
        //                    }

        //                    hasData = true;
        //                    break;
        //                }

        //                return hasData;
        //            }
        //        } 
        //    });
        //}
    }
}
