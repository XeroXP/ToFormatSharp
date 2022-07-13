using ToFormatSharp.Core;
using ToFormatSharp.Core.Extensions;

namespace BigSharp.ToFormat
{
    public static class ToFormatExtension
    {
        public static string ToFormat(this Big big, FormatOptions? fmt = null) => ToFormat(big, null, null, fmt);
        public static string ToFormat(this Big big, long? dp, FormatOptions? fmt = null) => ToFormat(big, dp, null, fmt);
        public static string ToFormat(this Big big, long? dp, RoundingMode? rm, FormatOptions? fmt = null)
        {
            if (!big.e.IsTrue() && big.e != 0) return big.ToString();

            var arr = big.ToFixed(dp, rm).Split('.');
            return ToFormatSharp.Core.ToFormatExtension.ToFormat(() => big.ToString(), arr, big.e, big.s, fmt);
        }
    }
}