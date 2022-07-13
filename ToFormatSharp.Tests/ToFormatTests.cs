using BigSharp;
using BigSharp.ToFormat;
using DecimalSharp;
using DecimalSharp.Core;
using DecimalSharp.Light.ToFormat;
using DecimalSharp.ToFormat;
using NUnit.Framework;
using ToFormatSharp.Core;

namespace ToFormatSharp.Tests
{
    [TestFixture, Category("ToFormat")]
    public class ToFormatTests
    {
        [SetUp]
        public void Setup()
        {
        }

        internal static long passed = 0, total = 0;
        internal static FormatOptions format;

        public void BigT(string expected, BigArgument value, long? dp = null)
        {
            ++total;
            var actual = new Big(value).ToFormat(dp, format);
            if (expected == actual)
            {
                ++passed;
            }
            else
            {
                Assert.Fail(
                    "Test number: " + total + " failed (Big)\n" +
                    "  Expected: " + expected + "\n" +
                    "  Actual:   " + actual
                    );
            }
        }

        public void BigDecimalT(string expected, BigDecimalArgument<BigDecimal> value, long? dp = null)
        {
            ++total;
            var actual = new BigDecimal(value).ToFormat(dp, format);
            if (expected == actual)
            {
                ++passed;
            }
            else
            {
                Assert.Fail(
                    "Test number: " + total + " failed (BigDecimal)\n" +
                    "  Expected: " + expected + "\n" +
                    "  Actual:   " + actual
                    );
            }
        }

        public void BigDecimalLightT(string expected, BigDecimalArgument<BigDecimalLight> value, long? dp = null)
        {
            ++total;
            var actual = new BigDecimalLight(value).ToFormat(dp, format);
            if (expected == actual)
            {
                ++passed;
            }
            else
            {
                Assert.Fail(
                    "Test number: " + total + " failed (BigDecimal)\n" +
                    "  Expected: " + expected + "\n" +
                    "  Actual:   " + actual
                    );
            }
        }

        [Test]
        public void TestBig()
        {
            format = new FormatOptions
            {
                DecimalSeparator = ".",
                GroupSeparator = ",",
                GroupSize = 3,
                SecondaryGroupSize = 0,
                FractionGroupSeparator = " ",
                FractionGroupSize = 0
            };

            BigT("0", 0);
            BigT("1", 1);
            BigT("-1", -1);
            BigT("123.456", 123.456);

            BigT("123.456", 123.456, 3);

            BigT("0.0", 0, 1);
            BigT("1.00", 1, 2);
            BigT("-1.000", -1, 3);
            BigT("123.4560", 123.456, 4);

            BigT("9,876.54321", 9876.54321);
            BigT("4,018,736,400,000,000,000,000", "4.0187364e+21");

            BigT("999,999,999,999,999", 999999999999999);
            BigT("99,999,999,999,999", 99999999999999);
            BigT("9,999,999,999,999", 9999999999999);
            BigT("999,999,999,999", 999999999999);
            BigT("99,999,999,999", 99999999999);
            BigT("9,999,999,999", 9999999999);
            BigT("999,999,999", 999999999);
            BigT("99,999,999", 99999999);
            BigT("9,999,999", 9999999);
            BigT("999,999", 999999);
            BigT("99,999", 99999);
            BigT("9,999", 9999);
            BigT("999", 999);
            BigT("99", 99);
            BigT("9", 9);

            BigT("76,852.342091", "7.6852342091e+4");

            format.GroupSeparator = " ";

            BigT("76 852.34", "7.6852342091e+4", 2);
            BigT("76 852.342091", "7.6852342091e+4");
            BigT("76 852.3420910871", "7.6852342091087145832640897e+4", 10);

            format.FractionGroupSize = 5;

            BigT("4 018 736 400 000 000 000 000", "4.0187364e+21");
            BigT("76 852.34209 10871 45832 64089", "7.685234209108714583264089e+4", 20);
            BigT("76 852.34209 10871 45832 64089 7", "7.6852342091087145832640897e+4", 21);
            BigT("76 852.34209 10871 45832 64089 70000", "7.6852342091087145832640897e+4", 25);

            BigT("999 999 999 999 999", 999999999999999, 0);
            BigT("99 999 999 999 999.0", 99999999999999, 1);
            BigT("9 999 999 999 999.00", 9999999999999, 2);
            BigT("999 999 999 999.000", 999999999999, 3);
            BigT("99 999 999 999.0000", 99999999999, 4);
            BigT("9 999 999 999.00000", 9999999999, 5);
            BigT("999 999 999.00000 0", 999999999, 6);
            BigT("99 999 999.00000 00", 99999999, 7);
            BigT("9 999 999.00000 000", 9999999, 8);
            BigT("999 999.00000 0000", 999999, 9);
            BigT("99 999.00000 00000", 99999, 10);
            BigT("9 999.00000 00000 0", 9999, 11);
            BigT("999.00000 00000 00", 999, 12);
            BigT("99.00000 00000 000", 99, 13);
            BigT("9.00000 00000 0000", 9, 14);

            BigT("1.00000 00000 00000", 1, 15);
            BigT("1.00000 00000 0000", 1, 14);
            BigT("1.00000 00000 000", 1, 13);
            BigT("1.00000 00000 00", 1, 12);
            BigT("1.00000 00000 0", 1, 11);
            BigT("1.00000 00000", 1, 10);
            BigT("1.00000 0000", 1, 9);

            format.FractionGroupSize = 0;

            BigT("4 018 736 400 000 000 000 000", "4.0187364e+21");
            BigT("76 852.34209108714583264089", "7.685234209108714583264089e+4", 20);
            BigT("76 852.342091087145832640897", "7.6852342091087145832640897e+4", 21);
            BigT("76 852.3420910871458326408970000", "7.6852342091087145832640897e+4", 25);

            BigT("999 999 999 999 999", 999999999999999, 0);
            BigT("99 999 999 999 999.0", 99999999999999, 1);
            BigT("9 999 999 999 999.00", 9999999999999, 2);
            BigT("999 999 999 999.000", 999999999999, 3);
            BigT("99 999 999 999.0000", 99999999999, 4);
            BigT("9 999 999 999.00000", 9999999999, 5);
            BigT("999 999 999.000000", 999999999, 6);
            BigT("99 999 999.0000000", 99999999, 7);
            BigT("9 999 999.00000000", 9999999, 8);
            BigT("999 999.000000000", 999999, 9);
            BigT("99 999.0000000000", 99999, 10);
            BigT("9 999.00000000000", 9999, 11);
            BigT("999.000000000000", 999, 12);
            BigT("99.0000000000000", 99, 13);
            BigT("9.00000000000000", 9, 14);

            BigT("1.000000000000000", 1, 15);
            BigT("1.00000000000000", 1, 14);
            BigT("1.0000000000000", 1, 13);
            BigT("1.000000000000", 1, 12);
            BigT("1.00000000000", 1, 11);
            BigT("1.0000000000", 1, 10);
            BigT("1.000000000", 1, 9);

            format = new FormatOptions
            {
                DecimalSeparator = ".",
                GroupSeparator = ",",
                GroupSize = 3,
                SecondaryGroupSize = 2
            };

            BigT("9,876.54321", 9876.54321);
            BigT("10,00,037.123", "1000037.123456789", 3);
            BigT("4,01,87,36,40,00,00,00,00,00,000", "4.0187364e+21");

            BigT("99,99,99,99,99,99,999", 999999999999999);
            BigT("9,99,99,99,99,99,999", 99999999999999);
            BigT("99,99,99,99,99,999", 9999999999999);
            BigT("9,99,99,99,99,999", 999999999999);
            BigT("99,99,99,99,999", 99999999999);
            BigT("9,99,99,99,999", 9999999999);
            BigT("99,99,99,999", 999999999);
            BigT("9,99,99,999", 99999999);
            BigT("99,99,999", 9999999);
            BigT("9,99,999", 999999);
            BigT("99,999", 99999);
            BigT("9,999", 9999);
            BigT("999", 999);
            BigT("99", 99);
            BigT("9", 9);

            format.DecimalSeparator = ",";
            format.GroupSeparator = ".";

            BigT("1.23.45.60.000,000000000008", "1.23456000000000000000789e+9", 12);

            format.GroupSeparator = "";

            BigT("10000000000123456789000000,0000000001", "10000000000123456789000000.000000000100000001", 10);

            format.GroupSeparator = " ";
            format.GroupSize = 1;
            format.SecondaryGroupSize = 4;

            BigT("4658 0734 6509 8347 6580 3645 0,6", "4658073465098347658036450.59764985763489569875659876459", 1);

            format.FractionGroupSize = 2;
            format.FractionGroupSeparator = ":";
            format.SecondaryGroupSize = null;

            BigT("4 6 5 8 0 7 3 4 6 5 0 9 8 3 4 7 6 5 8 0 3 6 4 5 0,59:76:49:85:76:34:89:56:98:75:65:98:76:45:9", "4658073465098347658036450.59764985763489569875659876459");

            Assert.Pass();
        }

        [Test]
        public void TestBigDecimal()
        {
            format = new FormatOptions
            {
                DecimalSeparator = ".",
                GroupSeparator = ",",
                GroupSize = 3,
                SecondaryGroupSize = 0,
                FractionGroupSeparator = " ",
                FractionGroupSize = 0
            };

            BigDecimalT("0", 0);
            BigDecimalT("1", 1);
            BigDecimalT("-1", -1);
            BigDecimalT("123.456", 123.456);
            BigDecimalT("NaN", double.NaN);
            BigDecimalT("Infinity", double.PositiveInfinity);
            BigDecimalT("-Infinity", double.NegativeInfinity);

            BigDecimalT("123.456", 123.456, 3);
            BigDecimalT("NaN", double.NaN, 0);
            BigDecimalT("Infinity", double.PositiveInfinity, 3);
            BigDecimalT("-Infinity", double.NegativeInfinity, 0);

            BigDecimalT("0.0", 0, 1);
            BigDecimalT("1.00", 1, 2);
            BigDecimalT("-1.000", -1, 3);
            BigDecimalT("123.4560", 123.456, 4);
            BigDecimalT("NaN", double.NaN, 5);
            BigDecimalT("Infinity", double.PositiveInfinity, 6);
            BigDecimalT("-Infinity", double.NegativeInfinity, 7);

            BigDecimalT("9,876.54321", 9876.54321);
            BigDecimalT("4,018,736,400,000,000,000,000", "4.0187364e+21");

            BigDecimalT("999,999,999,999,999", 999999999999999);
            BigDecimalT("99,999,999,999,999", 99999999999999);
            BigDecimalT("9,999,999,999,999", 9999999999999);
            BigDecimalT("999,999,999,999", 999999999999);
            BigDecimalT("99,999,999,999", 99999999999);
            BigDecimalT("9,999,999,999", 9999999999);
            BigDecimalT("999,999,999", 999999999);
            BigDecimalT("99,999,999", 99999999);
            BigDecimalT("9,999,999", 9999999);
            BigDecimalT("999,999", 999999);
            BigDecimalT("99,999", 99999);
            BigDecimalT("9,999", 9999);
            BigDecimalT("999", 999);
            BigDecimalT("99", 99);
            BigDecimalT("9", 9);

            BigDecimalT("76,852.342091", "7.6852342091e+4");

            format.GroupSeparator = " ";

            BigDecimalT("76 852.34", "7.6852342091e+4", 2);
            BigDecimalT("76 852.342091", "7.6852342091e+4");
            BigDecimalT("76 852.3420910871", "7.6852342091087145832640897e+4", 10);

            format.FractionGroupSize = 5;

            BigDecimalT("4 018 736 400 000 000 000 000", "4.0187364e+21");
            BigDecimalT("76 852.34209 10871 45832 64089", "7.685234209108714583264089e+4", 20);
            BigDecimalT("76 852.34209 10871 45832 64089 7", "7.6852342091087145832640897e+4", 21);
            BigDecimalT("76 852.34209 10871 45832 64089 70000", "7.6852342091087145832640897e+4", 25);

            BigDecimalT("999 999 999 999 999", 999999999999999, 0);
            BigDecimalT("99 999 999 999 999.0", 99999999999999, 1);
            BigDecimalT("9 999 999 999 999.00", 9999999999999, 2);
            BigDecimalT("999 999 999 999.000", 999999999999, 3);
            BigDecimalT("99 999 999 999.0000", 99999999999, 4);
            BigDecimalT("9 999 999 999.00000", 9999999999, 5);
            BigDecimalT("999 999 999.00000 0", 999999999, 6);
            BigDecimalT("99 999 999.00000 00", 99999999, 7);
            BigDecimalT("9 999 999.00000 000", 9999999, 8);
            BigDecimalT("999 999.00000 0000", 999999, 9);
            BigDecimalT("99 999.00000 00000", 99999, 10);
            BigDecimalT("9 999.00000 00000 0", 9999, 11);
            BigDecimalT("999.00000 00000 00", 999, 12);
            BigDecimalT("99.00000 00000 000", 99, 13);
            BigDecimalT("9.00000 00000 0000", 9, 14);

            BigDecimalT("1.00000 00000 00000", 1, 15);
            BigDecimalT("1.00000 00000 0000", 1, 14);
            BigDecimalT("1.00000 00000 000", 1, 13);
            BigDecimalT("1.00000 00000 00", 1, 12);
            BigDecimalT("1.00000 00000 0", 1, 11);
            BigDecimalT("1.00000 00000", 1, 10);
            BigDecimalT("1.00000 0000", 1, 9);

            format.FractionGroupSize = 0;

            BigDecimalT("4 018 736 400 000 000 000 000", "4.0187364e+21");
            BigDecimalT("76 852.34209108714583264089", "7.685234209108714583264089e+4", 20);
            BigDecimalT("76 852.342091087145832640897", "7.6852342091087145832640897e+4", 21);
            BigDecimalT("76 852.3420910871458326408970000", "7.6852342091087145832640897e+4", 25);

            BigDecimalT("999 999 999 999 999", 999999999999999, 0);
            BigDecimalT("99 999 999 999 999.0", 99999999999999, 1);
            BigDecimalT("9 999 999 999 999.00", 9999999999999, 2);
            BigDecimalT("999 999 999 999.000", 999999999999, 3);
            BigDecimalT("99 999 999 999.0000", 99999999999, 4);
            BigDecimalT("9 999 999 999.00000", 9999999999, 5);
            BigDecimalT("999 999 999.000000", 999999999, 6);
            BigDecimalT("99 999 999.0000000", 99999999, 7);
            BigDecimalT("9 999 999.00000000", 9999999, 8);
            BigDecimalT("999 999.000000000", 999999, 9);
            BigDecimalT("99 999.0000000000", 99999, 10);
            BigDecimalT("9 999.00000000000", 9999, 11);
            BigDecimalT("999.000000000000", 999, 12);
            BigDecimalT("99.0000000000000", 99, 13);
            BigDecimalT("9.00000000000000", 9, 14);

            BigDecimalT("1.000000000000000", 1, 15);
            BigDecimalT("1.00000000000000", 1, 14);
            BigDecimalT("1.0000000000000", 1, 13);
            BigDecimalT("1.000000000000", 1, 12);
            BigDecimalT("1.00000000000", 1, 11);
            BigDecimalT("1.0000000000", 1, 10);
            BigDecimalT("1.000000000", 1, 9);

            format = new FormatOptions
            {
                DecimalSeparator = ".",
                GroupSeparator = ",",
                GroupSize = 3,
                SecondaryGroupSize = 2
            };

            BigDecimalT("9,876.54321", 9876.54321);
            BigDecimalT("10,00,037.123", "1000037.123456789", 3);
            BigDecimalT("4,01,87,36,40,00,00,00,00,00,000", "4.0187364e+21");

            BigDecimalT("99,99,99,99,99,99,999", 999999999999999);
            BigDecimalT("9,99,99,99,99,99,999", 99999999999999);
            BigDecimalT("99,99,99,99,99,999", 9999999999999);
            BigDecimalT("9,99,99,99,99,999", 999999999999);
            BigDecimalT("99,99,99,99,999", 99999999999);
            BigDecimalT("9,99,99,99,999", 9999999999);
            BigDecimalT("99,99,99,999", 999999999);
            BigDecimalT("9,99,99,999", 99999999);
            BigDecimalT("99,99,999", 9999999);
            BigDecimalT("9,99,999", 999999);
            BigDecimalT("99,999", 99999);
            BigDecimalT("9,999", 9999);
            BigDecimalT("999", 999);
            BigDecimalT("99", 99);
            BigDecimalT("9", 9);

            format.DecimalSeparator = ",";
            format.GroupSeparator = ".";

            BigDecimalT("1.23.45.60.000,000000000008", "1.23456000000000000000789e+9", 12);

            format.GroupSeparator = "";

            BigDecimalT("10000000000123456789000000,0000000001", "10000000000123456789000000.000000000100000001", 10);

            format.GroupSeparator = " ";
            format.GroupSize = 1;
            format.SecondaryGroupSize = 4;

            BigDecimalT("4658 0734 6509 8347 6580 3645 0,6", "4658073465098347658036450.59764985763489569875659876459", 1);

            format.FractionGroupSize = 2;
            format.FractionGroupSeparator = ":";
            format.SecondaryGroupSize = null;

            BigDecimalT("4 6 5 8 0 7 3 4 6 5 0 9 8 3 4 7 6 5 8 0 3 6 4 5 0,59:76:49:85:76:34:89:56:98:75:65:98:76:45:9", "4658073465098347658036450.59764985763489569875659876459");

            Assert.Pass();
        }

        [Test]
        public void TestBigDecimalLight()
        {
            format = new FormatOptions
            {
                DecimalSeparator = ".",
                GroupSeparator = ",",
                GroupSize = 3,
                SecondaryGroupSize = 0,
                FractionGroupSeparator = " ",
                FractionGroupSize = 0
            };

            BigDecimalLightT("0", 0);
            BigDecimalLightT("1", 1);
            BigDecimalLightT("-1", -1);
            BigDecimalLightT("123.456", 123.456);

            BigDecimalLightT("123.456", 123.456, 3);

            BigDecimalLightT("0.0", 0, 1);
            BigDecimalLightT("1.00", 1, 2);
            BigDecimalLightT("-1.000", -1, 3);
            BigDecimalLightT("123.4560", 123.456, 4);

            BigDecimalLightT("9,876.54321", 9876.54321);
            BigDecimalLightT("4,018,736,400,000,000,000,000", "4.0187364e+21");

            BigDecimalLightT("999,999,999,999,999", 999999999999999);
            BigDecimalLightT("99,999,999,999,999", 99999999999999);
            BigDecimalLightT("9,999,999,999,999", 9999999999999);
            BigDecimalLightT("999,999,999,999", 999999999999);
            BigDecimalLightT("99,999,999,999", 99999999999);
            BigDecimalLightT("9,999,999,999", 9999999999);
            BigDecimalLightT("999,999,999", 999999999);
            BigDecimalLightT("99,999,999", 99999999);
            BigDecimalLightT("9,999,999", 9999999);
            BigDecimalLightT("999,999", 999999);
            BigDecimalLightT("99,999", 99999);
            BigDecimalLightT("9,999", 9999);
            BigDecimalLightT("999", 999);
            BigDecimalLightT("99", 99);
            BigDecimalLightT("9", 9);

            BigDecimalLightT("76,852.342091", "7.6852342091e+4");

            format.GroupSeparator = " ";

            BigDecimalLightT("76 852.34", "7.6852342091e+4", 2);
            BigDecimalLightT("76 852.342091", "7.6852342091e+4");
            BigDecimalLightT("76 852.3420910871", "7.6852342091087145832640897e+4", 10);

            format.FractionGroupSize = 5;

            BigDecimalLightT("4 018 736 400 000 000 000 000", "4.0187364e+21");
            BigDecimalLightT("76 852.34209 10871 45832 64089", "7.685234209108714583264089e+4", 20);
            BigDecimalLightT("76 852.34209 10871 45832 64089 7", "7.6852342091087145832640897e+4", 21);
            BigDecimalLightT("76 852.34209 10871 45832 64089 70000", "7.6852342091087145832640897e+4", 25);

            BigDecimalLightT("999 999 999 999 999", 999999999999999, 0);
            BigDecimalLightT("99 999 999 999 999.0", 99999999999999, 1);
            BigDecimalLightT("9 999 999 999 999.00", 9999999999999, 2);
            BigDecimalLightT("999 999 999 999.000", 999999999999, 3);
            BigDecimalLightT("99 999 999 999.0000", 99999999999, 4);
            BigDecimalLightT("9 999 999 999.00000", 9999999999, 5);
            BigDecimalLightT("999 999 999.00000 0", 999999999, 6);
            BigDecimalLightT("99 999 999.00000 00", 99999999, 7);
            BigDecimalLightT("9 999 999.00000 000", 9999999, 8);
            BigDecimalLightT("999 999.00000 0000", 999999, 9);
            BigDecimalLightT("99 999.00000 00000", 99999, 10);
            BigDecimalLightT("9 999.00000 00000 0", 9999, 11);
            BigDecimalLightT("999.00000 00000 00", 999, 12);
            BigDecimalLightT("99.00000 00000 000", 99, 13);
            BigDecimalLightT("9.00000 00000 0000", 9, 14);

            BigDecimalLightT("1.00000 00000 00000", 1, 15);
            BigDecimalLightT("1.00000 00000 0000", 1, 14);
            BigDecimalLightT("1.00000 00000 000", 1, 13);
            BigDecimalLightT("1.00000 00000 00", 1, 12);
            BigDecimalLightT("1.00000 00000 0", 1, 11);
            BigDecimalLightT("1.00000 00000", 1, 10);
            BigDecimalLightT("1.00000 0000", 1, 9);

            format.FractionGroupSize = 0;

            BigDecimalLightT("4 018 736 400 000 000 000 000", "4.0187364e+21");
            BigDecimalLightT("76 852.34209108714583264089", "7.685234209108714583264089e+4", 20);
            BigDecimalLightT("76 852.342091087145832640897", "7.6852342091087145832640897e+4", 21);
            BigDecimalLightT("76 852.3420910871458326408970000", "7.6852342091087145832640897e+4", 25);

            BigDecimalLightT("999 999 999 999 999", 999999999999999, 0);
            BigDecimalLightT("99 999 999 999 999.0", 99999999999999, 1);
            BigDecimalLightT("9 999 999 999 999.00", 9999999999999, 2);
            BigDecimalLightT("999 999 999 999.000", 999999999999, 3);
            BigDecimalLightT("99 999 999 999.0000", 99999999999, 4);
            BigDecimalLightT("9 999 999 999.00000", 9999999999, 5);
            BigDecimalLightT("999 999 999.000000", 999999999, 6);
            BigDecimalLightT("99 999 999.0000000", 99999999, 7);
            BigDecimalLightT("9 999 999.00000000", 9999999, 8);
            BigDecimalLightT("999 999.000000000", 999999, 9);
            BigDecimalLightT("99 999.0000000000", 99999, 10);
            BigDecimalLightT("9 999.00000000000", 9999, 11);
            BigDecimalLightT("999.000000000000", 999, 12);
            BigDecimalLightT("99.0000000000000", 99, 13);
            BigDecimalLightT("9.00000000000000", 9, 14);

            BigDecimalLightT("1.000000000000000", 1, 15);
            BigDecimalLightT("1.00000000000000", 1, 14);
            BigDecimalLightT("1.0000000000000", 1, 13);
            BigDecimalLightT("1.000000000000", 1, 12);
            BigDecimalLightT("1.00000000000", 1, 11);
            BigDecimalLightT("1.0000000000", 1, 10);
            BigDecimalLightT("1.000000000", 1, 9);

            format = new FormatOptions
            {
                DecimalSeparator = ".",
                GroupSeparator = ",",
                GroupSize = 3,
                SecondaryGroupSize = 2
            };

            BigDecimalLightT("9,876.54321", 9876.54321);
            BigDecimalLightT("10,00,037.123", "1000037.123456789", 3);
            BigDecimalLightT("4,01,87,36,40,00,00,00,00,00,000", "4.0187364e+21");

            BigDecimalLightT("99,99,99,99,99,99,999", 999999999999999);
            BigDecimalLightT("9,99,99,99,99,99,999", 99999999999999);
            BigDecimalLightT("99,99,99,99,99,999", 9999999999999);
            BigDecimalLightT("9,99,99,99,99,999", 999999999999);
            BigDecimalLightT("99,99,99,99,999", 99999999999);
            BigDecimalLightT("9,99,99,99,999", 9999999999);
            BigDecimalLightT("99,99,99,999", 999999999);
            BigDecimalLightT("9,99,99,999", 99999999);
            BigDecimalLightT("99,99,999", 9999999);
            BigDecimalLightT("9,99,999", 999999);
            BigDecimalLightT("99,999", 99999);
            BigDecimalLightT("9,999", 9999);
            BigDecimalLightT("999", 999);
            BigDecimalLightT("99", 99);
            BigDecimalLightT("9", 9);

            format.DecimalSeparator = ",";
            format.GroupSeparator = ".";

            BigDecimalLightT("1.23.45.60.000,000000000008", "1.23456000000000000000789e+9", 12);

            format.GroupSeparator = "";

            BigDecimalLightT("10000000000123456789000000,0000000001", "10000000000123456789000000.000000000100000001", 10);

            format.GroupSeparator = " ";
            format.GroupSize = 1;
            format.SecondaryGroupSize = 4;

            BigDecimalLightT("4658 0734 6509 8347 6580 3645 0,6", "4658073465098347658036450.59764985763489569875659876459", 1);

            format.FractionGroupSize = 2;
            format.FractionGroupSeparator = ":";
            format.SecondaryGroupSize = null;

            BigDecimalLightT("4 6 5 8 0 7 3 4 6 5 0 9 8 3 4 7 6 5 8 0 3 6 4 5 0,59:76:49:85:76:34:89:56:98:75:65:98:76:45:9", "4658073465098347658036450.59764985763489569875659876459");

            Assert.Pass();
        }
    }
}