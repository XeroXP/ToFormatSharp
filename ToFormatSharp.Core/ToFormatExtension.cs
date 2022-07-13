using System.Text.RegularExpressions;
using ToFormatSharp.Core.Extensions;

namespace ToFormatSharp.Core
{
    public static class ToFormatExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ret">Function that returns string form.</param>
        /// <param name="arr">Fixed parts splitted by decimal point.</param>
        /// <param name="e">Exponent.</param>
        /// <param name="s">Sign.</param>
        /// <param name="fmt">FormatOptions.</param>
        /// <returns></returns>
        public static string ToFormat(Func<string> ret, string[] arr, long? e, int? s, FormatOptions? fmt)
        {
            if (!e.IsTrue() && e != 0) return ret();   // Infinity/NaN

            if (fmt == null) fmt = FormatOptions.DefaultOptions();

            long g1, g2, i;
            long nd;                            // number of integer digits
            LongString intd;                          // integer digits
            LongString intp;                          // integer part
            string? fracp;                         // fraction part
            string? dsep;                          // decimalSeparator
            string? gsep;                          // groupSeparator
            long? gsize;                         // groupSize
            long? sgsize;                        // secondaryGroupSize
            string? fgsep;                         // fractionGroupSeparator
            long? fgsize;                        // fractionGroupSize

            intp = arr[0];
            fracp = arr.LongLength > 1 ? arr[1] : null;
            intd = s < 0 ? intp.Slice(1) : intp;
            nd = intd.LongLength;

            dsep = fmt.DecimalSeparator;
            if (dsep == null)
            {
                dsep = ".";
            }

            gsep = fmt.GroupSeparator;

            if (!string.IsNullOrEmpty(gsep))
            {
                gsize = fmt.GroupSize;
                if (gsize == null)
                {
                    gsize = 0;
                }

                sgsize = fmt.SecondaryGroupSize;
                if (sgsize == null)
                {
                    sgsize = 0;
                }

                if (sgsize.IsTrue())
                {
                    g1 = sgsize.Value;
                    g2 = gsize.Value;
                    nd -= g2;
                }
                else
                {
                    g1 = gsize.Value;
                    g2 = sgsize.Value;
                }

                if (g1 > 0 && nd > 0)
                {
                    i = (nd % g1).IsTrue() ? nd % g1 : g1;
                    intp = intd.Substring(0, i);
                    for (; i < nd; i += g1) intp += gsep + intd.Substring(i, g1);
                    if (g2 > 0) intp += gsep + intd.Slice(i);
                    if (s < 0) intp = '-' + intp;
                }
            }

            if (!string.IsNullOrEmpty(fracp))
            {
                fgsep = fmt.FractionGroupSeparator;

                if (!string.IsNullOrEmpty(fgsep))
                {
                    fgsize = fmt.FractionGroupSize;
                    if (fgsize == null)
                    {
                        fgsize = 0;
                    }

                    fgsize = +fgsize;

                    if (fgsize.IsTrue())
                    {
                        fracp = Regex.Replace(fracp, "\\d{" + fgsize + "}\\B", "$&" + fgsep);
                    }
                }

                return intp + dsep + fracp;
            }
            else
            {

                return intp;
            }
        }
    }
}
