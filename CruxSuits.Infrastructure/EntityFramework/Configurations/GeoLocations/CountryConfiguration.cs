using CruxSuits.Infrastructure.EntityFramework.Entities.GeoLocations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Configurations.GeoLocations
{
    internal class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        private static IEnumerable<Country> GetWorldCountries()
        {
            DateTime now = new DateTime(2025, 01, 01);

            return new List<Country>
            {
                new Country { Id = "AF", Id3Digit = "AFG", PhoneCode = "93",   CurrencyId = "AFN", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Afghanistan - AFN
                new Country { Id = "AL", Id3Digit = "ALB", PhoneCode = "355",  CurrencyId = "ALL", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Albania - ALL
                new Country { Id = "DZ", Id3Digit = "DZA", PhoneCode = "213",  CurrencyId = "DZD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Algeria - DZD
                new Country { Id = "AS", Id3Digit = "ASM", PhoneCode = "1684", CurrencyId = "USD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false }, // American Samoa - USD
                new Country { Id = "AD", Id3Digit = "AND", PhoneCode = "376",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Andorra - EUR
                new Country { Id = "AO", Id3Digit = "AGO", PhoneCode = "244",  CurrencyId = "AOA", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Angola - AOA
                new Country { Id = "AI", Id3Digit = "AIA", PhoneCode = "1264", CurrencyId = "XCD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false }, // Anguilla - USD
                new Country { Id = "AQ", Id3Digit = "ATA", PhoneCode = "672",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Antarctica - (EUR placeholder)
                new Country { Id = "AG", Id3Digit = "ATG", PhoneCode = "1268", CurrencyId = "XCD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false }, // Antigua and Barbuda - XCD
                new Country { Id = "AR", Id3Digit = "ARG", PhoneCode = "54",   CurrencyId = "ARS", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Argentina - ARS
                new Country { Id = "AM", Id3Digit = "ARM", PhoneCode = "374",  CurrencyId = "AMD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Armenia - AMD
                new Country { Id = "AW", Id3Digit = "ABW", PhoneCode = "297",  CurrencyId = "AWG", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Aruba - AWG
                new Country { Id = "AU", Id3Digit = "AUS", PhoneCode = "61",   CurrencyId = "AUD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Australia - AUD
                new Country { Id = "AT", Id3Digit = "AUT", PhoneCode = "43",   CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Austria - EUR
                new Country { Id = "AZ", Id3Digit = "AZE", PhoneCode = "994",  CurrencyId = "AZN", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Azerbaijan - AZN

                new Country { Id = "BS", Id3Digit = "BHS", PhoneCode = "1242", CurrencyId = "BSD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Bahamas - BSD
                new Country { Id = "BH", Id3Digit = "BHR", PhoneCode = "973",  CurrencyId = "BHD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Bahrain - BHD
                new Country { Id = "BD", Id3Digit = "BGD", PhoneCode = "880",  CurrencyId = "BDT", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Bangladesh - BDT
                new Country { Id = "BB", Id3Digit = "BRB", PhoneCode = "1246", CurrencyId = "BBD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Barbados - BBD
                new Country { Id = "BY", Id3Digit = "BLR", PhoneCode = "375",  CurrencyId = "BYN", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Belarus - BYN
                new Country { Id = "BE", Id3Digit = "BEL", PhoneCode = "32",   CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Belgium - EUR
                new Country { Id = "BZ", Id3Digit = "BLZ", PhoneCode = "501",  CurrencyId = "BZD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Belize - BZD
                new Country { Id = "BJ", Id3Digit = "BEN", PhoneCode = "229",  CurrencyId = "XOF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false }, // Benin - XOF
                new Country { Id = "BM", Id3Digit = "BMU", PhoneCode = "1441", CurrencyId = "BMD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Bermuda - BMD
                new Country { Id = "BT", Id3Digit = "BTN", PhoneCode = "975",  CurrencyId = "BTN", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Bhutan - BTN
                new Country { Id = "BO", Id3Digit = "BOL", PhoneCode = "591",  CurrencyId = "BOB", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Bolivia - BOB
                new Country { Id = "BA", Id3Digit = "BIH", PhoneCode = "387",  CurrencyId = "BAM", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Bosnia and Herzegovina - BAM
                new Country { Id = "BW", Id3Digit = "BWA", PhoneCode = "267",  CurrencyId = "BWP", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Botswana - BWP
                new Country { Id = "BV", Id3Digit = "BVT", PhoneCode = "47",   CurrencyId = "NOK", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Bouvet Island - EUR (territory)
                new Country { Id = "BR", Id3Digit = "BRA", PhoneCode = "55",   CurrencyId = "BRL", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Brazil - BRL
                new Country { Id = "IO", Id3Digit = "IOT", PhoneCode = "246",  CurrencyId = "USD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false }, // British Indian Ocean Territory - USD
                new Country { Id = "BN", Id3Digit = "BRN", PhoneCode = "673",  CurrencyId = "BND", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Brunei Darussalam - BND
                new Country { Id = "BG", Id3Digit = "BGR", PhoneCode = "359",  CurrencyId = "BGN", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Bulgaria - BGN
                new Country { Id = "BF", Id3Digit = "BFA", PhoneCode = "226",  CurrencyId = "XOF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false }, // Burkina Faso - XOF
                new Country { Id = "BI", Id3Digit = "BDI", PhoneCode = "257",  CurrencyId = "BIF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Burundi - BIF
                
                new Country { Id = "CA", Id3Digit = "CAN", PhoneCode = "1",    CurrencyId = "CAD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },
                new Country { Id = "CX", Id3Digit = "CXR", PhoneCode = "61",   CurrencyId = "AUD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Christmas Island - AUD
                new Country { Id = "CC", Id3Digit = "CCK", PhoneCode = "61",   CurrencyId = "AUD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Cocos (Keeling) Islands - AUD
                new Country { Id = "CO", Id3Digit = "COL", PhoneCode = "57",   CurrencyId = "COP", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Colombia - COP
                new Country { Id = "KM", Id3Digit = "KHM", PhoneCode = "269",  CurrencyId = "KMF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Comoros - KMF
                new Country { Id = "CG", Id3Digit = "COG", PhoneCode = "242",  CurrencyId = "XAF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Congo - XAF
                new Country { Id = "CD", Id3Digit = "COD", PhoneCode = "243",  CurrencyId = "CDF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Congo (DRC) - CDF
                new Country { Id = "CK", Id3Digit = "COK", PhoneCode = "682",  CurrencyId = "NZD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Cook Islands - USD (placeholder)
                new Country { Id = "CL", Id3Digit = "CHL", PhoneCode = "56",   CurrencyId = "CLP", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },
                new Country { Id = "CN", Id3Digit = "CHN", PhoneCode = "86",   CurrencyId = "CNY", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },
                new Country { Id = "CR", Id3Digit = "CRI", PhoneCode = "506",  CurrencyId = "CRC", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Costa Rica - CRC
                new Country { Id = "HR", Id3Digit = "HRV", PhoneCode = "385",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Croatia - EUR
                new Country { Id = "CU", Id3Digit = "CUB", PhoneCode = "53",   CurrencyId = "CUP", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Cuba - CUP
                new Country { Id = "CW", Id3Digit = "CUW", PhoneCode = "599",  CurrencyId = "XCG", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Curaçao - USD
                new Country { Id = "CY", Id3Digit = "CYP", PhoneCode = "357",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Cyprus - EUR
                new Country { Id = "CZ", Id3Digit = "CZE", PhoneCode = "420",  CurrencyId = "CZK", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Czechia - CZK

                new Country { Id = "DK", Id3Digit = "DNK", PhoneCode = "45",   CurrencyId = "DKK", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Denmark - DKK
                new Country { Id = "DJ", Id3Digit = "DJI", PhoneCode = "253",  CurrencyId = "DJF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Djibouti - DJF
                new Country { Id = "DM", Id3Digit = "DMA", PhoneCode = "1767", CurrencyId = "XCD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Dominica - XCD
                new Country { Id = "DO", Id3Digit = "DOM", PhoneCode = "1809", CurrencyId = "DOP", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Dominican Republic - DOP

                new Country { Id = "EC", Id3Digit = "ECU", PhoneCode = "593",  CurrencyId = "USD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Ecuador - USD
                new Country { Id = "EG", Id3Digit = "EGY", PhoneCode = "20",   CurrencyId = "EGP", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Egypt - EGP
                new Country { Id = "SV", Id3Digit = "SLV", PhoneCode = "503",  CurrencyId = "USD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // El Salvador - USD
                new Country { Id = "GQ", Id3Digit = "GNQ", PhoneCode = "240",  CurrencyId = "XAF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Equatorial Guinea - XAF
                new Country { Id = "ER", Id3Digit = "ERI", PhoneCode = "291",  CurrencyId = "ERN", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Eritrea - ERN
                new Country { Id = "EE", Id3Digit = "EST", PhoneCode = "372",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Estonia - EUR
                new Country { Id = "SZ", Id3Digit = "SWZ", PhoneCode = "268",  CurrencyId = "SZL", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Eswatini - SZL
                new Country { Id = "ET", Id3Digit = "ETH", PhoneCode = "251",  CurrencyId = "ETB", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Ethiopia - ETB

                new Country { Id = "FK", Id3Digit = "FLK", PhoneCode = "500",  CurrencyId = "FKP", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Falkland Islands - FKP
                new Country { Id = "FO", Id3Digit = "FRO", PhoneCode = "298",  CurrencyId = "DKK", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Faroe Islands - DKK
                new Country { Id = "FJ", Id3Digit = "FJI", PhoneCode = "679",  CurrencyId = "FJD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Fiji - FJD
                new Country { Id = "FI", Id3Digit = "FIN", PhoneCode = "358",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Finland - EUR
                new Country { Id = "FR", Id3Digit = "FRA", PhoneCode = "33",   CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // France - EUR
                new Country { Id = "GF", Id3Digit = "GUF", PhoneCode = "594",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // French Guiana - EUR
                new Country { Id = "PF", Id3Digit = "PYF", PhoneCode = "689",  CurrencyId = "XPF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // French Polynesia - XPF
                new Country { Id = "TF", Id3Digit = "ATF", PhoneCode = "262",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // French Southern Territories - EUR

                new Country { Id = "GA", Id3Digit = "GAB", PhoneCode = "241",  CurrencyId = "XAF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Gabon - XAF
                new Country { Id = "GM", Id3Digit = "GMB", PhoneCode = "220",  CurrencyId = "GMD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Gambia - GMD
                new Country { Id = "GE", Id3Digit = "GEO", PhoneCode = "995",  CurrencyId = "GEL", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Georgia - EUR (uses GEL in reality, not part of ISO4217 in this list, fallback to EUR)
                new Country { Id = "DE", Id3Digit = "DEU", PhoneCode = "49",   CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Germany - EUR
                new Country { Id = "GH", Id3Digit = "GHA", PhoneCode = "233",  CurrencyId = "GHS", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Ghana - GHS
                new Country { Id = "GI", Id3Digit = "GIB", PhoneCode = "350",  CurrencyId = "GIP", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Gibraltar - GIP
                new Country { Id = "GR", Id3Digit = "GRC", PhoneCode = "30",   CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Greece - EUR
                new Country { Id = "GL", Id3Digit = "GRL", PhoneCode = "299",  CurrencyId = "DKK", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Greenland - DKK
                new Country { Id = "GD", Id3Digit = "GRD", PhoneCode = "1473", CurrencyId = "XCD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Grenada - XCD
                new Country { Id = "GP", Id3Digit = "GLP", PhoneCode = "590",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Guadeloupe - EUR
                new Country { Id = "GU", Id3Digit = "GUM", PhoneCode = "1671", CurrencyId = "USD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Guam - USD
                new Country { Id = "GT", Id3Digit = "GTM", PhoneCode = "502",  CurrencyId = "GTQ", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Guatemala - GTQ
                new Country { Id = "GG", Id3Digit = "GGY", PhoneCode = "44",   CurrencyId = "GBP", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Guernsey - GBP
                new Country { Id = "GN", Id3Digit = "GIN", PhoneCode = "224",  CurrencyId = "GNF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Guinea - GNF
                new Country { Id = "GW", Id3Digit = "GNB", PhoneCode = "245",  CurrencyId = "XOF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Guinea-Bissau - XOF
                new Country { Id = "GY", Id3Digit = "GUY", PhoneCode = "592",  CurrencyId = "GYD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Guyana - GYD

                new Country { Id = "HT", Id3Digit = "HTI", PhoneCode = "509",  CurrencyId = "HTG", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Haiti - HTG
                new Country { Id = "HM", Id3Digit = "HMD", PhoneCode = "672",  CurrencyId = "AUD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Heard Island and McDonald Islands - AUD
                new Country { Id = "VA", Id3Digit = "VAT", PhoneCode = "379",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Holy See (Vatican City State) - EUR
                new Country { Id = "HN", Id3Digit = "HND", PhoneCode = "504",  CurrencyId = "HNL", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Honduras - HNL
                new Country { Id = "HK", Id3Digit = "HKG", PhoneCode = "852",  CurrencyId = "HKD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Hong Kong - HKD
                new Country { Id = "HU", Id3Digit = "HUN", PhoneCode = "36",   CurrencyId = "HUF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Hungary - HUF

                new Country { Id = "IS", Id3Digit = "ISL", PhoneCode = "354",  CurrencyId = "ISK", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Iceland - ISK
                new Country { Id = "IN", Id3Digit = "IND", PhoneCode = "91",   CurrencyId = "INR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // India - INR
                new Country { Id = "ID", Id3Digit = "IDN", PhoneCode = "62",   CurrencyId = "IDR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Indonesia - IDR
                new Country { Id = "IR", Id3Digit = "IRN", PhoneCode = "98",   CurrencyId = "IRR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Iran - IRR
                new Country { Id = "IQ", Id3Digit = "IRQ", PhoneCode = "964",  CurrencyId = "IQD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Iraq - IQD
                new Country { Id = "IE", Id3Digit = "IRL", PhoneCode = "353",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Ireland - EUR
                new Country { Id = "IM", Id3Digit = "IMN", PhoneCode = "44",   CurrencyId = "GBP", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Isle of Man - GBP
                new Country { Id = "IT", Id3Digit = "ITA", PhoneCode = "39",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Italy - EUR

                new Country { Id = "JM", Id3Digit = "JAM", PhoneCode = "1876", CurrencyId = "JMD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Jamaica - JMD
                new Country { Id = "JP", Id3Digit = "JPN", PhoneCode = "81",   CurrencyId = "JPY", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Japan - JPY
                new Country { Id = "JE", Id3Digit = "JEY", PhoneCode = "44",   CurrencyId = "GBP", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Jersey - GBP
                new Country { Id = "JO", Id3Digit = "JOR", PhoneCode = "962",  CurrencyId = "JOD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Jordan - JOD

                new Country { Id = "KZ", Id3Digit = "KAZ", PhoneCode = "7",    CurrencyId = "KZT", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Kazakhstan - KZT
                new Country { Id = "KE", Id3Digit = "KEN", PhoneCode = "254",  CurrencyId = "KES", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Kenya - KES
                new Country { Id = "KI", Id3Digit = "KIR", PhoneCode = "686",  CurrencyId = "AUD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Kiribati - USD
                new Country { Id = "KP", Id3Digit = "PRK", PhoneCode = "850",  CurrencyId = "KPW", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Korea (North) - KPW
                new Country { Id = "KR", Id3Digit = "KOR", PhoneCode = "82",   CurrencyId = "KRW", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Korea (South) - KRW
                new Country { Id = "KW", Id3Digit = "KWT", PhoneCode = "965",  CurrencyId = "KWD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Kuwait - KWD
                new Country { Id = "KG", Id3Digit = "KGZ", PhoneCode = "996",  CurrencyId = "KGS", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Kyrgyzstan - KGS

                new Country { Id = "LA", Id3Digit = "LAO", PhoneCode = "856",  CurrencyId = "LAK", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Laos - LAK
                new Country { Id = "LV", Id3Digit = "LVA", PhoneCode = "371",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Latvia - EUR
                new Country { Id = "LB", Id3Digit = "LBN", PhoneCode = "961",  CurrencyId = "LBP", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Lebanon - LBP
                new Country { Id = "LS", Id3Digit = "LSO", PhoneCode = "266",  CurrencyId = "LSL", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Lesotho - LSL
                new Country { Id = "LR", Id3Digit = "LBR", PhoneCode = "231",  CurrencyId = "LRD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Liberia - LRD
                new Country { Id = "LY", Id3Digit = "LBY", PhoneCode = "218",  CurrencyId = "LYD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Libya - LYD
                new Country { Id = "LI", Id3Digit = "LIE", PhoneCode = "423",  CurrencyId = "CHF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Liechtenstein - EUR (CHF in reality, EUR placeholder if CHF not seeded)
                new Country { Id = "LT", Id3Digit = "LTU", PhoneCode = "370",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Lithuania - EUR
                new Country { Id = "LU", Id3Digit = "LUX", PhoneCode = "352",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Luxembourg - EUR

                new Country { Id = "MO", Id3Digit = "MAC", PhoneCode = "853",  CurrencyId = "MOP", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Macao - MOP
                new Country { Id = "MG", Id3Digit = "MDG", PhoneCode = "261",  CurrencyId = "MGA", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Madagascar - MGA
                new Country { Id = "MW", Id3Digit = "MWI", PhoneCode = "265",  CurrencyId = "MWK", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Malawi - MWK
                new Country { Id = "MY", Id3Digit = "MYS", PhoneCode = "60",   CurrencyId = "MYR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Malaysia - MYR
                new Country { Id = "MV", Id3Digit = "MDV", PhoneCode = "960",  CurrencyId = "MVR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Maldives - MVR
                new Country { Id = "ML", Id3Digit = "MLI", PhoneCode = "223",  CurrencyId = "XOF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Mali - XOF
                new Country { Id = "MT", Id3Digit = "MLT", PhoneCode = "356",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Malta - EUR
                new Country { Id = "MH", Id3Digit = "MHL", PhoneCode = "692",  CurrencyId = "USD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Marshall Islands - USD
                new Country { Id = "MQ", Id3Digit = "MTQ", PhoneCode = "596",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Martinique - EUR
                new Country { Id = "MR", Id3Digit = "MRT", PhoneCode = "222",  CurrencyId = "MRU", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Mauritania - MRU
                new Country { Id = "MU", Id3Digit = "MUS", PhoneCode = "230",  CurrencyId = "MUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Mauritius - MUR
                new Country { Id = "YT", Id3Digit = "MYT", PhoneCode = "262",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Mayotte - EUR
                new Country { Id = "MX", Id3Digit = "MEX", PhoneCode = "52",   CurrencyId = "MXN", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Mexico - MXN
                new Country { Id = "FM", Id3Digit = "FSM", PhoneCode = "691",  CurrencyId = "USD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Micronesia - USD
                new Country { Id = "MD", Id3Digit = "MDA", PhoneCode = "373",  CurrencyId = "MDL", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Moldova - MDL
                new Country { Id = "MC", Id3Digit = "MCO", PhoneCode = "377",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Monaco - EUR
                new Country { Id = "MN", Id3Digit = "MNG", PhoneCode = "976",  CurrencyId = "MNT", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Mongolia - MNT
                new Country { Id = "ME", Id3Digit = "MNE", PhoneCode = "382",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Montenegro - EUR
                new Country { Id = "MS", Id3Digit = "MSR", PhoneCode = "1664", CurrencyId = "XCD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Montserrat - USD
                new Country { Id = "MA", Id3Digit = "MAR", PhoneCode = "212",  CurrencyId = "MAD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Morocco - MAD
                new Country { Id = "MZ", Id3Digit = "MOZ", PhoneCode = "258",  CurrencyId = "MZN", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Mozambique - MZN
                new Country { Id = "MM", Id3Digit = "MMR", PhoneCode = "95",   CurrencyId = "MMK", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Myanmar - MMK

                new Country { Id = "NA", Id3Digit = "NAM", PhoneCode = "264",  CurrencyId = "NAD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Namibia - NAD
                new Country { Id = "NR", Id3Digit = "NRU", PhoneCode = "674",  CurrencyId = "AUD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Nauru - USD
                new Country { Id = "NP", Id3Digit = "NPL", PhoneCode = "977",  CurrencyId = "NPR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Nepal - NPR
                new Country { Id = "NL", Id3Digit = "NLD", PhoneCode = "31",   CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Netherlands - EUR
                new Country { Id = "NC", Id3Digit = "NCL", PhoneCode = "687",  CurrencyId = "XPF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // New Caledonia - XPF
                new Country { Id = "NZ", Id3Digit = "NZL", PhoneCode = "64",   CurrencyId = "NZD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // New Zealand - NZD
                new Country { Id = "NI", Id3Digit = "NIC", PhoneCode = "505",  CurrencyId = "NIO", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Nicaragua - NIO
                new Country { Id = "NE", Id3Digit = "NER", PhoneCode = "227",  CurrencyId = "XOF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Niger - XOF
                new Country { Id = "NG", Id3Digit = "NGA", PhoneCode = "234",  CurrencyId = "NGN", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Nigeria - NGN
                new Country { Id = "NU", Id3Digit = "NIU", PhoneCode = "683",  CurrencyId = "NZD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Niue - NZD
                new Country { Id = "NF", Id3Digit = "NFK", PhoneCode = "672",  CurrencyId = "AUD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Norfolk Island - AUD
                new Country { Id = "MP", Id3Digit = "MNP", PhoneCode = "1670", CurrencyId = "USD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Northern Mariana Islands - USD
                new Country { Id = "NO", Id3Digit = "NOR", PhoneCode = "47",   CurrencyId = "NOK", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Norway - NOK

                new Country { Id = "OM", Id3Digit = "OMN", PhoneCode = "968",  CurrencyId = "OMR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Oman - OMR

                new Country { Id = "PK", Id3Digit = "PAK", PhoneCode = "92",    CurrencyId = "PKR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false }, // Pakistan - PKR
                new Country { Id = "PW", Id3Digit = "PLW", PhoneCode = "680",   CurrencyId = "USD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false }, // Palau - USD
                new Country { Id = "PS", Id3Digit = "PSE", PhoneCode = "970",   CurrencyId = "USD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false }, // Palestine - USD (no official currency)
                new Country { Id = "PA", Id3Digit = "PAN", PhoneCode = "507",   CurrencyId = "PAB", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false }, // Panama - PAB
                new Country { Id = "PG", Id3Digit = "PNG", PhoneCode = "675",   CurrencyId = "PGK", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false }, // Papua New Guinea - PGK
                new Country { Id = "PY", Id3Digit = "PRY", PhoneCode = "595",   CurrencyId = "PYG", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false }, // Paraguay - PYG
                new Country { Id = "PE", Id3Digit = "PER", PhoneCode = "51",    CurrencyId = "PEN", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false }, // Peru - PEN
                new Country { Id = "PH", Id3Digit = "PHL", PhoneCode = "63",    CurrencyId = "PHP", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false }, // Philippines - PHP
                new Country { Id = "PN", Id3Digit = "PCN", PhoneCode = "64",    CurrencyId = "NZD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false }, // Pitcairn Islands - NZD
                new Country { Id = "PL", Id3Digit = "POL", PhoneCode = "48",    CurrencyId = "PLN", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false }, // Poland - PLN
                new Country { Id = "PT", Id3Digit = "PRT", PhoneCode = "351",   CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Portugal - EUR
                new Country { Id = "PR", Id3Digit = "PRI", PhoneCode = "1787",  CurrencyId = "USD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false }, // Puerto Rico - USD

                new Country { Id = "QA", Id3Digit = "QAT", PhoneCode = "974",   CurrencyId = "QAR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Qatar - QAR

                new Country { Id = "RE", Id3Digit = "REU", PhoneCode = "262",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Réunion - EUR
                new Country { Id = "RO", Id3Digit = "ROU", PhoneCode = "40",   CurrencyId = "RON", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Romania - RON
                new Country { Id = "RU", Id3Digit = "RUS", PhoneCode = "7",    CurrencyId = "RUB", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Russia - RUB
                new Country { Id = "RW", Id3Digit = "RWA", PhoneCode = "250",  CurrencyId = "RWF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Rwanda - RWF

                new Country { Id = "BL", Id3Digit = "BLM", PhoneCode = "590",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Saint Barthélemy - EUR
                new Country { Id = "SH", Id3Digit = "SHN", PhoneCode = "290",  CurrencyId = "SHP", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Saint Helena - SHP
                new Country { Id = "KN", Id3Digit = "KNA", PhoneCode = "1869", CurrencyId = "XCD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Saint Kitts and Nevis - XCD
                new Country { Id = "LC", Id3Digit = "LCA", PhoneCode = "1758", CurrencyId = "XCD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Saint Lucia - XCD
                new Country { Id = "MF", Id3Digit = "MAF", PhoneCode = "590",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Saint Martin - EUR
                new Country { Id = "PM", Id3Digit = "SPM", PhoneCode = "508",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Saint Pierre and Miquelon - EUR
                new Country { Id = "VC", Id3Digit = "VCT", PhoneCode = "1784", CurrencyId = "XCD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Saint Vincent and the Grenadines - XCD
                new Country { Id = "WS", Id3Digit = "WSM", PhoneCode = "685",  CurrencyId = "WST", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Samoa - WST
                new Country { Id = "SM", Id3Digit = "SMR", PhoneCode = "378",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // San Marino - EUR
                new Country { Id = "ST", Id3Digit = "STP", PhoneCode = "239",  CurrencyId = "STN", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Sao Tome and Principe - STN
                new Country { Id = "SA", Id3Digit = "SAU", PhoneCode = "966",  CurrencyId = "SAR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Saudi Arabia - SAR
                new Country { Id = "SN", Id3Digit = "SEN", PhoneCode = "221",  CurrencyId = "XOF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Senegal - XOF
                new Country { Id = "RS", Id3Digit = "SRB", PhoneCode = "381",  CurrencyId = "RSD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Serbia - RSD
                new Country { Id = "SC", Id3Digit = "SYC", PhoneCode = "248",  CurrencyId = "SCR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Seychelles - SCR
                new Country { Id = "SL", Id3Digit = "SLE", PhoneCode = "232",  CurrencyId = "SLE", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Sierra Leone - SLE
                new Country { Id = "SG", Id3Digit = "SGP", PhoneCode = "65",   CurrencyId = "SGD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Singapore - SGD
                new Country { Id = "SX", Id3Digit = "SXM", PhoneCode = "1721", CurrencyId = "XCG", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Sint Maarten - USD
                new Country { Id = "SK", Id3Digit = "SVK", PhoneCode = "421",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Slovakia - EUR
                new Country { Id = "SI", Id3Digit = "SVN", PhoneCode = "386",  CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Slovenia - EUR
                new Country { Id = "SB", Id3Digit = "SLB", PhoneCode = "677",  CurrencyId = "SBD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Solomon Islands - SBD
                new Country { Id = "SO", Id3Digit = "SOM", PhoneCode = "252",  CurrencyId = "SOS", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Somalia - SOS
                new Country { Id = "ZA", Id3Digit = "ZAF", PhoneCode = "27",   CurrencyId = "ZAR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // South Africa - ZAR
                new Country { Id = "GS", Id3Digit = "SGS", PhoneCode = "500",  CurrencyId = "USD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // South Georgia and the South Sandwich Islands - USD
                new Country { Id = "SS", Id3Digit = "SSD", PhoneCode = "211",  CurrencyId = "SSP", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // South Sudan - SSP
                new Country { Id = "ES", Id3Digit = "ESP", PhoneCode = "34",   CurrencyId = "EUR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Spain - EUR
                new Country { Id = "LK", Id3Digit = "LKA", PhoneCode = "94",   CurrencyId = "LKR", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Sri Lanka - LKR
                new Country { Id = "SD", Id3Digit = "SDN", PhoneCode = "249",  CurrencyId = "SDG", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Sudan - SDG
                new Country { Id = "SR", Id3Digit = "SUR", PhoneCode = "597",  CurrencyId = "SRD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Suriname - SRD
                new Country { Id = "SJ", Id3Digit = "SJM", PhoneCode = "47",   CurrencyId = "NOK", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Svalbard and Jan Mayen - NOK
                new Country { Id = "SE", Id3Digit = "SWE", PhoneCode = "46",   CurrencyId = "SEK", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Sweden - SEK
                new Country { Id = "CH", Id3Digit = "CHE", PhoneCode = "41",   CurrencyId = "CHF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Switzerland - EUR placeholder (CHF not seeded)
                new Country { Id = "SY", Id3Digit = "SYR", PhoneCode = "963",  CurrencyId = "SYP", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },    // Syrian Arab Republic - SYP

                new Country { Id = "TW", Id3Digit = "TWN", PhoneCode = "886",  CurrencyId = "TWD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Taiwan - TWD
                new Country { Id = "TJ", Id3Digit = "TJK", PhoneCode = "992",  CurrencyId = "TJS", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Tajikistan - TJS
                new Country { Id = "TZ", Id3Digit = "TZA", PhoneCode = "255",  CurrencyId = "TZS", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Tanzania - TZS
                new Country { Id = "TH", Id3Digit = "THA", PhoneCode = "66",   CurrencyId = "THB", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Thailand - THB
                new Country { Id = "TL", Id3Digit = "TLS", PhoneCode = "670",  CurrencyId = "USD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Timor-Leste - USD
                new Country { Id = "TG", Id3Digit = "TGO", PhoneCode = "228",  CurrencyId = "XOF", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Togo - XOF
                new Country { Id = "TK", Id3Digit = "TKL", PhoneCode = "690",  CurrencyId = "NZD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Tokelau - NZD
                new Country { Id = "TO", Id3Digit = "TON", PhoneCode = "676",  CurrencyId = "TOP", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Tonga - TOP
                new Country { Id = "TT", Id3Digit = "TTO", PhoneCode = "1868", CurrencyId = "TTD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Trinidad and Tobago - TTD
                new Country { Id = "TN", Id3Digit = "TUN", PhoneCode = "216",  CurrencyId = "TND", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Tunisia - TND
                new Country { Id = "TR", Id3Digit = "TUR", PhoneCode = "90",   CurrencyId = "TRY", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Türkiye - TRY
                new Country { Id = "TM", Id3Digit = "TKM", PhoneCode = "993",  CurrencyId = "TMT", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Turkmenistan - TMT
                new Country { Id = "TC", Id3Digit = "TCA", PhoneCode = "1649", CurrencyId = "USD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Turks and Caicos Islands - USD
                new Country { Id = "TV", Id3Digit = "TUV", PhoneCode = "688",  CurrencyId = "AUD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Tuvalu - USD

                new Country { Id = "UG", Id3Digit = "UGA", PhoneCode = "256",  CurrencyId = "UGX", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Uganda - UGX
                new Country { Id = "UA", Id3Digit = "UKR", PhoneCode = "380",  CurrencyId = "UAH", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Ukraine - UAH
                new Country { Id = "AE", Id3Digit = "ARE", PhoneCode = "971",  CurrencyId = "AED", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // United Arab Emirates - AED
                new Country { Id = "GB", Id3Digit = "GBR", PhoneCode = "44",   CurrencyId = "GBP", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // United Kingdom - GBP
                new Country { Id = "US", Id3Digit = "USA", PhoneCode = "1",    CurrencyId = "USD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // United States - USD
                new Country { Id = "UM", Id3Digit = "UMI", PhoneCode = "1",    CurrencyId = "USD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // United States Minor Outlying Islands - USD
                new Country { Id = "UY", Id3Digit = "URY", PhoneCode = "598",  CurrencyId = "UYU", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Uruguay - UYU
                new Country { Id = "UZ", Id3Digit = "UZB", PhoneCode = "998",  CurrencyId = "UZS", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Uzbekistan - UZS

                new Country { Id = "VU", Id3Digit = "VUT", PhoneCode = "678",  CurrencyId = "VUV", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Vanuatu - VUV
                new Country { Id = "VE", Id3Digit = "VEN", PhoneCode = "58",   CurrencyId = "VES", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Venezuela - VES
                new Country { Id = "VN", Id3Digit = "VNM", PhoneCode = "84",   CurrencyId = "VND", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Vietnam - VND
                new Country { Id = "VG", Id3Digit = "VGB", PhoneCode = "1284", CurrencyId = "USD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Virgin Islands (British) - USD
                new Country { Id = "VI", Id3Digit = "VIR", PhoneCode = "1340", CurrencyId = "USD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Virgin Islands (U.S.) - USD

                new Country { Id = "EH", Id3Digit = "ESH", PhoneCode = "212", CurrencyId = "MAD", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Western Sahara - MAD (same as Morocco)

                new Country { Id = "YE", Id3Digit = "YEM", PhoneCode = "967", CurrencyId = "YER", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },   // Yemen - YER

                new Country { Id = "ZM", Id3Digit = "ZMB", PhoneCode = "260", CurrencyId = "ZMW", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },  // Zambia - ZMW
                new Country { Id = "ZW", Id3Digit = "ZWE", PhoneCode = "263", CurrencyId = "ZWL", CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false }   // Zimbabwe - ZWL
            };
        }
        public void Configure(EntityTypeBuilder<Country> builder)
        {

            builder.HasQueryFilter(f => !f.IsDeleted);

            builder.HasIndex(i => i.Id).IsUnique();
            builder.HasIndex(i => i.Id3Digit).IsUnique();

            builder.HasData(GetWorldCountries());
        }
    }
}
