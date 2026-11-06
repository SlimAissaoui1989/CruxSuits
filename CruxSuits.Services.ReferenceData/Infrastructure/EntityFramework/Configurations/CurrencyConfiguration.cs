using CruxSuits.Services.ReferenceData.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.Shared.EFConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.ReferenceData.Infrastructure.EntityFramework.Configurations
{
    internal class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.Property(x => x.Names)
                .HasJsonLongCollectionConversion();

            builder.HasIndex(x => x.NumericCode)
                .IsUnique();

            DateTime defaultCreatedDate = new(2000, 1, 1);

            builder.HasData(
                new Currency
                {
                    Id = "AED",
                    NumericCode = "784",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 1, 2, 3 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "AFN",
                    NumericCode = "971",
                    Symbol = "؋",
                    MinorUnit = 2,
                    Names = new long[] { 4, 5, 6 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "ALL",
                    NumericCode = "008",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 7, 8, 9 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "AMD",
                    NumericCode = "051",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 10, 11, 12 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "AOA",
                    NumericCode = "973",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 13, 14, 15 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "ARS",
                    NumericCode = "032",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 16, 17, 18 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "AUD",
                    NumericCode = "036",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 19, 20, 21 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "AWG",
                    NumericCode = "533",
                    Symbol = "ƒ",
                    MinorUnit = 2,
                    Names = new long[] { 22, 23, 24 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "AZN",
                    NumericCode = "944",
                    Symbol = "₼",
                    MinorUnit = 2,
                    Names = new long[] { 25, 26, 27 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BAM",
                    NumericCode = "977",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 28, 29, 30 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BBD",
                    NumericCode = "052",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 31, 32, 33 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BDT",
                    NumericCode = "050",
                    Symbol = "৳",
                    MinorUnit = 2,
                    Names = new long[] { 34, 35, 36 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BHD",
                    NumericCode = "048",
                    Symbol = null,
                    MinorUnit = 3,
                    Names = new long[] { 37, 38, 39 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BIF",
                    NumericCode = "108",
                    Symbol = null,
                    MinorUnit = 0,
                    Names = new long[] { 40, 41, 42 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BMD",
                    NumericCode = "060",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 43, 44, 45 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BND",
                    NumericCode = "096",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 46, 47, 48 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BOB",
                    NumericCode = "068",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 49, 50, 51 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BOV",
                    NumericCode = "984",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 52, 53, 54 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BRL",
                    NumericCode = "986",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 55, 56, 57 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BSD",
                    NumericCode = "044",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 58, 59, 60 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BTN",
                    NumericCode = "064",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 61, 62, 63 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BWP",
                    NumericCode = "072",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 64, 65, 66 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BYN",
                    NumericCode = "933",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 67, 68, 69 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BZD",
                    NumericCode = "084",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 70, 71, 72 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CAD",
                    NumericCode = "124",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 73, 74, 75 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CDF",
                    NumericCode = "976",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 76, 77, 78 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CHE",
                    NumericCode = "947",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 79, 80, 81 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CHF",
                    NumericCode = "756",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 82, 83, 84 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CHW",
                    NumericCode = "948",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 85, 86, 87 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CLF",
                    NumericCode = "990",
                    Symbol = null,
                    MinorUnit = 4,
                    Names = new long[] { 88, 89, 90 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CLP",
                    NumericCode = "152",
                    Symbol = "$",
                    MinorUnit = 0,
                    Names = new long[] { 91, 92, 93 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CNY",
                    NumericCode = "156",
                    Symbol = "¥",
                    MinorUnit = 2,
                    Names = new long[] { 94, 95, 96 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "COP",
                    NumericCode = "170",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 97, 98, 99 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "COU",
                    NumericCode = "970",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 100, 101, 102 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CRC",
                    NumericCode = "188",
                    Symbol = "₡",
                    MinorUnit = 2,
                    Names = new long[] { 103, 104, 105 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CUP",
                    NumericCode = "192",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 106, 107, 108 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CVE",
                    NumericCode = "132",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 109, 110, 111 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CZK",
                    NumericCode = "203",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 112, 113, 114 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "DJF",
                    NumericCode = "262",
                    Symbol = null,
                    MinorUnit = 0,
                    Names = new long[] { 115, 116, 117 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "DKK",
                    NumericCode = "208",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 118, 119, 120 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "DOP",
                    NumericCode = "214",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 121, 122, 123 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "DZD",
                    NumericCode = "012",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 124, 125, 126 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "EGP",
                    NumericCode = "818",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 127, 128, 129 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "ERN",
                    NumericCode = "232",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 130, 131, 132 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "ETB",
                    NumericCode = "230",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 133, 134, 135 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "EUR",
                    NumericCode = "978",
                    Symbol = "€",
                    MinorUnit = 2,
                    Names = new long[] { 136, 137, 138 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "FJD",
                    NumericCode = "242",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 139, 140, 141 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "FKP",
                    NumericCode = "238",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 142, 143, 144 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "GBP",
                    NumericCode = "826",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 145, 146, 147 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "GEL",
                    NumericCode = "981",
                    Symbol = "₾",
                    MinorUnit = 2,
                    Names = new long[] { 148, 149, 150 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "GHS",
                    NumericCode = "936",
                    Symbol = "₵",
                    MinorUnit = 2,
                    Names = new long[] { 151, 152, 153 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "GIP",
                    NumericCode = "292",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 154, 155, 156 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "GMD",
                    NumericCode = "270",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 157, 158, 159 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "GNF",
                    NumericCode = "324",
                    Symbol = null,
                    MinorUnit = 0,
                    Names = new long[] { 160, 161, 162 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "GTQ",
                    NumericCode = "320",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 163, 164, 165 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "GYD",
                    NumericCode = "328",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 166, 167, 168 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "HKD",
                    NumericCode = "344",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 169, 170, 171 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "HNL",
                    NumericCode = "340",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 172, 173, 174 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "HTG",
                    NumericCode = "332",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 175, 176, 177 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "HUF",
                    NumericCode = "348",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 178, 179, 180 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "IDR",
                    NumericCode = "360",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 181, 182, 183 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "INR",
                    NumericCode = "356",
                    Symbol = "₹",
                    MinorUnit = 2,
                    Names = new long[] { 184, 185, 186 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "IQD",
                    NumericCode = "368",
                    Symbol = null,
                    MinorUnit = 3,
                    Names = new long[] { 187, 188, 189 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "IRR",
                    NumericCode = "364",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 190, 191, 192 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "ISK",
                    NumericCode = "352",
                    Symbol = null,
                    MinorUnit = 0,
                    Names = new long[] { 193, 194, 195 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "JMD",
                    NumericCode = "388",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 196, 197, 198 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "JOD",
                    NumericCode = "400",
                    Symbol = null,
                    MinorUnit = 3,
                    Names = new long[] { 199, 200, 201 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "JPY",
                    NumericCode = "392",
                    Symbol = "¥",
                    MinorUnit = 0,
                    Names = new long[] { 202, 203, 204 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "KES",
                    NumericCode = "404",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 205, 206, 207 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "KGS",
                    NumericCode = "417",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 208, 209, 210 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "KHR",
                    NumericCode = "116",
                    Symbol = "៛",
                    MinorUnit = 2,
                    Names = new long[] { 211, 212, 213 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "KMF",
                    NumericCode = "174",
                    Symbol = null,
                    MinorUnit = 0,
                    Names = new long[] { 214, 215, 216 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "KPW",
                    NumericCode = "408",
                    Symbol = "₩",
                    MinorUnit = 2,
                    Names = new long[] { 217, 218, 219 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "KRW",
                    NumericCode = "410",
                    Symbol = "₩",
                    MinorUnit = 0,
                    Names = new long[] { 220, 221, 222 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "KWD",
                    NumericCode = "414",
                    Symbol = null,
                    MinorUnit = 3,
                    Names = new long[] { 223, 224, 225 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "KYD",
                    NumericCode = "136",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 226, 227, 228 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "KZT",
                    NumericCode = "398",
                    Symbol = "₸",
                    MinorUnit = 2,
                    Names = new long[] { 229, 230, 231 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "LAK",
                    NumericCode = "418",
                    Symbol = "₭",
                    MinorUnit = 2,
                    Names = new long[] { 232, 233, 234 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "LBP",
                    NumericCode = "422",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 235, 236, 237 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "LKR",
                    NumericCode = "144",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 238, 239, 240 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "LRD",
                    NumericCode = "430",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 241, 242, 243 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "LSL",
                    NumericCode = "426",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 244, 245, 246 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "LYD",
                    NumericCode = "434",
                    Symbol = null,
                    MinorUnit = 3,
                    Names = new long[] { 247, 248, 249 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MAD",
                    NumericCode = "504",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 250, 251, 252 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MDL",
                    NumericCode = "498",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 253, 254, 255 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MGA",
                    NumericCode = "969",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 256, 257, 258 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MKD",
                    NumericCode = "807",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 259, 260, 261 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MMK",
                    NumericCode = "104",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 262, 263, 264 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MNT",
                    NumericCode = "496",
                    Symbol = "₮",
                    MinorUnit = 2,
                    Names = new long[] { 265, 266, 267 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MOP",
                    NumericCode = "446",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 268, 269, 270 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MRU",
                    NumericCode = "929",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 271, 272, 273 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MUR",
                    NumericCode = "480",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 274, 275, 276 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MVR",
                    NumericCode = "462",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 277, 278, 279 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MWK",
                    NumericCode = "454",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 280, 281, 282 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MXN",
                    NumericCode = "484",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 283, 284, 285 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MXV",
                    NumericCode = "979",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 286, 287, 288 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MYR",
                    NumericCode = "458",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 289, 290, 291 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MZN",
                    NumericCode = "943",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 292, 293, 294 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "NAD",
                    NumericCode = "516",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 295, 296, 297 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "NGN",
                    NumericCode = "566",
                    Symbol = "₦",
                    MinorUnit = 2,
                    Names = new long[] { 298, 299, 300 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "NIO",
                    NumericCode = "558",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 301, 302, 303 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "NOK",
                    NumericCode = "578",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 304, 305, 306 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "NPR",
                    NumericCode = "524",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 307, 308, 309 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "NZD",
                    NumericCode = "554",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 310, 311, 312 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "OMR",
                    NumericCode = "512",
                    Symbol = null,
                    MinorUnit = 3,
                    Names = new long[] { 313, 314, 315 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "PAB",
                    NumericCode = "590",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 316, 317, 318 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "PEN",
                    NumericCode = "604",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 319, 320, 321 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "PGK",
                    NumericCode = "598",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 322, 323, 324 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "PHP",
                    NumericCode = "608",
                    Symbol = "₱",
                    MinorUnit = 2,
                    Names = new long[] { 325, 326, 327 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "PKR",
                    NumericCode = "586",
                    Symbol = "₨",
                    MinorUnit = 2,
                    Names = new long[] { 328, 329, 330 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "PLN",
                    NumericCode = "985",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 331, 332, 333 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "PYG",
                    NumericCode = "600",
                    Symbol = "₲",
                    MinorUnit = 0,
                    Names = new long[] { 334, 335, 336 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "QAR",
                    NumericCode = "634",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 337, 338, 339 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "RON",
                    NumericCode = "946",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 340, 341, 342 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "RSD",
                    NumericCode = "941",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 343, 344, 345 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "RUB",
                    NumericCode = "643",
                    Symbol = "₽",
                    MinorUnit = 2,
                    Names = new long[] { 346, 347, 348 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "RWF",
                    NumericCode = "646",
                    Symbol = null,
                    MinorUnit = 0,
                    Names = new long[] { 349, 350, 351 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SAR",
                    NumericCode = "682",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 352, 353, 354 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SBD",
                    NumericCode = "090",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 355, 356, 357 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SCR",
                    NumericCode = "690",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 358, 359, 360 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SDG",
                    NumericCode = "938",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 361, 362, 363 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SEK",
                    NumericCode = "752",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 364, 365, 366 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SGD",
                    NumericCode = "702",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 367, 368, 369 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SHP",
                    NumericCode = "654",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 370, 371, 372 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SLE",
                    NumericCode = "925",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 373, 374, 375 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SOS",
                    NumericCode = "706",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 376, 377, 378 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SRD",
                    NumericCode = "968",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 379, 380, 381 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SSP",
                    NumericCode = "728",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 382, 383, 384 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "STN",
                    NumericCode = "930",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 385, 386, 387 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SVC",
                    NumericCode = "222",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 388, 389, 390 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SYP",
                    NumericCode = "760",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 391, 392, 393 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SZL",
                    NumericCode = "748",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 394, 395, 396 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "THB",
                    NumericCode = "764",
                    Symbol = "฿",
                    MinorUnit = 2,
                    Names = new long[] { 397, 398, 399 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "TJS",
                    NumericCode = "972",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 400, 401, 402 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "TMT",
                    NumericCode = "934",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 403, 404, 405 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "TND",
                    NumericCode = "788",
                    Symbol = null,
                    MinorUnit = 3,
                    Names = new long[] { 406, 407, 408 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "TOP",
                    NumericCode = "776",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 409, 410, 411 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "TRY",
                    NumericCode = "949",
                    Symbol = "₺",
                    MinorUnit = 2,
                    Names = new long[] { 412, 413, 414 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "TTD",
                    NumericCode = "780",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 415, 416, 417 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "TWD",
                    NumericCode = "901",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 418, 419, 420 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "TZS",
                    NumericCode = "834",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 421, 422, 423 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "UAH",
                    NumericCode = "980",
                    Symbol = "₴",
                    MinorUnit = 2,
                    Names = new long[] { 424, 425, 426 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "UGX",
                    NumericCode = "800",
                    Symbol = null,
                    MinorUnit = 0,
                    Names = new long[] { 427, 428, 429 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "USD",
                    NumericCode = "840",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 430, 431, 432 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "USN",
                    NumericCode = "997",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 433, 434, 435 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "UYI",
                    NumericCode = "940",
                    Symbol = null,
                    MinorUnit = 0,
                    Names = new long[] { 436, 437, 438 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "UYU",
                    NumericCode = "858",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 439, 440, 441 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "UYW",
                    NumericCode = "927",
                    Symbol = null,
                    MinorUnit = 4,
                    Names = new long[] { 442, 443, 444 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "UZS",
                    NumericCode = "860",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 445, 446, 447 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "VED",
                    NumericCode = "926",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 448, 449, 450 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "VES",
                    NumericCode = "928",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 451, 452, 453 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "VND",
                    NumericCode = "704",
                    Symbol = "₫",
                    MinorUnit = 0,
                    Names = new long[] { 454, 455, 456 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "VUV",
                    NumericCode = "548",
                    Symbol = null,
                    MinorUnit = 0,
                    Names = new long[] { 457, 458, 459 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "WST",
                    NumericCode = "882",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 460, 461, 462 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XAD",
                    NumericCode = "396",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 463, 464, 465 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XAF",
                    NumericCode = "950",
                    Symbol = null,
                    MinorUnit = 0,
                    Names = new long[] { 466, 467, 468 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XAG",
                    NumericCode = "961",
                    Symbol = null,
                    MinorUnit = null,
                    Names = new long[] { 469, 470, 471 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XAU",
                    NumericCode = "959",
                    Symbol = null,
                    MinorUnit = null,
                    Names = new long[] { 472, 473, 474 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XBA",
                    NumericCode = "955",
                    Symbol = null,
                    MinorUnit = null,
                    Names = new long[] { 475, 476, 477 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XBB",
                    NumericCode = "956",
                    Symbol = null,
                    MinorUnit = null,
                    Names = new long[] { 478, 479, 480 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XBC",
                    NumericCode = "957",
                    Symbol = null,
                    MinorUnit = null,
                    Names = new long[] { 481, 482, 483 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XBD",
                    NumericCode = "958",
                    Symbol = null,
                    MinorUnit = null,
                    Names = new long[] { 484, 485, 486 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XCD",
                    NumericCode = "951",
                    Symbol = "$",
                    MinorUnit = 2,
                    Names = new long[] { 487, 488, 489 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XCG",
                    NumericCode = "532",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 490, 491, 492 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XDR",
                    NumericCode = "960",
                    Symbol = null,
                    MinorUnit = null,
                    Names = new long[] { 493, 494, 495 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XOF",
                    NumericCode = "952",
                    Symbol = null,
                    MinorUnit = 0,
                    Names = new long[] { 496, 497, 498 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XPD",
                    NumericCode = "964",
                    Symbol = null,
                    MinorUnit = null,
                    Names = new long[] { 499, 500, 501 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XPF",
                    NumericCode = "953",
                    Symbol = null,
                    MinorUnit = 0,
                    Names = new long[] { 502, 503, 504 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XPT",
                    NumericCode = "962",
                    Symbol = null,
                    MinorUnit = null,
                    Names = new long[] { 505, 506, 507 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XSU",
                    NumericCode = "994",
                    Symbol = null,
                    MinorUnit = null,
                    Names = new long[] { 508, 509, 510 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XTS",
                    NumericCode = "963",
                    Symbol = null,
                    MinorUnit = null,
                    Names = new long[] { 511, 512, 513 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XUA",
                    NumericCode = "965",
                    Symbol = null,
                    MinorUnit = null,
                    Names = new long[] { 514, 515, 516 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XXX",
                    NumericCode = "999",
                    Symbol = null,
                    MinorUnit = null,
                    Names = new long[] { 517, 518, 519 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "YER",
                    NumericCode = "886",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 520, 521, 522 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "ZAR",
                    NumericCode = "710",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 523, 524, 525 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "ZMW",
                    NumericCode = "967",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 526, 527, 528 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "ZWG",
                    NumericCode = "924",
                    Symbol = null,
                    MinorUnit = 2,
                    Names = new long[] { 529, 530, 531 },
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                }
            );
        }
    }
}
