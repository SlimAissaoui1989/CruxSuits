using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Configurations
{
    internal class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasIndex(x => x.Alpha3Code)
                .IsUnique();

            builder.HasIndex(x => x.NumericCode)
                .IsUnique();

            builder.HasOne(x => x.Currency)
                .WithMany()
                .HasForeignKey(x => x.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            DateTime defaultCreatedDate = new(2000, 1, 1);

            builder.HasData(
                new Country
                {
                    Id = "AD",
                    Alpha3Code = "AND",
                    NumericCode = "020",
                    PhoneCode = "+376",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "AE",
                    Alpha3Code = "ARE",
                    NumericCode = "784",
                    PhoneCode = "+971",
                    CurrencyId = "AED",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "AF",
                    Alpha3Code = "AFG",
                    NumericCode = "004",
                    PhoneCode = "+93",
                    CurrencyId = "AFN",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "AG",
                    Alpha3Code = "ATG",
                    NumericCode = "028",
                    PhoneCode = "+1268",
                    CurrencyId = "XCD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "AI",
                    Alpha3Code = "AIA",
                    NumericCode = "660",
                    PhoneCode = "+1264",
                    CurrencyId = "XCD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "AL",
                    Alpha3Code = "ALB",
                    NumericCode = "008",
                    PhoneCode = "+355",
                    CurrencyId = "ALL",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "AM",
                    Alpha3Code = "ARM",
                    NumericCode = "051",
                    PhoneCode = "+374",
                    CurrencyId = "AMD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "AO",
                    Alpha3Code = "AGO",
                    NumericCode = "024",
                    PhoneCode = "+244",
                    CurrencyId = "AOA",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "AQ",
                    Alpha3Code = "ATA",
                    NumericCode = "010",
                    PhoneCode = null,
                    CurrencyId = "XXX",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "AR",
                    Alpha3Code = "ARG",
                    NumericCode = "032",
                    PhoneCode = "+54",
                    CurrencyId = "ARS",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "AS",
                    Alpha3Code = "ASM",
                    NumericCode = "016",
                    PhoneCode = "+1684",
                    CurrencyId = "USD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "AT",
                    Alpha3Code = "AUT",
                    NumericCode = "040",
                    PhoneCode = "+43",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "AU",
                    Alpha3Code = "AUS",
                    NumericCode = "036",
                    PhoneCode = "+61",
                    CurrencyId = "AUD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "AW",
                    Alpha3Code = "ABW",
                    NumericCode = "533",
                    PhoneCode = "+297",
                    CurrencyId = "AWG",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "AX",
                    Alpha3Code = "ALA",
                    NumericCode = "248",
                    PhoneCode = "+35818",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "AZ",
                    Alpha3Code = "AZE",
                    NumericCode = "031",
                    PhoneCode = "+994",
                    CurrencyId = "AZN",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BA",
                    Alpha3Code = "BIH",
                    NumericCode = "070",
                    PhoneCode = "+387",
                    CurrencyId = "BAM",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BB",
                    Alpha3Code = "BRB",
                    NumericCode = "052",
                    PhoneCode = "+1246",
                    CurrencyId = "BBD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BD",
                    Alpha3Code = "BGD",
                    NumericCode = "050",
                    PhoneCode = "+880",
                    CurrencyId = "BDT",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BE",
                    Alpha3Code = "BEL",
                    NumericCode = "056",
                    PhoneCode = "+32",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BF",
                    Alpha3Code = "BFA",
                    NumericCode = "854",
                    PhoneCode = "+226",
                    CurrencyId = "XOF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BG",
                    Alpha3Code = "BGR",
                    NumericCode = "100",
                    PhoneCode = "+359",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BH",
                    Alpha3Code = "BHR",
                    NumericCode = "048",
                    PhoneCode = "+973",
                    CurrencyId = "BHD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BI",
                    Alpha3Code = "BDI",
                    NumericCode = "108",
                    PhoneCode = "+257",
                    CurrencyId = "BIF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BJ",
                    Alpha3Code = "BEN",
                    NumericCode = "204",
                    PhoneCode = "+229",
                    CurrencyId = "XOF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BL",
                    Alpha3Code = "BLM",
                    NumericCode = "652",
                    PhoneCode = "+590",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BM",
                    Alpha3Code = "BMU",
                    NumericCode = "060",
                    PhoneCode = "+1441",
                    CurrencyId = "BMD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BN",
                    Alpha3Code = "BRN",
                    NumericCode = "096",
                    PhoneCode = "+673",
                    CurrencyId = "BND",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BO",
                    Alpha3Code = "BOL",
                    NumericCode = "068",
                    PhoneCode = "+591",
                    CurrencyId = "BOB",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BQ",
                    Alpha3Code = "BES",
                    NumericCode = "535",
                    PhoneCode = "+599",
                    CurrencyId = "USD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BR",
                    Alpha3Code = "BRA",
                    NumericCode = "076",
                    PhoneCode = "+55",
                    CurrencyId = "BRL",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BS",
                    Alpha3Code = "BHS",
                    NumericCode = "044",
                    PhoneCode = "+1242",
                    CurrencyId = "BSD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BT",
                    Alpha3Code = "BTN",
                    NumericCode = "064",
                    PhoneCode = "+975",
                    CurrencyId = "BTN",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BV",
                    Alpha3Code = "BVT",
                    NumericCode = "074",
                    PhoneCode = "+47",
                    CurrencyId = "NOK",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BW",
                    Alpha3Code = "BWA",
                    NumericCode = "072",
                    PhoneCode = "+267",
                    CurrencyId = "BWP",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BY",
                    Alpha3Code = "BLR",
                    NumericCode = "112",
                    PhoneCode = "+375",
                    CurrencyId = "BYN",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "BZ",
                    Alpha3Code = "BLZ",
                    NumericCode = "084",
                    PhoneCode = "+501",
                    CurrencyId = "BZD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "CA",
                    Alpha3Code = "CAN",
                    NumericCode = "124",
                    PhoneCode = "+1",
                    CurrencyId = "CAD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "CC",
                    Alpha3Code = "CCK",
                    NumericCode = "166",
                    PhoneCode = "+61",
                    CurrencyId = "AUD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "CD",
                    Alpha3Code = "COD",
                    NumericCode = "180",
                    PhoneCode = "+243",
                    CurrencyId = "CDF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "CF",
                    Alpha3Code = "CAF",
                    NumericCode = "140",
                    PhoneCode = "+236",
                    CurrencyId = "XAF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "CG",
                    Alpha3Code = "COG",
                    NumericCode = "178",
                    PhoneCode = "+242",
                    CurrencyId = "XAF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "CH",
                    Alpha3Code = "CHE",
                    NumericCode = "756",
                    PhoneCode = "+41",
                    CurrencyId = "CHF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "CI",
                    Alpha3Code = "CIV",
                    NumericCode = "384",
                    PhoneCode = "+225",
                    CurrencyId = "XOF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "CK",
                    Alpha3Code = "COK",
                    NumericCode = "184",
                    PhoneCode = "+682",
                    CurrencyId = "NZD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "CL",
                    Alpha3Code = "CHL",
                    NumericCode = "152",
                    PhoneCode = "+56",
                    CurrencyId = "CLP",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "CM",
                    Alpha3Code = "CMR",
                    NumericCode = "120",
                    PhoneCode = "+237",
                    CurrencyId = "XAF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "CN",
                    Alpha3Code = "CHN",
                    NumericCode = "156",
                    PhoneCode = "+86",
                    CurrencyId = "CNY",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "CO",
                    Alpha3Code = "COL",
                    NumericCode = "170",
                    PhoneCode = "+57",
                    CurrencyId = "COP",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "CR",
                    Alpha3Code = "CRI",
                    NumericCode = "188",
                    PhoneCode = "+506",
                    CurrencyId = "CRC",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "CU",
                    Alpha3Code = "CUB",
                    NumericCode = "192",
                    PhoneCode = "+53",
                    CurrencyId = "CUP",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "CV",
                    Alpha3Code = "CPV",
                    NumericCode = "132",
                    PhoneCode = "+238",
                    CurrencyId = "CVE",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "CW",
                    Alpha3Code = "CUW",
                    NumericCode = "531",
                    PhoneCode = "+599",
                    CurrencyId = "XCG",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "CX",
                    Alpha3Code = "CXR",
                    NumericCode = "162",
                    PhoneCode = "+61",
                    CurrencyId = "AUD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "CY",
                    Alpha3Code = "CYP",
                    NumericCode = "196",
                    PhoneCode = "+357",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "CZ",
                    Alpha3Code = "CZE",
                    NumericCode = "203",
                    PhoneCode = "+420",
                    CurrencyId = "CZK",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "DE",
                    Alpha3Code = "DEU",
                    NumericCode = "276",
                    PhoneCode = "+49",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "DJ",
                    Alpha3Code = "DJI",
                    NumericCode = "262",
                    PhoneCode = "+253",
                    CurrencyId = "DJF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "DK",
                    Alpha3Code = "DNK",
                    NumericCode = "208",
                    PhoneCode = "+45",
                    CurrencyId = "DKK",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "DM",
                    Alpha3Code = "DMA",
                    NumericCode = "212",
                    PhoneCode = "+1767",
                    CurrencyId = "XCD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "DO",
                    Alpha3Code = "DOM",
                    NumericCode = "214",
                    PhoneCode = "+1",
                    CurrencyId = "DOP",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "DZ",
                    Alpha3Code = "DZA",
                    NumericCode = "012",
                    PhoneCode = "+213",
                    CurrencyId = "DZD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "EC",
                    Alpha3Code = "ECU",
                    NumericCode = "218",
                    PhoneCode = "+593",
                    CurrencyId = "USD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "EE",
                    Alpha3Code = "EST",
                    NumericCode = "233",
                    PhoneCode = "+372",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "EG",
                    Alpha3Code = "EGY",
                    NumericCode = "818",
                    PhoneCode = "+20",
                    CurrencyId = "EGP",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "EH",
                    Alpha3Code = "ESH",
                    NumericCode = "732",
                    PhoneCode = "+212",
                    CurrencyId = "MAD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "ER",
                    Alpha3Code = "ERI",
                    NumericCode = "232",
                    PhoneCode = "+291",
                    CurrencyId = "ERN",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "ES",
                    Alpha3Code = "ESP",
                    NumericCode = "724",
                    PhoneCode = "+34",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "ET",
                    Alpha3Code = "ETH",
                    NumericCode = "231",
                    PhoneCode = "+251",
                    CurrencyId = "ETB",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "FI",
                    Alpha3Code = "FIN",
                    NumericCode = "246",
                    PhoneCode = "+358",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "FJ",
                    Alpha3Code = "FJI",
                    NumericCode = "242",
                    PhoneCode = "+679",
                    CurrencyId = "FJD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "FK",
                    Alpha3Code = "FLK",
                    NumericCode = "238",
                    PhoneCode = "+500",
                    CurrencyId = "FKP",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "FM",
                    Alpha3Code = "FSM",
                    NumericCode = "583",
                    PhoneCode = "+691",
                    CurrencyId = "USD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "FO",
                    Alpha3Code = "FRO",
                    NumericCode = "234",
                    PhoneCode = "+298",
                    CurrencyId = "DKK",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "FR",
                    Alpha3Code = "FRA",
                    NumericCode = "250",
                    PhoneCode = "+33",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "GA",
                    Alpha3Code = "GAB",
                    NumericCode = "266",
                    PhoneCode = "+241",
                    CurrencyId = "XAF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "GB",
                    Alpha3Code = "GBR",
                    NumericCode = "826",
                    PhoneCode = "+44",
                    CurrencyId = "GBP",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "GD",
                    Alpha3Code = "GRD",
                    NumericCode = "308",
                    PhoneCode = "+1473",
                    CurrencyId = "XCD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "GE",
                    Alpha3Code = "GEO",
                    NumericCode = "268",
                    PhoneCode = "+995",
                    CurrencyId = "GEL",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "GF",
                    Alpha3Code = "GUF",
                    NumericCode = "254",
                    PhoneCode = "+594",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "GG",
                    Alpha3Code = "GGY",
                    NumericCode = "831",
                    PhoneCode = "+44",
                    CurrencyId = "GBP",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "GH",
                    Alpha3Code = "GHA",
                    NumericCode = "288",
                    PhoneCode = "+233",
                    CurrencyId = "GHS",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "GI",
                    Alpha3Code = "GIB",
                    NumericCode = "292",
                    PhoneCode = "+350",
                    CurrencyId = "GIP",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "GL",
                    Alpha3Code = "GRL",
                    NumericCode = "304",
                    PhoneCode = "+299",
                    CurrencyId = "DKK",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "GM",
                    Alpha3Code = "GMB",
                    NumericCode = "270",
                    PhoneCode = "+220",
                    CurrencyId = "GMD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "GN",
                    Alpha3Code = "GIN",
                    NumericCode = "324",
                    PhoneCode = "+224",
                    CurrencyId = "GNF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "GP",
                    Alpha3Code = "GLP",
                    NumericCode = "312",
                    PhoneCode = "+590",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "GQ",
                    Alpha3Code = "GNQ",
                    NumericCode = "226",
                    PhoneCode = "+240",
                    CurrencyId = "XAF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "GR",
                    Alpha3Code = "GRC",
                    NumericCode = "300",
                    PhoneCode = "+30",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "GS",
                    Alpha3Code = "SGS",
                    NumericCode = "239",
                    PhoneCode = "+500",
                    CurrencyId = "SHP",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "GT",
                    Alpha3Code = "GTM",
                    NumericCode = "320",
                    PhoneCode = "+502",
                    CurrencyId = "GTQ",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "GU",
                    Alpha3Code = "GUM",
                    NumericCode = "316",
                    PhoneCode = "+1671",
                    CurrencyId = "USD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "GW",
                    Alpha3Code = "GNB",
                    NumericCode = "624",
                    PhoneCode = "+245",
                    CurrencyId = "XOF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "GY",
                    Alpha3Code = "GUY",
                    NumericCode = "328",
                    PhoneCode = "+592",
                    CurrencyId = "GYD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "HK",
                    Alpha3Code = "HKG",
                    NumericCode = "344",
                    PhoneCode = "+852",
                    CurrencyId = "HKD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "HM",
                    Alpha3Code = "HMD",
                    NumericCode = "334",
                    PhoneCode = null,
                    CurrencyId = "AUD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "HN",
                    Alpha3Code = "HND",
                    NumericCode = "340",
                    PhoneCode = "+504",
                    CurrencyId = "HNL",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "HR",
                    Alpha3Code = "HRV",
                    NumericCode = "191",
                    PhoneCode = "+385",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "HT",
                    Alpha3Code = "HTI",
                    NumericCode = "332",
                    PhoneCode = "+509",
                    CurrencyId = "HTG",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "HU",
                    Alpha3Code = "HUN",
                    NumericCode = "348",
                    PhoneCode = "+36",
                    CurrencyId = "HUF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "ID",
                    Alpha3Code = "IDN",
                    NumericCode = "360",
                    PhoneCode = "+62",
                    CurrencyId = "IDR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "IE",
                    Alpha3Code = "IRL",
                    NumericCode = "372",
                    PhoneCode = "+353",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "IM",
                    Alpha3Code = "IMN",
                    NumericCode = "833",
                    PhoneCode = "+44",
                    CurrencyId = "GBP",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "IN",
                    Alpha3Code = "IND",
                    NumericCode = "356",
                    PhoneCode = "+91",
                    CurrencyId = "INR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "IO",
                    Alpha3Code = "IOT",
                    NumericCode = "086",
                    PhoneCode = "+246",
                    CurrencyId = "USD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "IQ",
                    Alpha3Code = "IRQ",
                    NumericCode = "368",
                    PhoneCode = "+964",
                    CurrencyId = "IQD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "IR",
                    Alpha3Code = "IRN",
                    NumericCode = "364",
                    PhoneCode = "+98",
                    CurrencyId = "IRR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "IS",
                    Alpha3Code = "ISL",
                    NumericCode = "352",
                    PhoneCode = "+354",
                    CurrencyId = "ISK",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "IT",
                    Alpha3Code = "ITA",
                    NumericCode = "380",
                    PhoneCode = "+39",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "JE",
                    Alpha3Code = "JEY",
                    NumericCode = "832",
                    PhoneCode = "+44",
                    CurrencyId = "GBP",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "JM",
                    Alpha3Code = "JAM",
                    NumericCode = "388",
                    PhoneCode = "+1876",
                    CurrencyId = "JMD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "JO",
                    Alpha3Code = "JOR",
                    NumericCode = "400",
                    PhoneCode = "+962",
                    CurrencyId = "JOD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "JP",
                    Alpha3Code = "JPN",
                    NumericCode = "392",
                    PhoneCode = "+81",
                    CurrencyId = "JPY",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "KE",
                    Alpha3Code = "KEN",
                    NumericCode = "404",
                    PhoneCode = "+254",
                    CurrencyId = "KES",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "KG",
                    Alpha3Code = "KGZ",
                    NumericCode = "417",
                    PhoneCode = "+996",
                    CurrencyId = "KGS",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "KH",
                    Alpha3Code = "KHM",
                    NumericCode = "116",
                    PhoneCode = "+855",
                    CurrencyId = "KHR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "KI",
                    Alpha3Code = "KIR",
                    NumericCode = "296",
                    PhoneCode = "+686",
                    CurrencyId = "AUD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "KM",
                    Alpha3Code = "COM",
                    NumericCode = "174",
                    PhoneCode = "+269",
                    CurrencyId = "KMF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "KN",
                    Alpha3Code = "KNA",
                    NumericCode = "659",
                    PhoneCode = "+1869",
                    CurrencyId = "XCD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "KP",
                    Alpha3Code = "PRK",
                    NumericCode = "408",
                    PhoneCode = "+850",
                    CurrencyId = "KPW",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "KR",
                    Alpha3Code = "KOR",
                    NumericCode = "410",
                    PhoneCode = "+82",
                    CurrencyId = "KRW",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "KW",
                    Alpha3Code = "KWT",
                    NumericCode = "414",
                    PhoneCode = "+965",
                    CurrencyId = "KWD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "KY",
                    Alpha3Code = "CYM",
                    NumericCode = "136",
                    PhoneCode = "+1345",
                    CurrencyId = "KYD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "KZ",
                    Alpha3Code = "KAZ",
                    NumericCode = "398",
                    PhoneCode = "+76,+77",
                    CurrencyId = "KZT",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "LA",
                    Alpha3Code = "LAO",
                    NumericCode = "418",
                    PhoneCode = "+856",
                    CurrencyId = "LAK",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "LB",
                    Alpha3Code = "LBN",
                    NumericCode = "422",
                    PhoneCode = "+961",
                    CurrencyId = "LBP",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "LC",
                    Alpha3Code = "LCA",
                    NumericCode = "662",
                    PhoneCode = "+1758",
                    CurrencyId = "XCD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "LI",
                    Alpha3Code = "LIE",
                    NumericCode = "438",
                    PhoneCode = "+423",
                    CurrencyId = "CHF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "LK",
                    Alpha3Code = "LKA",
                    NumericCode = "144",
                    PhoneCode = "+94",
                    CurrencyId = "LKR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "LR",
                    Alpha3Code = "LBR",
                    NumericCode = "430",
                    PhoneCode = "+231",
                    CurrencyId = "LRD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "LS",
                    Alpha3Code = "LSO",
                    NumericCode = "426",
                    PhoneCode = "+266",
                    CurrencyId = "LSL",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "LT",
                    Alpha3Code = "LTU",
                    NumericCode = "440",
                    PhoneCode = "+370",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "LU",
                    Alpha3Code = "LUX",
                    NumericCode = "442",
                    PhoneCode = "+352",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "LV",
                    Alpha3Code = "LVA",
                    NumericCode = "428",
                    PhoneCode = "+371",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "LY",
                    Alpha3Code = "LBY",
                    NumericCode = "434",
                    PhoneCode = "+218",
                    CurrencyId = "LYD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MA",
                    Alpha3Code = "MAR",
                    NumericCode = "504",
                    PhoneCode = "+212",
                    CurrencyId = "MAD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MC",
                    Alpha3Code = "MCO",
                    NumericCode = "492",
                    PhoneCode = "+377",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MD",
                    Alpha3Code = "MDA",
                    NumericCode = "498",
                    PhoneCode = "+373",
                    CurrencyId = "MDL",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "ME",
                    Alpha3Code = "MNE",
                    NumericCode = "499",
                    PhoneCode = "+382",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MF",
                    Alpha3Code = "MAF",
                    NumericCode = "663",
                    PhoneCode = "+590",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MG",
                    Alpha3Code = "MDG",
                    NumericCode = "450",
                    PhoneCode = "+261",
                    CurrencyId = "MGA",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MH",
                    Alpha3Code = "MHL",
                    NumericCode = "584",
                    PhoneCode = "+692",
                    CurrencyId = "USD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MK",
                    Alpha3Code = "MKD",
                    NumericCode = "807",
                    PhoneCode = "+389",
                    CurrencyId = "MKD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "ML",
                    Alpha3Code = "MLI",
                    NumericCode = "466",
                    PhoneCode = "+223",
                    CurrencyId = "XOF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MM",
                    Alpha3Code = "MMR",
                    NumericCode = "104",
                    PhoneCode = "+95",
                    CurrencyId = "MMK",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MN",
                    Alpha3Code = "MNG",
                    NumericCode = "496",
                    PhoneCode = "+976",
                    CurrencyId = "MNT",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MO",
                    Alpha3Code = "MAC",
                    NumericCode = "446",
                    PhoneCode = "+853",
                    CurrencyId = "MOP",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MP",
                    Alpha3Code = "MNP",
                    NumericCode = "580",
                    PhoneCode = "+1670",
                    CurrencyId = "USD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MQ",
                    Alpha3Code = "MTQ",
                    NumericCode = "474",
                    PhoneCode = "+596",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MR",
                    Alpha3Code = "MRT",
                    NumericCode = "478",
                    PhoneCode = "+222",
                    CurrencyId = "MRU",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MS",
                    Alpha3Code = "MSR",
                    NumericCode = "500",
                    PhoneCode = "+1664",
                    CurrencyId = "XCD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MT",
                    Alpha3Code = "MLT",
                    NumericCode = "470",
                    PhoneCode = "+356",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MU",
                    Alpha3Code = "MUS",
                    NumericCode = "480",
                    PhoneCode = "+230",
                    CurrencyId = "MUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MV",
                    Alpha3Code = "MDV",
                    NumericCode = "462",
                    PhoneCode = "+960",
                    CurrencyId = "MVR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MW",
                    Alpha3Code = "MWI",
                    NumericCode = "454",
                    PhoneCode = "+265",
                    CurrencyId = "MWK",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MX",
                    Alpha3Code = "MEX",
                    NumericCode = "484",
                    PhoneCode = "+52",
                    CurrencyId = "MXN",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MY",
                    Alpha3Code = "MYS",
                    NumericCode = "458",
                    PhoneCode = "+60",
                    CurrencyId = "MYR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "MZ",
                    Alpha3Code = "MOZ",
                    NumericCode = "508",
                    PhoneCode = "+258",
                    CurrencyId = "MZN",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "NA",
                    Alpha3Code = "NAM",
                    NumericCode = "516",
                    PhoneCode = "+264",
                    CurrencyId = "NAD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "NC",
                    Alpha3Code = "NCL",
                    NumericCode = "540",
                    PhoneCode = "+687",
                    CurrencyId = "XPF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "NE",
                    Alpha3Code = "NER",
                    NumericCode = "562",
                    PhoneCode = "+227",
                    CurrencyId = "XOF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "NF",
                    Alpha3Code = "NFK",
                    NumericCode = "574",
                    PhoneCode = "+672",
                    CurrencyId = "AUD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "NG",
                    Alpha3Code = "NGA",
                    NumericCode = "566",
                    PhoneCode = "+234",
                    CurrencyId = "NGN",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "NI",
                    Alpha3Code = "NIC",
                    NumericCode = "558",
                    PhoneCode = "+505",
                    CurrencyId = "NIO",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "NL",
                    Alpha3Code = "NLD",
                    NumericCode = "528",
                    PhoneCode = "+31",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "NO",
                    Alpha3Code = "NOR",
                    NumericCode = "578",
                    PhoneCode = "+47",
                    CurrencyId = "NOK",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "NP",
                    Alpha3Code = "NPL",
                    NumericCode = "524",
                    PhoneCode = "+977",
                    CurrencyId = "NPR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "NR",
                    Alpha3Code = "NRU",
                    NumericCode = "520",
                    PhoneCode = "+674",
                    CurrencyId = "AUD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "NU",
                    Alpha3Code = "NIU",
                    NumericCode = "570",
                    PhoneCode = "+683",
                    CurrencyId = "NZD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "NZ",
                    Alpha3Code = "NZL",
                    NumericCode = "554",
                    PhoneCode = "+64",
                    CurrencyId = "NZD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "OM",
                    Alpha3Code = "OMN",
                    NumericCode = "512",
                    PhoneCode = "+968",
                    CurrencyId = "OMR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "PA",
                    Alpha3Code = "PAN",
                    NumericCode = "591",
                    PhoneCode = "+507",
                    CurrencyId = "PAB",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "PE",
                    Alpha3Code = "PER",
                    NumericCode = "604",
                    PhoneCode = "+51",
                    CurrencyId = "PEN",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "PF",
                    Alpha3Code = "PYF",
                    NumericCode = "258",
                    PhoneCode = "+689",
                    CurrencyId = "XPF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "PG",
                    Alpha3Code = "PNG",
                    NumericCode = "598",
                    PhoneCode = "+675",
                    CurrencyId = "PGK",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "PH",
                    Alpha3Code = "PHL",
                    NumericCode = "608",
                    PhoneCode = "+63",
                    CurrencyId = "PHP",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "PK",
                    Alpha3Code = "PAK",
                    NumericCode = "586",
                    PhoneCode = "+92",
                    CurrencyId = "PKR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "PL",
                    Alpha3Code = "POL",
                    NumericCode = "616",
                    PhoneCode = "+48",
                    CurrencyId = "PLN",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "PM",
                    Alpha3Code = "SPM",
                    NumericCode = "666",
                    PhoneCode = "+508",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "PN",
                    Alpha3Code = "PCN",
                    NumericCode = "612",
                    PhoneCode = "+64",
                    CurrencyId = "NZD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "PR",
                    Alpha3Code = "PRI",
                    NumericCode = "630",
                    PhoneCode = "+1",
                    CurrencyId = "USD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "PS",
                    Alpha3Code = "PSE",
                    NumericCode = "275",
                    PhoneCode = "+970",
                    CurrencyId = "JOD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "PT",
                    Alpha3Code = "PRT",
                    NumericCode = "620",
                    PhoneCode = "+351",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "PW",
                    Alpha3Code = "PLW",
                    NumericCode = "585",
                    PhoneCode = "+680",
                    CurrencyId = "USD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "PY",
                    Alpha3Code = "PRY",
                    NumericCode = "600",
                    PhoneCode = "+595",
                    CurrencyId = "PYG",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "QA",
                    Alpha3Code = "QAT",
                    NumericCode = "634",
                    PhoneCode = "+974",
                    CurrencyId = "QAR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "RE",
                    Alpha3Code = "REU",
                    NumericCode = "638",
                    PhoneCode = "+262",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "RO",
                    Alpha3Code = "ROU",
                    NumericCode = "642",
                    PhoneCode = "+40",
                    CurrencyId = "RON",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "RS",
                    Alpha3Code = "SRB",
                    NumericCode = "688",
                    PhoneCode = "+381",
                    CurrencyId = "RSD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "RU",
                    Alpha3Code = "RUS",
                    NumericCode = "643",
                    PhoneCode = "+7",
                    CurrencyId = "RUB",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "RW",
                    Alpha3Code = "RWA",
                    NumericCode = "646",
                    PhoneCode = "+250",
                    CurrencyId = "RWF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "SA",
                    Alpha3Code = "SAU",
                    NumericCode = "682",
                    PhoneCode = "+966",
                    CurrencyId = "SAR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "SB",
                    Alpha3Code = "SLB",
                    NumericCode = "090",
                    PhoneCode = "+677",
                    CurrencyId = "SBD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "SC",
                    Alpha3Code = "SYC",
                    NumericCode = "690",
                    PhoneCode = "+248",
                    CurrencyId = "SCR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "SD",
                    Alpha3Code = "SDN",
                    NumericCode = "729",
                    PhoneCode = "+249",
                    CurrencyId = "SDG",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "SE",
                    Alpha3Code = "SWE",
                    NumericCode = "752",
                    PhoneCode = "+46",
                    CurrencyId = "SEK",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "SG",
                    Alpha3Code = "SGP",
                    NumericCode = "702",
                    PhoneCode = "+65",
                    CurrencyId = "SGD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "SH",
                    Alpha3Code = "SHN",
                    NumericCode = "654",
                    PhoneCode = "+290,+247",
                    CurrencyId = "GBP",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "SI",
                    Alpha3Code = "SVN",
                    NumericCode = "705",
                    PhoneCode = "+386",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "SJ",
                    Alpha3Code = "SJM",
                    NumericCode = "744",
                    PhoneCode = "+4779",
                    CurrencyId = "NOK",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "SK",
                    Alpha3Code = "SVK",
                    NumericCode = "703",
                    PhoneCode = "+421",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "SL",
                    Alpha3Code = "SLE",
                    NumericCode = "694",
                    PhoneCode = "+232",
                    CurrencyId = "SLE",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "SM",
                    Alpha3Code = "SMR",
                    NumericCode = "674",
                    PhoneCode = "+378",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "SN",
                    Alpha3Code = "SEN",
                    NumericCode = "686",
                    PhoneCode = "+221",
                    CurrencyId = "XOF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "SO",
                    Alpha3Code = "SOM",
                    NumericCode = "706",
                    PhoneCode = "+252",
                    CurrencyId = "SOS",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "SR",
                    Alpha3Code = "SUR",
                    NumericCode = "740",
                    PhoneCode = "+597",
                    CurrencyId = "SRD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "SS",
                    Alpha3Code = "SSD",
                    NumericCode = "728",
                    PhoneCode = "+211",
                    CurrencyId = "SSP",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "ST",
                    Alpha3Code = "STP",
                    NumericCode = "678",
                    PhoneCode = "+239",
                    CurrencyId = "STN",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "SV",
                    Alpha3Code = "SLV",
                    NumericCode = "222",
                    PhoneCode = "+503",
                    CurrencyId = "USD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "SX",
                    Alpha3Code = "SXM",
                    NumericCode = "534",
                    PhoneCode = "+1721",
                    CurrencyId = "XCG",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "SY",
                    Alpha3Code = "SYR",
                    NumericCode = "760",
                    PhoneCode = "+963",
                    CurrencyId = "SYP",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "SZ",
                    Alpha3Code = "SWZ",
                    NumericCode = "748",
                    PhoneCode = "+268",
                    CurrencyId = "SZL",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "TC",
                    Alpha3Code = "TCA",
                    NumericCode = "796",
                    PhoneCode = "+1649",
                    CurrencyId = "USD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "TD",
                    Alpha3Code = "TCD",
                    NumericCode = "148",
                    PhoneCode = "+235",
                    CurrencyId = "XAF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "TF",
                    Alpha3Code = "ATF",
                    NumericCode = "260",
                    PhoneCode = "+262",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "TG",
                    Alpha3Code = "TGO",
                    NumericCode = "768",
                    PhoneCode = "+228",
                    CurrencyId = "XOF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "TH",
                    Alpha3Code = "THA",
                    NumericCode = "764",
                    PhoneCode = "+66",
                    CurrencyId = "THB",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "TJ",
                    Alpha3Code = "TJK",
                    NumericCode = "762",
                    PhoneCode = "+992",
                    CurrencyId = "TJS",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "TK",
                    Alpha3Code = "TKL",
                    NumericCode = "772",
                    PhoneCode = "+690",
                    CurrencyId = "NZD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "TL",
                    Alpha3Code = "TLS",
                    NumericCode = "626",
                    PhoneCode = "+670",
                    CurrencyId = "USD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "TM",
                    Alpha3Code = "TKM",
                    NumericCode = "795",
                    PhoneCode = "+993",
                    CurrencyId = "TMT",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "TN",
                    Alpha3Code = "TUN",
                    NumericCode = "788",
                    PhoneCode = "+216",
                    CurrencyId = "TND",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "TO",
                    Alpha3Code = "TON",
                    NumericCode = "776",
                    PhoneCode = "+676",
                    CurrencyId = "TOP",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "TR",
                    Alpha3Code = "TUR",
                    NumericCode = "792",
                    PhoneCode = "+90",
                    CurrencyId = "TRY",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "TT",
                    Alpha3Code = "TTO",
                    NumericCode = "780",
                    PhoneCode = "+1868",
                    CurrencyId = "TTD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "TV",
                    Alpha3Code = "TUV",
                    NumericCode = "798",
                    PhoneCode = "+688",
                    CurrencyId = "AUD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "TW",
                    Alpha3Code = "TWN",
                    NumericCode = "158",
                    PhoneCode = "+886",
                    CurrencyId = "TWD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "TZ",
                    Alpha3Code = "TZA",
                    NumericCode = "834",
                    PhoneCode = "+255",
                    CurrencyId = "TZS",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "UA",
                    Alpha3Code = "UKR",
                    NumericCode = "804",
                    PhoneCode = "+380",
                    CurrencyId = "UAH",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "UG",
                    Alpha3Code = "UGA",
                    NumericCode = "800",
                    PhoneCode = "+256",
                    CurrencyId = "UGX",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "UM",
                    Alpha3Code = "UMI",
                    NumericCode = "581",
                    PhoneCode = "+268",
                    CurrencyId = "USD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "US",
                    Alpha3Code = "USA",
                    NumericCode = "840",
                    PhoneCode = "+1",
                    CurrencyId = "USD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "UY",
                    Alpha3Code = "URY",
                    NumericCode = "858",
                    PhoneCode = "+598",
                    CurrencyId = "UYU",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "UZ",
                    Alpha3Code = "UZB",
                    NumericCode = "860",
                    PhoneCode = "+998",
                    CurrencyId = "UZS",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "VA",
                    Alpha3Code = "VAT",
                    NumericCode = "336",
                    PhoneCode = "+39,+379",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "VC",
                    Alpha3Code = "VCT",
                    NumericCode = "670",
                    PhoneCode = "+1784",
                    CurrencyId = "XCD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "VE",
                    Alpha3Code = "VEN",
                    NumericCode = "862",
                    PhoneCode = "+58",
                    CurrencyId = "VES",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "VG",
                    Alpha3Code = "VGB",
                    NumericCode = "092",
                    PhoneCode = "+1284",
                    CurrencyId = "USD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "VI",
                    Alpha3Code = "VIR",
                    NumericCode = "850",
                    PhoneCode = "+1340",
                    CurrencyId = "USD",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "VN",
                    Alpha3Code = "VNM",
                    NumericCode = "704",
                    PhoneCode = "+84",
                    CurrencyId = "VND",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "VU",
                    Alpha3Code = "VUT",
                    NumericCode = "548",
                    PhoneCode = "+678",
                    CurrencyId = "VUV",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "WF",
                    Alpha3Code = "WLF",
                    NumericCode = "876",
                    PhoneCode = "+681",
                    CurrencyId = "XPF",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "WS",
                    Alpha3Code = "WSM",
                    NumericCode = "882",
                    PhoneCode = "+685",
                    CurrencyId = "WST",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "YE",
                    Alpha3Code = "YEM",
                    NumericCode = "887",
                    PhoneCode = "+967",
                    CurrencyId = "YER",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "YT",
                    Alpha3Code = "MYT",
                    NumericCode = "175",
                    PhoneCode = "+262",
                    CurrencyId = "EUR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "ZA",
                    Alpha3Code = "ZAF",
                    NumericCode = "710",
                    PhoneCode = "+27",
                    CurrencyId = "ZAR",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "ZM",
                    Alpha3Code = "ZMB",
                    NumericCode = "894",
                    PhoneCode = "+260",
                    CurrencyId = "ZMW",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Country
                {
                    Id = "ZW",
                    Alpha3Code = "ZWE",
                    NumericCode = "716",
                    PhoneCode = "+263",
                    CurrencyId = "ZWG",
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                }
            );
        }
    }
}

