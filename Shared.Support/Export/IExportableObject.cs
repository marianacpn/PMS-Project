using Shared.Support.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Shared.Support.Export
{
    public interface IExportableObject
    {
        IEnumerable<string> GetColumns()
        {
            return GetAllExportableColumns().Select(e => e.Key.ColumnName).ToList();
        }

        Dictionary<ExportableAttribute, string> GetAllExportableColumns()
        {
            var propertyInfos = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(
                                           prop => Attribute.IsDefined(prop, typeof(ExportableAttribute))).ToList();


            if (propertyInfos.Count() == 0)
                throw new InfoException($"Classe {GetType()} não contem propriedades para Exporte");

            Dictionary<ExportableAttribute, string> keyValuePairs = new Dictionary<ExportableAttribute, string>();

            foreach (var item in propertyInfos)
            {
                ExportableAttribute attribute = item.GetCustomAttribute(typeof(ExportableAttribute)) as ExportableAttribute;

                keyValuePairs.Add(attribute, item.Name);
            }

            return keyValuePairs.OrderBy(e => e.Key.ColumnOrder).ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
        }
    }
}
