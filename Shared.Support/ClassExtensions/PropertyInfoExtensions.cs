using System;
using System.Reflection;

namespace Shared.Support.ClassExtensions
{
    public static class PropertyInfoExtensions
    {
        public static string GetTextFromType(PropertyInfo pi, object value)
        {
            return (pi.PropertyType.Name.ToLower()) switch
            {
                "boolean" => (((bool)value) ? "Sim" : "Não"),
                "datetime" => ((DateTime)value).ToString("dd/MM/yyyy HH:mm:ss"),
                "decimal" => ((decimal)value).ToStringBrazilianFormat(),
                "nullable`1" => ValidateNullableType(pi, value),
                _ => value?.ToString() ?? string.Empty,
            };
        }

        private static string ValidateNullableType(PropertyInfo pi, object value)
        {
            return Nullable.GetUnderlyingType(pi.PropertyType).Name.ToLower() switch
            {
                "datetime" => value != null ? ((DateTime)value).ToString("dd/MM/yyyy HH:mm:ss") : "-",
                _ => value?.ToString() ?? string.Empty,
            };
        }
    }
}
