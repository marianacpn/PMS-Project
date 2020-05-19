using System.Globalization;

namespace Shared.Support.ClassExtensions
{
    public static class IntExtensions
    {
        public static string ToStringBrazilianFormat(this int value)
        {
            return value.ToString("N2", new CultureInfo("pt-BR"));
        }
        public static string ToStringBrazilianFormat(this decimal value)
        {
            return value.ToString("N2", new CultureInfo("pt-BR"));
        }


        public static string ToStringUsFormat(this int value)
        {
            return value.ToString("N2", new CultureInfo("en-US"));
        }
        public static string ToStringUsFormat(this decimal value)
        {
            return value.ToString("N2", new CultureInfo("en-US"));
        }
    }
}
