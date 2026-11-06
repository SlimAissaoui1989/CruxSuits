using CruxSuits.Services.ReferenceData.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.ReferenceData.Infrastructure.EntityFramework.Configurations
{
    internal class LocalizedNameConfiguration : IEntityTypeConfiguration<LocalizedName>
    {
        public void Configure(EntityTypeBuilder<LocalizedName> builder)
        {
            builder.HasIndex(x => new { x.ResourceType, x.ResourceId, x.LanguageId })
                .IsUnique();

            builder.HasOne(x => x.Language)
                .WithMany(x => x.LocalizedNames)
                .HasForeignKey(x => x.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                /* *************** CURRENCY NAMES *************** */
                new LocalizedName{Id = 1,LanguageId = "en",ResourceType = "Currency",ResourceId = "AED",Name = "United Arab Emirates Dirham",},
                new LocalizedName{Id = 2,LanguageId = "ar",ResourceType = "Currency",ResourceId = "AED",Name = "درهم إماراتي",},
                new LocalizedName{Id = 3,LanguageId = "fr",ResourceType = "Currency",ResourceId = "AED",Name = "dirham des Émirats arabes unis",},

                new LocalizedName{Id = 4,LanguageId = "en",ResourceType = "Currency",ResourceId = "AFN",Name = "Afghan Afghani",},
                new LocalizedName{Id = 5,LanguageId = "ar",ResourceType = "Currency",ResourceId = "AFN",Name = "أفغاني",},
                new LocalizedName{Id = 6,LanguageId = "fr",ResourceType = "Currency",ResourceId = "AFN",Name = "afghani afghan",},

                new LocalizedName{Id = 7,LanguageId = "en",ResourceType = "Currency",ResourceId = "ALL",Name = "Albanian Lek",},
                new LocalizedName{Id = 8,LanguageId = "ar",ResourceType = "Currency",ResourceId = "ALL",Name = "ليك ألباني",},
                new LocalizedName{Id = 9,LanguageId = "fr",ResourceType = "Currency",ResourceId = "ALL",Name = "lek albanais",},

                new LocalizedName{Id = 10,LanguageId = "en",ResourceType = "Currency",ResourceId = "AMD",Name = "Armenian Dram",},
                new LocalizedName{Id = 11,LanguageId = "ar",ResourceType = "Currency",ResourceId = "AMD",Name = "درام أرميني",},
                new LocalizedName{Id = 12,LanguageId = "fr",ResourceType = "Currency",ResourceId = "AMD",Name = "dram arménien",},

                new LocalizedName{Id = 13,LanguageId = "en",ResourceType = "Currency",ResourceId = "AOA",Name = "Angolan Kwanza",},
                new LocalizedName{Id = 14,LanguageId = "ar",ResourceType = "Currency",ResourceId = "AOA",Name = "كوانزا أنغولي",},
                new LocalizedName{Id = 15,LanguageId = "fr",ResourceType = "Currency",ResourceId = "AOA",Name = "kwanza angolais",},

                new LocalizedName{Id = 16,LanguageId = "en",ResourceType = "Currency",ResourceId = "ARS",Name = "Argentine Peso",},
                new LocalizedName{Id = 17,LanguageId = "ar",ResourceType = "Currency",ResourceId = "ARS",Name = "بيزو أرجنتيني",},
                new LocalizedName{Id = 18,LanguageId = "fr",ResourceType = "Currency",ResourceId = "ARS",Name = "peso argentin",},

                new LocalizedName{Id = 19,LanguageId = "en",ResourceType = "Currency",ResourceId = "AUD",Name = "Australian Dollar",},
                new LocalizedName{Id = 20,LanguageId = "ar",ResourceType = "Currency",ResourceId = "AUD",Name = "دولار أسترالي",},
                new LocalizedName{Id = 21,LanguageId = "fr",ResourceType = "Currency",ResourceId = "AUD",Name = "dollar australien",},

                new LocalizedName{Id = 22,LanguageId = "en",ResourceType = "Currency",ResourceId = "AWG",Name = "Aruban Florin",},
                new LocalizedName{Id = 23,LanguageId = "ar",ResourceType = "Currency",ResourceId = "AWG",Name = "فلورن أروبي",},
                new LocalizedName{Id = 24,LanguageId = "fr",ResourceType = "Currency",ResourceId = "AWG",Name = "florin arubais",},

                new LocalizedName{Id = 25,LanguageId = "en",ResourceType = "Currency",ResourceId = "AZN",Name = "Azerbaijani Manat",},
                new LocalizedName{Id = 26,LanguageId = "ar",ResourceType = "Currency",ResourceId = "AZN",Name = "مانات أذربيجان",},
                new LocalizedName{Id = 27,LanguageId = "fr",ResourceType = "Currency",ResourceId = "AZN",Name = "manat azéri",},

                new LocalizedName{Id = 28,LanguageId = "en",ResourceType = "Currency",ResourceId = "BAM",Name = "Bosnia-Herzegovina Convertible Mark",},
                new LocalizedName{Id = 29,LanguageId = "ar",ResourceType = "Currency",ResourceId = "BAM",Name = "مارك البوسنة والهرسك قابل للتحويل",},
                new LocalizedName{Id = 30,LanguageId = "fr",ResourceType = "Currency",ResourceId = "BAM",Name = "mark convertible bosniaque",},

                new LocalizedName{Id = 31,LanguageId = "en",ResourceType = "Currency",ResourceId = "BBD",Name = "Barbadian Dollar",},
                new LocalizedName{Id = 32,LanguageId = "ar",ResourceType = "Currency",ResourceId = "BBD",Name = "دولار بربادوسي",},
                new LocalizedName{Id = 33,LanguageId = "fr",ResourceType = "Currency",ResourceId = "BBD",Name = "dollar barbadien",},

                new LocalizedName{Id = 34,LanguageId = "en",ResourceType = "Currency",ResourceId = "BDT",Name = "Bangladeshi Taka",},
                new LocalizedName{Id = 35,LanguageId = "ar",ResourceType = "Currency",ResourceId = "BDT",Name = "تاكا بنغلاديشي",},
                new LocalizedName{Id = 36,LanguageId = "fr",ResourceType = "Currency",ResourceId = "BDT",Name = "taka bangladeshi",},

                new LocalizedName{Id = 37,LanguageId = "en",ResourceType = "Currency",ResourceId = "BHD",Name = "Bahraini Dinar",},
                new LocalizedName{Id = 38,LanguageId = "ar",ResourceType = "Currency",ResourceId = "BHD",Name = "دينار بحريني",},
                new LocalizedName{Id = 39,LanguageId = "fr",ResourceType = "Currency",ResourceId = "BHD",Name = "dinar bahreïni",},

                new LocalizedName{Id = 40,LanguageId = "en",ResourceType = "Currency",ResourceId = "BIF",Name = "Burundian Franc",},
                new LocalizedName{Id = 41,LanguageId = "ar",ResourceType = "Currency",ResourceId = "BIF",Name = "فرنك بروندي",},
                new LocalizedName{Id = 42,LanguageId = "fr",ResourceType = "Currency",ResourceId = "BIF",Name = "franc burundais",},

                new LocalizedName{Id = 43,LanguageId = "en",ResourceType = "Currency",ResourceId = "BMD",Name = "Bermudan Dollar",},
                new LocalizedName{Id = 44,LanguageId = "ar",ResourceType = "Currency",ResourceId = "BMD",Name = "دولار برمودي",},
                new LocalizedName{Id = 45,LanguageId = "fr",ResourceType = "Currency",ResourceId = "BMD",Name = "dollar bermudien",},

                new LocalizedName{Id = 46,LanguageId = "en",ResourceType = "Currency",ResourceId = "BND",Name = "Brunei Dollar",},
                new LocalizedName{Id = 47,LanguageId = "ar",ResourceType = "Currency",ResourceId = "BND",Name = "دولار بروناي",},
                new LocalizedName{Id = 48,LanguageId = "fr",ResourceType = "Currency",ResourceId = "BND",Name = "dollar brunéien",},

                new LocalizedName{Id = 49,LanguageId = "en",ResourceType = "Currency",ResourceId = "BOB",Name = "Bolivian Boliviano",},
                new LocalizedName{Id = 50,LanguageId = "ar",ResourceType = "Currency",ResourceId = "BOB",Name = "بوليفيانو بوليفي",},
                new LocalizedName{Id = 51,LanguageId = "fr",ResourceType = "Currency",ResourceId = "BOB",Name = "boliviano bolivien",},

                new LocalizedName{Id = 52,LanguageId = "en",ResourceType = "Currency",ResourceId = "BOV",Name = "Bolivian Mvdol",},
                new LocalizedName{Id = 53,LanguageId = "ar",ResourceType = "Currency",ResourceId = "BOV",Name = "مودفول بوليفي",},
                new LocalizedName{Id = 54,LanguageId = "fr",ResourceType = "Currency",ResourceId = "BOV",Name = "Mvdol bolivien",},

                new LocalizedName{Id = 55,LanguageId = "en",ResourceType = "Currency",ResourceId = "BRL",Name = "Brazilian Real",},
                new LocalizedName{Id = 56,LanguageId = "ar",ResourceType = "Currency",ResourceId = "BRL",Name = "ريال برازيلي",},
                new LocalizedName{Id = 57,LanguageId = "fr",ResourceType = "Currency",ResourceId = "BRL",Name = "réal brésilien",},

                new LocalizedName{Id = 58,LanguageId = "en",ResourceType = "Currency",ResourceId = "BSD",Name = "Bahamian Dollar",},
                new LocalizedName{Id = 59,LanguageId = "ar",ResourceType = "Currency",ResourceId = "BSD",Name = "دولار باهامي",},
                new LocalizedName{Id = 60,LanguageId = "fr",ResourceType = "Currency",ResourceId = "BSD",Name = "dollar bahaméen",},

                new LocalizedName{Id = 61,LanguageId = "en",ResourceType = "Currency",ResourceId = "BTN",Name = "Bhutanese Ngultrum",},
                new LocalizedName{Id = 62,LanguageId = "ar",ResourceType = "Currency",ResourceId = "BTN",Name = "نولتوم بوتاني",},
                new LocalizedName{Id = 63,LanguageId = "fr",ResourceType = "Currency",ResourceId = "BTN",Name = "ngultrum bouthanais",},

                new LocalizedName{Id = 64,LanguageId = "en",ResourceType = "Currency",ResourceId = "BWP",Name = "Botswanan Pula",},
                new LocalizedName{Id = 65,LanguageId = "ar",ResourceType = "Currency",ResourceId = "BWP",Name = "بولا بتسواني",},
                new LocalizedName{Id = 66,LanguageId = "fr",ResourceType = "Currency",ResourceId = "BWP",Name = "pula botswanais",},

                new LocalizedName{Id = 67,LanguageId = "en",ResourceType = "Currency",ResourceId = "BYN",Name = "Belarusian Ruble",},
                new LocalizedName{Id = 68,LanguageId = "ar",ResourceType = "Currency",ResourceId = "BYN",Name = "روبل بيلاروسي",},
                new LocalizedName{Id = 69,LanguageId = "fr",ResourceType = "Currency",ResourceId = "BYN",Name = "rouble biélorusse",},

                new LocalizedName{Id = 70,LanguageId = "en",ResourceType = "Currency",ResourceId = "BZD",Name = "Belize Dollar",},
                new LocalizedName{Id = 71,LanguageId = "ar",ResourceType = "Currency",ResourceId = "BZD",Name = "دولار بليزي",},
                new LocalizedName{Id = 72,LanguageId = "fr",ResourceType = "Currency",ResourceId = "BZD",Name = "dollar bélizéen",},

                new LocalizedName{Id = 73,LanguageId = "en",ResourceType = "Currency",ResourceId = "CAD",Name = "Canadian Dollar",},
                new LocalizedName{Id = 74,LanguageId = "ar",ResourceType = "Currency",ResourceId = "CAD",Name = "دولار كندي",},
                new LocalizedName{Id = 75,LanguageId = "fr",ResourceType = "Currency",ResourceId = "CAD",Name = "dollar canadien",},

                new LocalizedName{Id = 76,LanguageId = "en",ResourceType = "Currency",ResourceId = "CDF",Name = "Congolese Franc",},
                new LocalizedName{Id = 77,LanguageId = "ar",ResourceType = "Currency",ResourceId = "CDF",Name = "فرنك كونغولي",},
                new LocalizedName{Id = 78,LanguageId = "fr",ResourceType = "Currency",ResourceId = "CDF",Name = "franc congolais",},

                new LocalizedName{Id = 79,LanguageId = "en",ResourceType = "Currency",ResourceId = "CHE",Name = "WIR Euro",},
                new LocalizedName{Id = 80,LanguageId = "ar",ResourceType = "Currency",ResourceId = "CHE",Name = "يورو WIR",},
                new LocalizedName{Id = 81,LanguageId = "fr",ResourceType = "Currency",ResourceId = "CHE",Name = "euro WIR",},

                new LocalizedName{Id = 82,LanguageId = "en",ResourceType = "Currency",ResourceId = "CHF",Name = "Swiss Franc",},
                new LocalizedName{Id = 83,LanguageId = "ar",ResourceType = "Currency",ResourceId = "CHF",Name = "فرنك سويسري",},
                new LocalizedName{Id = 84,LanguageId = "fr",ResourceType = "Currency",ResourceId = "CHF",Name = "franc suisse",},

                new LocalizedName{Id = 85,LanguageId = "en",ResourceType = "Currency",ResourceId = "CHW",Name = "WIR Franc",},
                new LocalizedName{Id = 86,LanguageId = "ar",ResourceType = "Currency",ResourceId = "CHW",Name = "فرنك WIR",},
                new LocalizedName{Id = 87,LanguageId = "fr",ResourceType = "Currency",ResourceId = "CHW",Name = "franc WIR",},

                new LocalizedName{Id = 88,LanguageId = "en",ResourceType = "Currency",ResourceId = "CLF",Name = "Unidad de Fomento",},
                new LocalizedName{Id = 89,LanguageId = "ar",ResourceType = "Currency",ResourceId = "CLF",Name = "وحدة التنمية التشيلية",},
                new LocalizedName{Id = 90,LanguageId = "fr",ResourceType = "Currency",ResourceId = "CLF",Name = "unité de compte chilienne",},

                new LocalizedName{Id = 91,LanguageId = "en",ResourceType = "Currency",ResourceId = "CLP",Name = "Chilean Peso",},
                new LocalizedName{Id = 92,LanguageId = "ar",ResourceType = "Currency",ResourceId = "CLP",Name = "بيزو تشيلي",},
                new LocalizedName{Id = 93,LanguageId = "fr",ResourceType = "Currency",ResourceId = "CLP",Name = "peso chilien",},

                new LocalizedName{Id = 94,LanguageId = "en",ResourceType = "Currency",ResourceId = "CNY",Name = "Chinese Yuan",},
                new LocalizedName{Id = 95,LanguageId = "ar",ResourceType = "Currency",ResourceId = "CNY",Name = "يوان صيني",},
                new LocalizedName{Id = 96,LanguageId = "fr",ResourceType = "Currency",ResourceId = "CNY",Name = "yuan renminbi chinois",},

                new LocalizedName{Id = 97,LanguageId = "en",ResourceType = "Currency",ResourceId = "COP",Name = "Colombian Peso",},
                new LocalizedName{Id = 98,LanguageId = "ar",ResourceType = "Currency",ResourceId = "COP",Name = "بيزو كولومبي",},
                new LocalizedName{Id = 99,LanguageId = "fr",ResourceType = "Currency",ResourceId = "COP",Name = "peso colombien",},

                new LocalizedName{Id = 100,LanguageId = "en",ResourceType = "Currency",ResourceId = "COU",Name = "Unidad de Valor Real (UVR)",},
                new LocalizedName{Id = 101,LanguageId = "ar",ResourceType = "Currency",ResourceId = "COU",Name = "وحدة القيمة الحقيقية الكولومبية",},
                new LocalizedName{Id = 102,LanguageId = "fr",ResourceType = "Currency",ResourceId = "COU",Name = "unité de valeur réelle colombienne",},

                new LocalizedName{Id = 103,LanguageId = "en",ResourceType = "Currency",ResourceId = "CRC",Name = "Costa Rican Colón",},
                new LocalizedName{Id = 104,LanguageId = "ar",ResourceType = "Currency",ResourceId = "CRC",Name = "كولن كوستاريكي",},
                new LocalizedName{Id = 105,LanguageId = "fr",ResourceType = "Currency",ResourceId = "CRC",Name = "colón costaricain",},

                new LocalizedName{Id = 106,LanguageId = "en",ResourceType = "Currency",ResourceId = "CUP",Name = "Cuban Peso",},
                new LocalizedName{Id = 107,LanguageId = "ar",ResourceType = "Currency",ResourceId = "CUP",Name = "بيزو كوبي",},
                new LocalizedName{Id = 108,LanguageId = "fr",ResourceType = "Currency",ResourceId = "CUP",Name = "peso cubain",},

                new LocalizedName{Id = 109,LanguageId = "en",ResourceType = "Currency",ResourceId = "CVE",Name = "Cape Verdean Escudo",},
                new LocalizedName{Id = 110,LanguageId = "ar",ResourceType = "Currency",ResourceId = "CVE",Name = "اسكودو الرأس الأخضر",},
                new LocalizedName{Id = 111,LanguageId = "fr",ResourceType = "Currency",ResourceId = "CVE",Name = "escudo capverdien",},

                new LocalizedName{Id = 112,LanguageId = "en",ResourceType = "Currency",ResourceId = "CZK",Name = "Czech Koruna",},
                new LocalizedName{Id = 113,LanguageId = "ar",ResourceType = "Currency",ResourceId = "CZK",Name = "كرونة تشيكية",},
                new LocalizedName{Id = 114,LanguageId = "fr",ResourceType = "Currency",ResourceId = "CZK",Name = "couronne tchèque",},

                new LocalizedName{Id = 115,LanguageId = "en",ResourceType = "Currency",ResourceId = "DJF",Name = "Djiboutian Franc",},
                new LocalizedName{Id = 116,LanguageId = "ar",ResourceType = "Currency",ResourceId = "DJF",Name = "فرنك جيبوتي",},
                new LocalizedName{Id = 117,LanguageId = "fr",ResourceType = "Currency",ResourceId = "DJF",Name = "franc djiboutien",},

                new LocalizedName{Id = 118,LanguageId = "en",ResourceType = "Currency",ResourceId = "DKK",Name = "Danish Krone",},
                new LocalizedName{Id = 119,LanguageId = "ar",ResourceType = "Currency",ResourceId = "DKK",Name = "كرونة دنماركية",},
                new LocalizedName{Id = 120,LanguageId = "fr",ResourceType = "Currency",ResourceId = "DKK",Name = "couronne danoise",},

                new LocalizedName{Id = 121,LanguageId = "en",ResourceType = "Currency",ResourceId = "DOP",Name = "Dominican Peso",},
                new LocalizedName{Id = 122,LanguageId = "ar",ResourceType = "Currency",ResourceId = "DOP",Name = "بيزو الدومنيكان",},
                new LocalizedName{Id = 123,LanguageId = "fr",ResourceType = "Currency",ResourceId = "DOP",Name = "peso dominicain",},

                new LocalizedName{Id = 124,LanguageId = "en",ResourceType = "Currency",ResourceId = "DZD",Name = "Algerian Dinar",},
                new LocalizedName{Id = 125,LanguageId = "ar",ResourceType = "Currency",ResourceId = "DZD",Name = "دينار جزائري",},
                new LocalizedName{Id = 126,LanguageId = "fr",ResourceType = "Currency",ResourceId = "DZD",Name = "dinar algérien",},

                new LocalizedName{Id = 127,LanguageId = "en",ResourceType = "Currency",ResourceId = "EGP",Name = "Egyptian Pound",},
                new LocalizedName{Id = 128,LanguageId = "ar",ResourceType = "Currency",ResourceId = "EGP",Name = "جنيه مصري",},
                new LocalizedName{Id = 129,LanguageId = "fr",ResourceType = "Currency",ResourceId = "EGP",Name = "livre égyptienne",},

                new LocalizedName{Id = 130,LanguageId = "en",ResourceType = "Currency",ResourceId = "ERN",Name = "Eritrean Nakfa",},
                new LocalizedName{Id = 131,LanguageId = "ar",ResourceType = "Currency",ResourceId = "ERN",Name = "ناكفا أريتري",},
                new LocalizedName{Id = 132,LanguageId = "fr",ResourceType = "Currency",ResourceId = "ERN",Name = "nafka érythréen",},

                new LocalizedName{Id = 133,LanguageId = "en",ResourceType = "Currency",ResourceId = "ETB",Name = "Ethiopian Birr",},
                new LocalizedName{Id = 134,LanguageId = "ar",ResourceType = "Currency",ResourceId = "ETB",Name = "بير أثيوبي",},
                new LocalizedName{Id = 135,LanguageId = "fr",ResourceType = "Currency",ResourceId = "ETB",Name = "birr éthiopien",},

                new LocalizedName{Id = 136,LanguageId = "en",ResourceType = "Currency",ResourceId = "EUR",Name = "Euro",},
                new LocalizedName{Id = 137,LanguageId = "ar",ResourceType = "Currency",ResourceId = "EUR",Name = "يورو",},
                new LocalizedName{Id = 138,LanguageId = "fr",ResourceType = "Currency",ResourceId = "EUR",Name = "euro",},

                new LocalizedName{Id = 139,LanguageId = "en",ResourceType = "Currency",ResourceId = "FJD",Name = "Fijian Dollar",},
                new LocalizedName{Id = 140,LanguageId = "ar",ResourceType = "Currency",ResourceId = "FJD",Name = "دولار فيجي",},
                new LocalizedName{Id = 141,LanguageId = "fr",ResourceType = "Currency",ResourceId = "FJD",Name = "dollar fidjien",},

                new LocalizedName{Id = 142,LanguageId = "en",ResourceType = "Currency",ResourceId = "FKP",Name = "Falkland Islands Pound",},
                new LocalizedName{Id = 143,LanguageId = "ar",ResourceType = "Currency",ResourceId = "FKP",Name = "جنيه جزر فوكلاند",},
                new LocalizedName{Id = 144,LanguageId = "fr",ResourceType = "Currency",ResourceId = "FKP",Name = "livre des îles Malouines",},

                new LocalizedName{Id = 145,LanguageId = "en",ResourceType = "Currency",ResourceId = "GBP",Name = "British Pound",},
                new LocalizedName{Id = 146,LanguageId = "ar",ResourceType = "Currency",ResourceId = "GBP",Name = "جنيه إسترليني",},
                new LocalizedName{Id = 147,LanguageId = "fr",ResourceType = "Currency",ResourceId = "GBP",Name = "livre sterling",},

                new LocalizedName{Id = 148,LanguageId = "en",ResourceType = "Currency",ResourceId = "GEL",Name = "Georgian Lari",},
                new LocalizedName{Id = 149,LanguageId = "ar",ResourceType = "Currency",ResourceId = "GEL",Name = "لارى جورجي",},
                new LocalizedName{Id = 150,LanguageId = "fr",ResourceType = "Currency",ResourceId = "GEL",Name = "lari géorgien",},

                new LocalizedName{Id = 151,LanguageId = "en",ResourceType = "Currency",ResourceId = "GHS",Name = "Ghanaian Cedi",},
                new LocalizedName{Id = 152,LanguageId = "ar",ResourceType = "Currency",ResourceId = "GHS",Name = "سيدي غانا",},
                new LocalizedName{Id = 153,LanguageId = "fr",ResourceType = "Currency",ResourceId = "GHS",Name = "cédi ghanéen",},

                new LocalizedName{Id = 154,LanguageId = "en",ResourceType = "Currency",ResourceId = "GIP",Name = "Gibraltar Pound",},
                new LocalizedName{Id = 155,LanguageId = "ar",ResourceType = "Currency",ResourceId = "GIP",Name = "جنيه جبل طارق",},
                new LocalizedName{Id = 156,LanguageId = "fr",ResourceType = "Currency",ResourceId = "GIP",Name = "livre de Gibraltar",},

                new LocalizedName{Id = 157,LanguageId = "en",ResourceType = "Currency",ResourceId = "GMD",Name = "Gambian Dalasi",},
                new LocalizedName{Id = 158,LanguageId = "ar",ResourceType = "Currency",ResourceId = "GMD",Name = "دلاسي غامبي",},
                new LocalizedName{Id = 159,LanguageId = "fr",ResourceType = "Currency",ResourceId = "GMD",Name = "dalasi gambien",},

                new LocalizedName{Id = 160,LanguageId = "en",ResourceType = "Currency",ResourceId = "GNF",Name = "Guinean Franc",},
                new LocalizedName{Id = 161,LanguageId = "ar",ResourceType = "Currency",ResourceId = "GNF",Name = "فرنك غينيا",},
                new LocalizedName{Id = 162,LanguageId = "fr",ResourceType = "Currency",ResourceId = "GNF",Name = "franc guinéen",},

                new LocalizedName{Id = 163,LanguageId = "en",ResourceType = "Currency",ResourceId = "GTQ",Name = "Guatemalan Quetzal",},
                new LocalizedName{Id = 164,LanguageId = "ar",ResourceType = "Currency",ResourceId = "GTQ",Name = "كوتزال غواتيمالا",},
                new LocalizedName{Id = 165,LanguageId = "fr",ResourceType = "Currency",ResourceId = "GTQ",Name = "quetzal guatémaltèque",},

                new LocalizedName{Id = 166,LanguageId = "en",ResourceType = "Currency",ResourceId = "GYD",Name = "Guyanaese Dollar",},
                new LocalizedName{Id = 167,LanguageId = "ar",ResourceType = "Currency",ResourceId = "GYD",Name = "دولار غيانا",},
                new LocalizedName{Id = 168,LanguageId = "fr",ResourceType = "Currency",ResourceId = "GYD",Name = "dollar du Guyana",},

                new LocalizedName{Id = 169,LanguageId = "en",ResourceType = "Currency",ResourceId = "HKD",Name = "Hong Kong Dollar",},
                new LocalizedName{Id = 170,LanguageId = "ar",ResourceType = "Currency",ResourceId = "HKD",Name = "دولار هونغ كونغ",},
                new LocalizedName{Id = 171,LanguageId = "fr",ResourceType = "Currency",ResourceId = "HKD",Name = "dollar de Hong Kong",},

                new LocalizedName{Id = 172,LanguageId = "en",ResourceType = "Currency",ResourceId = "HNL",Name = "Honduran Lempira",},
                new LocalizedName{Id = 173,LanguageId = "ar",ResourceType = "Currency",ResourceId = "HNL",Name = "ليمبيرا هنداروس",},
                new LocalizedName{Id = 174,LanguageId = "fr",ResourceType = "Currency",ResourceId = "HNL",Name = "lempira hondurien",},

                new LocalizedName{Id = 175,LanguageId = "en",ResourceType = "Currency",ResourceId = "HTG",Name = "Haitian Gourde",},
                new LocalizedName{Id = 176,LanguageId = "ar",ResourceType = "Currency",ResourceId = "HTG",Name = "جوردى هايتي",},
                new LocalizedName{Id = 177,LanguageId = "fr",ResourceType = "Currency",ResourceId = "HTG",Name = "gourde haïtienne",},

                new LocalizedName{Id = 178,LanguageId = "en",ResourceType = "Currency",ResourceId = "HUF",Name = "Hungarian Forint",},
                new LocalizedName{Id = 179,LanguageId = "ar",ResourceType = "Currency",ResourceId = "HUF",Name = "فورينت هنغاري",},
                new LocalizedName{Id = 180,LanguageId = "fr",ResourceType = "Currency",ResourceId = "HUF",Name = "forint hongrois",},

                new LocalizedName{Id = 181,LanguageId = "en",ResourceType = "Currency",ResourceId = "IDR",Name = "Indonesian Rupiah",},
                new LocalizedName{Id = 182,LanguageId = "ar",ResourceType = "Currency",ResourceId = "IDR",Name = "روبية إندونيسية",},
                new LocalizedName{Id = 183,LanguageId = "fr",ResourceType = "Currency",ResourceId = "IDR",Name = "roupie indonésienne",},

                new LocalizedName{Id = 184,LanguageId = "en",ResourceType = "Currency",ResourceId = "INR",Name = "Indian Rupee",},
                new LocalizedName{Id = 185,LanguageId = "ar",ResourceType = "Currency",ResourceId = "INR",Name = "روبية هندي",},
                new LocalizedName{Id = 186,LanguageId = "fr",ResourceType = "Currency",ResourceId = "INR",Name = "roupie indienne",},

                new LocalizedName{Id = 187,LanguageId = "en",ResourceType = "Currency",ResourceId = "IQD",Name = "Iraqi Dinar",},
                new LocalizedName{Id = 188,LanguageId = "ar",ResourceType = "Currency",ResourceId = "IQD",Name = "دينار عراقي",},
                new LocalizedName{Id = 189,LanguageId = "fr",ResourceType = "Currency",ResourceId = "IQD",Name = "dinar irakien",},

                new LocalizedName{Id = 190,LanguageId = "en",ResourceType = "Currency",ResourceId = "IRR",Name = "Iranian Rial",},
                new LocalizedName{Id = 191,LanguageId = "ar",ResourceType = "Currency",ResourceId = "IRR",Name = "ريال إيراني",},
                new LocalizedName{Id = 192,LanguageId = "fr",ResourceType = "Currency",ResourceId = "IRR",Name = "riyal iranien",},

                new LocalizedName{Id = 193,LanguageId = "en",ResourceType = "Currency",ResourceId = "ISK",Name = "Icelandic Króna",},
                new LocalizedName{Id = 194,LanguageId = "ar",ResourceType = "Currency",ResourceId = "ISK",Name = "كرونة أيسلندية",},
                new LocalizedName{Id = 195,LanguageId = "fr",ResourceType = "Currency",ResourceId = "ISK",Name = "couronne islandaise",},

                new LocalizedName{Id = 196,LanguageId = "en",ResourceType = "Currency",ResourceId = "JMD",Name = "Jamaican Dollar",},
                new LocalizedName{Id = 197,LanguageId = "ar",ResourceType = "Currency",ResourceId = "JMD",Name = "دولار جامايكي",},
                new LocalizedName{Id = 198,LanguageId = "fr",ResourceType = "Currency",ResourceId = "JMD",Name = "dollar jamaïcain",},

                new LocalizedName{Id = 199,LanguageId = "en",ResourceType = "Currency",ResourceId = "JOD",Name = "Jordanian Dinar",},
                new LocalizedName{Id = 200,LanguageId = "ar",ResourceType = "Currency",ResourceId = "JOD",Name = "دينار أردني",},
                new LocalizedName{Id = 201,LanguageId = "fr",ResourceType = "Currency",ResourceId = "JOD",Name = "dinar jordanien",},

                new LocalizedName{Id = 202,LanguageId = "en",ResourceType = "Currency",ResourceId = "JPY",Name = "Japanese Yen",},
                new LocalizedName{Id = 203,LanguageId = "ar",ResourceType = "Currency",ResourceId = "JPY",Name = "ين ياباني",},
                new LocalizedName{Id = 204,LanguageId = "fr",ResourceType = "Currency",ResourceId = "JPY",Name = "yen japonais",},

                new LocalizedName{Id = 205,LanguageId = "en",ResourceType = "Currency",ResourceId = "KES",Name = "Kenyan Shilling",},
                new LocalizedName{Id = 206,LanguageId = "ar",ResourceType = "Currency",ResourceId = "KES",Name = "شلن كينيي",},
                new LocalizedName{Id = 207,LanguageId = "fr",ResourceType = "Currency",ResourceId = "KES",Name = "shilling kényan",},

                new LocalizedName{Id = 208,LanguageId = "en",ResourceType = "Currency",ResourceId = "KGS",Name = "Kyrgyz Som",},
                new LocalizedName{Id = 209,LanguageId = "ar",ResourceType = "Currency",ResourceId = "KGS",Name = "سوم قيرغستاني",},
                new LocalizedName{Id = 210,LanguageId = "fr",ResourceType = "Currency",ResourceId = "KGS",Name = "som kirghize",},

                new LocalizedName{Id = 211,LanguageId = "en",ResourceType = "Currency",ResourceId = "KHR",Name = "Cambodian Riel",},
                new LocalizedName{Id = 212,LanguageId = "ar",ResourceType = "Currency",ResourceId = "KHR",Name = "رييال كمبودي",},
                new LocalizedName{Id = 213,LanguageId = "fr",ResourceType = "Currency",ResourceId = "KHR",Name = "riel cambodgien",},

                new LocalizedName{Id = 214,LanguageId = "en",ResourceType = "Currency",ResourceId = "KMF",Name = "Comorian Franc",},
                new LocalizedName{Id = 215,LanguageId = "ar",ResourceType = "Currency",ResourceId = "KMF",Name = "فرنك جزر القمر",},
                new LocalizedName{Id = 216,LanguageId = "fr",ResourceType = "Currency",ResourceId = "KMF",Name = "franc comorien",},

                new LocalizedName{Id = 217,LanguageId = "en",ResourceType = "Currency",ResourceId = "KPW",Name = "North Korean Won",},
                new LocalizedName{Id = 218,LanguageId = "ar",ResourceType = "Currency",ResourceId = "KPW",Name = "وون كوريا الشمالية",},
                new LocalizedName{Id = 219,LanguageId = "fr",ResourceType = "Currency",ResourceId = "KPW",Name = "won nord-coréen",},

                new LocalizedName{Id = 220,LanguageId = "en",ResourceType = "Currency",ResourceId = "KRW",Name = "South Korean Won",},
                new LocalizedName{Id = 221,LanguageId = "ar",ResourceType = "Currency",ResourceId = "KRW",Name = "وون كوريا الجنوبية",},
                new LocalizedName{Id = 222,LanguageId = "fr",ResourceType = "Currency",ResourceId = "KRW",Name = "won sud-coréen",},

                new LocalizedName{Id = 223,LanguageId = "en",ResourceType = "Currency",ResourceId = "KWD",Name = "Kuwaiti Dinar",},
                new LocalizedName{Id = 224,LanguageId = "ar",ResourceType = "Currency",ResourceId = "KWD",Name = "دينار كويتي",},
                new LocalizedName{Id = 225,LanguageId = "fr",ResourceType = "Currency",ResourceId = "KWD",Name = "dinar koweïtien",},

                new LocalizedName{Id = 226,LanguageId = "en",ResourceType = "Currency",ResourceId = "KYD",Name = "Cayman Islands Dollar",},
                new LocalizedName{Id = 227,LanguageId = "ar",ResourceType = "Currency",ResourceId = "KYD",Name = "دولار جزر كيمن",},
                new LocalizedName{Id = 228,LanguageId = "fr",ResourceType = "Currency",ResourceId = "KYD",Name = "dollar des îles Caïmans",},

                new LocalizedName{Id = 229,LanguageId = "en",ResourceType = "Currency",ResourceId = "KZT",Name = "Kazakhstani Tenge",},
                new LocalizedName{Id = 230,LanguageId = "ar",ResourceType = "Currency",ResourceId = "KZT",Name = "تينغ كازاخستاني",},
                new LocalizedName{Id = 231,LanguageId = "fr",ResourceType = "Currency",ResourceId = "KZT",Name = "tenge kazakh",},

                new LocalizedName{Id = 232,LanguageId = "en",ResourceType = "Currency",ResourceId = "LAK",Name = "Laotian Kip",},
                new LocalizedName{Id = 233,LanguageId = "ar",ResourceType = "Currency",ResourceId = "LAK",Name = "كيب لاوسي",},
                new LocalizedName{Id = 234,LanguageId = "fr",ResourceType = "Currency",ResourceId = "LAK",Name = "kip laotien",},

                new LocalizedName{Id = 235,LanguageId = "en",ResourceType = "Currency",ResourceId = "LBP",Name = "Lebanese Pound",},
                new LocalizedName{Id = 236,LanguageId = "ar",ResourceType = "Currency",ResourceId = "LBP",Name = "جنيه لبناني",},
                new LocalizedName{Id = 237,LanguageId = "fr",ResourceType = "Currency",ResourceId = "LBP",Name = "livre libanaise",},

                new LocalizedName{Id = 238,LanguageId = "en",ResourceType = "Currency",ResourceId = "LKR",Name = "Sri Lankan Rupee",},
                new LocalizedName{Id = 239,LanguageId = "ar",ResourceType = "Currency",ResourceId = "LKR",Name = "روبية سريلانكية",},
                new LocalizedName{Id = 240,LanguageId = "fr",ResourceType = "Currency",ResourceId = "LKR",Name = "roupie srilankaise",},

                new LocalizedName{Id = 241,LanguageId = "en",ResourceType = "Currency",ResourceId = "LRD",Name = "Liberian Dollar",},
                new LocalizedName{Id = 242,LanguageId = "ar",ResourceType = "Currency",ResourceId = "LRD",Name = "دولار ليبيري",},
                new LocalizedName{Id = 243,LanguageId = "fr",ResourceType = "Currency",ResourceId = "LRD",Name = "dollar libérien",},

                new LocalizedName{Id = 244,LanguageId = "en",ResourceType = "Currency",ResourceId = "LSL",Name = "Lesotho Loti",},
                new LocalizedName{Id = 245,LanguageId = "ar",ResourceType = "Currency",ResourceId = "LSL",Name = "لوتي ليسوتو",},
                new LocalizedName{Id = 246,LanguageId = "fr",ResourceType = "Currency",ResourceId = "LSL",Name = "loti lesothan",},

                new LocalizedName{Id = 247,LanguageId = "en",ResourceType = "Currency",ResourceId = "LYD",Name = "Libyan Dinar",},
                new LocalizedName{Id = 248,LanguageId = "ar",ResourceType = "Currency",ResourceId = "LYD",Name = "دينار ليبي",},
                new LocalizedName{Id = 249,LanguageId = "fr",ResourceType = "Currency",ResourceId = "LYD",Name = "dinar libyen",},

                new LocalizedName{Id = 250,LanguageId = "en",ResourceType = "Currency",ResourceId = "MAD",Name = "Moroccan Dirham",},
                new LocalizedName{Id = 251,LanguageId = "ar",ResourceType = "Currency",ResourceId = "MAD",Name = "درهم مغربي",},
                new LocalizedName{Id = 252,LanguageId = "fr",ResourceType = "Currency",ResourceId = "MAD",Name = "dirham marocain",},

                new LocalizedName{Id = 253,LanguageId = "en",ResourceType = "Currency",ResourceId = "MDL",Name = "Moldovan Leu",},
                new LocalizedName{Id = 254,LanguageId = "ar",ResourceType = "Currency",ResourceId = "MDL",Name = "ليو مولدوفي",},
                new LocalizedName{Id = 255,LanguageId = "fr",ResourceType = "Currency",ResourceId = "MDL",Name = "leu moldave",},

                new LocalizedName{Id = 256,LanguageId = "en",ResourceType = "Currency",ResourceId = "MGA",Name = "Malagasy Ariary",},
                new LocalizedName{Id = 257,LanguageId = "ar",ResourceType = "Currency",ResourceId = "MGA",Name = "أرياري مدغشقر",},
                new LocalizedName{Id = 258,LanguageId = "fr",ResourceType = "Currency",ResourceId = "MGA",Name = "ariary malgache",},

                new LocalizedName{Id = 259,LanguageId = "en",ResourceType = "Currency",ResourceId = "MKD",Name = "Macedonian Denar",},
                new LocalizedName{Id = 260,LanguageId = "ar",ResourceType = "Currency",ResourceId = "MKD",Name = "دينار مقدوني",},
                new LocalizedName{Id = 261,LanguageId = "fr",ResourceType = "Currency",ResourceId = "MKD",Name = "denar macédonien",},

                new LocalizedName{Id = 262,LanguageId = "en",ResourceType = "Currency",ResourceId = "MMK",Name = "Myanmar Kyat",},
                new LocalizedName{Id = 263,LanguageId = "ar",ResourceType = "Currency",ResourceId = "MMK",Name = "كيات ميانمار",},
                new LocalizedName{Id = 264,LanguageId = "fr",ResourceType = "Currency",ResourceId = "MMK",Name = "kyat myanmarais",},

                new LocalizedName{Id = 265,LanguageId = "en",ResourceType = "Currency",ResourceId = "MNT",Name = "Mongolian Tugrik",},
                new LocalizedName{Id = 266,LanguageId = "ar",ResourceType = "Currency",ResourceId = "MNT",Name = "توغروغ منغولي",},
                new LocalizedName{Id = 267,LanguageId = "fr",ResourceType = "Currency",ResourceId = "MNT",Name = "tugrik mongol",},

                new LocalizedName{Id = 268,LanguageId = "en",ResourceType = "Currency",ResourceId = "MOP",Name = "Macanese Pataca",},
                new LocalizedName{Id = 269,LanguageId = "ar",ResourceType = "Currency",ResourceId = "MOP",Name = "باتاكا ماكاوي",},
                new LocalizedName{Id = 270,LanguageId = "fr",ResourceType = "Currency",ResourceId = "MOP",Name = "pataca macanaise",},

                new LocalizedName{Id = 271,LanguageId = "en",ResourceType = "Currency",ResourceId = "MRU",Name = "Mauritanian Ouguiya",},
                new LocalizedName{Id = 272,LanguageId = "ar",ResourceType = "Currency",ResourceId = "MRU",Name = "أوقية موريتانية",},
                new LocalizedName{Id = 273,LanguageId = "fr",ResourceType = "Currency",ResourceId = "MRU",Name = "ouguiya mauritanien",},

                new LocalizedName{Id = 274,LanguageId = "en",ResourceType = "Currency",ResourceId = "MUR",Name = "Mauritian Rupee",},
                new LocalizedName{Id = 275,LanguageId = "ar",ResourceType = "Currency",ResourceId = "MUR",Name = "روبية موريشيوسية",},
                new LocalizedName{Id = 276,LanguageId = "fr",ResourceType = "Currency",ResourceId = "MUR",Name = "roupie mauricienne",},

                new LocalizedName{Id = 277,LanguageId = "en",ResourceType = "Currency",ResourceId = "MVR",Name = "Maldivian Rufiyaa",},
                new LocalizedName{Id = 278,LanguageId = "ar",ResourceType = "Currency",ResourceId = "MVR",Name = "روفيه جزر المالديف",},
                new LocalizedName{Id = 279,LanguageId = "fr",ResourceType = "Currency",ResourceId = "MVR",Name = "rufiyaa maldivienne",},

                new LocalizedName{Id = 280,LanguageId = "en",ResourceType = "Currency",ResourceId = "MWK",Name = "Malawian Kwacha",},
                new LocalizedName{Id = 281,LanguageId = "ar",ResourceType = "Currency",ResourceId = "MWK",Name = "كواشا مالاوي",},
                new LocalizedName{Id = 282,LanguageId = "fr",ResourceType = "Currency",ResourceId = "MWK",Name = "kwacha malawite",},

                new LocalizedName{Id = 283,LanguageId = "en",ResourceType = "Currency",ResourceId = "MXN",Name = "Mexican Peso",},
                new LocalizedName{Id = 284,LanguageId = "ar",ResourceType = "Currency",ResourceId = "MXN",Name = "بيزو مكسيكي",},
                new LocalizedName{Id = 285,LanguageId = "fr",ResourceType = "Currency",ResourceId = "MXN",Name = "peso mexicain",},

                new LocalizedName{Id = 286,LanguageId = "en",ResourceType = "Currency",ResourceId = "MXV",Name = "Mexican Unidad de Inversión (UDI)",},
                new LocalizedName{Id = 287,LanguageId = "ar",ResourceType = "Currency",ResourceId = "MXV",Name = "وحدة الاستثمار المكسيكية (UDI)",},
                new LocalizedName{Id = 288,LanguageId = "fr",ResourceType = "Currency",ResourceId = "MXV",Name = "unité d’investissement mexicaine (UDI)",},

                new LocalizedName{Id = 289,LanguageId = "en",ResourceType = "Currency",ResourceId = "MYR",Name = "Malaysian Ringgit",},
                new LocalizedName{Id = 290,LanguageId = "ar",ResourceType = "Currency",ResourceId = "MYR",Name = "رينغيت ماليزي",},
                new LocalizedName{Id = 291,LanguageId = "fr",ResourceType = "Currency",ResourceId = "MYR",Name = "ringgit malais",},

                new LocalizedName{Id = 292,LanguageId = "en",ResourceType = "Currency",ResourceId = "MZN",Name = "Mozambican Metical",},
                new LocalizedName{Id = 293,LanguageId = "ar",ResourceType = "Currency",ResourceId = "MZN",Name = "متكال موزمبيقي",},
                new LocalizedName{Id = 294,LanguageId = "fr",ResourceType = "Currency",ResourceId = "MZN",Name = "metical mozambicain",},

                new LocalizedName{Id = 295,LanguageId = "en",ResourceType = "Currency",ResourceId = "NAD",Name = "Namibian Dollar",},
                new LocalizedName{Id = 296,LanguageId = "ar",ResourceType = "Currency",ResourceId = "NAD",Name = "دولار ناميبي",},
                new LocalizedName{Id = 297,LanguageId = "fr",ResourceType = "Currency",ResourceId = "NAD",Name = "dollar namibien",},

                new LocalizedName{Id = 298,LanguageId = "en",ResourceType = "Currency",ResourceId = "NGN",Name = "Nigerian Naira",},
                new LocalizedName{Id = 299,LanguageId = "ar",ResourceType = "Currency",ResourceId = "NGN",Name = "نايرا نيجيري",},
                new LocalizedName{Id = 300,LanguageId = "fr",ResourceType = "Currency",ResourceId = "NGN",Name = "naira nigérian",},

                new LocalizedName{Id = 301,LanguageId = "en",ResourceType = "Currency",ResourceId = "NIO",Name = "Nicaraguan Córdoba",},
                new LocalizedName{Id = 302,LanguageId = "ar",ResourceType = "Currency",ResourceId = "NIO",Name = "قرطبة نيكاراغوا",},
                new LocalizedName{Id = 303,LanguageId = "fr",ResourceType = "Currency",ResourceId = "NIO",Name = "córdoba oro nicaraguayen",},

                new LocalizedName{Id = 304,LanguageId = "en",ResourceType = "Currency",ResourceId = "NOK",Name = "Norwegian Krone",},
                new LocalizedName{Id = 305,LanguageId = "ar",ResourceType = "Currency",ResourceId = "NOK",Name = "كرونة نرويجية",},
                new LocalizedName{Id = 306,LanguageId = "fr",ResourceType = "Currency",ResourceId = "NOK",Name = "couronne norvégienne",},

                new LocalizedName{Id = 307,LanguageId = "en",ResourceType = "Currency",ResourceId = "NPR",Name = "Nepalese Rupee",},
                new LocalizedName{Id = 308,LanguageId = "ar",ResourceType = "Currency",ResourceId = "NPR",Name = "روبية نيبالي",},
                new LocalizedName{Id = 309,LanguageId = "fr",ResourceType = "Currency",ResourceId = "NPR",Name = "roupie népalaise",},

                new LocalizedName{Id = 310,LanguageId = "en",ResourceType = "Currency",ResourceId = "NZD",Name = "New Zealand Dollar",},
                new LocalizedName{Id = 311,LanguageId = "ar",ResourceType = "Currency",ResourceId = "NZD",Name = "دولار نيوزيلندي",},
                new LocalizedName{Id = 312,LanguageId = "fr",ResourceType = "Currency",ResourceId = "NZD",Name = "dollar néo-zélandais",},

                new LocalizedName{Id = 313,LanguageId = "en",ResourceType = "Currency",ResourceId = "OMR",Name = "Omani Rial",},
                new LocalizedName{Id = 314,LanguageId = "ar",ResourceType = "Currency",ResourceId = "OMR",Name = "ريال عماني",},
                new LocalizedName{Id = 315,LanguageId = "fr",ResourceType = "Currency",ResourceId = "OMR",Name = "riyal omanais",},

                new LocalizedName{Id = 316,LanguageId = "en",ResourceType = "Currency",ResourceId = "PAB",Name = "Panamanian Balboa",},
                new LocalizedName{Id = 317,LanguageId = "ar",ResourceType = "Currency",ResourceId = "PAB",Name = "بالبوا بنمي",},
                new LocalizedName{Id = 318,LanguageId = "fr",ResourceType = "Currency",ResourceId = "PAB",Name = "balboa panaméen",},

                new LocalizedName{Id = 319,LanguageId = "en",ResourceType = "Currency",ResourceId = "PEN",Name = "Peruvian Sol",},
                new LocalizedName{Id = 320,LanguageId = "ar",ResourceType = "Currency",ResourceId = "PEN",Name = "سول بيروفي",},
                new LocalizedName{Id = 321,LanguageId = "fr",ResourceType = "Currency",ResourceId = "PEN",Name = "sol péruvien",},

                new LocalizedName{Id = 322,LanguageId = "en",ResourceType = "Currency",ResourceId = "PGK",Name = "Papua New Guinean Kina",},
                new LocalizedName{Id = 323,LanguageId = "ar",ResourceType = "Currency",ResourceId = "PGK",Name = "كينا بابوا غينيا الجديدة",},
                new LocalizedName{Id = 324,LanguageId = "fr",ResourceType = "Currency",ResourceId = "PGK",Name = "kina papouan-néo-guinéen",},

                new LocalizedName{Id = 325,LanguageId = "en",ResourceType = "Currency",ResourceId = "PHP",Name = "Philippine Peso",},
                new LocalizedName{Id = 326,LanguageId = "ar",ResourceType = "Currency",ResourceId = "PHP",Name = "بيزو فلبيني",},
                new LocalizedName{Id = 327,LanguageId = "fr",ResourceType = "Currency",ResourceId = "PHP",Name = "peso philippin",},

                new LocalizedName{Id = 328,LanguageId = "en",ResourceType = "Currency",ResourceId = "PKR",Name = "Pakistani Rupee",},
                new LocalizedName{Id = 329,LanguageId = "ar",ResourceType = "Currency",ResourceId = "PKR",Name = "روبية باكستاني",},
                new LocalizedName{Id = 330,LanguageId = "fr",ResourceType = "Currency",ResourceId = "PKR",Name = "roupie pakistanaise",},

                new LocalizedName{Id = 331,LanguageId = "en",ResourceType = "Currency",ResourceId = "PLN",Name = "Polish Zloty",},
                new LocalizedName{Id = 332,LanguageId = "ar",ResourceType = "Currency",ResourceId = "PLN",Name = "زلوتي بولندي",},
                new LocalizedName{Id = 333,LanguageId = "fr",ResourceType = "Currency",ResourceId = "PLN",Name = "zloty polonais",},

                new LocalizedName{Id = 334,LanguageId = "en",ResourceType = "Currency",ResourceId = "PYG",Name = "Paraguayan Guarani",},
                new LocalizedName{Id = 335,LanguageId = "ar",ResourceType = "Currency",ResourceId = "PYG",Name = "غواراني باراغواي",},
                new LocalizedName{Id = 336,LanguageId = "fr",ResourceType = "Currency",ResourceId = "PYG",Name = "guaraní paraguayen",},

                new LocalizedName{Id = 337,LanguageId = "en",ResourceType = "Currency",ResourceId = "QAR",Name = "Qatari Riyal",},
                new LocalizedName{Id = 338,LanguageId = "ar",ResourceType = "Currency",ResourceId = "QAR",Name = "ريال قطري",},
                new LocalizedName{Id = 339,LanguageId = "fr",ResourceType = "Currency",ResourceId = "QAR",Name = "riyal qatari",},

                new LocalizedName{Id = 340,LanguageId = "en",ResourceType = "Currency",ResourceId = "RON",Name = "Romanian Leu",},
                new LocalizedName{Id = 341,LanguageId = "ar",ResourceType = "Currency",ResourceId = "RON",Name = "ليو روماني",},
                new LocalizedName{Id = 342,LanguageId = "fr",ResourceType = "Currency",ResourceId = "RON",Name = "leu roumain",},

                new LocalizedName{Id = 343,LanguageId = "en",ResourceType = "Currency",ResourceId = "RSD",Name = "Serbian Dinar",},
                new LocalizedName{Id = 344,LanguageId = "ar",ResourceType = "Currency",ResourceId = "RSD",Name = "دينار صربي",},
                new LocalizedName{Id = 345,LanguageId = "fr",ResourceType = "Currency",ResourceId = "RSD",Name = "dinar serbe",},

                new LocalizedName{Id = 346,LanguageId = "en",ResourceType = "Currency",ResourceId = "RUB",Name = "Russian Ruble",},
                new LocalizedName{Id = 347,LanguageId = "ar",ResourceType = "Currency",ResourceId = "RUB",Name = "روبل روسي",},
                new LocalizedName{Id = 348,LanguageId = "fr",ResourceType = "Currency",ResourceId = "RUB",Name = "rouble russe",},

                new LocalizedName{Id = 349,LanguageId = "en",ResourceType = "Currency",ResourceId = "RWF",Name = "Rwandan Franc",},
                new LocalizedName{Id = 350,LanguageId = "ar",ResourceType = "Currency",ResourceId = "RWF",Name = "فرنك رواندي",},
                new LocalizedName{Id = 351,LanguageId = "fr",ResourceType = "Currency",ResourceId = "RWF",Name = "franc rwandais",},

                new LocalizedName{Id = 352,LanguageId = "en",ResourceType = "Currency",ResourceId = "SAR",Name = "Saudi Riyal",},
                new LocalizedName{Id = 353,LanguageId = "ar",ResourceType = "Currency",ResourceId = "SAR",Name = "ريال سعودي",},
                new LocalizedName{Id = 354,LanguageId = "fr",ResourceType = "Currency",ResourceId = "SAR",Name = "riyal saoudien",},

                new LocalizedName{Id = 355,LanguageId = "en",ResourceType = "Currency",ResourceId = "SBD",Name = "Solomon Islands Dollar",},
                new LocalizedName{Id = 356,LanguageId = "ar",ResourceType = "Currency",ResourceId = "SBD",Name = "دولار جزر سليمان",},
                new LocalizedName{Id = 357,LanguageId = "fr",ResourceType = "Currency",ResourceId = "SBD",Name = "dollar des îles Salomon",},

                new LocalizedName{Id = 358,LanguageId = "en",ResourceType = "Currency",ResourceId = "SCR",Name = "Seychellois Rupee",},
                new LocalizedName{Id = 359,LanguageId = "ar",ResourceType = "Currency",ResourceId = "SCR",Name = "روبية سيشيلية",},
                new LocalizedName{Id = 360,LanguageId = "fr",ResourceType = "Currency",ResourceId = "SCR",Name = "roupie des Seychelles",},

                new LocalizedName{Id = 361,LanguageId = "en",ResourceType = "Currency",ResourceId = "SDG",Name = "Sudanese Pound",},
                new LocalizedName{Id = 362,LanguageId = "ar",ResourceType = "Currency",ResourceId = "SDG",Name = "جنيه سوداني",},
                new LocalizedName{Id = 363,LanguageId = "fr",ResourceType = "Currency",ResourceId = "SDG",Name = "livre soudanaise",},

                new LocalizedName{Id = 364,LanguageId = "en",ResourceType = "Currency",ResourceId = "SEK",Name = "Swedish Krona",},
                new LocalizedName{Id = 365,LanguageId = "ar",ResourceType = "Currency",ResourceId = "SEK",Name = "كرونة سويدية",},
                new LocalizedName{Id = 366,LanguageId = "fr",ResourceType = "Currency",ResourceId = "SEK",Name = "couronne suédoise",},

                new LocalizedName{Id = 367,LanguageId = "en",ResourceType = "Currency",ResourceId = "SGD",Name = "Singapore Dollar",},
                new LocalizedName{Id = 368,LanguageId = "ar",ResourceType = "Currency",ResourceId = "SGD",Name = "دولار سنغافوري",},
                new LocalizedName{Id = 369,LanguageId = "fr",ResourceType = "Currency",ResourceId = "SGD",Name = "dollar de Singapour",},

                new LocalizedName{Id = 370,LanguageId = "en",ResourceType = "Currency",ResourceId = "SHP",Name = "St. Helena Pound",},
                new LocalizedName{Id = 371,LanguageId = "ar",ResourceType = "Currency",ResourceId = "SHP",Name = "جنيه سانت هيلين",},
                new LocalizedName{Id = 372,LanguageId = "fr",ResourceType = "Currency",ResourceId = "SHP",Name = "livre de Sainte-Hélène",},

                new LocalizedName{Id = 373,LanguageId = "en",ResourceType = "Currency",ResourceId = "SLE",Name = "Sierra Leonean Leone",},
                new LocalizedName{Id = 374,LanguageId = "ar",ResourceType = "Currency",ResourceId = "SLE",Name = "ليون سيراليوني",},
                new LocalizedName{Id = 375,LanguageId = "fr",ResourceType = "Currency",ResourceId = "SLE",Name = "leone sierra-léonais",},

                new LocalizedName{Id = 376,LanguageId = "en",ResourceType = "Currency",ResourceId = "SOS",Name = "Somali Shilling",},
                new LocalizedName{Id = 377,LanguageId = "ar",ResourceType = "Currency",ResourceId = "SOS",Name = "شلن صومالي",},
                new LocalizedName{Id = 378,LanguageId = "fr",ResourceType = "Currency",ResourceId = "SOS",Name = "shilling somalien",},

                new LocalizedName{Id = 379,LanguageId = "en",ResourceType = "Currency",ResourceId = "SRD",Name = "Surinamese Dollar",},
                new LocalizedName{Id = 380,LanguageId = "ar",ResourceType = "Currency",ResourceId = "SRD",Name = "دولار سورينامي",},
                new LocalizedName{Id = 381,LanguageId = "fr",ResourceType = "Currency",ResourceId = "SRD",Name = "dollar surinamais",},

                new LocalizedName{Id = 382,LanguageId = "en",ResourceType = "Currency",ResourceId = "SSP",Name = "South Sudanese Pound",},
                new LocalizedName{Id = 383,LanguageId = "ar",ResourceType = "Currency",ResourceId = "SSP",Name = "جنيه جنوب السودان",},
                new LocalizedName{Id = 384,LanguageId = "fr",ResourceType = "Currency",ResourceId = "SSP",Name = "livre sud-soudanaise",},

                new LocalizedName{Id = 385,LanguageId = "en",ResourceType = "Currency",ResourceId = "STN",Name = "São Tomé & Príncipe Dobra",},
                new LocalizedName{Id = 386,LanguageId = "ar",ResourceType = "Currency",ResourceId = "STN",Name = "دوبرا ساو تومي وبرينسيبي",},
                new LocalizedName{Id = 387,LanguageId = "fr",ResourceType = "Currency",ResourceId = "STN",Name = "dobra santoméen",},

                new LocalizedName{Id = 388,LanguageId = "en",ResourceType = "Currency",ResourceId = "SVC",Name = "Salvadoran Colón",},
                new LocalizedName{Id = 389,LanguageId = "ar",ResourceType = "Currency",ResourceId = "SVC",Name = "كولون سلفادوري",},
                new LocalizedName{Id = 390,LanguageId = "fr",ResourceType = "Currency",ResourceId = "SVC",Name = "colón salvadorien",},

                new LocalizedName{Id = 391,LanguageId = "en",ResourceType = "Currency",ResourceId = "SYP",Name = "Syrian Pound",},
                new LocalizedName{Id = 392,LanguageId = "ar",ResourceType = "Currency",ResourceId = "SYP",Name = "ليرة سورية",},
                new LocalizedName{Id = 393,LanguageId = "fr",ResourceType = "Currency",ResourceId = "SYP",Name = "livre syrienne",},

                new LocalizedName{Id = 394,LanguageId = "en",ResourceType = "Currency",ResourceId = "SZL",Name = "Swazi Lilangeni",},
                new LocalizedName{Id = 395,LanguageId = "ar",ResourceType = "Currency",ResourceId = "SZL",Name = "ليلانجيني سوازيلندي",},
                new LocalizedName{Id = 396,LanguageId = "fr",ResourceType = "Currency",ResourceId = "SZL",Name = "lilangeni swazi",},

                new LocalizedName{Id = 397,LanguageId = "en",ResourceType = "Currency",ResourceId = "THB",Name = "Thai Baht",},
                new LocalizedName{Id = 398,LanguageId = "ar",ResourceType = "Currency",ResourceId = "THB",Name = "باخت تايلاندي",},
                new LocalizedName{Id = 399,LanguageId = "fr",ResourceType = "Currency",ResourceId = "THB",Name = "baht thaïlandais",},

                new LocalizedName{Id = 400,LanguageId = "en",ResourceType = "Currency",ResourceId = "TJS",Name = "Tajikistani Somoni",},
                new LocalizedName{Id = 401,LanguageId = "ar",ResourceType = "Currency",ResourceId = "TJS",Name = "سوموني طاجيكستاني",},
                new LocalizedName{Id = 402,LanguageId = "fr",ResourceType = "Currency",ResourceId = "TJS",Name = "somoni tadjik",},

                new LocalizedName{Id = 403,LanguageId = "en",ResourceType = "Currency",ResourceId = "TMT",Name = "Turkmenistani Manat",},
                new LocalizedName{Id = 404,LanguageId = "ar",ResourceType = "Currency",ResourceId = "TMT",Name = "مانات تركمانستان",},
                new LocalizedName{Id = 405,LanguageId = "fr",ResourceType = "Currency",ResourceId = "TMT",Name = "nouveau manat turkmène",},

                new LocalizedName{Id = 406,LanguageId = "en",ResourceType = "Currency",ResourceId = "TND",Name = "Tunisian Dinar",},
                new LocalizedName{Id = 407,LanguageId = "ar",ResourceType = "Currency",ResourceId = "TND",Name = "دينار تونسي",},
                new LocalizedName{Id = 408,LanguageId = "fr",ResourceType = "Currency",ResourceId = "TND",Name = "dinar tunisien",},

                new LocalizedName{Id = 409,LanguageId = "en",ResourceType = "Currency",ResourceId = "TOP",Name = "Tongan Paʻanga",},
                new LocalizedName{Id = 410,LanguageId = "ar",ResourceType = "Currency",ResourceId = "TOP",Name = "بانغا تونغا",},
                new LocalizedName{Id = 411,LanguageId = "fr",ResourceType = "Currency",ResourceId = "TOP",Name = "pa’anga tongan",},

                new LocalizedName{Id = 412,LanguageId = "en",ResourceType = "Currency",ResourceId = "TRY",Name = "Turkish Lira",},
                new LocalizedName{Id = 413,LanguageId = "ar",ResourceType = "Currency",ResourceId = "TRY",Name = "ليرة تركية",},
                new LocalizedName{Id = 414,LanguageId = "fr",ResourceType = "Currency",ResourceId = "TRY",Name = "livre turque",},

                new LocalizedName{Id = 415,LanguageId = "en",ResourceType = "Currency",ResourceId = "TTD",Name = "Trinidad & Tobago Dollar",},
                new LocalizedName{Id = 416,LanguageId = "ar",ResourceType = "Currency",ResourceId = "TTD",Name = "دولار ترينداد وتوباغو",},
                new LocalizedName{Id = 417,LanguageId = "fr",ResourceType = "Currency",ResourceId = "TTD",Name = "dollar de Trinité-et-Tobago",},

                new LocalizedName{Id = 418,LanguageId = "en",ResourceType = "Currency",ResourceId = "TWD",Name = "New Taiwan Dollar",},
                new LocalizedName{Id = 419,LanguageId = "ar",ResourceType = "Currency",ResourceId = "TWD",Name = "دولار تايواني",},
                new LocalizedName{Id = 420,LanguageId = "fr",ResourceType = "Currency",ResourceId = "TWD",Name = "nouveau dollar taïwanais",},

                new LocalizedName{Id = 421,LanguageId = "en",ResourceType = "Currency",ResourceId = "TZS",Name = "Tanzanian Shilling",},
                new LocalizedName{Id = 422,LanguageId = "ar",ResourceType = "Currency",ResourceId = "TZS",Name = "شلن تنزاني",},
                new LocalizedName{Id = 423,LanguageId = "fr",ResourceType = "Currency",ResourceId = "TZS",Name = "shilling tanzanien",},

                new LocalizedName{Id = 424,LanguageId = "en",ResourceType = "Currency",ResourceId = "UAH",Name = "Ukrainian Hryvnia",},
                new LocalizedName{Id = 425,LanguageId = "ar",ResourceType = "Currency",ResourceId = "UAH",Name = "هريفنيا أوكراني",},
                new LocalizedName{Id = 426,LanguageId = "fr",ResourceType = "Currency",ResourceId = "UAH",Name = "hryvnia ukrainienne",},

                new LocalizedName{Id = 427,LanguageId = "en",ResourceType = "Currency",ResourceId = "UGX",Name = "Ugandan Shilling",},
                new LocalizedName{Id = 428,LanguageId = "ar",ResourceType = "Currency",ResourceId = "UGX",Name = "شلن أوغندي",},
                new LocalizedName{Id = 429,LanguageId = "fr",ResourceType = "Currency",ResourceId = "UGX",Name = "shilling ougandais",},

                new LocalizedName{Id = 430,LanguageId = "en",ResourceType = "Currency",ResourceId = "USD",Name = "US Dollar",},
                new LocalizedName{Id = 431,LanguageId = "ar",ResourceType = "Currency",ResourceId = "USD",Name = "دولار أمريكي",},
                new LocalizedName{Id = 432,LanguageId = "fr",ResourceType = "Currency",ResourceId = "USD",Name = "dollar des États-Unis",},

                new LocalizedName{Id = 433,LanguageId = "en",ResourceType = "Currency",ResourceId = "USN",Name = "US Dollar (Next day)",},
                new LocalizedName{Id = 434,LanguageId = "ar",ResourceType = "Currency",ResourceId = "USN",Name = "دولار أمريكي (اليوم التالي)",},
                new LocalizedName{Id = 435,LanguageId = "fr",ResourceType = "Currency",ResourceId = "USN",Name = "dollar américain (jour suivant)",},

                new LocalizedName{Id = 436,LanguageId = "en",ResourceType = "Currency",ResourceId = "UYI",Name = "Uruguay Peso en Unidades Indexadas (UI)",},
                new LocalizedName{Id = 437,LanguageId = "ar",ResourceType = "Currency",ResourceId = "UYI",Name = "بيزو أوروغواي بوحدات مفهرسة (UI)",},
                new LocalizedName{Id = 438,LanguageId = "fr",ResourceType = "Currency",ResourceId = "UYI",Name = "peso uruguayen en unités indexées (UI)",},

                new LocalizedName{Id = 439,LanguageId = "en",ResourceType = "Currency",ResourceId = "UYU",Name = "Uruguayan Peso",},
                new LocalizedName{Id = 440,LanguageId = "ar",ResourceType = "Currency",ResourceId = "UYU",Name = "بيزو اوروغواي",},
                new LocalizedName{Id = 441,LanguageId = "fr",ResourceType = "Currency",ResourceId = "UYU",Name = "peso uruguayen",},

                new LocalizedName{Id = 442,LanguageId = "en",ResourceType = "Currency",ResourceId = "UYW",Name = "Unidad Previsional",},
                new LocalizedName{Id = 443,LanguageId = "ar",ResourceType = "Currency",ResourceId = "UYW",Name = "وحدة التقاعد الأوروغوايانية",},
                new LocalizedName{Id = 444,LanguageId = "fr",ResourceType = "Currency",ResourceId = "UYW",Name = "unité prévisionnelle uruguayenne",},

                new LocalizedName{Id = 445,LanguageId = "en",ResourceType = "Currency",ResourceId = "UZS",Name = "Uzbekistani Som",},
                new LocalizedName{Id = 446,LanguageId = "ar",ResourceType = "Currency",ResourceId = "UZS",Name = "سوم أوزبكستاني",},
                new LocalizedName{Id = 447,LanguageId = "fr",ResourceType = "Currency",ResourceId = "UZS",Name = "sum ouzbek",},

                new LocalizedName{Id = 448,LanguageId = "en",ResourceType = "Currency",ResourceId = "VED",Name = "Venezuelan Digital Bolívar",},
                new LocalizedName{Id = 449,LanguageId = "ar",ResourceType = "Currency",ResourceId = "VED",Name = "بوليفار رقمي فنزويلي",},
                new LocalizedName{Id = 450,LanguageId = "fr",ResourceType = "Currency",ResourceId = "VED",Name = "bolívar numérique vénézuélien",},

                new LocalizedName{Id = 451,LanguageId = "en",ResourceType = "Currency",ResourceId = "VES",Name = "Venezuelan Bolívar",},
                new LocalizedName{Id = 452,LanguageId = "ar",ResourceType = "Currency",ResourceId = "VES",Name = "بوليفار فنزويلي",},
                new LocalizedName{Id = 453,LanguageId = "fr",ResourceType = "Currency",ResourceId = "VES",Name = "bolivar vénézuélien",},

                new LocalizedName{Id = 454,LanguageId = "en",ResourceType = "Currency",ResourceId = "VND",Name = "Vietnamese Dong",},
                new LocalizedName{Id = 455,LanguageId = "ar",ResourceType = "Currency",ResourceId = "VND",Name = "دونج فيتنامي",},
                new LocalizedName{Id = 456,LanguageId = "fr",ResourceType = "Currency",ResourceId = "VND",Name = "dông vietnamien",},

                new LocalizedName{Id = 457,LanguageId = "en",ResourceType = "Currency",ResourceId = "VUV",Name = "Vanuatu Vatu",},
                new LocalizedName{Id = 458,LanguageId = "ar",ResourceType = "Currency",ResourceId = "VUV",Name = "فاتو فانواتو",},
                new LocalizedName{Id = 459,LanguageId = "fr",ResourceType = "Currency",ResourceId = "VUV",Name = "vatu vanuatuan",},

                new LocalizedName{Id = 460,LanguageId = "en",ResourceType = "Currency",ResourceId = "WST",Name = "Samoan Tala",},
                new LocalizedName{Id = 461,LanguageId = "ar",ResourceType = "Currency",ResourceId = "WST",Name = "تالا ساموا",},
                new LocalizedName{Id = 462,LanguageId = "fr",ResourceType = "Currency",ResourceId = "WST",Name = "tala samoan",},

                new LocalizedName{Id = 463,LanguageId = "en",ResourceType = "Currency",ResourceId = "XAD",Name = "Arab Accounting Dinar",},
                new LocalizedName{Id = 464,LanguageId = "ar",ResourceType = "Currency",ResourceId = "XAD",Name = "دينار محاسبي عربي",},
                new LocalizedName{Id = 465,LanguageId = "fr",ResourceType = "Currency",ResourceId = "XAD",Name = "dinar comptable arabe",},

                new LocalizedName{Id = 466,LanguageId = "en",ResourceType = "Currency",ResourceId = "XAF",Name = "Central African CFA Franc",},
                new LocalizedName{Id = 467,LanguageId = "ar",ResourceType = "Currency",ResourceId = "XAF",Name = "فرنك وسط أفريقي",},
                new LocalizedName{Id = 468,LanguageId = "fr",ResourceType = "Currency",ResourceId = "XAF",Name = "franc CFA (BEAC)",},

                new LocalizedName{Id = 469,LanguageId = "en",ResourceType = "Currency",ResourceId = "XAG",Name = "Silver (one troy ounce)",},
                new LocalizedName{Id = 470,LanguageId = "ar",ResourceType = "Currency",ResourceId = "XAG",Name = "فضة (أونصة تروي واحدة)",},
                new LocalizedName{Id = 471,LanguageId = "fr",ResourceType = "Currency",ResourceId = "XAG",Name = "argent (une once troy)",},

                new LocalizedName{Id = 472,LanguageId = "en",ResourceType = "Currency",ResourceId = "XAU",Name = "Gold (one troy ounce)",},
                new LocalizedName{Id = 473,LanguageId = "ar",ResourceType = "Currency",ResourceId = "XAU",Name = "ذهب (أونصة تروي واحدة)",},
                new LocalizedName{Id = 474,LanguageId = "fr",ResourceType = "Currency",ResourceId = "XAU",Name = "or (une once troy)",},

                new LocalizedName{Id = 475,LanguageId = "en",ResourceType = "Currency",ResourceId = "XBA",Name = "European Composite Unit (EURCO)",},
                new LocalizedName{Id = 476,LanguageId = "ar",ResourceType = "Currency",ResourceId = "XBA",Name = "وحدة أوروبية مركبة (EURCO)",},
                new LocalizedName{Id = 477,LanguageId = "fr",ResourceType = "Currency",ResourceId = "XBA",Name = "unité composite européenne (EURCO)",},

                new LocalizedName{Id = 478,LanguageId = "en",ResourceType = "Currency",ResourceId = "XBB",Name = "European Monetary Unit (EMU-6)",},
                new LocalizedName{Id = 479,LanguageId = "ar",ResourceType = "Currency",ResourceId = "XBB",Name = "وحدة نقدية أوروبية (EMU-6)",},
                new LocalizedName{Id = 480,LanguageId = "fr",ResourceType = "Currency",ResourceId = "XBB",Name = "unité monétaire européenne (EMU-6)",},

                new LocalizedName{Id = 481,LanguageId = "en",ResourceType = "Currency",ResourceId = "XBC",Name = "European Unit of Account 9 (EUA-9)",},
                new LocalizedName{Id = 482,LanguageId = "ar",ResourceType = "Currency",ResourceId = "XBC",Name = "وحدة الحساب الأوروبية 9 (EUA-9)",},
                new LocalizedName{Id = 483,LanguageId = "fr",ResourceType = "Currency",ResourceId = "XBC",Name = "unité de compte européenne 9 (EUA-9)",},

                new LocalizedName{Id = 484,LanguageId = "en",ResourceType = "Currency",ResourceId = "XBD",Name = "European Unit of Account 17 (EUA-17)",},
                new LocalizedName{Id = 485,LanguageId = "ar",ResourceType = "Currency",ResourceId = "XBD",Name = "وحدة الحساب الأوروبية 17 (EUA-17)",},
                new LocalizedName{Id = 486,LanguageId = "fr",ResourceType = "Currency",ResourceId = "XBD",Name = "unité de compte européenne 17 (EUA-17)",},

                new LocalizedName{Id = 487,LanguageId = "en",ResourceType = "Currency",ResourceId = "XCD",Name = "East Caribbean Dollar",},
                new LocalizedName{Id = 488,LanguageId = "ar",ResourceType = "Currency",ResourceId = "XCD",Name = "دولار شرق الكاريبي",},
                new LocalizedName{Id = 489,LanguageId = "fr",ResourceType = "Currency",ResourceId = "XCD",Name = "dollar des Caraïbes orientales",},

                new LocalizedName{Id = 490,LanguageId = "en",ResourceType = "Currency",ResourceId = "XCG",Name = "Caribbean Guilder",},
                new LocalizedName{Id = 491,LanguageId = "ar",ResourceType = "Currency",ResourceId = "XCG",Name = "غيلدر كاريبي",},
                new LocalizedName{Id = 492,LanguageId = "fr",ResourceType = "Currency",ResourceId = "XCG",Name = "florin caribéen",},

                new LocalizedName{Id = 493,LanguageId = "en",ResourceType = "Currency",ResourceId = "XDR",Name = "Special Drawing Rights",},
                new LocalizedName{Id = 494,LanguageId = "ar",ResourceType = "Currency",ResourceId = "XDR",Name = "حقوق السحب الخاصة",},
                new LocalizedName{Id = 495,LanguageId = "fr",ResourceType = "Currency",ResourceId = "XDR",Name = "droit de tirage spécial",},

                new LocalizedName{Id = 496,LanguageId = "en",ResourceType = "Currency",ResourceId = "XOF",Name = "West African CFA Franc",},
                new LocalizedName{Id = 497,LanguageId = "ar",ResourceType = "Currency",ResourceId = "XOF",Name = "فرنك غرب أفريقي",},
                new LocalizedName{Id = 498,LanguageId = "fr",ResourceType = "Currency",ResourceId = "XOF",Name = "franc CFA (BCEAO)",},

                new LocalizedName{Id = 499,LanguageId = "en",ResourceType = "Currency",ResourceId = "XPD",Name = "Palladium (one troy ounce)",},
                new LocalizedName{Id = 500,LanguageId = "ar",ResourceType = "Currency",ResourceId = "XPD",Name = "بلاديوم (أونصة تروي واحدة)",},
                new LocalizedName{Id = 501,LanguageId = "fr",ResourceType = "Currency",ResourceId = "XPD",Name = "palladium (une once troy)",},

                new LocalizedName{Id = 502,LanguageId = "en",ResourceType = "Currency",ResourceId = "XPF",Name = "CFP Franc",},
                new LocalizedName{Id = 503,LanguageId = "ar",ResourceType = "Currency",ResourceId = "XPF",Name = "فرنك سي إف بي",},
                new LocalizedName{Id = 504,LanguageId = "fr",ResourceType = "Currency",ResourceId = "XPF",Name = "franc CFP",},

                new LocalizedName{Id = 505,LanguageId = "en",ResourceType = "Currency",ResourceId = "XPT",Name = "Platinum (one troy ounce)",},
                new LocalizedName{Id = 506,LanguageId = "ar",ResourceType = "Currency",ResourceId = "XPT",Name = "بلاتين (أونصة تروي واحدة)",},
                new LocalizedName{Id = 507,LanguageId = "fr",ResourceType = "Currency",ResourceId = "XPT",Name = "platine (une once troy)",},

                new LocalizedName{Id = 508,LanguageId = "en",ResourceType = "Currency",ResourceId = "XSU",Name = "Sucre",},
                new LocalizedName{Id = 509,LanguageId = "ar",ResourceType = "Currency",ResourceId = "XSU",Name = "سوكر",},
                new LocalizedName{Id = 510,LanguageId = "fr",ResourceType = "Currency",ResourceId = "XSU",Name = "sucre",},

                new LocalizedName{Id = 511,LanguageId = "en",ResourceType = "Currency",ResourceId = "XTS",Name = "Code reserved for testing purposes",},
                new LocalizedName{Id = 512,LanguageId = "ar",ResourceType = "Currency",ResourceId = "XTS",Name = "رمز مخصص لأغراض الاختبار",},
                new LocalizedName{Id = 513,LanguageId = "fr",ResourceType = "Currency",ResourceId = "XTS",Name = "code réservé à des fins de test",},

                new LocalizedName{Id = 514,LanguageId = "en",ResourceType = "Currency",ResourceId = "XUA",Name = "ADB Unit of Account",},
                new LocalizedName{Id = 515,LanguageId = "ar",ResourceType = "Currency",ResourceId = "XUA",Name = "وحدة حساب مجموعة بنك التنمية الأفريقي",},
                new LocalizedName{Id = 516,LanguageId = "fr",ResourceType = "Currency",ResourceId = "XUA",Name = "unité de compte du groupe de la Banque africaine de développement",},

                new LocalizedName{Id = 517,LanguageId = "en",ResourceType = "Currency",ResourceId = "XXX",Name = "No currency",},
                new LocalizedName{Id = 518,LanguageId = "ar",ResourceType = "Currency",ResourceId = "XXX",Name = "لا توجد عملة",},
                new LocalizedName{Id = 519,LanguageId = "fr",ResourceType = "Currency",ResourceId = "XXX",Name = "aucune devise",},

                new LocalizedName{Id = 520,LanguageId = "en",ResourceType = "Currency",ResourceId = "YER",Name = "Yemeni Rial",},
                new LocalizedName{Id = 521,LanguageId = "ar",ResourceType = "Currency",ResourceId = "YER",Name = "ريال يمني",},
                new LocalizedName{Id = 522,LanguageId = "fr",ResourceType = "Currency",ResourceId = "YER",Name = "riyal yéménite",},

                new LocalizedName{Id = 523,LanguageId = "en",ResourceType = "Currency",ResourceId = "ZAR",Name = "South African Rand",},
                new LocalizedName{Id = 524,LanguageId = "ar",ResourceType = "Currency",ResourceId = "ZAR",Name = "راند جنوب أفريقيا",},
                new LocalizedName{Id = 525,LanguageId = "fr",ResourceType = "Currency",ResourceId = "ZAR",Name = "rand sud-africain",},

                new LocalizedName{Id = 526,LanguageId = "en",ResourceType = "Currency",ResourceId = "ZMW",Name = "Zambian Kwacha",},
                new LocalizedName{Id = 527,LanguageId = "ar",ResourceType = "Currency",ResourceId = "ZMW",Name = "كواشا زامبي",},
                new LocalizedName{Id = 528,LanguageId = "fr",ResourceType = "Currency",ResourceId = "ZMW",Name = "kwacha zambien",},

                new LocalizedName{Id = 529,LanguageId = "en",ResourceType = "Currency",ResourceId = "ZWG",Name = "Zimbabwe Gold",},
                new LocalizedName{Id = 530,LanguageId = "ar",ResourceType = "Currency",ResourceId = "ZWG",Name = "ذهب زيمبابوي",},
                new LocalizedName{Id = 531,LanguageId = "fr",ResourceType = "Currency",ResourceId = "ZWG",Name = "or zimbabwéen",},

                /* *************** COUNTRY NAMES *************** */
                new LocalizedName{Id = 532,LanguageId = "en",ResourceType = "Country",ResourceId = "AD",Name = "Andorra",},
                new LocalizedName{Id = 533,LanguageId = "ar",ResourceType = "Country",ResourceId = "AD",Name = "أندورا",},
                new LocalizedName{Id = 534,LanguageId = "fr",ResourceType = "Country",ResourceId = "AD",Name = "Andorre",},

                new LocalizedName{Id = 535,LanguageId = "en",ResourceType = "Country",ResourceId = "AE",Name = "United Arab Emirates",},
                new LocalizedName{Id = 536,LanguageId = "ar",ResourceType = "Country",ResourceId = "AE",Name = "الإمارات العربية المتحدة",},
                new LocalizedName{Id = 537,LanguageId = "fr",ResourceType = "Country",ResourceId = "AE",Name = "Émirats arabes unis",},

                new LocalizedName{Id = 538,LanguageId = "en",ResourceType = "Country",ResourceId = "AF",Name = "Afghanistan",},
                new LocalizedName{Id = 539,LanguageId = "ar",ResourceType = "Country",ResourceId = "AF",Name = "أفغانستان",},
                new LocalizedName{Id = 540,LanguageId = "fr",ResourceType = "Country",ResourceId = "AF",Name = "Afghanistan",},

                new LocalizedName{Id = 541,LanguageId = "en",ResourceType = "Country",ResourceId = "AG",Name = "Antigua & Barbuda",},
                new LocalizedName{Id = 542,LanguageId = "ar",ResourceType = "Country",ResourceId = "AG",Name = "أنتيغوا وبربودا",},
                new LocalizedName{Id = 543,LanguageId = "fr",ResourceType = "Country",ResourceId = "AG",Name = "Antigua-et-Barbuda",},

                new LocalizedName{Id = 544,LanguageId = "en",ResourceType = "Country",ResourceId = "AI",Name = "Anguilla",},
                new LocalizedName{Id = 545,LanguageId = "ar",ResourceType = "Country",ResourceId = "AI",Name = "أنغويلا",},
                new LocalizedName{Id = 546,LanguageId = "fr",ResourceType = "Country",ResourceId = "AI",Name = "Anguilla",},

                new LocalizedName{Id = 547,LanguageId = "en",ResourceType = "Country",ResourceId = "AL",Name = "Albania",},
                new LocalizedName{Id = 548,LanguageId = "ar",ResourceType = "Country",ResourceId = "AL",Name = "ألبانيا",},
                new LocalizedName{Id = 549,LanguageId = "fr",ResourceType = "Country",ResourceId = "AL",Name = "Albanie",},

                new LocalizedName{Id = 550,LanguageId = "en",ResourceType = "Country",ResourceId = "AM",Name = "Armenia",},
                new LocalizedName{Id = 551,LanguageId = "ar",ResourceType = "Country",ResourceId = "AM",Name = "أرمينيا",},
                new LocalizedName{Id = 552,LanguageId = "fr",ResourceType = "Country",ResourceId = "AM",Name = "Arménie",},

                new LocalizedName{Id = 553,LanguageId = "en",ResourceType = "Country",ResourceId = "AO",Name = "Angola",},
                new LocalizedName{Id = 554,LanguageId = "ar",ResourceType = "Country",ResourceId = "AO",Name = "أنغولا",},
                new LocalizedName{Id = 555,LanguageId = "fr",ResourceType = "Country",ResourceId = "AO",Name = "Angola",},

                new LocalizedName{Id = 556,LanguageId = "en",ResourceType = "Country",ResourceId = "AQ",Name = "Antarctica",},
                new LocalizedName{Id = 557,LanguageId = "ar",ResourceType = "Country",ResourceId = "AQ",Name = "أنتاركتيكا",},
                new LocalizedName{Id = 558,LanguageId = "fr",ResourceType = "Country",ResourceId = "AQ",Name = "Antarctique",},

                new LocalizedName{Id = 559,LanguageId = "en",ResourceType = "Country",ResourceId = "AR",Name = "Argentina",},
                new LocalizedName{Id = 560,LanguageId = "ar",ResourceType = "Country",ResourceId = "AR",Name = "الأرجنتين",},
                new LocalizedName{Id = 561,LanguageId = "fr",ResourceType = "Country",ResourceId = "AR",Name = "Argentine",},

                new LocalizedName{Id = 562,LanguageId = "en",ResourceType = "Country",ResourceId = "AS",Name = "American Samoa",},
                new LocalizedName{Id = 563,LanguageId = "ar",ResourceType = "Country",ResourceId = "AS",Name = "ساموا الأمريكية",},
                new LocalizedName{Id = 564,LanguageId = "fr",ResourceType = "Country",ResourceId = "AS",Name = "Samoa américaines",},

                new LocalizedName{Id = 565,LanguageId = "en",ResourceType = "Country",ResourceId = "AT",Name = "Austria",},
                new LocalizedName{Id = 566,LanguageId = "ar",ResourceType = "Country",ResourceId = "AT",Name = "النمسا",},
                new LocalizedName{Id = 567,LanguageId = "fr",ResourceType = "Country",ResourceId = "AT",Name = "Autriche",},

                new LocalizedName{Id = 568,LanguageId = "en",ResourceType = "Country",ResourceId = "AU",Name = "Australia",},
                new LocalizedName{Id = 569,LanguageId = "ar",ResourceType = "Country",ResourceId = "AU",Name = "أستراليا",},
                new LocalizedName{Id = 570,LanguageId = "fr",ResourceType = "Country",ResourceId = "AU",Name = "Australie",},

                new LocalizedName{Id = 571,LanguageId = "en",ResourceType = "Country",ResourceId = "AW",Name = "Aruba",},
                new LocalizedName{Id = 572,LanguageId = "ar",ResourceType = "Country",ResourceId = "AW",Name = "أروبا",},
                new LocalizedName{Id = 573,LanguageId = "fr",ResourceType = "Country",ResourceId = "AW",Name = "Aruba",},

                new LocalizedName{Id = 574,LanguageId = "en",ResourceType = "Country",ResourceId = "AX",Name = "Åland Islands",},
                new LocalizedName{Id = 575,LanguageId = "ar",ResourceType = "Country",ResourceId = "AX",Name = "جزر آلاند",},
                new LocalizedName{Id = 576,LanguageId = "fr",ResourceType = "Country",ResourceId = "AX",Name = "Îles Åland",},

                new LocalizedName{Id = 577,LanguageId = "en",ResourceType = "Country",ResourceId = "AZ",Name = "Azerbaijan",},
                new LocalizedName{Id = 578,LanguageId = "ar",ResourceType = "Country",ResourceId = "AZ",Name = "أذربيجان",},
                new LocalizedName{Id = 579,LanguageId = "fr",ResourceType = "Country",ResourceId = "AZ",Name = "Azerbaïdjan",},

                new LocalizedName{Id = 580,LanguageId = "en",ResourceType = "Country",ResourceId = "BA",Name = "Bosnia & Herzegovina",},
                new LocalizedName{Id = 581,LanguageId = "ar",ResourceType = "Country",ResourceId = "BA",Name = "البوسنة والهرسك",},
                new LocalizedName{Id = 582,LanguageId = "fr",ResourceType = "Country",ResourceId = "BA",Name = "Bosnie-Herzégovine",},

                new LocalizedName{Id = 583,LanguageId = "en",ResourceType = "Country",ResourceId = "BB",Name = "Barbados",},
                new LocalizedName{Id = 584,LanguageId = "ar",ResourceType = "Country",ResourceId = "BB",Name = "بربادوس",},
                new LocalizedName{Id = 585,LanguageId = "fr",ResourceType = "Country",ResourceId = "BB",Name = "Barbade",},

                new LocalizedName{Id = 586,LanguageId = "en",ResourceType = "Country",ResourceId = "BD",Name = "Bangladesh",},
                new LocalizedName{Id = 587,LanguageId = "ar",ResourceType = "Country",ResourceId = "BD",Name = "بنغلاديش",},
                new LocalizedName{Id = 588,LanguageId = "fr",ResourceType = "Country",ResourceId = "BD",Name = "Bangladesh",},

                new LocalizedName{Id = 589,LanguageId = "en",ResourceType = "Country",ResourceId = "BE",Name = "Belgium",},
                new LocalizedName{Id = 590,LanguageId = "ar",ResourceType = "Country",ResourceId = "BE",Name = "بلجيكا",},
                new LocalizedName{Id = 591,LanguageId = "fr",ResourceType = "Country",ResourceId = "BE",Name = "Belgique",},

                new LocalizedName{Id = 592,LanguageId = "en",ResourceType = "Country",ResourceId = "BF",Name = "Burkina Faso",},
                new LocalizedName{Id = 593,LanguageId = "ar",ResourceType = "Country",ResourceId = "BF",Name = "بوركينا فاسو",},
                new LocalizedName{Id = 594,LanguageId = "fr",ResourceType = "Country",ResourceId = "BF",Name = "Burkina Faso",},

                new LocalizedName{Id = 595,LanguageId = "en",ResourceType = "Country",ResourceId = "BG",Name = "Bulgaria",},
                new LocalizedName{Id = 596,LanguageId = "ar",ResourceType = "Country",ResourceId = "BG",Name = "بلغاريا",},
                new LocalizedName{Id = 597,LanguageId = "fr",ResourceType = "Country",ResourceId = "BG",Name = "Bulgarie",},

                new LocalizedName{Id = 598,LanguageId = "en",ResourceType = "Country",ResourceId = "BH",Name = "Bahrain",},
                new LocalizedName{Id = 599,LanguageId = "ar",ResourceType = "Country",ResourceId = "BH",Name = "البحرين",},
                new LocalizedName{Id = 600,LanguageId = "fr",ResourceType = "Country",ResourceId = "BH",Name = "Bahreïn",},

                new LocalizedName{Id = 601,LanguageId = "en",ResourceType = "Country",ResourceId = "BI",Name = "Burundi",},
                new LocalizedName{Id = 602,LanguageId = "ar",ResourceType = "Country",ResourceId = "BI",Name = "بوروندي",},
                new LocalizedName{Id = 603,LanguageId = "fr",ResourceType = "Country",ResourceId = "BI",Name = "Burundi",},

                new LocalizedName{Id = 604,LanguageId = "en",ResourceType = "Country",ResourceId = "BJ",Name = "Benin",},
                new LocalizedName{Id = 605,LanguageId = "ar",ResourceType = "Country",ResourceId = "BJ",Name = "بنين",},
                new LocalizedName{Id = 606,LanguageId = "fr",ResourceType = "Country",ResourceId = "BJ",Name = "Bénin",},

                new LocalizedName{Id = 607,LanguageId = "en",ResourceType = "Country",ResourceId = "BL",Name = "St. Barthélemy",},
                new LocalizedName{Id = 608,LanguageId = "ar",ResourceType = "Country",ResourceId = "BL",Name = "سان بارتليمي",},
                new LocalizedName{Id = 609,LanguageId = "fr",ResourceType = "Country",ResourceId = "BL",Name = "Saint-Barthélemy",},

                new LocalizedName{Id = 610,LanguageId = "en",ResourceType = "Country",ResourceId = "BM",Name = "Bermuda",},
                new LocalizedName{Id = 611,LanguageId = "ar",ResourceType = "Country",ResourceId = "BM",Name = "برمودا",},
                new LocalizedName{Id = 612,LanguageId = "fr",ResourceType = "Country",ResourceId = "BM",Name = "Bermudes",},

                new LocalizedName{Id = 613,LanguageId = "en",ResourceType = "Country",ResourceId = "BN",Name = "Brunei",},
                new LocalizedName{Id = 614,LanguageId = "ar",ResourceType = "Country",ResourceId = "BN",Name = "بروناي",},
                new LocalizedName{Id = 615,LanguageId = "fr",ResourceType = "Country",ResourceId = "BN",Name = "Brunei",},

                new LocalizedName{Id = 616,LanguageId = "en",ResourceType = "Country",ResourceId = "BO",Name = "Bolivia",},
                new LocalizedName{Id = 617,LanguageId = "ar",ResourceType = "Country",ResourceId = "BO",Name = "بوليفيا",},
                new LocalizedName{Id = 618,LanguageId = "fr",ResourceType = "Country",ResourceId = "BO",Name = "Bolivie",},

                new LocalizedName{Id = 619,LanguageId = "en",ResourceType = "Country",ResourceId = "BQ",Name = "Caribbean Netherlands",},
                new LocalizedName{Id = 620,LanguageId = "ar",ResourceType = "Country",ResourceId = "BQ",Name = "هولندا الكاريبية",},
                new LocalizedName{Id = 621,LanguageId = "fr",ResourceType = "Country",ResourceId = "BQ",Name = "Pays-Bas caribéens",},

                new LocalizedName{Id = 622,LanguageId = "en",ResourceType = "Country",ResourceId = "BR",Name = "Brazil",},
                new LocalizedName{Id = 623,LanguageId = "ar",ResourceType = "Country",ResourceId = "BR",Name = "البرازيل",},
                new LocalizedName{Id = 624,LanguageId = "fr",ResourceType = "Country",ResourceId = "BR",Name = "Brésil",},

                new LocalizedName{Id = 625,LanguageId = "en",ResourceType = "Country",ResourceId = "BS",Name = "Bahamas",},
                new LocalizedName{Id = 626,LanguageId = "ar",ResourceType = "Country",ResourceId = "BS",Name = "جزر البهاما",},
                new LocalizedName{Id = 627,LanguageId = "fr",ResourceType = "Country",ResourceId = "BS",Name = "Bahamas",},

                new LocalizedName{Id = 628,LanguageId = "en",ResourceType = "Country",ResourceId = "BT",Name = "Bhutan",},
                new LocalizedName{Id = 629,LanguageId = "ar",ResourceType = "Country",ResourceId = "BT",Name = "بوتان",},
                new LocalizedName{Id = 630,LanguageId = "fr",ResourceType = "Country",ResourceId = "BT",Name = "Bhoutan",},

                new LocalizedName{Id = 631,LanguageId = "en",ResourceType = "Country",ResourceId = "BV",Name = "Bouvet Island",},
                new LocalizedName{Id = 632,LanguageId = "ar",ResourceType = "Country",ResourceId = "BV",Name = "جزيرة بوفيه",},
                new LocalizedName{Id = 633,LanguageId = "fr",ResourceType = "Country",ResourceId = "BV",Name = "Île Bouvet",},

                new LocalizedName{Id = 634,LanguageId = "en",ResourceType = "Country",ResourceId = "BW",Name = "Botswana",},
                new LocalizedName{Id = 635,LanguageId = "ar",ResourceType = "Country",ResourceId = "BW",Name = "بوتسوانا",},
                new LocalizedName{Id = 636,LanguageId = "fr",ResourceType = "Country",ResourceId = "BW",Name = "Botswana",},

                new LocalizedName{Id = 637,LanguageId = "en",ResourceType = "Country",ResourceId = "BY",Name = "Belarus",},
                new LocalizedName{Id = 638,LanguageId = "ar",ResourceType = "Country",ResourceId = "BY",Name = "بيلاروس",},
                new LocalizedName{Id = 639,LanguageId = "fr",ResourceType = "Country",ResourceId = "BY",Name = "Biélorussie",},

                new LocalizedName{Id = 640,LanguageId = "en",ResourceType = "Country",ResourceId = "BZ",Name = "Belize",},
                new LocalizedName{Id = 641,LanguageId = "ar",ResourceType = "Country",ResourceId = "BZ",Name = "بليز",},
                new LocalizedName{Id = 642,LanguageId = "fr",ResourceType = "Country",ResourceId = "BZ",Name = "Belize",},

                new LocalizedName{Id = 643,LanguageId = "en",ResourceType = "Country",ResourceId = "CA",Name = "Canada",},
                new LocalizedName{Id = 644,LanguageId = "ar",ResourceType = "Country",ResourceId = "CA",Name = "كندا",},
                new LocalizedName{Id = 645,LanguageId = "fr",ResourceType = "Country",ResourceId = "CA",Name = "Canada",},

                new LocalizedName{Id = 646,LanguageId = "en",ResourceType = "Country",ResourceId = "CC",Name = "Cocos (Keeling) Islands",},
                new LocalizedName{Id = 647,LanguageId = "ar",ResourceType = "Country",ResourceId = "CC",Name = "جزر كوكوس (كيلينغ)",},
                new LocalizedName{Id = 648,LanguageId = "fr",ResourceType = "Country",ResourceId = "CC",Name = "Îles Cocos",},

                new LocalizedName{Id = 649,LanguageId = "en",ResourceType = "Country",ResourceId = "CD",Name = "Congo - Kinshasa",},
                new LocalizedName{Id = 650,LanguageId = "ar",ResourceType = "Country",ResourceId = "CD",Name = "الكونغو - كينشاسا",},
                new LocalizedName{Id = 651,LanguageId = "fr",ResourceType = "Country",ResourceId = "CD",Name = "Congo-Kinshasa",},

                new LocalizedName{Id = 652,LanguageId = "en",ResourceType = "Country",ResourceId = "CF",Name = "Central African Republic",},
                new LocalizedName{Id = 653,LanguageId = "ar",ResourceType = "Country",ResourceId = "CF",Name = "جمهورية أفريقيا الوسطى",},
                new LocalizedName{Id = 654,LanguageId = "fr",ResourceType = "Country",ResourceId = "CF",Name = "République centrafricaine",},

                new LocalizedName{Id = 655,LanguageId = "en",ResourceType = "Country",ResourceId = "CG",Name = "Congo - Brazzaville",},
                new LocalizedName{Id = 656,LanguageId = "ar",ResourceType = "Country",ResourceId = "CG",Name = "الكونغو - برازافيل",},
                new LocalizedName{Id = 657,LanguageId = "fr",ResourceType = "Country",ResourceId = "CG",Name = "Congo-Brazzaville",},

                new LocalizedName{Id = 658,LanguageId = "en",ResourceType = "Country",ResourceId = "CH",Name = "Switzerland",},
                new LocalizedName{Id = 659,LanguageId = "ar",ResourceType = "Country",ResourceId = "CH",Name = "سويسرا",},
                new LocalizedName{Id = 660,LanguageId = "fr",ResourceType = "Country",ResourceId = "CH",Name = "Suisse",},

                new LocalizedName{Id = 661,LanguageId = "en",ResourceType = "Country",ResourceId = "CI",Name = "Côte d’Ivoire",},
                new LocalizedName{Id = 662,LanguageId = "ar",ResourceType = "Country",ResourceId = "CI",Name = "ساحل العاج",},
                new LocalizedName{Id = 663,LanguageId = "fr",ResourceType = "Country",ResourceId = "CI",Name = "Côte d’Ivoire",},

                new LocalizedName{Id = 664,LanguageId = "en",ResourceType = "Country",ResourceId = "CK",Name = "Cook Islands",},
                new LocalizedName{Id = 665,LanguageId = "ar",ResourceType = "Country",ResourceId = "CK",Name = "جزر كوك",},
                new LocalizedName{Id = 666,LanguageId = "fr",ResourceType = "Country",ResourceId = "CK",Name = "Îles Cook",},

                new LocalizedName{Id = 667,LanguageId = "en",ResourceType = "Country",ResourceId = "CL",Name = "Chile",},
                new LocalizedName{Id = 668,LanguageId = "ar",ResourceType = "Country",ResourceId = "CL",Name = "تشيلي",},
                new LocalizedName{Id = 669,LanguageId = "fr",ResourceType = "Country",ResourceId = "CL",Name = "Chili",},

                new LocalizedName{Id = 670,LanguageId = "en",ResourceType = "Country",ResourceId = "CM",Name = "Cameroon",},
                new LocalizedName{Id = 671,LanguageId = "ar",ResourceType = "Country",ResourceId = "CM",Name = "الكاميرون",},
                new LocalizedName{Id = 672,LanguageId = "fr",ResourceType = "Country",ResourceId = "CM",Name = "Cameroun",},

                new LocalizedName{Id = 673,LanguageId = "en",ResourceType = "Country",ResourceId = "CN",Name = "China",},
                new LocalizedName{Id = 674,LanguageId = "ar",ResourceType = "Country",ResourceId = "CN",Name = "الصين",},
                new LocalizedName{Id = 675,LanguageId = "fr",ResourceType = "Country",ResourceId = "CN",Name = "Chine",},

                new LocalizedName{Id = 676,LanguageId = "en",ResourceType = "Country",ResourceId = "CO",Name = "Colombia",},
                new LocalizedName{Id = 677,LanguageId = "ar",ResourceType = "Country",ResourceId = "CO",Name = "كولومبيا",},
                new LocalizedName{Id = 678,LanguageId = "fr",ResourceType = "Country",ResourceId = "CO",Name = "Colombie",},

                new LocalizedName{Id = 679,LanguageId = "en",ResourceType = "Country",ResourceId = "CR",Name = "Costa Rica",},
                new LocalizedName{Id = 680,LanguageId = "ar",ResourceType = "Country",ResourceId = "CR",Name = "كوستاريكا",},
                new LocalizedName{Id = 681,LanguageId = "fr",ResourceType = "Country",ResourceId = "CR",Name = "Costa Rica",},

                new LocalizedName{Id = 682,LanguageId = "en",ResourceType = "Country",ResourceId = "CU",Name = "Cuba",},
                new LocalizedName{Id = 683,LanguageId = "ar",ResourceType = "Country",ResourceId = "CU",Name = "كوبا",},
                new LocalizedName{Id = 684,LanguageId = "fr",ResourceType = "Country",ResourceId = "CU",Name = "Cuba",},

                new LocalizedName{Id = 685,LanguageId = "en",ResourceType = "Country",ResourceId = "CV",Name = "Cape Verde",},
                new LocalizedName{Id = 686,LanguageId = "ar",ResourceType = "Country",ResourceId = "CV",Name = "الرأس الأخضر",},
                new LocalizedName{Id = 687,LanguageId = "fr",ResourceType = "Country",ResourceId = "CV",Name = "Cap-Vert",},

                new LocalizedName{Id = 688,LanguageId = "en",ResourceType = "Country",ResourceId = "CW",Name = "Curaçao",},
                new LocalizedName{Id = 689,LanguageId = "ar",ResourceType = "Country",ResourceId = "CW",Name = "كوراساو",},
                new LocalizedName{Id = 690,LanguageId = "fr",ResourceType = "Country",ResourceId = "CW",Name = "Curaçao",},

                new LocalizedName{Id = 691,LanguageId = "en",ResourceType = "Country",ResourceId = "CX",Name = "Christmas Island",},
                new LocalizedName{Id = 692,LanguageId = "ar",ResourceType = "Country",ResourceId = "CX",Name = "جزيرة كريسماس",},
                new LocalizedName{Id = 693,LanguageId = "fr",ResourceType = "Country",ResourceId = "CX",Name = "Île Christmas",},

                new LocalizedName{Id = 694,LanguageId = "en",ResourceType = "Country",ResourceId = "CY",Name = "Cyprus",},
                new LocalizedName{Id = 695,LanguageId = "ar",ResourceType = "Country",ResourceId = "CY",Name = "قبرص",},
                new LocalizedName{Id = 696,LanguageId = "fr",ResourceType = "Country",ResourceId = "CY",Name = "Chypre",},

                new LocalizedName{Id = 697,LanguageId = "en",ResourceType = "Country",ResourceId = "CZ",Name = "Czechia",},
                new LocalizedName{Id = 698,LanguageId = "ar",ResourceType = "Country",ResourceId = "CZ",Name = "التشيك",},
                new LocalizedName{Id = 699,LanguageId = "fr",ResourceType = "Country",ResourceId = "CZ",Name = "Tchéquie",},

                new LocalizedName{Id = 700,LanguageId = "en",ResourceType = "Country",ResourceId = "DE",Name = "Germany",},
                new LocalizedName{Id = 701,LanguageId = "ar",ResourceType = "Country",ResourceId = "DE",Name = "ألمانيا",},
                new LocalizedName{Id = 702,LanguageId = "fr",ResourceType = "Country",ResourceId = "DE",Name = "Allemagne",},

                new LocalizedName{Id = 703,LanguageId = "en",ResourceType = "Country",ResourceId = "DJ",Name = "Djibouti",},
                new LocalizedName{Id = 704,LanguageId = "ar",ResourceType = "Country",ResourceId = "DJ",Name = "جيبوتي",},
                new LocalizedName{Id = 705,LanguageId = "fr",ResourceType = "Country",ResourceId = "DJ",Name = "Djibouti",},

                new LocalizedName{Id = 706,LanguageId = "en",ResourceType = "Country",ResourceId = "DK",Name = "Denmark",},
                new LocalizedName{Id = 707,LanguageId = "ar",ResourceType = "Country",ResourceId = "DK",Name = "الدانمرك",},
                new LocalizedName{Id = 708,LanguageId = "fr",ResourceType = "Country",ResourceId = "DK",Name = "Danemark",},

                new LocalizedName{Id = 709,LanguageId = "en",ResourceType = "Country",ResourceId = "DM",Name = "Dominica",},
                new LocalizedName{Id = 710,LanguageId = "ar",ResourceType = "Country",ResourceId = "DM",Name = "دومينيكا",},
                new LocalizedName{Id = 711,LanguageId = "fr",ResourceType = "Country",ResourceId = "DM",Name = "Dominique",},

                new LocalizedName{Id = 712,LanguageId = "en",ResourceType = "Country",ResourceId = "DO",Name = "Dominican Republic",},
                new LocalizedName{Id = 713,LanguageId = "ar",ResourceType = "Country",ResourceId = "DO",Name = "جمهورية الدومينيكان",},
                new LocalizedName{Id = 714,LanguageId = "fr",ResourceType = "Country",ResourceId = "DO",Name = "République dominicaine",},

                new LocalizedName{Id = 715,LanguageId = "en",ResourceType = "Country",ResourceId = "DZ",Name = "Algeria",},
                new LocalizedName{Id = 716,LanguageId = "ar",ResourceType = "Country",ResourceId = "DZ",Name = "الجزائر",},
                new LocalizedName{Id = 717,LanguageId = "fr",ResourceType = "Country",ResourceId = "DZ",Name = "Algérie",},

                new LocalizedName{Id = 718,LanguageId = "en",ResourceType = "Country",ResourceId = "EC",Name = "Ecuador",},
                new LocalizedName{Id = 719,LanguageId = "ar",ResourceType = "Country",ResourceId = "EC",Name = "الإكوادور",},
                new LocalizedName{Id = 720,LanguageId = "fr",ResourceType = "Country",ResourceId = "EC",Name = "Équateur",},

                new LocalizedName{Id = 721,LanguageId = "en",ResourceType = "Country",ResourceId = "EE",Name = "Estonia",},
                new LocalizedName{Id = 722,LanguageId = "ar",ResourceType = "Country",ResourceId = "EE",Name = "إستونيا",},
                new LocalizedName{Id = 723,LanguageId = "fr",ResourceType = "Country",ResourceId = "EE",Name = "Estonie",},

                new LocalizedName{Id = 724,LanguageId = "en",ResourceType = "Country",ResourceId = "EG",Name = "Egypt",},
                new LocalizedName{Id = 725,LanguageId = "ar",ResourceType = "Country",ResourceId = "EG",Name = "مصر",},
                new LocalizedName{Id = 726,LanguageId = "fr",ResourceType = "Country",ResourceId = "EG",Name = "Égypte",},

                new LocalizedName{Id = 727,LanguageId = "en",ResourceType = "Country",ResourceId = "EH",Name = "Western Sahara",},
                new LocalizedName{Id = 728,LanguageId = "ar",ResourceType = "Country",ResourceId = "EH",Name = "الصحراء الغربية",},
                new LocalizedName{Id = 729,LanguageId = "fr",ResourceType = "Country",ResourceId = "EH",Name = "Sahara occidental",},

                new LocalizedName{Id = 730,LanguageId = "en",ResourceType = "Country",ResourceId = "ER",Name = "Eritrea",},
                new LocalizedName{Id = 731,LanguageId = "ar",ResourceType = "Country",ResourceId = "ER",Name = "إريتريا",},
                new LocalizedName{Id = 732,LanguageId = "fr",ResourceType = "Country",ResourceId = "ER",Name = "Érythrée",},

                new LocalizedName{Id = 733,LanguageId = "en",ResourceType = "Country",ResourceId = "ES",Name = "Spain",},
                new LocalizedName{Id = 734,LanguageId = "ar",ResourceType = "Country",ResourceId = "ES",Name = "إسبانيا",},
                new LocalizedName{Id = 735,LanguageId = "fr",ResourceType = "Country",ResourceId = "ES",Name = "Espagne",},

                new LocalizedName{Id = 736,LanguageId = "en",ResourceType = "Country",ResourceId = "ET",Name = "Ethiopia",},
                new LocalizedName{Id = 737,LanguageId = "ar",ResourceType = "Country",ResourceId = "ET",Name = "إثيوبيا",},
                new LocalizedName{Id = 738,LanguageId = "fr",ResourceType = "Country",ResourceId = "ET",Name = "Éthiopie",},

                new LocalizedName{Id = 739,LanguageId = "en",ResourceType = "Country",ResourceId = "FI",Name = "Finland",},
                new LocalizedName{Id = 740,LanguageId = "ar",ResourceType = "Country",ResourceId = "FI",Name = "فنلندا",},
                new LocalizedName{Id = 741,LanguageId = "fr",ResourceType = "Country",ResourceId = "FI",Name = "Finlande",},

                new LocalizedName{Id = 742,LanguageId = "en",ResourceType = "Country",ResourceId = "FJ",Name = "Fiji",},
                new LocalizedName{Id = 743,LanguageId = "ar",ResourceType = "Country",ResourceId = "FJ",Name = "فيجي",},
                new LocalizedName{Id = 744,LanguageId = "fr",ResourceType = "Country",ResourceId = "FJ",Name = "Fidji",},

                new LocalizedName{Id = 745,LanguageId = "en",ResourceType = "Country",ResourceId = "FK",Name = "Falkland Islands (Islas Malvinas)",},
                new LocalizedName{Id = 746,LanguageId = "ar",ResourceType = "Country",ResourceId = "FK",Name = "جزر فوكلاند - جزر مالفيناس",},
                new LocalizedName{Id = 747,LanguageId = "fr",ResourceType = "Country",ResourceId = "FK",Name = "Îles Malouines (Îles Falkland)",},

                new LocalizedName{Id = 748,LanguageId = "en",ResourceType = "Country",ResourceId = "FM",Name = "Micronesia",},
                new LocalizedName{Id = 749,LanguageId = "ar",ResourceType = "Country",ResourceId = "FM",Name = "ميكرونيزيا",},
                new LocalizedName{Id = 750,LanguageId = "fr",ResourceType = "Country",ResourceId = "FM",Name = "Micronésie",},

                new LocalizedName{Id = 751,LanguageId = "en",ResourceType = "Country",ResourceId = "FO",Name = "Faroe Islands",},
                new LocalizedName{Id = 752,LanguageId = "ar",ResourceType = "Country",ResourceId = "FO",Name = "جزر فارو",},
                new LocalizedName{Id = 753,LanguageId = "fr",ResourceType = "Country",ResourceId = "FO",Name = "Îles Féroé",},

                new LocalizedName{Id = 754,LanguageId = "en",ResourceType = "Country",ResourceId = "FR",Name = "France",},
                new LocalizedName{Id = 755,LanguageId = "ar",ResourceType = "Country",ResourceId = "FR",Name = "فرنسا",},
                new LocalizedName{Id = 756,LanguageId = "fr",ResourceType = "Country",ResourceId = "FR",Name = "France",},

                new LocalizedName{Id = 757,LanguageId = "en",ResourceType = "Country",ResourceId = "GA",Name = "Gabon",},
                new LocalizedName{Id = 758,LanguageId = "ar",ResourceType = "Country",ResourceId = "GA",Name = "الغابون",},
                new LocalizedName{Id = 759,LanguageId = "fr",ResourceType = "Country",ResourceId = "GA",Name = "Gabon",},

                new LocalizedName{Id = 760,LanguageId = "en",ResourceType = "Country",ResourceId = "GB",Name = "United Kingdom",},
                new LocalizedName{Id = 761,LanguageId = "ar",ResourceType = "Country",ResourceId = "GB",Name = "المملكة المتحدة",},
                new LocalizedName{Id = 762,LanguageId = "fr",ResourceType = "Country",ResourceId = "GB",Name = "Royaume-Uni",},

                new LocalizedName{Id = 763,LanguageId = "en",ResourceType = "Country",ResourceId = "GD",Name = "Grenada",},
                new LocalizedName{Id = 764,LanguageId = "ar",ResourceType = "Country",ResourceId = "GD",Name = "غرينادا",},
                new LocalizedName{Id = 765,LanguageId = "fr",ResourceType = "Country",ResourceId = "GD",Name = "Grenade",},

                new LocalizedName{Id = 766,LanguageId = "en",ResourceType = "Country",ResourceId = "GE",Name = "Georgia",},
                new LocalizedName{Id = 767,LanguageId = "ar",ResourceType = "Country",ResourceId = "GE",Name = "جورجيا",},
                new LocalizedName{Id = 768,LanguageId = "fr",ResourceType = "Country",ResourceId = "GE",Name = "Géorgie",},

                new LocalizedName{Id = 769,LanguageId = "en",ResourceType = "Country",ResourceId = "GF",Name = "French Guiana",},
                new LocalizedName{Id = 770,LanguageId = "ar",ResourceType = "Country",ResourceId = "GF",Name = "غويانا الفرنسية",},
                new LocalizedName{Id = 771,LanguageId = "fr",ResourceType = "Country",ResourceId = "GF",Name = "Guyane française",},

                new LocalizedName{Id = 772,LanguageId = "en",ResourceType = "Country",ResourceId = "GG",Name = "Guernsey",},
                new LocalizedName{Id = 773,LanguageId = "ar",ResourceType = "Country",ResourceId = "GG",Name = "غيرنزي",},
                new LocalizedName{Id = 774,LanguageId = "fr",ResourceType = "Country",ResourceId = "GG",Name = "Guernesey",},

                new LocalizedName{Id = 775,LanguageId = "en",ResourceType = "Country",ResourceId = "GH",Name = "Ghana",},
                new LocalizedName{Id = 776,LanguageId = "ar",ResourceType = "Country",ResourceId = "GH",Name = "غانا",},
                new LocalizedName{Id = 777,LanguageId = "fr",ResourceType = "Country",ResourceId = "GH",Name = "Ghana",},

                new LocalizedName{Id = 778,LanguageId = "en",ResourceType = "Country",ResourceId = "GI",Name = "Gibraltar",},
                new LocalizedName{Id = 779,LanguageId = "ar",ResourceType = "Country",ResourceId = "GI",Name = "جبل طارق",},
                new LocalizedName{Id = 780,LanguageId = "fr",ResourceType = "Country",ResourceId = "GI",Name = "Gibraltar",},

                new LocalizedName{Id = 781,LanguageId = "en",ResourceType = "Country",ResourceId = "GL",Name = "Greenland",},
                new LocalizedName{Id = 782,LanguageId = "ar",ResourceType = "Country",ResourceId = "GL",Name = "غرينلاند",},
                new LocalizedName{Id = 783,LanguageId = "fr",ResourceType = "Country",ResourceId = "GL",Name = "Groenland",},

                new LocalizedName{Id = 784,LanguageId = "en",ResourceType = "Country",ResourceId = "GM",Name = "Gambia",},
                new LocalizedName{Id = 785,LanguageId = "ar",ResourceType = "Country",ResourceId = "GM",Name = "غامبيا",},
                new LocalizedName{Id = 786,LanguageId = "fr",ResourceType = "Country",ResourceId = "GM",Name = "Gambie",},

                new LocalizedName{Id = 787,LanguageId = "en",ResourceType = "Country",ResourceId = "GN",Name = "Guinea",},
                new LocalizedName{Id = 788,LanguageId = "ar",ResourceType = "Country",ResourceId = "GN",Name = "غينيا",},
                new LocalizedName{Id = 789,LanguageId = "fr",ResourceType = "Country",ResourceId = "GN",Name = "Guinée",},

                new LocalizedName{Id = 790,LanguageId = "en",ResourceType = "Country",ResourceId = "GP",Name = "Guadeloupe",},
                new LocalizedName{Id = 791,LanguageId = "ar",ResourceType = "Country",ResourceId = "GP",Name = "غوادلوب",},
                new LocalizedName{Id = 792,LanguageId = "fr",ResourceType = "Country",ResourceId = "GP",Name = "Guadeloupe",},

                new LocalizedName{Id = 793,LanguageId = "en",ResourceType = "Country",ResourceId = "GQ",Name = "Equatorial Guinea",},
                new LocalizedName{Id = 794,LanguageId = "ar",ResourceType = "Country",ResourceId = "GQ",Name = "غينيا الاستوائية",},
                new LocalizedName{Id = 795,LanguageId = "fr",ResourceType = "Country",ResourceId = "GQ",Name = "Guinée équatoriale",},

                new LocalizedName{Id = 796,LanguageId = "en",ResourceType = "Country",ResourceId = "GR",Name = "Greece",},
                new LocalizedName{Id = 797,LanguageId = "ar",ResourceType = "Country",ResourceId = "GR",Name = "اليونان",},
                new LocalizedName{Id = 798,LanguageId = "fr",ResourceType = "Country",ResourceId = "GR",Name = "Grèce",},

                new LocalizedName{Id = 799,LanguageId = "en",ResourceType = "Country",ResourceId = "GS",Name = "South Georgia & South Sandwich Islands",},
                new LocalizedName{Id = 800,LanguageId = "ar",ResourceType = "Country",ResourceId = "GS",Name = "جورجيا الجنوبية وجزر ساندويتش الجنوبية",},
                new LocalizedName{Id = 801,LanguageId = "fr",ResourceType = "Country",ResourceId = "GS",Name = "Géorgie du Sud-et-les Îles Sandwich du Sud",},

                new LocalizedName{Id = 802,LanguageId = "en",ResourceType = "Country",ResourceId = "GT",Name = "Guatemala",},
                new LocalizedName{Id = 803,LanguageId = "ar",ResourceType = "Country",ResourceId = "GT",Name = "غواتيمالا",},
                new LocalizedName{Id = 804,LanguageId = "fr",ResourceType = "Country",ResourceId = "GT",Name = "Guatemala",},

                new LocalizedName{Id = 805,LanguageId = "en",ResourceType = "Country",ResourceId = "GU",Name = "Guam",},
                new LocalizedName{Id = 806,LanguageId = "ar",ResourceType = "Country",ResourceId = "GU",Name = "غوام",},
                new LocalizedName{Id = 807,LanguageId = "fr",ResourceType = "Country",ResourceId = "GU",Name = "Guam",},

                new LocalizedName{Id = 808,LanguageId = "en",ResourceType = "Country",ResourceId = "GW",Name = "Guinea-Bissau",},
                new LocalizedName{Id = 809,LanguageId = "ar",ResourceType = "Country",ResourceId = "GW",Name = "غينيا بيساو",},
                new LocalizedName{Id = 810,LanguageId = "fr",ResourceType = "Country",ResourceId = "GW",Name = "Guinée-Bissau",},

                new LocalizedName{Id = 811,LanguageId = "en",ResourceType = "Country",ResourceId = "GY",Name = "Guyana",},
                new LocalizedName{Id = 812,LanguageId = "ar",ResourceType = "Country",ResourceId = "GY",Name = "غيانا",},
                new LocalizedName{Id = 813,LanguageId = "fr",ResourceType = "Country",ResourceId = "GY",Name = "Guyana",},

                new LocalizedName{Id = 814,LanguageId = "en",ResourceType = "Country",ResourceId = "HK",Name = "Hong Kong",},
                new LocalizedName{Id = 815,LanguageId = "ar",ResourceType = "Country",ResourceId = "HK",Name = "هونغ كونغ",},
                new LocalizedName{Id = 816,LanguageId = "fr",ResourceType = "Country",ResourceId = "HK",Name = "Hong Kong",},

                new LocalizedName{Id = 817,LanguageId = "en",ResourceType = "Country",ResourceId = "HM",Name = "Heard & McDonald Islands",},
                new LocalizedName{Id = 818,LanguageId = "ar",ResourceType = "Country",ResourceId = "HM",Name = "جزيرة هيرد وجزر ماكدونالد",},
                new LocalizedName{Id = 819,LanguageId = "fr",ResourceType = "Country",ResourceId = "HM",Name = "Îles Heard-et-MacDonald",},

                new LocalizedName{Id = 820,LanguageId = "en",ResourceType = "Country",ResourceId = "HN",Name = "Honduras",},
                new LocalizedName{Id = 821,LanguageId = "ar",ResourceType = "Country",ResourceId = "HN",Name = "هندوراس",},
                new LocalizedName{Id = 822,LanguageId = "fr",ResourceType = "Country",ResourceId = "HN",Name = "Honduras",},

                new LocalizedName{Id = 823,LanguageId = "en",ResourceType = "Country",ResourceId = "HR",Name = "Croatia",},
                new LocalizedName{Id = 824,LanguageId = "ar",ResourceType = "Country",ResourceId = "HR",Name = "كرواتيا",},
                new LocalizedName{Id = 825,LanguageId = "fr",ResourceType = "Country",ResourceId = "HR",Name = "Croatie",},

                new LocalizedName{Id = 826,LanguageId = "en",ResourceType = "Country",ResourceId = "HT",Name = "Haiti",},
                new LocalizedName{Id = 827,LanguageId = "ar",ResourceType = "Country",ResourceId = "HT",Name = "هايتي",},
                new LocalizedName{Id = 828,LanguageId = "fr",ResourceType = "Country",ResourceId = "HT",Name = "Haïti",},

                new LocalizedName{Id = 829,LanguageId = "en",ResourceType = "Country",ResourceId = "HU",Name = "Hungary",},
                new LocalizedName{Id = 830,LanguageId = "ar",ResourceType = "Country",ResourceId = "HU",Name = "هنغاريا",},
                new LocalizedName{Id = 831,LanguageId = "fr",ResourceType = "Country",ResourceId = "HU",Name = "Hongrie",},

                new LocalizedName{Id = 832,LanguageId = "en",ResourceType = "Country",ResourceId = "ID",Name = "Indonesia",},
                new LocalizedName{Id = 833,LanguageId = "ar",ResourceType = "Country",ResourceId = "ID",Name = "إندونيسيا",},
                new LocalizedName{Id = 834,LanguageId = "fr",ResourceType = "Country",ResourceId = "ID",Name = "Indonésie",},

                new LocalizedName{Id = 835,LanguageId = "en",ResourceType = "Country",ResourceId = "IE",Name = "Ireland",},
                new LocalizedName{Id = 836,LanguageId = "ar",ResourceType = "Country",ResourceId = "IE",Name = "أيرلندا",},
                new LocalizedName{Id = 837,LanguageId = "fr",ResourceType = "Country",ResourceId = "IE",Name = "Irlande",},

                new LocalizedName{Id = 838,LanguageId = "en",ResourceType = "Country",ResourceId = "IM",Name = "Isle of Man",},
                new LocalizedName{Id = 839,LanguageId = "ar",ResourceType = "Country",ResourceId = "IM",Name = "جزيرة مان",},
                new LocalizedName{Id = 840,LanguageId = "fr",ResourceType = "Country",ResourceId = "IM",Name = "Île de Man",},

                new LocalizedName{Id = 841,LanguageId = "en",ResourceType = "Country",ResourceId = "IN",Name = "India",},
                new LocalizedName{Id = 842,LanguageId = "ar",ResourceType = "Country",ResourceId = "IN",Name = "الهند",},
                new LocalizedName{Id = 843,LanguageId = "fr",ResourceType = "Country",ResourceId = "IN",Name = "Inde",},

                new LocalizedName{Id = 844,LanguageId = "en",ResourceType = "Country",ResourceId = "IO",Name = "British Indian Ocean Territory",},
                new LocalizedName{Id = 845,LanguageId = "ar",ResourceType = "Country",ResourceId = "IO",Name = "الإقليم البريطاني في المحيط الهندي",},
                new LocalizedName{Id = 846,LanguageId = "fr",ResourceType = "Country",ResourceId = "IO",Name = "Territoire britannique de l’océan Indien",},

                new LocalizedName{Id = 847,LanguageId = "en",ResourceType = "Country",ResourceId = "IQ",Name = "Iraq",},
                new LocalizedName{Id = 848,LanguageId = "ar",ResourceType = "Country",ResourceId = "IQ",Name = "العراق",},
                new LocalizedName{Id = 849,LanguageId = "fr",ResourceType = "Country",ResourceId = "IQ",Name = "Irak",},

                new LocalizedName{Id = 850,LanguageId = "en",ResourceType = "Country",ResourceId = "IR",Name = "Iran",},
                new LocalizedName{Id = 851,LanguageId = "ar",ResourceType = "Country",ResourceId = "IR",Name = "إيران",},
                new LocalizedName{Id = 852,LanguageId = "fr",ResourceType = "Country",ResourceId = "IR",Name = "Iran",},

                new LocalizedName{Id = 853,LanguageId = "en",ResourceType = "Country",ResourceId = "IS",Name = "Iceland",},
                new LocalizedName{Id = 854,LanguageId = "ar",ResourceType = "Country",ResourceId = "IS",Name = "آيسلندا",},
                new LocalizedName{Id = 855,LanguageId = "fr",ResourceType = "Country",ResourceId = "IS",Name = "Islande",},

                new LocalizedName{Id = 856,LanguageId = "en",ResourceType = "Country",ResourceId = "IT",Name = "Italy",},
                new LocalizedName{Id = 857,LanguageId = "ar",ResourceType = "Country",ResourceId = "IT",Name = "إيطاليا",},
                new LocalizedName{Id = 858,LanguageId = "fr",ResourceType = "Country",ResourceId = "IT",Name = "Italie",},

                new LocalizedName{Id = 859,LanguageId = "en",ResourceType = "Country",ResourceId = "JE",Name = "Jersey",},
                new LocalizedName{Id = 860,LanguageId = "ar",ResourceType = "Country",ResourceId = "JE",Name = "جيرسي",},
                new LocalizedName{Id = 861,LanguageId = "fr",ResourceType = "Country",ResourceId = "JE",Name = "Jersey",},

                new LocalizedName{Id = 862,LanguageId = "en",ResourceType = "Country",ResourceId = "JM",Name = "Jamaica",},
                new LocalizedName{Id = 863,LanguageId = "ar",ResourceType = "Country",ResourceId = "JM",Name = "جامايكا",},
                new LocalizedName{Id = 864,LanguageId = "fr",ResourceType = "Country",ResourceId = "JM",Name = "Jamaïque",},

                new LocalizedName{Id = 865,LanguageId = "en",ResourceType = "Country",ResourceId = "JO",Name = "Jordan",},
                new LocalizedName{Id = 866,LanguageId = "ar",ResourceType = "Country",ResourceId = "JO",Name = "الأردن",},
                new LocalizedName{Id = 867,LanguageId = "fr",ResourceType = "Country",ResourceId = "JO",Name = "Jordanie",},

                new LocalizedName{Id = 868,LanguageId = "en",ResourceType = "Country",ResourceId = "JP",Name = "Japan",},
                new LocalizedName{Id = 869,LanguageId = "ar",ResourceType = "Country",ResourceId = "JP",Name = "اليابان",},
                new LocalizedName{Id = 870,LanguageId = "fr",ResourceType = "Country",ResourceId = "JP",Name = "Japon",},

                new LocalizedName{Id = 871,LanguageId = "en",ResourceType = "Country",ResourceId = "KE",Name = "Kenya",},
                new LocalizedName{Id = 872,LanguageId = "ar",ResourceType = "Country",ResourceId = "KE",Name = "كينيا",},
                new LocalizedName{Id = 873,LanguageId = "fr",ResourceType = "Country",ResourceId = "KE",Name = "Kenya",},

                new LocalizedName{Id = 874,LanguageId = "en",ResourceType = "Country",ResourceId = "KG",Name = "Kyrgyzstan",},
                new LocalizedName{Id = 875,LanguageId = "ar",ResourceType = "Country",ResourceId = "KG",Name = "قيرغيزستان",},
                new LocalizedName{Id = 876,LanguageId = "fr",ResourceType = "Country",ResourceId = "KG",Name = "Kirghizstan",},

                new LocalizedName{Id = 877,LanguageId = "en",ResourceType = "Country",ResourceId = "KH",Name = "Cambodia",},
                new LocalizedName{Id = 878,LanguageId = "ar",ResourceType = "Country",ResourceId = "KH",Name = "كمبوديا",},
                new LocalizedName{Id = 879,LanguageId = "fr",ResourceType = "Country",ResourceId = "KH",Name = "Cambodge",},

                new LocalizedName{Id = 880,LanguageId = "en",ResourceType = "Country",ResourceId = "KI",Name = "Kiribati",},
                new LocalizedName{Id = 881,LanguageId = "ar",ResourceType = "Country",ResourceId = "KI",Name = "كيريباتي",},
                new LocalizedName{Id = 882,LanguageId = "fr",ResourceType = "Country",ResourceId = "KI",Name = "Kiribati",},

                new LocalizedName{Id = 883,LanguageId = "en",ResourceType = "Country",ResourceId = "KM",Name = "Comoros",},
                new LocalizedName{Id = 884,LanguageId = "ar",ResourceType = "Country",ResourceId = "KM",Name = "جزر القمر",},
                new LocalizedName{Id = 885,LanguageId = "fr",ResourceType = "Country",ResourceId = "KM",Name = "Comores",},

                new LocalizedName{Id = 886,LanguageId = "en",ResourceType = "Country",ResourceId = "KN",Name = "St. Kitts & Nevis",},
                new LocalizedName{Id = 887,LanguageId = "ar",ResourceType = "Country",ResourceId = "KN",Name = "سانت كيتس ونيفيس",},
                new LocalizedName{Id = 888,LanguageId = "fr",ResourceType = "Country",ResourceId = "KN",Name = "Saint-Christophe-et-Niévès",},

                new LocalizedName{Id = 889,LanguageId = "en",ResourceType = "Country",ResourceId = "KP",Name = "North Korea",},
                new LocalizedName{Id = 890,LanguageId = "ar",ResourceType = "Country",ResourceId = "KP",Name = "كوريا الشمالية",},
                new LocalizedName{Id = 891,LanguageId = "fr",ResourceType = "Country",ResourceId = "KP",Name = "Corée du Nord",},

                new LocalizedName{Id = 892,LanguageId = "en",ResourceType = "Country",ResourceId = "KR",Name = "South Korea",},
                new LocalizedName{Id = 893,LanguageId = "ar",ResourceType = "Country",ResourceId = "KR",Name = "كوريا الجنوبية",},
                new LocalizedName{Id = 894,LanguageId = "fr",ResourceType = "Country",ResourceId = "KR",Name = "Corée du Sud",},

                new LocalizedName{Id = 895,LanguageId = "en",ResourceType = "Country",ResourceId = "KW",Name = "Kuwait",},
                new LocalizedName{Id = 896,LanguageId = "ar",ResourceType = "Country",ResourceId = "KW",Name = "الكويت",},
                new LocalizedName{Id = 897,LanguageId = "fr",ResourceType = "Country",ResourceId = "KW",Name = "Koweït",},

                new LocalizedName{Id = 898,LanguageId = "en",ResourceType = "Country",ResourceId = "KY",Name = "Cayman Islands",},
                new LocalizedName{Id = 899,LanguageId = "ar",ResourceType = "Country",ResourceId = "KY",Name = "جزر كايمان",},
                new LocalizedName{Id = 900,LanguageId = "fr",ResourceType = "Country",ResourceId = "KY",Name = "Îles Caïmans",},

                new LocalizedName{Id = 901,LanguageId = "en",ResourceType = "Country",ResourceId = "KZ",Name = "Kazakhstan",},
                new LocalizedName{Id = 902,LanguageId = "ar",ResourceType = "Country",ResourceId = "KZ",Name = "كازاخستان",},
                new LocalizedName{Id = 903,LanguageId = "fr",ResourceType = "Country",ResourceId = "KZ",Name = "Kazakhstan",},

                new LocalizedName{Id = 904,LanguageId = "en",ResourceType = "Country",ResourceId = "LA",Name = "Laos",},
                new LocalizedName{Id = 905,LanguageId = "ar",ResourceType = "Country",ResourceId = "LA",Name = "لاوس",},
                new LocalizedName{Id = 906,LanguageId = "fr",ResourceType = "Country",ResourceId = "LA",Name = "Laos",},

                new LocalizedName{Id = 907,LanguageId = "en",ResourceType = "Country",ResourceId = "LB",Name = "Lebanon",},
                new LocalizedName{Id = 908,LanguageId = "ar",ResourceType = "Country",ResourceId = "LB",Name = "لبنان",},
                new LocalizedName{Id = 909,LanguageId = "fr",ResourceType = "Country",ResourceId = "LB",Name = "Liban",},

                new LocalizedName{Id = 910,LanguageId = "en",ResourceType = "Country",ResourceId = "LC",Name = "St. Lucia",},
                new LocalizedName{Id = 911,LanguageId = "ar",ResourceType = "Country",ResourceId = "LC",Name = "سانت لوسيا",},
                new LocalizedName{Id = 912,LanguageId = "fr",ResourceType = "Country",ResourceId = "LC",Name = "Sainte-Lucie",},

                new LocalizedName{Id = 913,LanguageId = "en",ResourceType = "Country",ResourceId = "LI",Name = "Liechtenstein",},
                new LocalizedName{Id = 914,LanguageId = "ar",ResourceType = "Country",ResourceId = "LI",Name = "ليختنشتاين",},
                new LocalizedName{Id = 915,LanguageId = "fr",ResourceType = "Country",ResourceId = "LI",Name = "Liechtenstein",},

                new LocalizedName{Id = 916,LanguageId = "en",ResourceType = "Country",ResourceId = "LK",Name = "Sri Lanka",},
                new LocalizedName{Id = 917,LanguageId = "ar",ResourceType = "Country",ResourceId = "LK",Name = "سريلانكا",},
                new LocalizedName{Id = 918,LanguageId = "fr",ResourceType = "Country",ResourceId = "LK",Name = "Sri Lanka",},

                new LocalizedName{Id = 919,LanguageId = "en",ResourceType = "Country",ResourceId = "LR",Name = "Liberia",},
                new LocalizedName{Id = 920,LanguageId = "ar",ResourceType = "Country",ResourceId = "LR",Name = "ليبيريا",},
                new LocalizedName{Id = 921,LanguageId = "fr",ResourceType = "Country",ResourceId = "LR",Name = "Liberia",},

                new LocalizedName{Id = 922,LanguageId = "en",ResourceType = "Country",ResourceId = "LS",Name = "Lesotho",},
                new LocalizedName{Id = 923,LanguageId = "ar",ResourceType = "Country",ResourceId = "LS",Name = "ليسوتو",},
                new LocalizedName{Id = 924,LanguageId = "fr",ResourceType = "Country",ResourceId = "LS",Name = "Lesotho",},

                new LocalizedName{Id = 925,LanguageId = "en",ResourceType = "Country",ResourceId = "LT",Name = "Lithuania",},
                new LocalizedName{Id = 926,LanguageId = "ar",ResourceType = "Country",ResourceId = "LT",Name = "ليتوانيا",},
                new LocalizedName{Id = 927,LanguageId = "fr",ResourceType = "Country",ResourceId = "LT",Name = "Lituanie",},

                new LocalizedName{Id = 928,LanguageId = "en",ResourceType = "Country",ResourceId = "LU",Name = "Luxembourg",},
                new LocalizedName{Id = 929,LanguageId = "ar",ResourceType = "Country",ResourceId = "LU",Name = "لوكسمبورغ",},
                new LocalizedName{Id = 930,LanguageId = "fr",ResourceType = "Country",ResourceId = "LU",Name = "Luxembourg",},

                new LocalizedName{Id = 931,LanguageId = "en",ResourceType = "Country",ResourceId = "LV",Name = "Latvia",},
                new LocalizedName{Id = 932,LanguageId = "ar",ResourceType = "Country",ResourceId = "LV",Name = "لاتفيا",},
                new LocalizedName{Id = 933,LanguageId = "fr",ResourceType = "Country",ResourceId = "LV",Name = "Lettonie",},

                new LocalizedName{Id = 934,LanguageId = "en",ResourceType = "Country",ResourceId = "LY",Name = "Libya",},
                new LocalizedName{Id = 935,LanguageId = "ar",ResourceType = "Country",ResourceId = "LY",Name = "ليبيا",},
                new LocalizedName{Id = 936,LanguageId = "fr",ResourceType = "Country",ResourceId = "LY",Name = "Libye",},

                new LocalizedName{Id = 937,LanguageId = "en",ResourceType = "Country",ResourceId = "MA",Name = "Morocco",},
                new LocalizedName{Id = 938,LanguageId = "ar",ResourceType = "Country",ResourceId = "MA",Name = "المغرب",},
                new LocalizedName{Id = 939,LanguageId = "fr",ResourceType = "Country",ResourceId = "MA",Name = "Maroc",},

                new LocalizedName{Id = 940,LanguageId = "en",ResourceType = "Country",ResourceId = "MC",Name = "Monaco",},
                new LocalizedName{Id = 941,LanguageId = "ar",ResourceType = "Country",ResourceId = "MC",Name = "موناكو",},
                new LocalizedName{Id = 942,LanguageId = "fr",ResourceType = "Country",ResourceId = "MC",Name = "Monaco",},

                new LocalizedName{Id = 943,LanguageId = "en",ResourceType = "Country",ResourceId = "MD",Name = "Moldova",},
                new LocalizedName{Id = 944,LanguageId = "ar",ResourceType = "Country",ResourceId = "MD",Name = "مولدوفا",},
                new LocalizedName{Id = 945,LanguageId = "fr",ResourceType = "Country",ResourceId = "MD",Name = "Moldavie",},

                new LocalizedName{Id = 946,LanguageId = "en",ResourceType = "Country",ResourceId = "ME",Name = "Montenegro",},
                new LocalizedName{Id = 947,LanguageId = "ar",ResourceType = "Country",ResourceId = "ME",Name = "الجبل الأسود",},
                new LocalizedName{Id = 948,LanguageId = "fr",ResourceType = "Country",ResourceId = "ME",Name = "Monténégro",},

                new LocalizedName{Id = 949,LanguageId = "en",ResourceType = "Country",ResourceId = "MF",Name = "St. Martin",},
                new LocalizedName{Id = 950,LanguageId = "ar",ResourceType = "Country",ResourceId = "MF",Name = "سان مارتن",},
                new LocalizedName{Id = 951,LanguageId = "fr",ResourceType = "Country",ResourceId = "MF",Name = "Saint-Martin",},

                new LocalizedName{Id = 952,LanguageId = "en",ResourceType = "Country",ResourceId = "MG",Name = "Madagascar",},
                new LocalizedName{Id = 953,LanguageId = "ar",ResourceType = "Country",ResourceId = "MG",Name = "مدغشقر",},
                new LocalizedName{Id = 954,LanguageId = "fr",ResourceType = "Country",ResourceId = "MG",Name = "Madagascar",},

                new LocalizedName{Id = 955,LanguageId = "en",ResourceType = "Country",ResourceId = "MH",Name = "Marshall Islands",},
                new LocalizedName{Id = 956,LanguageId = "ar",ResourceType = "Country",ResourceId = "MH",Name = "جزر مارشال",},
                new LocalizedName{Id = 957,LanguageId = "fr",ResourceType = "Country",ResourceId = "MH",Name = "Îles Marshall",},

                new LocalizedName{Id = 958,LanguageId = "en",ResourceType = "Country",ResourceId = "MK",Name = "North Macedonia",},
                new LocalizedName{Id = 959,LanguageId = "ar",ResourceType = "Country",ResourceId = "MK",Name = "مقدونيا الشمالية",},
                new LocalizedName{Id = 960,LanguageId = "fr",ResourceType = "Country",ResourceId = "MK",Name = "Macédoine du Nord",},

                new LocalizedName{Id = 961,LanguageId = "en",ResourceType = "Country",ResourceId = "ML",Name = "Mali",},
                new LocalizedName{Id = 962,LanguageId = "ar",ResourceType = "Country",ResourceId = "ML",Name = "مالي",},
                new LocalizedName{Id = 963,LanguageId = "fr",ResourceType = "Country",ResourceId = "ML",Name = "Mali",},

                new LocalizedName{Id = 964,LanguageId = "en",ResourceType = "Country",ResourceId = "MM",Name = "Myanmar (Burma)",},
                new LocalizedName{Id = 965,LanguageId = "ar",ResourceType = "Country",ResourceId = "MM",Name = "ميانمار (بورما)",},
                new LocalizedName{Id = 966,LanguageId = "fr",ResourceType = "Country",ResourceId = "MM",Name = "Myanmar (Birmanie)",},

                new LocalizedName{Id = 967,LanguageId = "en",ResourceType = "Country",ResourceId = "MN",Name = "Mongolia",},
                new LocalizedName{Id = 968,LanguageId = "ar",ResourceType = "Country",ResourceId = "MN",Name = "منغوليا",},
                new LocalizedName{Id = 969,LanguageId = "fr",ResourceType = "Country",ResourceId = "MN",Name = "Mongolie",},

                new LocalizedName{Id = 970,LanguageId = "en",ResourceType = "Country",ResourceId = "MO",Name = "Macao",},
                new LocalizedName{Id = 971,LanguageId = "ar",ResourceType = "Country",ResourceId = "MO",Name = "مكاو",},
                new LocalizedName{Id = 972,LanguageId = "fr",ResourceType = "Country",ResourceId = "MO",Name = "Macao",},

                new LocalizedName{Id = 973,LanguageId = "en",ResourceType = "Country",ResourceId = "MP",Name = "Northern Mariana Islands",},
                new LocalizedName{Id = 974,LanguageId = "ar",ResourceType = "Country",ResourceId = "MP",Name = "جزر ماريانا الشمالية",},
                new LocalizedName{Id = 975,LanguageId = "fr",ResourceType = "Country",ResourceId = "MP",Name = "Îles Mariannes du Nord",},

                new LocalizedName{Id = 976,LanguageId = "en",ResourceType = "Country",ResourceId = "MQ",Name = "Martinique",},
                new LocalizedName{Id = 977,LanguageId = "ar",ResourceType = "Country",ResourceId = "MQ",Name = "جزر المارتينيك",},
                new LocalizedName{Id = 978,LanguageId = "fr",ResourceType = "Country",ResourceId = "MQ",Name = "Martinique",},

                new LocalizedName{Id = 979,LanguageId = "en",ResourceType = "Country",ResourceId = "MR",Name = "Mauritania",},
                new LocalizedName{Id = 980,LanguageId = "ar",ResourceType = "Country",ResourceId = "MR",Name = "موريتانيا",},
                new LocalizedName{Id = 981,LanguageId = "fr",ResourceType = "Country",ResourceId = "MR",Name = "Mauritanie",},

                new LocalizedName{Id = 982,LanguageId = "en",ResourceType = "Country",ResourceId = "MS",Name = "Montserrat",},
                new LocalizedName{Id = 983,LanguageId = "ar",ResourceType = "Country",ResourceId = "MS",Name = "مونتسرات",},
                new LocalizedName{Id = 984,LanguageId = "fr",ResourceType = "Country",ResourceId = "MS",Name = "Montserrat",},

                new LocalizedName{Id = 985,LanguageId = "en",ResourceType = "Country",ResourceId = "MT",Name = "Malta",},
                new LocalizedName{Id = 986,LanguageId = "ar",ResourceType = "Country",ResourceId = "MT",Name = "مالطا",},
                new LocalizedName{Id = 987,LanguageId = "fr",ResourceType = "Country",ResourceId = "MT",Name = "Malte",},

                new LocalizedName{Id = 988,LanguageId = "en",ResourceType = "Country",ResourceId = "MU",Name = "Mauritius",},
                new LocalizedName{Id = 989,LanguageId = "ar",ResourceType = "Country",ResourceId = "MU",Name = "موريشيوس",},
                new LocalizedName{Id = 990,LanguageId = "fr",ResourceType = "Country",ResourceId = "MU",Name = "Maurice",},

                new LocalizedName{Id = 991,LanguageId = "en",ResourceType = "Country",ResourceId = "MV",Name = "Maldives",},
                new LocalizedName{Id = 992,LanguageId = "ar",ResourceType = "Country",ResourceId = "MV",Name = "جزر المالديف",},
                new LocalizedName{Id = 993,LanguageId = "fr",ResourceType = "Country",ResourceId = "MV",Name = "Maldives",},

                new LocalizedName{Id = 994,LanguageId = "en",ResourceType = "Country",ResourceId = "MW",Name = "Malawi",},
                new LocalizedName{Id = 995,LanguageId = "ar",ResourceType = "Country",ResourceId = "MW",Name = "ملاوي",},
                new LocalizedName{Id = 996,LanguageId = "fr",ResourceType = "Country",ResourceId = "MW",Name = "Malawi",},

                new LocalizedName{Id = 997,LanguageId = "en",ResourceType = "Country",ResourceId = "MX",Name = "Mexico",},
                new LocalizedName{Id = 998,LanguageId = "ar",ResourceType = "Country",ResourceId = "MX",Name = "المكسيك",},
                new LocalizedName{Id = 999,LanguageId = "fr",ResourceType = "Country",ResourceId = "MX",Name = "Mexique",},

                new LocalizedName{Id = 1000,LanguageId = "en",ResourceType = "Country",ResourceId = "MY",Name = "Malaysia",},
                new LocalizedName{Id = 1001,LanguageId = "ar",ResourceType = "Country",ResourceId = "MY",Name = "ماليزيا",},
                new LocalizedName{Id = 1002,LanguageId = "fr",ResourceType = "Country",ResourceId = "MY",Name = "Malaisie",},

                new LocalizedName{Id = 1003,LanguageId = "en",ResourceType = "Country",ResourceId = "MZ",Name = "Mozambique",},
                new LocalizedName{Id = 1004,LanguageId = "ar",ResourceType = "Country",ResourceId = "MZ",Name = "موزمبيق",},
                new LocalizedName{Id = 1005,LanguageId = "fr",ResourceType = "Country",ResourceId = "MZ",Name = "Mozambique",},

                new LocalizedName{Id = 1006,LanguageId = "en",ResourceType = "Country",ResourceId = "NA",Name = "Namibia",},
                new LocalizedName{Id = 1007,LanguageId = "ar",ResourceType = "Country",ResourceId = "NA",Name = "ناميبيا",},
                new LocalizedName{Id = 1008,LanguageId = "fr",ResourceType = "Country",ResourceId = "NA",Name = "Namibie",},

                new LocalizedName{Id = 1009,LanguageId = "en",ResourceType = "Country",ResourceId = "NC",Name = "New Caledonia",},
                new LocalizedName{Id = 1010,LanguageId = "ar",ResourceType = "Country",ResourceId = "NC",Name = "كاليدونيا الجديدة",},
                new LocalizedName{Id = 1011,LanguageId = "fr",ResourceType = "Country",ResourceId = "NC",Name = "Nouvelle-Calédonie",},

                new LocalizedName{Id = 1012,LanguageId = "en",ResourceType = "Country",ResourceId = "NE",Name = "Niger",},
                new LocalizedName{Id = 1013,LanguageId = "ar",ResourceType = "Country",ResourceId = "NE",Name = "النيجر",},
                new LocalizedName{Id = 1014,LanguageId = "fr",ResourceType = "Country",ResourceId = "NE",Name = "Niger",},

                new LocalizedName{Id = 1015,LanguageId = "en",ResourceType = "Country",ResourceId = "NF",Name = "Norfolk Island",},
                new LocalizedName{Id = 1016,LanguageId = "ar",ResourceType = "Country",ResourceId = "NF",Name = "جزيرة نورفولك",},
                new LocalizedName{Id = 1017,LanguageId = "fr",ResourceType = "Country",ResourceId = "NF",Name = "Île Norfolk",},

                new LocalizedName{Id = 1018,LanguageId = "en",ResourceType = "Country",ResourceId = "NG",Name = "Nigeria",},
                new LocalizedName{Id = 1019,LanguageId = "ar",ResourceType = "Country",ResourceId = "NG",Name = "نيجيريا",},
                new LocalizedName{Id = 1020,LanguageId = "fr",ResourceType = "Country",ResourceId = "NG",Name = "Nigeria",},

                new LocalizedName{Id = 1021,LanguageId = "en",ResourceType = "Country",ResourceId = "NI",Name = "Nicaragua",},
                new LocalizedName{Id = 1022,LanguageId = "ar",ResourceType = "Country",ResourceId = "NI",Name = "نيكاراغوا",},
                new LocalizedName{Id = 1023,LanguageId = "fr",ResourceType = "Country",ResourceId = "NI",Name = "Nicaragua",},

                new LocalizedName{Id = 1024,LanguageId = "en",ResourceType = "Country",ResourceId = "NL",Name = "Netherlands",},
                new LocalizedName{Id = 1025,LanguageId = "ar",ResourceType = "Country",ResourceId = "NL",Name = "هولندا",},
                new LocalizedName{Id = 1026,LanguageId = "fr",ResourceType = "Country",ResourceId = "NL",Name = "Pays-Bas",},

                new LocalizedName{Id = 1027,LanguageId = "en",ResourceType = "Country",ResourceId = "NO",Name = "Norway",},
                new LocalizedName{Id = 1028,LanguageId = "ar",ResourceType = "Country",ResourceId = "NO",Name = "النرويج",},
                new LocalizedName{Id = 1029,LanguageId = "fr",ResourceType = "Country",ResourceId = "NO",Name = "Norvège",},

                new LocalizedName{Id = 1030,LanguageId = "en",ResourceType = "Country",ResourceId = "NP",Name = "Nepal",},
                new LocalizedName{Id = 1031,LanguageId = "ar",ResourceType = "Country",ResourceId = "NP",Name = "نيبال",},
                new LocalizedName{Id = 1032,LanguageId = "fr",ResourceType = "Country",ResourceId = "NP",Name = "Népal",},

                new LocalizedName{Id = 1033,LanguageId = "en",ResourceType = "Country",ResourceId = "NR",Name = "Nauru",},
                new LocalizedName{Id = 1034,LanguageId = "ar",ResourceType = "Country",ResourceId = "NR",Name = "ناورو",},
                new LocalizedName{Id = 1035,LanguageId = "fr",ResourceType = "Country",ResourceId = "NR",Name = "Nauru",},

                new LocalizedName{Id = 1036,LanguageId = "en",ResourceType = "Country",ResourceId = "NU",Name = "Niue",},
                new LocalizedName{Id = 1037,LanguageId = "ar",ResourceType = "Country",ResourceId = "NU",Name = "نيوي",},
                new LocalizedName{Id = 1038,LanguageId = "fr",ResourceType = "Country",ResourceId = "NU",Name = "Niue",},

                new LocalizedName{Id = 1039,LanguageId = "en",ResourceType = "Country",ResourceId = "NZ",Name = "New Zealand",},
                new LocalizedName{Id = 1040,LanguageId = "ar",ResourceType = "Country",ResourceId = "NZ",Name = "نيوزيلندا",},
                new LocalizedName{Id = 1041,LanguageId = "fr",ResourceType = "Country",ResourceId = "NZ",Name = "Nouvelle-Zélande",},

                new LocalizedName{Id = 1042,LanguageId = "en",ResourceType = "Country",ResourceId = "OM",Name = "Oman",},
                new LocalizedName{Id = 1043,LanguageId = "ar",ResourceType = "Country",ResourceId = "OM",Name = "عُمان",},
                new LocalizedName{Id = 1044,LanguageId = "fr",ResourceType = "Country",ResourceId = "OM",Name = "Oman",},

                new LocalizedName{Id = 1045,LanguageId = "en",ResourceType = "Country",ResourceId = "PA",Name = "Panama",},
                new LocalizedName{Id = 1046,LanguageId = "ar",ResourceType = "Country",ResourceId = "PA",Name = "بنما",},
                new LocalizedName{Id = 1047,LanguageId = "fr",ResourceType = "Country",ResourceId = "PA",Name = "Panama",},

                new LocalizedName{Id = 1048,LanguageId = "en",ResourceType = "Country",ResourceId = "PE",Name = "Peru",},
                new LocalizedName{Id = 1049,LanguageId = "ar",ResourceType = "Country",ResourceId = "PE",Name = "بيرو",},
                new LocalizedName{Id = 1050,LanguageId = "fr",ResourceType = "Country",ResourceId = "PE",Name = "Pérou",},

                new LocalizedName{Id = 1051,LanguageId = "en",ResourceType = "Country",ResourceId = "PF",Name = "French Polynesia",},
                new LocalizedName{Id = 1052,LanguageId = "ar",ResourceType = "Country",ResourceId = "PF",Name = "بولينيزيا الفرنسية",},
                new LocalizedName{Id = 1053,LanguageId = "fr",ResourceType = "Country",ResourceId = "PF",Name = "Polynésie française",},

                new LocalizedName{Id = 1054,LanguageId = "en",ResourceType = "Country",ResourceId = "PG",Name = "Papua New Guinea",},
                new LocalizedName{Id = 1055,LanguageId = "ar",ResourceType = "Country",ResourceId = "PG",Name = "بابوا غينيا الجديدة",},
                new LocalizedName{Id = 1056,LanguageId = "fr",ResourceType = "Country",ResourceId = "PG",Name = "Papouasie-Nouvelle-Guinée",},

                new LocalizedName{Id = 1057,LanguageId = "en",ResourceType = "Country",ResourceId = "PH",Name = "Philippines",},
                new LocalizedName{Id = 1058,LanguageId = "ar",ResourceType = "Country",ResourceId = "PH",Name = "الفلبين",},
                new LocalizedName{Id = 1059,LanguageId = "fr",ResourceType = "Country",ResourceId = "PH",Name = "Philippines",},

                new LocalizedName{Id = 1060,LanguageId = "en",ResourceType = "Country",ResourceId = "PK",Name = "Pakistan",},
                new LocalizedName{Id = 1061,LanguageId = "ar",ResourceType = "Country",ResourceId = "PK",Name = "باكستان",},
                new LocalizedName{Id = 1062,LanguageId = "fr",ResourceType = "Country",ResourceId = "PK",Name = "Pakistan",},

                new LocalizedName{Id = 1063,LanguageId = "en",ResourceType = "Country",ResourceId = "PL",Name = "Poland",},
                new LocalizedName{Id = 1064,LanguageId = "ar",ResourceType = "Country",ResourceId = "PL",Name = "بولندا",},
                new LocalizedName{Id = 1065,LanguageId = "fr",ResourceType = "Country",ResourceId = "PL",Name = "Pologne",},

                new LocalizedName{Id = 1066,LanguageId = "en",ResourceType = "Country",ResourceId = "PM",Name = "St. Pierre & Miquelon",},
                new LocalizedName{Id = 1067,LanguageId = "ar",ResourceType = "Country",ResourceId = "PM",Name = "سان بيير ومكويلون",},
                new LocalizedName{Id = 1068,LanguageId = "fr",ResourceType = "Country",ResourceId = "PM",Name = "Saint-Pierre-et-Miquelon",},

                new LocalizedName{Id = 1069,LanguageId = "en",ResourceType = "Country",ResourceId = "PN",Name = "Pitcairn Islands",},
                new LocalizedName{Id = 1070,LanguageId = "ar",ResourceType = "Country",ResourceId = "PN",Name = "جزر بيتكيرن",},
                new LocalizedName{Id = 1071,LanguageId = "fr",ResourceType = "Country",ResourceId = "PN",Name = "Îles Pitcairn",},

                new LocalizedName{Id = 1072,LanguageId = "en",ResourceType = "Country",ResourceId = "PR",Name = "Puerto Rico",},
                new LocalizedName{Id = 1073,LanguageId = "ar",ResourceType = "Country",ResourceId = "PR",Name = "بورتوريكو",},
                new LocalizedName{Id = 1074,LanguageId = "fr",ResourceType = "Country",ResourceId = "PR",Name = "Porto Rico",},

                new LocalizedName{Id = 1075,LanguageId = "en",ResourceType = "Country",ResourceId = "PS",Name = "Palestine",},
                new LocalizedName{Id = 1076,LanguageId = "ar",ResourceType = "Country",ResourceId = "PS",Name = "فلسطين",},
                new LocalizedName{Id = 1077,LanguageId = "fr",ResourceType = "Country",ResourceId = "PS",Name = "Palestine",},

                new LocalizedName{Id = 1078,LanguageId = "en",ResourceType = "Country",ResourceId = "PT",Name = "Portugal",},
                new LocalizedName{Id = 1079,LanguageId = "ar",ResourceType = "Country",ResourceId = "PT",Name = "البرتغال",},
                new LocalizedName{Id = 1080,LanguageId = "fr",ResourceType = "Country",ResourceId = "PT",Name = "Portugal",},

                new LocalizedName{Id = 1081,LanguageId = "en",ResourceType = "Country",ResourceId = "PW",Name = "Palau",},
                new LocalizedName{Id = 1082,LanguageId = "ar",ResourceType = "Country",ResourceId = "PW",Name = "بالاو",},
                new LocalizedName{Id = 1083,LanguageId = "fr",ResourceType = "Country",ResourceId = "PW",Name = "Palaos",},

                new LocalizedName{Id = 1084,LanguageId = "en",ResourceType = "Country",ResourceId = "PY",Name = "Paraguay",},
                new LocalizedName{Id = 1085,LanguageId = "ar",ResourceType = "Country",ResourceId = "PY",Name = "باراغواي",},
                new LocalizedName{Id = 1086,LanguageId = "fr",ResourceType = "Country",ResourceId = "PY",Name = "Paraguay",},

                new LocalizedName{Id = 1087,LanguageId = "en",ResourceType = "Country",ResourceId = "QA",Name = "Qatar",},
                new LocalizedName{Id = 1088,LanguageId = "ar",ResourceType = "Country",ResourceId = "QA",Name = "قطر",},
                new LocalizedName{Id = 1089,LanguageId = "fr",ResourceType = "Country",ResourceId = "QA",Name = "Qatar",},

                new LocalizedName{Id = 1090,LanguageId = "en",ResourceType = "Country",ResourceId = "RE",Name = "Réunion",},
                new LocalizedName{Id = 1091,LanguageId = "ar",ResourceType = "Country",ResourceId = "RE",Name = "روينيون",},
                new LocalizedName{Id = 1092,LanguageId = "fr",ResourceType = "Country",ResourceId = "RE",Name = "La Réunion",},

                new LocalizedName{Id = 1093,LanguageId = "en",ResourceType = "Country",ResourceId = "RO",Name = "Romania",},
                new LocalizedName{Id = 1094,LanguageId = "ar",ResourceType = "Country",ResourceId = "RO",Name = "رومانيا",},
                new LocalizedName{Id = 1095,LanguageId = "fr",ResourceType = "Country",ResourceId = "RO",Name = "Roumanie",},

                new LocalizedName{Id = 1096,LanguageId = "en",ResourceType = "Country",ResourceId = "RS",Name = "Serbia",},
                new LocalizedName{Id = 1097,LanguageId = "ar",ResourceType = "Country",ResourceId = "RS",Name = "صربيا",},
                new LocalizedName{Id = 1098,LanguageId = "fr",ResourceType = "Country",ResourceId = "RS",Name = "Serbie",},

                new LocalizedName{Id = 1099,LanguageId = "en",ResourceType = "Country",ResourceId = "RU",Name = "Russia",},
                new LocalizedName{Id = 1100,LanguageId = "ar",ResourceType = "Country",ResourceId = "RU",Name = "روسيا",},
                new LocalizedName{Id = 1101,LanguageId = "fr",ResourceType = "Country",ResourceId = "RU",Name = "Russie",},

                new LocalizedName{Id = 1102,LanguageId = "en",ResourceType = "Country",ResourceId = "RW",Name = "Rwanda",},
                new LocalizedName{Id = 1103,LanguageId = "ar",ResourceType = "Country",ResourceId = "RW",Name = "رواندا",},
                new LocalizedName{Id = 1104,LanguageId = "fr",ResourceType = "Country",ResourceId = "RW",Name = "Rwanda",},

                new LocalizedName{Id = 1105,LanguageId = "en",ResourceType = "Country",ResourceId = "SA",Name = "Saudi Arabia",},
                new LocalizedName{Id = 1106,LanguageId = "ar",ResourceType = "Country",ResourceId = "SA",Name = "المملكة العربية السعودية",},
                new LocalizedName{Id = 1107,LanguageId = "fr",ResourceType = "Country",ResourceId = "SA",Name = "Arabie saoudite",},

                new LocalizedName{Id = 1108,LanguageId = "en",ResourceType = "Country",ResourceId = "SB",Name = "Solomon Islands",},
                new LocalizedName{Id = 1109,LanguageId = "ar",ResourceType = "Country",ResourceId = "SB",Name = "جزر سليمان",},
                new LocalizedName{Id = 1110,LanguageId = "fr",ResourceType = "Country",ResourceId = "SB",Name = "Îles Salomon",},

                new LocalizedName{Id = 1111,LanguageId = "en",ResourceType = "Country",ResourceId = "SC",Name = "Seychelles",},
                new LocalizedName{Id = 1112,LanguageId = "ar",ResourceType = "Country",ResourceId = "SC",Name = "سيشل",},
                new LocalizedName{Id = 1113,LanguageId = "fr",ResourceType = "Country",ResourceId = "SC",Name = "Seychelles",},

                new LocalizedName{Id = 1114,LanguageId = "en",ResourceType = "Country",ResourceId = "SD",Name = "Sudan",},
                new LocalizedName{Id = 1115,LanguageId = "ar",ResourceType = "Country",ResourceId = "SD",Name = "السودان",},
                new LocalizedName{Id = 1116,LanguageId = "fr",ResourceType = "Country",ResourceId = "SD",Name = "Soudan",},

                new LocalizedName{Id = 1117,LanguageId = "en",ResourceType = "Country",ResourceId = "SE",Name = "Sweden",},
                new LocalizedName{Id = 1118,LanguageId = "ar",ResourceType = "Country",ResourceId = "SE",Name = "السويد",},
                new LocalizedName{Id = 1119,LanguageId = "fr",ResourceType = "Country",ResourceId = "SE",Name = "Suède",},

                new LocalizedName{Id = 1120,LanguageId = "en",ResourceType = "Country",ResourceId = "SG",Name = "Singapore",},
                new LocalizedName{Id = 1121,LanguageId = "ar",ResourceType = "Country",ResourceId = "SG",Name = "سنغافورة",},
                new LocalizedName{Id = 1122,LanguageId = "fr",ResourceType = "Country",ResourceId = "SG",Name = "Singapour",},

                new LocalizedName{Id = 1123,LanguageId = "en",ResourceType = "Country",ResourceId = "SH",Name = "St. Helena",},
                new LocalizedName{Id = 1124,LanguageId = "ar",ResourceType = "Country",ResourceId = "SH",Name = "سانت هيلينا",},
                new LocalizedName{Id = 1125,LanguageId = "fr",ResourceType = "Country",ResourceId = "SH",Name = "Sainte-Hélène",},

                new LocalizedName{Id = 1126,LanguageId = "en",ResourceType = "Country",ResourceId = "SI",Name = "Slovenia",},
                new LocalizedName{Id = 1127,LanguageId = "ar",ResourceType = "Country",ResourceId = "SI",Name = "سلوفينيا",},
                new LocalizedName{Id = 1128,LanguageId = "fr",ResourceType = "Country",ResourceId = "SI",Name = "Slovénie",},

                new LocalizedName{Id = 1129,LanguageId = "en",ResourceType = "Country",ResourceId = "SJ",Name = "Svalbard & Jan Mayen",},
                new LocalizedName{Id = 1130,LanguageId = "ar",ResourceType = "Country",ResourceId = "SJ",Name = "سفالبارد وجان ماين",},
                new LocalizedName{Id = 1131,LanguageId = "fr",ResourceType = "Country",ResourceId = "SJ",Name = "Svalbard et Jan Mayen",},

                new LocalizedName{Id = 1132,LanguageId = "en",ResourceType = "Country",ResourceId = "SK",Name = "Slovakia",},
                new LocalizedName{Id = 1133,LanguageId = "ar",ResourceType = "Country",ResourceId = "SK",Name = "سلوفاكيا",},
                new LocalizedName{Id = 1134,LanguageId = "fr",ResourceType = "Country",ResourceId = "SK",Name = "Slovaquie",},

                new LocalizedName{Id = 1135,LanguageId = "en",ResourceType = "Country",ResourceId = "SL",Name = "Sierra Leone",},
                new LocalizedName{Id = 1136,LanguageId = "ar",ResourceType = "Country",ResourceId = "SL",Name = "سيراليون",},
                new LocalizedName{Id = 1137,LanguageId = "fr",ResourceType = "Country",ResourceId = "SL",Name = "Sierra Leone",},

                new LocalizedName{Id = 1138,LanguageId = "en",ResourceType = "Country",ResourceId = "SM",Name = "San Marino",},
                new LocalizedName{Id = 1139,LanguageId = "ar",ResourceType = "Country",ResourceId = "SM",Name = "سان مارينو",},
                new LocalizedName{Id = 1140,LanguageId = "fr",ResourceType = "Country",ResourceId = "SM",Name = "Saint-Marin",},

                new LocalizedName{Id = 1141,LanguageId = "en",ResourceType = "Country",ResourceId = "SN",Name = "Senegal",},
                new LocalizedName{Id = 1142,LanguageId = "ar",ResourceType = "Country",ResourceId = "SN",Name = "السنغال",},
                new LocalizedName{Id = 1143,LanguageId = "fr",ResourceType = "Country",ResourceId = "SN",Name = "Sénégal",},

                new LocalizedName{Id = 1144,LanguageId = "en",ResourceType = "Country",ResourceId = "SO",Name = "Somalia",},
                new LocalizedName{Id = 1145,LanguageId = "ar",ResourceType = "Country",ResourceId = "SO",Name = "الصومال",},
                new LocalizedName{Id = 1146,LanguageId = "fr",ResourceType = "Country",ResourceId = "SO",Name = "Somalie",},

                new LocalizedName{Id = 1147,LanguageId = "en",ResourceType = "Country",ResourceId = "SR",Name = "Suriname",},
                new LocalizedName{Id = 1148,LanguageId = "ar",ResourceType = "Country",ResourceId = "SR",Name = "سورينام",},
                new LocalizedName{Id = 1149,LanguageId = "fr",ResourceType = "Country",ResourceId = "SR",Name = "Suriname",},

                new LocalizedName{Id = 1150,LanguageId = "en",ResourceType = "Country",ResourceId = "SS",Name = "South Sudan",},
                new LocalizedName{Id = 1151,LanguageId = "ar",ResourceType = "Country",ResourceId = "SS",Name = "جنوب السودان",},
                new LocalizedName{Id = 1152,LanguageId = "fr",ResourceType = "Country",ResourceId = "SS",Name = "Soudan du Sud",},

                new LocalizedName{Id = 1153,LanguageId = "en",ResourceType = "Country",ResourceId = "ST",Name = "São Tomé & Príncipe",},
                new LocalizedName{Id = 1154,LanguageId = "ar",ResourceType = "Country",ResourceId = "ST",Name = "ساو تومي وبرينسيبي",},
                new LocalizedName{Id = 1155,LanguageId = "fr",ResourceType = "Country",ResourceId = "ST",Name = "Sao Tomé-et-Principe",},

                new LocalizedName{Id = 1156,LanguageId = "en",ResourceType = "Country",ResourceId = "SV",Name = "El Salvador",},
                new LocalizedName{Id = 1157,LanguageId = "ar",ResourceType = "Country",ResourceId = "SV",Name = "السلفادور",},
                new LocalizedName{Id = 1158,LanguageId = "fr",ResourceType = "Country",ResourceId = "SV",Name = "Salvador",},

                new LocalizedName{Id = 1159,LanguageId = "en",ResourceType = "Country",ResourceId = "SX",Name = "Sint Maarten",},
                new LocalizedName{Id = 1160,LanguageId = "ar",ResourceType = "Country",ResourceId = "SX",Name = "سانت مارتن",},
                new LocalizedName{Id = 1161,LanguageId = "fr",ResourceType = "Country",ResourceId = "SX",Name = "Saint-Martin (partie néerlandaise)",},

                new LocalizedName{Id = 1162,LanguageId = "en",ResourceType = "Country",ResourceId = "SY",Name = "Syria",},
                new LocalizedName{Id = 1163,LanguageId = "ar",ResourceType = "Country",ResourceId = "SY",Name = "سوريا",},
                new LocalizedName{Id = 1164,LanguageId = "fr",ResourceType = "Country",ResourceId = "SY",Name = "Syrie",},

                new LocalizedName{Id = 1165,LanguageId = "en",ResourceType = "Country",ResourceId = "SZ",Name = "Eswatini",},
                new LocalizedName{Id = 1166,LanguageId = "ar",ResourceType = "Country",ResourceId = "SZ",Name = "إسواتيني",},
                new LocalizedName{Id = 1167,LanguageId = "fr",ResourceType = "Country",ResourceId = "SZ",Name = "Eswatini",},

                new LocalizedName{Id = 1168,LanguageId = "en",ResourceType = "Country",ResourceId = "TC",Name = "Turks & Caicos Islands",},
                new LocalizedName{Id = 1169,LanguageId = "ar",ResourceType = "Country",ResourceId = "TC",Name = "جزر توركس وكايكوس",},
                new LocalizedName{Id = 1170,LanguageId = "fr",ResourceType = "Country",ResourceId = "TC",Name = "Îles Turques-et-Caïques",},

                new LocalizedName{Id = 1171,LanguageId = "en",ResourceType = "Country",ResourceId = "TD",Name = "Chad",},
                new LocalizedName{Id = 1172,LanguageId = "ar",ResourceType = "Country",ResourceId = "TD",Name = "تشاد",},
                new LocalizedName{Id = 1173,LanguageId = "fr",ResourceType = "Country",ResourceId = "TD",Name = "Tchad",},

                new LocalizedName{Id = 1174,LanguageId = "en",ResourceType = "Country",ResourceId = "TF",Name = "French Southern Territories",},
                new LocalizedName{Id = 1175,LanguageId = "ar",ResourceType = "Country",ResourceId = "TF",Name = "الأقاليم الجنوبية الفرنسية",},
                new LocalizedName{Id = 1176,LanguageId = "fr",ResourceType = "Country",ResourceId = "TF",Name = "Terres australes françaises",},

                new LocalizedName{Id = 1177,LanguageId = "en",ResourceType = "Country",ResourceId = "TG",Name = "Togo",},
                new LocalizedName{Id = 1178,LanguageId = "ar",ResourceType = "Country",ResourceId = "TG",Name = "توغو",},
                new LocalizedName{Id = 1179,LanguageId = "fr",ResourceType = "Country",ResourceId = "TG",Name = "Togo",},

                new LocalizedName{Id = 1180,LanguageId = "en",ResourceType = "Country",ResourceId = "TH",Name = "Thailand",},
                new LocalizedName{Id = 1181,LanguageId = "ar",ResourceType = "Country",ResourceId = "TH",Name = "تايلاند",},
                new LocalizedName{Id = 1182,LanguageId = "fr",ResourceType = "Country",ResourceId = "TH",Name = "Thaïlande",},

                new LocalizedName{Id = 1183,LanguageId = "en",ResourceType = "Country",ResourceId = "TJ",Name = "Tajikistan",},
                new LocalizedName{Id = 1184,LanguageId = "ar",ResourceType = "Country",ResourceId = "TJ",Name = "طاجيكستان",},
                new LocalizedName{Id = 1185,LanguageId = "fr",ResourceType = "Country",ResourceId = "TJ",Name = "Tadjikistan",},

                new LocalizedName{Id = 1186,LanguageId = "en",ResourceType = "Country",ResourceId = "TK",Name = "Tokelau",},
                new LocalizedName{Id = 1187,LanguageId = "ar",ResourceType = "Country",ResourceId = "TK",Name = "توكيلاو",},
                new LocalizedName{Id = 1188,LanguageId = "fr",ResourceType = "Country",ResourceId = "TK",Name = "Tokelau",},

                new LocalizedName{Id = 1189,LanguageId = "en",ResourceType = "Country",ResourceId = "TL",Name = "Timor-Leste",},
                new LocalizedName{Id = 1190,LanguageId = "ar",ResourceType = "Country",ResourceId = "TL",Name = "تيمور - ليشتي",},
                new LocalizedName{Id = 1191,LanguageId = "fr",ResourceType = "Country",ResourceId = "TL",Name = "Timor oriental",},

                new LocalizedName{Id = 1192,LanguageId = "en",ResourceType = "Country",ResourceId = "TM",Name = "Turkmenistan",},
                new LocalizedName{Id = 1193,LanguageId = "ar",ResourceType = "Country",ResourceId = "TM",Name = "تركمانستان",},
                new LocalizedName{Id = 1194,LanguageId = "fr",ResourceType = "Country",ResourceId = "TM",Name = "Turkménistan",},

                new LocalizedName{Id = 1195,LanguageId = "en",ResourceType = "Country",ResourceId = "TN",Name = "Tunisia",},
                new LocalizedName{Id = 1196,LanguageId = "ar",ResourceType = "Country",ResourceId = "TN",Name = "تونس",},
                new LocalizedName{Id = 1197,LanguageId = "fr",ResourceType = "Country",ResourceId = "TN",Name = "Tunisie",},

                new LocalizedName{Id = 1198,LanguageId = "en",ResourceType = "Country",ResourceId = "TO",Name = "Tonga",},
                new LocalizedName{Id = 1199,LanguageId = "ar",ResourceType = "Country",ResourceId = "TO",Name = "تونغا",},
                new LocalizedName{Id = 1200,LanguageId = "fr",ResourceType = "Country",ResourceId = "TO",Name = "Tonga",},

                new LocalizedName{Id = 1201,LanguageId = "en",ResourceType = "Country",ResourceId = "TR",Name = "Türkiye",},
                new LocalizedName{Id = 1202,LanguageId = "ar",ResourceType = "Country",ResourceId = "TR",Name = "تركيا",},
                new LocalizedName{Id = 1203,LanguageId = "fr",ResourceType = "Country",ResourceId = "TR",Name = "Turquie",},

                new LocalizedName{Id = 1204,LanguageId = "en",ResourceType = "Country",ResourceId = "TT",Name = "Trinidad & Tobago",},
                new LocalizedName{Id = 1205,LanguageId = "ar",ResourceType = "Country",ResourceId = "TT",Name = "ترينيداد وتوباغو",},
                new LocalizedName{Id = 1206,LanguageId = "fr",ResourceType = "Country",ResourceId = "TT",Name = "Trinité-et-Tobago",},

                new LocalizedName{Id = 1207,LanguageId = "en",ResourceType = "Country",ResourceId = "TV",Name = "Tuvalu",},
                new LocalizedName{Id = 1208,LanguageId = "ar",ResourceType = "Country",ResourceId = "TV",Name = "توفالو",},
                new LocalizedName{Id = 1209,LanguageId = "fr",ResourceType = "Country",ResourceId = "TV",Name = "Tuvalu",},

                new LocalizedName{Id = 1210,LanguageId = "en",ResourceType = "Country",ResourceId = "TW",Name = "Taiwan",},
                new LocalizedName{Id = 1211,LanguageId = "ar",ResourceType = "Country",ResourceId = "TW",Name = "تايوان",},
                new LocalizedName{Id = 1212,LanguageId = "fr",ResourceType = "Country",ResourceId = "TW",Name = "Taïwan",},

                new LocalizedName{Id = 1213,LanguageId = "en",ResourceType = "Country",ResourceId = "TZ",Name = "Tanzania",},
                new LocalizedName{Id = 1214,LanguageId = "ar",ResourceType = "Country",ResourceId = "TZ",Name = "تنزانيا",},
                new LocalizedName{Id = 1215,LanguageId = "fr",ResourceType = "Country",ResourceId = "TZ",Name = "Tanzanie",},

                new LocalizedName{Id = 1216,LanguageId = "en",ResourceType = "Country",ResourceId = "UA",Name = "Ukraine",},
                new LocalizedName{Id = 1217,LanguageId = "ar",ResourceType = "Country",ResourceId = "UA",Name = "أوكرانيا",},
                new LocalizedName{Id = 1218,LanguageId = "fr",ResourceType = "Country",ResourceId = "UA",Name = "Ukraine",},

                new LocalizedName{Id = 1219,LanguageId = "en",ResourceType = "Country",ResourceId = "UG",Name = "Uganda",},
                new LocalizedName{Id = 1220,LanguageId = "ar",ResourceType = "Country",ResourceId = "UG",Name = "أوغندا",},
                new LocalizedName{Id = 1221,LanguageId = "fr",ResourceType = "Country",ResourceId = "UG",Name = "Ouganda",},

                new LocalizedName{Id = 1222,LanguageId = "en",ResourceType = "Country",ResourceId = "UM",Name = "U.S. Outlying Islands",},
                new LocalizedName{Id = 1223,LanguageId = "ar",ResourceType = "Country",ResourceId = "UM",Name = "جزر الولايات المتحدة النائية",},
                new LocalizedName{Id = 1224,LanguageId = "fr",ResourceType = "Country",ResourceId = "UM",Name = "Îles mineures éloignées des États-Unis",},

                new LocalizedName{Id = 1225,LanguageId = "en",ResourceType = "Country",ResourceId = "US",Name = "United States",},
                new LocalizedName{Id = 1226,LanguageId = "ar",ResourceType = "Country",ResourceId = "US",Name = "الولايات المتحدة",},
                new LocalizedName{Id = 1227,LanguageId = "fr",ResourceType = "Country",ResourceId = "US",Name = "États-Unis",},

                new LocalizedName{Id = 1228,LanguageId = "en",ResourceType = "Country",ResourceId = "UY",Name = "Uruguay",},
                new LocalizedName{Id = 1229,LanguageId = "ar",ResourceType = "Country",ResourceId = "UY",Name = "أورغواي",},
                new LocalizedName{Id = 1230,LanguageId = "fr",ResourceType = "Country",ResourceId = "UY",Name = "Uruguay",},

                new LocalizedName{Id = 1231,LanguageId = "en",ResourceType = "Country",ResourceId = "UZ",Name = "Uzbekistan",},
                new LocalizedName{Id = 1232,LanguageId = "ar",ResourceType = "Country",ResourceId = "UZ",Name = "أوزبكستان",},
                new LocalizedName{Id = 1233,LanguageId = "fr",ResourceType = "Country",ResourceId = "UZ",Name = "Ouzbékistan",},

                new LocalizedName{Id = 1234,LanguageId = "en",ResourceType = "Country",ResourceId = "VA",Name = "Vatican City",},
                new LocalizedName{Id = 1235,LanguageId = "ar",ResourceType = "Country",ResourceId = "VA",Name = "الفاتيكان",},
                new LocalizedName{Id = 1236,LanguageId = "fr",ResourceType = "Country",ResourceId = "VA",Name = "État de la Cité du Vatican",},

                new LocalizedName{Id = 1237,LanguageId = "en",ResourceType = "Country",ResourceId = "VC",Name = "St. Vincent & Grenadines",},
                new LocalizedName{Id = 1238,LanguageId = "ar",ResourceType = "Country",ResourceId = "VC",Name = "سانت فنسنت وجزر غرينادين",},
                new LocalizedName{Id = 1239,LanguageId = "fr",ResourceType = "Country",ResourceId = "VC",Name = "Saint-Vincent-et-les Grenadines",},

                new LocalizedName{Id = 1240,LanguageId = "en",ResourceType = "Country",ResourceId = "VE",Name = "Venezuela",},
                new LocalizedName{Id = 1241,LanguageId = "ar",ResourceType = "Country",ResourceId = "VE",Name = "فنزويلا",},
                new LocalizedName{Id = 1242,LanguageId = "fr",ResourceType = "Country",ResourceId = "VE",Name = "Venezuela",},

                new LocalizedName{Id = 1243,LanguageId = "en",ResourceType = "Country",ResourceId = "VG",Name = "British Virgin Islands",},
                new LocalizedName{Id = 1244,LanguageId = "ar",ResourceType = "Country",ResourceId = "VG",Name = "جزر فيرجن البريطانية",},
                new LocalizedName{Id = 1245,LanguageId = "fr",ResourceType = "Country",ResourceId = "VG",Name = "Îles Vierges britanniques",},

                new LocalizedName{Id = 1246,LanguageId = "en",ResourceType = "Country",ResourceId = "VI",Name = "U.S. Virgin Islands",},
                new LocalizedName{Id = 1247,LanguageId = "ar",ResourceType = "Country",ResourceId = "VI",Name = "جزر فيرجن الأمريكية",},
                new LocalizedName{Id = 1248,LanguageId = "fr",ResourceType = "Country",ResourceId = "VI",Name = "Îles Vierges des États-Unis",},

                new LocalizedName{Id = 1249,LanguageId = "en",ResourceType = "Country",ResourceId = "VN",Name = "Vietnam",},
                new LocalizedName{Id = 1250,LanguageId = "ar",ResourceType = "Country",ResourceId = "VN",Name = "فيتنام",},
                new LocalizedName{Id = 1251,LanguageId = "fr",ResourceType = "Country",ResourceId = "VN",Name = "Viêt Nam",},

                new LocalizedName{Id = 1252,LanguageId = "en",ResourceType = "Country",ResourceId = "VU",Name = "Vanuatu",},
                new LocalizedName{Id = 1253,LanguageId = "ar",ResourceType = "Country",ResourceId = "VU",Name = "فانواتو",},
                new LocalizedName{Id = 1254,LanguageId = "fr",ResourceType = "Country",ResourceId = "VU",Name = "Vanuatu",},

                new LocalizedName{Id = 1255,LanguageId = "en",ResourceType = "Country",ResourceId = "WF",Name = "Wallis & Futuna",},
                new LocalizedName{Id = 1256,LanguageId = "ar",ResourceType = "Country",ResourceId = "WF",Name = "جزر والس وفوتونا",},
                new LocalizedName{Id = 1257,LanguageId = "fr",ResourceType = "Country",ResourceId = "WF",Name = "Wallis-et-Futuna",},

                new LocalizedName{Id = 1258,LanguageId = "en",ResourceType = "Country",ResourceId = "WS",Name = "Samoa",},
                new LocalizedName{Id = 1259,LanguageId = "ar",ResourceType = "Country",ResourceId = "WS",Name = "ساموا",},
                new LocalizedName{Id = 1260,LanguageId = "fr",ResourceType = "Country",ResourceId = "WS",Name = "Samoa",},

                new LocalizedName{Id = 1261,LanguageId = "en",ResourceType = "Country",ResourceId = "YE",Name = "Yemen",},
                new LocalizedName{Id = 1262,LanguageId = "ar",ResourceType = "Country",ResourceId = "YE",Name = "اليمن",},
                new LocalizedName{Id = 1263,LanguageId = "fr",ResourceType = "Country",ResourceId = "YE",Name = "Yémen",},

                new LocalizedName{Id = 1264,LanguageId = "en",ResourceType = "Country",ResourceId = "YT",Name = "Mayotte",},
                new LocalizedName{Id = 1265,LanguageId = "ar",ResourceType = "Country",ResourceId = "YT",Name = "مايوت",},
                new LocalizedName{Id = 1266,LanguageId = "fr",ResourceType = "Country",ResourceId = "YT",Name = "Mayotte",},

                new LocalizedName{Id = 1267,LanguageId = "en",ResourceType = "Country",ResourceId = "ZA",Name = "South Africa",},
                new LocalizedName{Id = 1268,LanguageId = "ar",ResourceType = "Country",ResourceId = "ZA",Name = "جنوب أفريقيا",},
                new LocalizedName{Id = 1269,LanguageId = "fr",ResourceType = "Country",ResourceId = "ZA",Name = "Afrique du Sud",},

                new LocalizedName{Id = 1270,LanguageId = "en",ResourceType = "Country",ResourceId = "ZM",Name = "Zambia",},
                new LocalizedName{Id = 1271,LanguageId = "ar",ResourceType = "Country",ResourceId = "ZM",Name = "زامبيا",},
                new LocalizedName{Id = 1272,LanguageId = "fr",ResourceType = "Country",ResourceId = "ZM",Name = "Zambie",},

                new LocalizedName{Id = 1273,LanguageId = "en",ResourceType = "Country",ResourceId = "ZW",Name = "Zimbabwe",},
                new LocalizedName{Id = 1274,LanguageId = "ar",ResourceType = "Country",ResourceId = "ZW",Name = "زيمبابوي",},
                new LocalizedName{Id = 1275,LanguageId = "fr",ResourceType = "Country",ResourceId = "ZW",Name = "Zimbabwe",},

                /*
                 * ************************************************
                 * COMPANY LEGAL IDENTITY TYPE NAMES
                 * ************************************************
                 */
                new LocalizedName{Id = 1276,LanguageId = "en",ResourceType = "CompanyLegalIdentityType",ResourceId = "REG",Name = "Company registration number",},
                new LocalizedName{Id = 1277,LanguageId = "ar",ResourceType = "CompanyLegalIdentityType",ResourceId = "REG",Name = "رقم تسجيل الشركة",},
                new LocalizedName{Id = 1278,LanguageId = "fr",ResourceType = "CompanyLegalIdentityType",ResourceId = "REG",Name = "Numéro d’immatriculation de l’entreprise",},

                new LocalizedName{Id = 1279,LanguageId = "en",ResourceType = "CompanyLegalIdentityType",ResourceId = "TAX",Name = "Tax identification number",},
                new LocalizedName{Id = 1280,LanguageId = "ar",ResourceType = "CompanyLegalIdentityType",ResourceId = "TAX",Name = "رقم التعريف الضريبي",},
                new LocalizedName{Id = 1281,LanguageId = "fr",ResourceType = "CompanyLegalIdentityType",ResourceId = "TAX",Name = "Numéro d’identification fiscale",},

                new LocalizedName{Id = 1282,LanguageId = "en",ResourceType = "CompanyLegalIdentityType",ResourceId = "VAT",Name = "VAT registration number",},
                new LocalizedName{Id = 1283,LanguageId = "ar",ResourceType = "CompanyLegalIdentityType",ResourceId = "VAT",Name = "رقم التسجيل في ضريبة القيمة المضافة",},
                new LocalizedName{Id = 1284,LanguageId = "fr",ResourceType = "CompanyLegalIdentityType",ResourceId = "VAT",Name = "Numéro de TVA",},

                new LocalizedName{Id = 1285,LanguageId = "en",ResourceType = "CompanyLegalIdentityType",ResourceId = "LEI",Name = "Legal Entity Identifier",},
                new LocalizedName{Id = 1286,LanguageId = "ar",ResourceType = "CompanyLegalIdentityType",ResourceId = "LEI",Name = "معرّف الكيان القانوني",},
                new LocalizedName{Id = 1287,LanguageId = "fr",ResourceType = "CompanyLegalIdentityType",ResourceId = "LEI",Name = "Identifiant d’entité juridique",},

                new LocalizedName{Id = 1288,LanguageId = "en",ResourceType = "CompanyLegalIdentityType",ResourceId = "LIC",Name = "Business license number",},
                new LocalizedName{Id = 1289,LanguageId = "ar",ResourceType = "CompanyLegalIdentityType",ResourceId = "LIC",Name = "رقم رخصة الشركة",},
                new LocalizedName{Id = 1290,LanguageId = "fr",ResourceType = "CompanyLegalIdentityType",ResourceId = "LIC",Name = "Numéro de licence commerciale",},

                /*
                 * ************************************************
                 * PERSON IDENTITY TYPE NAMES
                 * ************************************************
                 */
                new LocalizedName{Id = 1291,LanguageId = "en",ResourceType = "PersonIdentityType",ResourceId = "PASS",Name = "Passport",},
                new LocalizedName{Id = 1292,LanguageId = "ar",ResourceType = "PersonIdentityType",ResourceId = "PASS",Name = "جواز السفر",},
                new LocalizedName{Id = 1293,LanguageId = "fr",ResourceType = "PersonIdentityType",ResourceId = "PASS",Name = "Passeport",},

                new LocalizedName{Id = 1294,LanguageId = "en",ResourceType = "PersonIdentityType",ResourceId = "ID",Name = "National identity card",},
                new LocalizedName{Id = 1295,LanguageId = "ar",ResourceType = "PersonIdentityType",ResourceId = "ID",Name = "بطاقة الهوية الوطنية",},
                new LocalizedName{Id = 1296,LanguageId = "fr",ResourceType = "PersonIdentityType",ResourceId = "ID",Name = "Carte nationale d’identité",},

                new LocalizedName{Id = 1297,LanguageId = "en",ResourceType = "PersonIdentityType",ResourceId = "DL",Name = "Driver’s license",},
                new LocalizedName{Id = 1298,LanguageId = "ar",ResourceType = "PersonIdentityType",ResourceId = "DL",Name = "رخصة القيادة",},
                new LocalizedName{Id = 1299,LanguageId = "fr",ResourceType = "PersonIdentityType",ResourceId = "DL",Name = "Permis de conduire",},

                new LocalizedName{Id = 1300,LanguageId = "en",ResourceType = "PersonIdentityType",ResourceId = "RC",Name = "Residence permit",},
                new LocalizedName{Id = 1301,LanguageId = "ar",ResourceType = "PersonIdentityType",ResourceId = "RC",Name = "تصريح الإقامة",},
                new LocalizedName{Id = 1302,LanguageId = "fr",ResourceType = "PersonIdentityType",ResourceId = "RC",Name = "Titre de séjour",},

                /*
                 * ************************************************
                 * GENDER NAMES
                 * ************************************************
                 */
                new LocalizedName{Id = 1303,LanguageId = "en",ResourceType = "Gender",ResourceId = "M",Name = "Male",},
                new LocalizedName{Id = 1304,LanguageId = "ar",ResourceType = "Gender",ResourceId = "M",Name = "ذكر",},
                new LocalizedName{Id = 1305,LanguageId = "fr",ResourceType = "Gender",ResourceId = "M",Name = "Masculin",},

                new LocalizedName{Id = 1306,LanguageId = "en",ResourceType = "Gender",ResourceId = "F",Name = "Female",},
                new LocalizedName{Id = 1307,LanguageId = "ar",ResourceType = "Gender",ResourceId = "F",Name = "أنثى",},
                new LocalizedName{Id = 1308,LanguageId = "fr",ResourceType = "Gender",ResourceId = "F",Name = "Féminin",},

                /*
                 * ************************************************
                 * PHONE TYPE NAMES
                 * ************************************************
                 */
                new LocalizedName{Id = 1309,LanguageId = "en",ResourceType = "PhoneType",ResourceId = "Mob",Name = "Mobile phone",},
                new LocalizedName{Id = 1310,LanguageId = "ar",ResourceType = "PhoneType",ResourceId = "Mob",Name = "هاتف محمول",},
                new LocalizedName{Id = 1311,LanguageId = "fr",ResourceType = "PhoneType",ResourceId = "Mob",Name = "Téléphone portable",},

                new LocalizedName{Id = 1312,LanguageId = "en",ResourceType = "PhoneType",ResourceId = "Hom",Name = "Home phone",},
                new LocalizedName{Id = 1313,LanguageId = "ar",ResourceType = "PhoneType",ResourceId = "Hom",Name = "هاتف المنزل",},
                new LocalizedName{Id = 1314,LanguageId = "fr",ResourceType = "PhoneType",ResourceId = "Hom",Name = "Téléphone fixe",},

                new LocalizedName{Id = 1315,LanguageId = "en",ResourceType = "PhoneType",ResourceId = "Wrk",Name = "Work phone",},
                new LocalizedName{Id = 1316,LanguageId = "ar",ResourceType = "PhoneType",ResourceId = "Wrk",Name = "هاتف العمل",},
                new LocalizedName{Id = 1317,LanguageId = "fr",ResourceType = "PhoneType",ResourceId = "Wrk",Name = "Téléphone professionnel",},

                new LocalizedName{Id = 1318,LanguageId = "en",ResourceType = "PhoneType",ResourceId = "Fax",Name = "Fax",},
                new LocalizedName{Id = 1319,LanguageId = "ar",ResourceType = "PhoneType",ResourceId = "Fax",Name = "فاكس",},
                new LocalizedName{Id = 1320,LanguageId = "fr",ResourceType = "PhoneType",ResourceId = "Fax",Name = "Télécopieur",}

            );
        }
    }
}
