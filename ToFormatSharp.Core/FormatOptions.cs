namespace ToFormatSharp.Core
{
    public class FormatOptions
    {
        public string? DecimalSeparator { get; set; }
        public string? GroupSeparator { get; set; }
        public long? GroupSize { get; set; }
        public long? SecondaryGroupSize { get; set; }
        public string? FractionGroupSeparator { get; set; }
        public long? FractionGroupSize { get; set; }

        public static FormatOptions DefaultOptions()
        {
            return new FormatOptions
            {
                DecimalSeparator = ".",
                GroupSeparator = ",",
                GroupSize = 3,
                SecondaryGroupSize = 0,
                FractionGroupSeparator = "",
                FractionGroupSize = 0
            };
        }
    }
}