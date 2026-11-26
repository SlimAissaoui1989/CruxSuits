using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Configurations
{
    internal class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasIndex(x => x.NumericCode)
                .IsUnique();

            // Existing seeds use zero for the default decimal separator.
            builder.Property(currency => currency.DecimalSeparator)
                .HasConversion(
                    separator => separator == '.' ? 0 : (int)separator,
                    code => code == 0 ? '.' : (char)code);

            DateTime defaultCreatedDate = new(2000, 1, 1);

            builder.HasData(
                new Currency
                {
                    Id = "AED",
                    NumericCode = "784",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "AFN",
                    NumericCode = "971",
                    Symbol = "؋",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "ALL",
                    NumericCode = "008",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "AMD",
                    NumericCode = "051",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "AOA",
                    NumericCode = "973",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "ARS",
                    NumericCode = "032",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "AUD",
                    NumericCode = "036",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "AWG",
                    NumericCode = "533",
                    Symbol = "ƒ",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "AZN",
                    NumericCode = "944",
                    Symbol = "₼",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BAM",
                    NumericCode = "977",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BBD",
                    NumericCode = "052",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BDT",
                    NumericCode = "050",
                    Symbol = "৳",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BHD",
                    NumericCode = "048",
                    Symbol = null,
                    MinorUnit = 3,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BIF",
                    NumericCode = "108",
                    Symbol = null,
                    MinorUnit = 0,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BMD",
                    NumericCode = "060",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BND",
                    NumericCode = "096",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BOB",
                    NumericCode = "068",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BOV",
                    NumericCode = "984",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BRL",
                    NumericCode = "986",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BSD",
                    NumericCode = "044",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BTN",
                    NumericCode = "064",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BWP",
                    NumericCode = "072",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BYN",
                    NumericCode = "933",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "BZD",
                    NumericCode = "084",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CAD",
                    NumericCode = "124",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CDF",
                    NumericCode = "976",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CHE",
                    NumericCode = "947",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CHF",
                    NumericCode = "756",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CHW",
                    NumericCode = "948",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CLF",
                    NumericCode = "990",
                    Symbol = null,
                    MinorUnit = 4,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CLP",
                    NumericCode = "152",
                    Symbol = "$",
                    MinorUnit = 0,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CNY",
                    NumericCode = "156",
                    Symbol = "¥",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "COP",
                    NumericCode = "170",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "COU",
                    NumericCode = "970",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CRC",
                    NumericCode = "188",
                    Symbol = "₡",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CUP",
                    NumericCode = "192",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CVE",
                    NumericCode = "132",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "CZK",
                    NumericCode = "203",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "DJF",
                    NumericCode = "262",
                    Symbol = null,
                    MinorUnit = 0,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "DKK",
                    NumericCode = "208",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "DOP",
                    NumericCode = "214",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "DZD",
                    NumericCode = "012",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "EGP",
                    NumericCode = "818",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "ERN",
                    NumericCode = "232",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "ETB",
                    NumericCode = "230",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "EUR",
                    NumericCode = "978",
                    Symbol = "€",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "FJD",
                    NumericCode = "242",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "FKP",
                    NumericCode = "238",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "GBP",
                    NumericCode = "826",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "GEL",
                    NumericCode = "981",
                    Symbol = "₾",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "GHS",
                    NumericCode = "936",
                    Symbol = "₵",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "GIP",
                    NumericCode = "292",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "GMD",
                    NumericCode = "270",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "GNF",
                    NumericCode = "324",
                    Symbol = null,
                    MinorUnit = 0,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "GTQ",
                    NumericCode = "320",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "GYD",
                    NumericCode = "328",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "HKD",
                    NumericCode = "344",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "HNL",
                    NumericCode = "340",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "HTG",
                    NumericCode = "332",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "HUF",
                    NumericCode = "348",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "IDR",
                    NumericCode = "360",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "INR",
                    NumericCode = "356",
                    Symbol = "₹",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "IQD",
                    NumericCode = "368",
                    Symbol = null,
                    MinorUnit = 3,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "IRR",
                    NumericCode = "364",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "ISK",
                    NumericCode = "352",
                    Symbol = null,
                    MinorUnit = 0,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "JMD",
                    NumericCode = "388",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "JOD",
                    NumericCode = "400",
                    Symbol = null,
                    MinorUnit = 3,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "JPY",
                    NumericCode = "392",
                    Symbol = "¥",
                    MinorUnit = 0,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "KES",
                    NumericCode = "404",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "KGS",
                    NumericCode = "417",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "KHR",
                    NumericCode = "116",
                    Symbol = "៛",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "KMF",
                    NumericCode = "174",
                    Symbol = null,
                    MinorUnit = 0,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "KPW",
                    NumericCode = "408",
                    Symbol = "₩",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "KRW",
                    NumericCode = "410",
                    Symbol = "₩",
                    MinorUnit = 0,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "KWD",
                    NumericCode = "414",
                    Symbol = null,
                    MinorUnit = 3,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "KYD",
                    NumericCode = "136",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "KZT",
                    NumericCode = "398",
                    Symbol = "₸",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "LAK",
                    NumericCode = "418",
                    Symbol = "₭",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "LBP",
                    NumericCode = "422",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "LKR",
                    NumericCode = "144",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "LRD",
                    NumericCode = "430",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "LSL",
                    NumericCode = "426",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "LYD",
                    NumericCode = "434",
                    Symbol = null,
                    MinorUnit = 3,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MAD",
                    NumericCode = "504",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MDL",
                    NumericCode = "498",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MGA",
                    NumericCode = "969",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MKD",
                    NumericCode = "807",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MMK",
                    NumericCode = "104",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MNT",
                    NumericCode = "496",
                    Symbol = "₮",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MOP",
                    NumericCode = "446",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MRU",
                    NumericCode = "929",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MUR",
                    NumericCode = "480",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MVR",
                    NumericCode = "462",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MWK",
                    NumericCode = "454",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MXN",
                    NumericCode = "484",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MXV",
                    NumericCode = "979",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MYR",
                    NumericCode = "458",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "MZN",
                    NumericCode = "943",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "NAD",
                    NumericCode = "516",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "NGN",
                    NumericCode = "566",
                    Symbol = "₦",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "NIO",
                    NumericCode = "558",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "NOK",
                    NumericCode = "578",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "NPR",
                    NumericCode = "524",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "NZD",
                    NumericCode = "554",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "OMR",
                    NumericCode = "512",
                    Symbol = null,
                    MinorUnit = 3,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "PAB",
                    NumericCode = "590",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "PEN",
                    NumericCode = "604",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "PGK",
                    NumericCode = "598",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "PHP",
                    NumericCode = "608",
                    Symbol = "₱",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "PKR",
                    NumericCode = "586",
                    Symbol = "₨",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "PLN",
                    NumericCode = "985",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "PYG",
                    NumericCode = "600",
                    Symbol = "₲",
                    MinorUnit = 0,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "QAR",
                    NumericCode = "634",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "RON",
                    NumericCode = "946",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "RSD",
                    NumericCode = "941",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "RUB",
                    NumericCode = "643",
                    Symbol = "₽",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "RWF",
                    NumericCode = "646",
                    Symbol = null,
                    MinorUnit = 0,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SAR",
                    NumericCode = "682",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SBD",
                    NumericCode = "090",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SCR",
                    NumericCode = "690",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SDG",
                    NumericCode = "938",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SEK",
                    NumericCode = "752",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SGD",
                    NumericCode = "702",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SHP",
                    NumericCode = "654",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SLE",
                    NumericCode = "925",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SOS",
                    NumericCode = "706",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SRD",
                    NumericCode = "968",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SSP",
                    NumericCode = "728",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "STN",
                    NumericCode = "930",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SVC",
                    NumericCode = "222",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SYP",
                    NumericCode = "760",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "SZL",
                    NumericCode = "748",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "THB",
                    NumericCode = "764",
                    Symbol = "฿",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "TJS",
                    NumericCode = "972",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "TMT",
                    NumericCode = "934",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "TND",
                    NumericCode = "788",
                    Symbol = null,
                    MinorUnit = 3,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "TOP",
                    NumericCode = "776",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "TRY",
                    NumericCode = "949",
                    Symbol = "₺",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "TTD",
                    NumericCode = "780",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "TWD",
                    NumericCode = "901",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "TZS",
                    NumericCode = "834",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "UAH",
                    NumericCode = "980",
                    Symbol = "₴",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "UGX",
                    NumericCode = "800",
                    Symbol = null,
                    MinorUnit = 0,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "USD",
                    NumericCode = "840",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "USN",
                    NumericCode = "997",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "UYI",
                    NumericCode = "940",
                    Symbol = null,
                    MinorUnit = 0,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "UYU",
                    NumericCode = "858",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "UYW",
                    NumericCode = "927",
                    Symbol = null,
                    MinorUnit = 4,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "UZS",
                    NumericCode = "860",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "VED",
                    NumericCode = "926",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "VES",
                    NumericCode = "928",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "VND",
                    NumericCode = "704",
                    Symbol = "₫",
                    MinorUnit = 0,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "VUV",
                    NumericCode = "548",
                    Symbol = null,
                    MinorUnit = 0,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "WST",
                    NumericCode = "882",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XAD",
                    NumericCode = "396",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XAF",
                    NumericCode = "950",
                    Symbol = null,
                    MinorUnit = 0,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XAG",
                    NumericCode = "961",
                    Symbol = null,
                    MinorUnit = null,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XAU",
                    NumericCode = "959",
                    Symbol = null,
                    MinorUnit = null,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XBA",
                    NumericCode = "955",
                    Symbol = null,
                    MinorUnit = null,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XBB",
                    NumericCode = "956",
                    Symbol = null,
                    MinorUnit = null,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XBC",
                    NumericCode = "957",
                    Symbol = null,
                    MinorUnit = null,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XBD",
                    NumericCode = "958",
                    Symbol = null,
                    MinorUnit = null,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XCD",
                    NumericCode = "951",
                    Symbol = "$",
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XCG",
                    NumericCode = "532",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XDR",
                    NumericCode = "960",
                    Symbol = null,
                    MinorUnit = null,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XOF",
                    NumericCode = "952",
                    Symbol = null,
                    MinorUnit = 0,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XPD",
                    NumericCode = "964",
                    Symbol = null,
                    MinorUnit = null,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XPF",
                    NumericCode = "953",
                    Symbol = null,
                    MinorUnit = 0,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XPT",
                    NumericCode = "962",
                    Symbol = null,
                    MinorUnit = null,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XSU",
                    NumericCode = "994",
                    Symbol = null,
                    MinorUnit = null,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XTS",
                    NumericCode = "963",
                    Symbol = null,
                    MinorUnit = null,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XUA",
                    NumericCode = "965",
                    Symbol = null,
                    MinorUnit = null,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "XXX",
                    NumericCode = "999",
                    Symbol = null,
                    MinorUnit = null,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "YER",
                    NumericCode = "886",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "ZAR",
                    NumericCode = "710",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "ZMW",
                    NumericCode = "967",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Currency
                {
                    Id = "ZWG",
                    NumericCode = "924",
                    Symbol = null,
                    MinorUnit = 2,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                }
            );
        }
    }
}
