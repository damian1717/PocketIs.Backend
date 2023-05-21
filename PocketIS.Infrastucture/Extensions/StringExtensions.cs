using System.Globalization;

namespace PocketIS.Infrastucture.Extensions
{
    public static class StringExtensions
    {
        public static string FormatInvariant(this string format, params object[] values)
        {
            return string.Format(CultureInfo.InvariantCulture, format, values);
        }

        public static string FormatCurrentCulture(this string format, params object[] values)
        {
            return string.Format(CultureInfo.CurrentCulture, format, values);
        }

        public static bool StartsWithOrdinal(this string left, string right)
        {
            return (left == null) ?
                right == null :
                left.StartsWith(right, StringComparison.OrdinalIgnoreCase);
        }

        public static string EnsurePrefixAbsent(this string value, string prefix)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(prefix))
            {
                return value;
            }

            if (value.StartsWithOrdinal(prefix))
            {
                return value[prefix.Length..];
            }

            return value;
        }

        public static bool EQ(this string left, string right)
        {
            return (left == null) ? right == null : left.Equals(right, StringComparison.OrdinalIgnoreCase);
        }

        public static bool NEQ(this string left, string right)
        {
            return (left == null) ? right != null : !left.Equals(right, StringComparison.OrdinalIgnoreCase);
        }

        public static bool Has(this string value, string subValue)
        {
            return !string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(subValue) && value.Contains(subValue, StringComparison.OrdinalIgnoreCase);
        }
    }
}
