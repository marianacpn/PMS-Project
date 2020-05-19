using IBSCommonStd;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Shared.Support.ClassExtensions
{
    public static class StringExtensions
    {
        public static string ReplaceInvalidCharAndSpaces(this string value)
        {
            if (value is null)
                return value;

            value = Regex.Replace(value, @"[^0-9a-zA-ZáàâãéèêíïóôõöúçñÁÀÂÃÉÈÊÍÏÓÔÕÖÚÇÑ]+", "");

            var normalizedString = value.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            value = stringBuilder.ToString().Normalize(NormalizationForm.FormC);

            value = value.Trim().Replace(" ", "").ToLower();

            return value;
        }

        public static string ReplaceInvalidChar(this string value)
        {
            if (value is null)
                return value;

            value = Regex.Replace(value, @"[^ 0-9a-zA-ZáàâãéèêíïóôõöúçñÁÀÂÃÉÈÊÍÏÓÔÕÖÚÇÑ]+", "");

            var normalizedString = value.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            value = stringBuilder.ToString().Normalize(NormalizationForm.FormC);

            value = value.Trim().ToLower();

            return value;
        }

        public static string ToUpperReplaceInvalidChar(this string value)
        {
            if (value is null)
                return value;

            value = Regex.Replace(value, @"[^ 0-9a-zA-ZáàâãéèêíïóôõöúçñÁÀÂÃÉÈÊÍÏÓÔÕÖÚÇÑ;]+", "");

            var normalizedString = value.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            value = stringBuilder.ToString().Normalize(NormalizationForm.FormC);

            value = value.Trim().ToUpper();

            return value;
        }

        public static string GetContentType(this string value)
        {
            return GetMimeTypes()[value];
        }

        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {"text/plain", ".txt"},
                {"application/pdf",".pdf"},
                {"application/vnd.ms-word",".docx"},
                {"application/vnd.ms-excel",".xls"},
                {"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",".xlsx"},
                {"image/png",".png"},
                {"image/jpeg",".jpg"},
                {"image/gif",".gif"},
                {"text/csv",".csv"}
            };
        }

        public static string GetContentFormatType(this string value)
        {
            return GetExtensionFormat()[value];
        }

        private static Dictionary<string, string> GetExtensionFormat()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg","image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        public static T DynamicConvert<T>(this string value)
        {
            try
            {
                if (typeof(T) == typeof(bool))
                    return (T)ParseToBool(value);
                else if (typeof(T) == typeof(DateTime))
                    return (T)ParseToDateTime(value);
                else if (typeof(T) == typeof(decimal))
                    return (T)ParseToDecimal(value);
                else if (typeof(T) == typeof(TimeSpan))
                    return (T)ParseToTimeSpan(value);

                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    // Cast ConvertFromString(string text) : object to (T)
                    return (T)converter.ConvertFromString(value);
                }

                return default;
            }
            catch (NotSupportedException)
            {
                return default;
            }
        }

        private static object ParseToTimeSpan(string value)
        {
            TimeSpan.TryParse(value, out TimeSpan timeSpan);

            return timeSpan;
        }

        private static object ParseToDecimal(string value)
        {
            return Convert.ToDecimal(value, new CultureInfo("en-US"));
        }

        private static object ParseToBool(this string value)
        {
            value = value.ToLower().Trim();

            if (value == "y" || value == "s" ||
                value == "yes" || value == "sim" ||
                value == "true" || value == "1")
                return true;

            return false;
        }

        private static object ParseToDateTime(this string value)
        {
            string[] formats = { "dd/MM/yyyy", "dd/MM/yyyy HH:mm", "dd/MM/yyyy HH:mm:ss:ffffff" };
            //string[] formats = { "M/d/yyyy", "d/M/yyyy", "dd/MM/yyyy", "dd-MM-yyyy","MM/dd/yyyy", "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yy/MM/dd", "yy/M/dd", "yy/MM/d", "yy/M/d","d/M/yyyy HH:mm:ss tt",
            //                    "M/d/yyyy HH:mm:ss tt", "M/d/yyyy HH:mm:ss tt", "M/d/yyyy h:mm:ss tt", "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm","dd/MM/yyyy HH:mm:ss:ffffff",
            //                    "dd/M/yyyy HH:mm:ss:ffffff","dd/M/yyyy HH:mm:ss:ffffff tt", "yyyy/MM/dd tt", "yyyy/MM/dd", "MM/dd/yyyy tt","MM/dd/yyyy", "M/dd/yyyy HH:mm:ss tt", "dd/MM/yyyy HH:mm:ss"};

            return DateTime.ParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        public static string Encrypt(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return IBSCommonCriptography.Encrypt(value);
        }

        public static string Encrypt(this int value)
        {
            return IBSCommonCriptography.Encrypt(value.ToString());
        }

        public static string Decrypt(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return IBSCommonCriptography.Decrypt(value);
        }

        public static string FormatCnpj(this string value)
        {
            if (!string.IsNullOrEmpty(value))
                value = value.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty).Replace("_", string.Empty);

            if (value.Length != 14)
                value = ZerosEsquerda(value, 14);

            return value;
        }

        private static string ZerosEsquerda(this string strString, int intTamanho)
        {
            string strResult = "";

            for (int intCont = 1; intCont <= (intTamanho - strString.Length); intCont++)
            {
                strResult += "0";
            }

            return strResult + strString;
        }
    }
}
