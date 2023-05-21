using PocketIS.Infrastucture.Extensions;

namespace PocketIS.Infrastucture.Validation
{
    /// <summary>
    /// Function Parameter Validation Helper
    /// </summary>
    public static class Check
    {
        private const string UnknownParamName = "argument";

        /// <summary>
        /// Checks for null argument and throws ArgumentNullException if so.
        /// </summary>
        /// <param name="value">Object to check.</param>
        /// <param name="paramName">The name of the parameter that caused the current exception.</param>
        /// <param name="messageFormat">The error message that explains the reason for the exception.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <exception cref="ArgumentNullException">ArgumentNullException will be thrown if argument is null.</exception>
        public static bool NotNull([ValidatedNotNull] object value, string paramName = UnknownParamName, string messageFormat = "", params object[] args)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName, messageFormat.FormatCurrentCulture(args));
            }

            return true;
        }

        public static void ForEmptyString([ValidatedNotNull] string value)
        {
            ForEmptyString(value, UnknownParamName);
        }

        public static void ForEmptyString([ValidatedNotNull] string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void ForEmptyString([ValidatedNotNull] string value, string parameterName, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(parameterName, message);
            }
        }

        public static void ForEmptyEnumerable<T>(IEnumerable<T> value)
        {
            ForEmptyEnumerable(value, UnknownParamName);
        }

        public static void ForEmptyEnumerable<T>(IEnumerable<T> value, string parameterName)
        {
            if (!value.Any())
            {
                throw new ArgumentException(parameterName);
            }
        }

        public static void ForEmptyEnumerable<T>(IEnumerable<T> value, string parameterName, string message)
        {
            if (!value.Any())
            {
                throw new ArgumentException(parameterName, message);
            }
        }
    }
}
