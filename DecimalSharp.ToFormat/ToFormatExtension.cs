using DecimalSharp.Core;
using ToFormatSharp.Core;
using ToFormatSharp.Core.Extensions;

namespace DecimalSharp.ToFormat
{
    public static class ToFormatExtension
    {
        public static string ToFormat(this BigDecimal bigDecimal, FormatOptions? fmt = null) => ToFormat(bigDecimal, null, null, fmt);
        public static string ToFormat(this BigDecimal bigDecimal, long? dp, FormatOptions? fmt = null) => ToFormat(bigDecimal, dp, null, fmt);
        public static string ToFormat(this BigDecimal bigDecimal, long? dp, RoundingMode? rm, FormatOptions? fmt = null)
        {
            if (!bigDecimal.e.IsTrue() && bigDecimal.e != 0) return bigDecimal.ToString();

            var arr = bigDecimal.ToFixed(dp, rm).Split('.');
            return ToFormatSharp.Core.ToFormatExtension.ToFormat(() => bigDecimal.ToString(), arr, bigDecimal.e, bigDecimal.s, fmt);
        }
    }
}