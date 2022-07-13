namespace ToFormatSharp.Core.Extensions
{
    public static class NumberExtensions
    {
        public static bool IsTrue<T>(this T[]? value)
        {
            return value != null && value.LongLength != 0;
        }

        public static bool IsTrue<T>(this T value) where T : struct
        {
            return !value.Equals(default(T));
        }

        public static bool IsTrue<T>(this T? value) where T : struct
        {
            if (value == null) return false;

            return value.HasValue && !value.Value.Equals(default(T));
        }
    }
}
