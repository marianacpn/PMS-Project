using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Shared.Support.ClassExtensions
{
    public static class EnumExtension
    {
        public static string EnumDisplayName(this Enum enumValue)
        {
            return enumValue.GetType().GetMember(enumValue.ToString())
                           .First()
                           .GetCustomAttribute<DisplayAttribute>()
                           .Name;
        }


        public static IEnumerable<string> GetStringValues<TEnum>() where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<Enum>()
                .Select(e => e.GetStringValue())
                .ToList();
        }

        public static IEnumerable<string> GetStringValuesOfType(Enum value)
        {
            return Enum.GetValues(value.GetType())
                .Cast<Enum>()
                .Select(e => e.GetStringValue())
                .ToList();
        }

        public static string GetStringValue(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            DisplayAttribute[] attributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

            string stringValue = null;
            if (attributes.Length > 0)
            {
                stringValue = attributes[0].Name;
            }

            return stringValue;
        }
    }
}
