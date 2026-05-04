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
    internal class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        private static IEnumerable<Currency> GetWorldCurrencies()
        {
            DateTime now = new DateTime(2025, 01, 01); 

            return new List<Currency>
            {
                 // ===== A =====
                new Currency { Id = "AED", Num = "784", Symbol = "د.إ", DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // United Arab Emirates Dirham
                new Currency { Id = "AFN", Num = "971", Symbol = "؋",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Afghan Afghani
                new Currency { Id = "ALL", Num = "008", Symbol = "L",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Albanian Lek
                new Currency { Id = "AMD", Num = "051", Symbol = "֏",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Armenian Dram
                new Currency { Id = "AOA", Num = "973", Symbol = "Kz", DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Angolan Kwanza
                new Currency { Id = "ARS", Num = "032", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Argentine Peso (symbol "$" reserved)
                new Currency { Id = "AUD", Num = "036", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Australian Dollar (symbol "$" reserved)
                new Currency { Id = "AWG", Num = "533", Symbol = "ƒ",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Aruban Florin
                new Currency { Id = "AZN", Num = "944", Symbol = "₼",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Azerbaijani Manat

                // ===== B =====
                new Currency { Id = "BAM", Num = "977", Symbol = "KM", DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Bosnia and Herzegovina Convertible Mark
                new Currency { Id = "BBD", Num = "052", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Barbados Dollar (symbol "$" reserved)
                new Currency { Id = "BDT", Num = "050", Symbol = "৳",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Bangladeshi Taka
                new Currency { Id = "BGN", Num = "975", Symbol = "лв", DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Bulgarian Lev
                new Currency { Id = "BHD", Num = "048", Symbol = null, DecimalSeparator = 3, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Bahraini Dinar
                new Currency { Id = "BIF", Num = "108", Symbol = null, DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Burundian Franc
                new Currency { Id = "BMD", Num = "060", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Bermudian Dollar (symbol "$" reserved)
                new Currency { Id = "BND", Num = "096", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Brunei Dollar (symbol "$" reserved)
                new Currency { Id = "BOB", Num = "068", Symbol = "Bs", DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Boliviano
                new Currency { Id = "BOV", Num = "984", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Bolivian Mvdol (funds code)
                new Currency { Id = "BRL", Num = "986", Symbol = "R$", DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Brazilian Real
                new Currency { Id = "BSD", Num = "044", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Bahamian Dollar (symbol "$" reserved)
                new Currency { Id = "BTN", Num = "064", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Bhutanese Ngultrum
                new Currency { Id = "BWP", Num = "072", Symbol = "P",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Botswana Pula
                new Currency { Id = "BYN", Num = "933", Symbol = "Br", DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Belarusian Ruble
                new Currency { Id = "BZD", Num = "084", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Belize Dollar (symbol "$" reserved)

                // ===== C =====
                new Currency { Id = "CAD", Num = "124", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Canadian Dollar (symbol "$" reserved)
                new Currency { Id = "CDF", Num = "976", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Congolese Franc
                new Currency { Id = "CHE", Num = "947", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // WIR Euro (funds code)
                new Currency { Id = "CHF", Num = "756", Symbol = "Fr", DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Swiss Franc
                new Currency { Id = "CHW", Num = "948", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // WIR Franc (funds code)
                new Currency { Id = "CLF", Num = "990", Symbol = null, DecimalSeparator = 4, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Chilean Unidad de Fomento (funds code)
                new Currency { Id = "CLP", Num = "152", Symbol = null, DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Chilean Peso (symbol "$" reserved)
                new Currency { Id = "CNY", Num = "156", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Chinese Yuan (shares "¥" → null)
                new Currency { Id = "COP", Num = "170", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Colombian Peso (symbol "$" reserved)
                new Currency { Id = "COU", Num = "970", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Colombian Unidad de Valor Real (funds code)
                new Currency { Id = "CRC", Num = "188", Symbol = "₡",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Costa Rican Colon
                new Currency { Id = "CUP", Num = "192", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Cuban Peso (symbol "$" reserved)
                new Currency { Id = "CVE", Num = "132", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Cabo Verde Escudo (often “Esc” or “$”, keep null)
                new Currency { Id = "CZK", Num = "203", Symbol = "Kč", DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Czech Koruna

                // ===== D =====
                new Currency { Id = "DJF", Num = "262", Symbol = null, DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false },  // Djiboutian Franc
                new Currency { Id = "DKK", Num = "208", Symbol = "kr", DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false },  // Danish Krone
                new Currency { Id = "DOP", Num = "214", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false },  // Dominican Peso (symbol "$" reserved)
                new Currency { Id = "DZD", Num = "012", Symbol = "دج", DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false },  // Algerian Dinar

                // ===== E =====
                new Currency { Id = "EGP", Num = "818", Symbol = null,  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false },  // Egyptian Pound
                new Currency { Id = "ERN", Num = "232", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false },  // Eritrean Nakfa
                new Currency { Id = "ETB", Num = "230", Symbol = "Br", DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false },  // Ethiopian Birr
                new Currency { Id = "EUR", Num = "978", Symbol = "€",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false },  // Euro

                // ===== F =====
                new Currency { Id = "FJD", Num = "242", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false },  // Fiji Dollar (symbol "$" reserved)
                new Currency { Id = "FKP", Num = "238", Symbol = null,  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false },  // Falkland Islands Pound

                 // ===== G =====
                 new Currency { Id = "GBP", Num = "826", Symbol = "£", DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 1, CreatedUserName = "system", IsDeleted = false },
                new Currency { Id = "GEL", Num = "981", Symbol = "₾",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Georgian Lari
                new Currency { Id = "GHS", Num = "936", Symbol = "₵",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Ghanaian Cedi
                new Currency { Id = "GIP", Num = "292", Symbol = "£",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Gibraltar Pound
                new Currency { Id = "GMD", Num = "270", Symbol = "D",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Gambian Dalasi
                new Currency { Id = "GNF", Num = "324", Symbol = null, DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Guinean Franc
                new Currency { Id = "GTQ", Num = "320", Symbol = "Q",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Guatemalan Quetzal
                new Currency { Id = "GYD", Num = "328", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Guyana Dollar (symbol "$" reserved)

                // ===== H =====
                new Currency { Id = "HKD", Num = "344", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Hong Kong Dollar (symbol "$" reserved)
                new Currency { Id = "HNL", Num = "340", Symbol = "L",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Honduran Lempira
                new Currency { Id = "HTG", Num = "332", Symbol = "G",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Haitian Gourde
                new Currency { Id = "HUF", Num = "348", Symbol = "Ft", DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Hungarian Forint

                // ===== I =====
                new Currency { Id = "IDR", Num = "360", Symbol = "Rp", DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Indonesian Rupiah
                new Currency { Id = "INR", Num = "356", Symbol = "₹",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Indian Rupee
                new Currency { Id = "IQD", Num = "368", Symbol = "ع.د", DecimalSeparator = 3, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Iraqi Dinar
                new Currency { Id = "IRR", Num = "364", Symbol = "﷼",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Iranian Rial
                new Currency { Id = "ISK", Num = "352", Symbol = "kr", DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Icelandic Króna

                // ===== J =====
                new Currency { Id = "JMD", Num = "388", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Jamaican Dollar (symbol "$" reserved)
                new Currency { Id = "JOD", Num = "400", Symbol = null, DecimalSeparator = 3, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Jordanian Dinar
                new Currency { Id = "JPY", Num = "392", Symbol = "¥",  DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Japanese Yen

                // ===== K =====
                new Currency { Id = "KES", Num = "404", Symbol = "Sh", DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Kenyan Shilling
                new Currency { Id = "KGS", Num = "417", Symbol = "с",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Kyrgyzstani Som
                new Currency { Id = "KHR", Num = "116", Symbol = "៛",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Cambodian Riel
                new Currency { Id = "KMF", Num = "174", Symbol = null, DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Comorian Franc
                new Currency { Id = "KRW", Num = "410", Symbol = null,  DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // South Korean Won
                new Currency { Id = "KPW", Num = "408", Symbol = "₩",  DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // North Korean Won
                new Currency { Id = "KWD", Num = "414", Symbol = null, DecimalSeparator = 3, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Kuwaiti Dinar
                new Currency { Id = "KYD", Num = "136", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Cayman Islands Dollar (symbol "$" reserved)
                new Currency { Id = "KZT", Num = "398", Symbol = "₸",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Kazakhstani Tenge

                // ===== L =====
                new Currency { Id = "LAK", Num = "418", Symbol = "₭",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Lao Kip
                new Currency { Id = "LBP", Num = "422", Symbol = "ل.ل", DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Lebanese Pound
                new Currency { Id = "LKR", Num = "144", Symbol = "₨",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Sri Lankan Rupee
                new Currency { Id = "LRD", Num = "430", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Liberian Dollar (symbol "$" reserved)
                new Currency { Id = "LSL", Num = "426", Symbol = "L",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Lesotho Loti
                new Currency { Id = "LYD", Num = "434", Symbol = "ل.د", DecimalSeparator = 3, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false },  // Libyan Dinar

                // ===== M =====
                new Currency { Id = "MAD", Num = "504", Symbol = "د.م.", DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Moroccan Dirham
                new Currency { Id = "MDL", Num = "498", Symbol = "L",    DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Moldovan Leu
                new Currency { Id = "MGA", Num = "969", Symbol = "Ar",   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Malagasy Ariary
                new Currency { Id = "MKD", Num = "807", Symbol = "ден",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Macedonian Denar
                new Currency { Id = "MMK", Num = "104", Symbol = "Ks",   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Myanmar Kyat
                new Currency { Id = "MNT", Num = "496", Symbol = "₮",    DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Mongolian Tögrög
                new Currency { Id = "MOP", Num = "446", Symbol = "P",    DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Macanese Pataca
                new Currency { Id = "MRU", Num = "929", Symbol = "UM",   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Mauritanian Ouguiya
                new Currency { Id = "MUR", Num = "480", Symbol = "₨",    DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Mauritian Rupee
                new Currency { Id = "MVR", Num = "462", Symbol = "Rf",   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Maldivian Rufiyaa
                new Currency { Id = "MWK", Num = "454", Symbol = "MK",   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Malawian Kwacha
                new Currency { Id = "MXN", Num = "484", Symbol = null,   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Mexican Peso (symbol "$" reserved)
                new Currency { Id = "MXV", Num = "979", Symbol = null,   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Mexican UDI (funds code)
                new Currency { Id = "MYR", Num = "458", Symbol = "RM",   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Malaysian Ringgit
                new Currency { Id = "MZN", Num = "943", Symbol = "MT",   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Mozambican Metical

                // ===== N =====
                new Currency { Id = "NAD", Num = "516", Symbol = null,   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Namibian Dollar (symbol "$" reserved)
                new Currency { Id = "NGN", Num = "566", Symbol = "₦",    DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Nigerian Naira
                new Currency { Id = "NIO", Num = "558", Symbol = "C$",   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Nicaraguan Córdoba
                new Currency { Id = "NOK", Num = "578", Symbol = "kr",   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Norwegian Krone
                new Currency { Id = "NPR", Num = "524", Symbol = "₨",    DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Nepalese Rupee
                new Currency { Id = "NZD", Num = "554", Symbol = null,   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // New Zealand Dollar (symbol "$" reserved)

                // ===== O =====
                new Currency { Id = "OMR", Num = "512", Symbol = null,   DecimalSeparator = 3, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Omani Rial

                // ===== P =====
                new Currency { Id = "PAB", Num = "590", Symbol = null,   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Panamanian Balboa
                new Currency { Id = "PEN", Num = "604", Symbol = "S/",   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Peruvian Sol
                new Currency { Id = "PGK", Num = "598", Symbol = "K",    DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Papua New Guinean Kina
                new Currency { Id = "PHP", Num = "608", Symbol = "₱",    DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Philippine Peso
                new Currency { Id = "PKR", Num = "586", Symbol = "₨",    DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Pakistani Rupee
                new Currency { Id = "PLN", Num = "985", Symbol = "zł",   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Polish Zloty
                new Currency { Id = "PYG", Num = "600", Symbol = "₲",    DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Paraguayan Guarani

                // ===== Q =====
                new Currency { Id = "QAR", Num = "634", Symbol = "﷼",    DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Qatari Riyal

                // ===== R =====
                new Currency { Id = "RON", Num = "946", Symbol = "lei",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Romanian Leu
                new Currency { Id = "RSD", Num = "941", Symbol = "дин",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Serbian Dinar
                new Currency { Id = "RUB", Num = "643", Symbol = "₽",    DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Russian Ruble
                new Currency { Id = "RWF", Num = "646", Symbol = null,   DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Rwandan Franc

                // ===== S =====
                new Currency { Id = "SAR", Num = "682", Symbol = "﷼",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Saudi Riyal
                new Currency { Id = "SBD", Num = "090", Symbol = null,  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Solomon Islands Dollar (symbol "$" reserved)
                new Currency { Id = "SCR", Num = "690", Symbol = "₨",   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Seychelles Rupee
                new Currency { Id = "SDG", Num = "938", Symbol = null,   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Sudanese Pound
                new Currency { Id = "SEK", Num = "752", Symbol = "kr",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Swedish Krona
                new Currency { Id = "SGD", Num = "702", Symbol = null,  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Singapore Dollar (symbol "$" reserved)
                new Currency { Id = "SHP", Num = "654", Symbol = null,   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Saint Helena Pound
                new Currency { Id = "SLE", Num = "925", Symbol = "Le",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Sierra Leonean Leone (new)
                new Currency { Id = "SLL", Num = "694", Symbol = "Le",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Sierra Leonean Leone (old/existing)
                new Currency { Id = "SOS", Num = "706", Symbol = "Sh",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Somali Shilling
                new Currency { Id = "SRD", Num = "968", Symbol = null,  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Surinam Dollar (symbol "$" reserved)
                new Currency { Id = "SSP", Num = "728", Symbol = null,   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // South Sudanese Pound
                new Currency { Id = "STN", Num = "930", Symbol = "Db",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // São Tomé and Príncipe Dobra
                new Currency { Id = "SVC", Num = "222", Symbol = null,  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // El Salvador Colon (ISO funds/legacy)
                new Currency { Id = "SYP", Num = "760", Symbol = null,   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Syrian Pound
                new Currency { Id = "SZL", Num = "748", Symbol = "E",   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Eswatini Lilangeni

                // ===== T =====
                new Currency { Id = "THB", Num = "764", Symbol = "฿",   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Thai Baht
                new Currency { Id = "TJS", Num = "972", Symbol = "SM",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Tajikistani Somoni
                new Currency { Id = "TMT", Num = "934", Symbol = "m",   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Turkmenistan Manat
                new Currency { Id = "TND", Num = "788", Symbol = "د.ت", DecimalSeparator = 3, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Tunisian Dinar
                new Currency { Id = "TOP", Num = "776", Symbol = "T$",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Tongan Paʻanga
                new Currency { Id = "TRY", Num = "949", Symbol = "₺",   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Turkish Lira
                new Currency { Id = "TTD", Num = "780", Symbol = null,  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Trinidad and Tobago Dollar (symbol "$" reserved)
                new Currency { Id = "TWD", Num = "901", Symbol = null,  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // New Taiwan Dollar (uses "NT$", reserve "$")
                new Currency { Id = "TZS", Num = "834", Symbol = "Sh",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Tanzanian Shilling

                // ===== U =====
                new Currency { Id = "UAH", Num = "980", Symbol = "₴",   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Ukrainian Hryvnia
                new Currency { Id = "UGX", Num = "800", Symbol = "USh", DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Ugandan Shilling
                new Currency { Id = "USD", Num = "840", Symbol = "$",   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // United States Dollar
                new Currency { Id = "USN", Num = "997", Symbol = null,  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // US Dollar (Next day) - funds code
                new Currency { Id = "UYI", Num = "940", Symbol = null,  DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Uruguay Peso en Unidades Indexadas (UI) - funds code
                new Currency { Id = "UYU", Num = "858", Symbol = null,  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Uruguayan Peso (uses "$", reserved)
                new Currency { Id = "UYW", Num = "927", Symbol = null,  DecimalSeparator = 4, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Unidad Previsional (Uy) - funds code
                new Currency { Id = "UZS", Num = "860", Symbol = "so'm",DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Uzbekistan Soʻm

                 // ===== V =====
                new Currency { Id = "VES", Num = "928", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Venezuelan Bolívar Soberano
                new Currency { Id = "VND", Num = "704", Symbol = "₫",  DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Vietnamese Đồng
                new Currency { Id = "VUV", Num = "548", Symbol = null, DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Vanuatu Vatu

                // ===== W =====
                new Currency { Id = "WST", Num = "882", Symbol = "T",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Samoan Tālā

                // ===== X (funds/units/metals + XCD) =====
                new Currency { Id = "XAF", Num = "950", Symbol = "Fr", DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // CFA Franc BEAC
                new Currency { Id = "XAG", Num = "961", Symbol = null, DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Silver (troy ounce)
                new Currency { Id = "XAU", Num = "959", Symbol = null, DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Gold (troy ounce)
                new Currency { Id = "XBA", Num = "955", Symbol = null, DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // European Composite Unit (EURCO)
                new Currency { Id = "XBB", Num = "956", Symbol = null, DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // European Monetary Unit (E.M.U.-6)
                new Currency { Id = "XBC", Num = "957", Symbol = null, DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // European Unit of Account 9 (E.U.A.-9)
                new Currency { Id = "XBD", Num = "958", Symbol = null, DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // European Unit of Account 17 (E.U.A.-17)
                new Currency { Id = "XCD", Num = "951", Symbol = null, DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // East Caribbean Dollar (symbol "$" reserved)
                new Currency { Id = "XCG", Num = "532", Symbol = "Fr", DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false },
                new Currency { Id = "XDR", Num = "960", Symbol = null, DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // SDR (IMF Special Drawing Rights)
                new Currency { Id = "XOF", Num = "952", Symbol = "Fr", DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // CFA Franc BCEAO
                new Currency { Id = "XPD", Num = "964", Symbol = null, DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Palladium (troy ounce)
                new Currency { Id = "XPF", Num = "953", Symbol = "₣",  DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // CFP Franc
                new Currency { Id = "XPT", Num = "962", Symbol = null, DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Platinum (troy ounce)
                new Currency { Id = "XSU", Num = "994", Symbol = null, DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Sucre (Unified System for Regional Compensation)
                new Currency { Id = "XTS", Num = "963", Symbol = null, DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Code reserved for testing
                new Currency { Id = "XUA", Num = "965", Symbol = null, DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // ADB Unit of Account
                new Currency { Id = "XXX", Num = "999", Symbol = null, DecimalSeparator = 0, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // No currency

                // ===== Y =====
                new Currency { Id = "YER", Num = "886", Symbol = "﷼",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Yemeni Rial

                // ===== Z =====
                new Currency { Id = "ZAR", Num = "710", Symbol = "R",   DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // South African Rand
                new Currency { Id = "ZMW", Num = "967", Symbol = "ZK",  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Zambian Kwacha
                new Currency { Id = "ZWL", Num = "932", Symbol = null,  DecimalSeparator = 2, CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }, // Zimbabwe Dollar (symbol "$" reserved)
            };

        }

        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasQueryFilter(f => !f.IsDeleted);

            builder.HasIndex(i => i.Num).IsUnique();

            builder.HasData(GetWorldCurrencies());
        }
    }
}
