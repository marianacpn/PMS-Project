using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Shared.Support.ClassExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Application.Common.Utilities
{
    public class SheetUtility : ISheetUtility
    {
        public Dictionary<string, int> MatchRequiredColumn(DataTable table, IEnumerable<string> requiredColumns)
        {
            if (table.Rows.Count == 0)
                throw new ErrorException("A planilha não contem nenhum dado para importe.");

            if(!(requiredColumns is object) || requiredColumns?.Count() == 0)
                throw new ErrorException("Colunar requeridas devem ser especificadas.");

            Dictionary<string, int> columnsKey = new Dictionary<string, int>();

            bool hasRequiredColumns = false;
            for (int i = 0; i < 1; i++)
            {
                if (requiredColumns.All(a => table.Rows[i].ItemArray.Select(e => e.ToString().ReplaceInvalidCharAndSpaces()).Contains(a.ReplaceInvalidCharAndSpaces())))
                {
                    requiredColumns.ToList().ForEach(item =>
                    {
                        columnsKey.Add(item, Array.IndexOf(table.Rows[i].ItemArray.Select(e => e.ToString().ReplaceInvalidCharAndSpaces()).ToArray(), item.ReplaceInvalidCharAndSpaces()));
                    });

                    hasRequiredColumns = true;
                }

                table.Rows[i].Delete();

                if (hasRequiredColumns)
                    break;
            }

            table.AcceptChanges();

            if (!hasRequiredColumns)
                throw new InvalidSheetForImportException("A planilha não está no padrão correto.");

            return columnsKey;
        }
    }
}
