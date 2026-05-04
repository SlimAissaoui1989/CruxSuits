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
    internal class CurrencyNameConfiguration : IEntityTypeConfiguration<CurrencyName>
    {
        private static IEnumerable<CurrencyName> GetWorldCurrencyNames()
        {
            return new List<CurrencyName>
            {
                // ===== A =====
                new CurrencyName { Id = "AED_EN", LanguageId = "EN", CurrencyId = "AED", Name = "United Arab Emirates dirham" },
                new CurrencyName { Id = "AED_FR", LanguageId = "FR", CurrencyId = "AED", Name = "Dirham des Émirats arabes unis" },
                new CurrencyName { Id = "AED_AR", LanguageId = "AR", CurrencyId = "AED", Name = "درهم إماراتي" },

                new CurrencyName { Id = "AFN_EN", LanguageId = "EN", CurrencyId = "AFN", Name = "Afghan afghani" },
                new CurrencyName { Id = "AFN_FR", LanguageId = "FR", CurrencyId = "AFN", Name = "Afghani afghan" },
                new CurrencyName { Id = "AFN_AR", LanguageId = "AR", CurrencyId = "AFN", Name = "أفغاني أفغانستاني" },

                new CurrencyName { Id = "ALL_EN", LanguageId = "EN", CurrencyId = "ALL", Name = "Albanian lek" },
                new CurrencyName { Id = "ALL_FR", LanguageId = "FR", CurrencyId = "ALL", Name = "Lek albanais" },
                new CurrencyName { Id = "ALL_AR", LanguageId = "AR", CurrencyId = "ALL", Name = "ليك ألباني" },

                new CurrencyName { Id = "AMD_EN", LanguageId = "EN", CurrencyId = "AMD", Name = "Armenian dram" },
                new CurrencyName { Id = "AMD_FR", LanguageId = "FR", CurrencyId = "AMD", Name = "Dram arménien" },
                new CurrencyName { Id = "AMD_AR", LanguageId = "AR", CurrencyId = "AMD", Name = "درام أرميني" },

                new CurrencyName { Id = "AOA_EN", LanguageId = "EN", CurrencyId = "AOA", Name = "Angolan kwanza" },
                new CurrencyName { Id = "AOA_FR", LanguageId = "FR", CurrencyId = "AOA", Name = "Kwanza angolais" },
                new CurrencyName { Id = "AOA_AR", LanguageId = "AR", CurrencyId = "AOA", Name = "كوانزا أنغولي" },

                new CurrencyName { Id = "ARS_EN", LanguageId = "EN", CurrencyId = "ARS", Name = "Argentine peso" },
                new CurrencyName { Id = "ARS_FR", LanguageId = "FR", CurrencyId = "ARS", Name = "Peso argentin" },
                new CurrencyName { Id = "ARS_AR", LanguageId = "AR", CurrencyId = "ARS", Name = "بيزو أرجنتيني" },

                new CurrencyName { Id = "AUD_EN", LanguageId = "EN", CurrencyId = "AUD", Name = "Australian dollar" },
                new CurrencyName { Id = "AUD_FR", LanguageId = "FR", CurrencyId = "AUD", Name = "Dollar australien" },
                new CurrencyName { Id = "AUD_AR", LanguageId = "AR", CurrencyId = "AUD", Name = "دولار أسترالي" },

                new CurrencyName { Id = "AWG_EN", LanguageId = "EN", CurrencyId = "AWG", Name = "Aruban florin" },
                new CurrencyName { Id = "AWG_FR", LanguageId = "FR", CurrencyId = "AWG", Name = "Florin arubais" },
                new CurrencyName { Id = "AWG_AR", LanguageId = "AR", CurrencyId = "AWG", Name = "فلورن أروبي" },

                new CurrencyName { Id = "AZN_EN", LanguageId = "EN", CurrencyId = "AZN", Name = "Azerbaijani manat" },
                new CurrencyName { Id = "AZN_FR", LanguageId = "FR", CurrencyId = "AZN", Name = "Manat azerbaïdjanais" },
                new CurrencyName { Id = "AZN_AR", LanguageId = "AR", CurrencyId = "AZN", Name = "مانات أذربيجاني" },

                // ===== B =====
                new CurrencyName { Id = "BAM_EN", LanguageId = "EN", CurrencyId = "BAM", Name = "Bosnia-Herzegovina convertible mark" },
                new CurrencyName { Id = "BAM_FR", LanguageId = "FR", CurrencyId = "BAM", Name = "Mark convertible de Bosnie-Herzégovine" },
                new CurrencyName { Id = "BAM_AR", LanguageId = "AR", CurrencyId = "BAM", Name = "مارك قابل للتحويل (البوسنة)" },

                new CurrencyName { Id = "BBD_EN", LanguageId = "EN", CurrencyId = "BBD", Name = "Barbados dollar" },
                new CurrencyName { Id = "BBD_FR", LanguageId = "FR", CurrencyId = "BBD", Name = "Dollar barbadien" },
                new CurrencyName { Id = "BBD_AR", LanguageId = "AR", CurrencyId = "BBD", Name = "دولار بربادوسي" },

                new CurrencyName { Id = "BDT_EN", LanguageId = "EN", CurrencyId = "BDT", Name = "Bangladeshi taka" },
                new CurrencyName { Id = "BDT_FR", LanguageId = "FR", CurrencyId = "BDT", Name = "Taka bangladais" },
                new CurrencyName { Id = "BDT_AR", LanguageId = "AR", CurrencyId = "BDT", Name = "تاكا بنغلاديشي" },

                new CurrencyName { Id = "BGN_EN", LanguageId = "EN", CurrencyId = "BGN", Name = "Bulgarian lev" },
                new CurrencyName { Id = "BGN_FR", LanguageId = "FR", CurrencyId = "BGN", Name = "Lev bulgare" },
                new CurrencyName { Id = "BGN_AR", LanguageId = "AR", CurrencyId = "BGN", Name = "ليف بلغاري" },

                new CurrencyName { Id = "BHD_EN", LanguageId = "EN", CurrencyId = "BHD", Name = "Bahraini dinar" },
                new CurrencyName { Id = "BHD_FR", LanguageId = "FR", CurrencyId = "BHD", Name = "Dinar bahreïni" },
                new CurrencyName { Id = "BHD_AR", LanguageId = "AR", CurrencyId = "BHD", Name = "دينار بحريني" },

                new CurrencyName { Id = "BIF_EN", LanguageId = "EN", CurrencyId = "BIF", Name = "Burundian franc" },
                new CurrencyName { Id = "BIF_FR", LanguageId = "FR", CurrencyId = "BIF", Name = "Franc burundais" },
                new CurrencyName { Id = "BIF_AR", LanguageId = "AR", CurrencyId = "BIF", Name = "فرنك بوروندي" },

                new CurrencyName { Id = "BMD_EN", LanguageId = "EN", CurrencyId = "BMD", Name = "Bermuda dollar" },
                new CurrencyName { Id = "BMD_FR", LanguageId = "FR", CurrencyId = "BMD", Name = "Dollar bermudien" },
                new CurrencyName { Id = "BMD_AR", LanguageId = "AR", CurrencyId = "BMD", Name = "دولار برمودي" },

                new CurrencyName { Id = "BND_EN", LanguageId = "EN", CurrencyId = "BND", Name = "Brunei dollar" },
                new CurrencyName { Id = "BND_FR", LanguageId = "FR", CurrencyId = "BND", Name = "Dollar de Brunei" },
                new CurrencyName { Id = "BND_AR", LanguageId = "AR", CurrencyId = "BND", Name = "دولار بروناي" },

                new CurrencyName { Id = "BOB_EN", LanguageId = "EN", CurrencyId = "BOB", Name = "Bolivian boliviano" },
                new CurrencyName { Id = "BOB_FR", LanguageId = "FR", CurrencyId = "BOB", Name = "Boliviano bolivien" },
                new CurrencyName { Id = "BOB_AR", LanguageId = "AR", CurrencyId = "BOB", Name = "بوليفيانو بوليفي" },

                new CurrencyName { Id = "BOV_EN", LanguageId = "EN", CurrencyId = "BOV", Name = "Bolivian Mvdol" },
                new CurrencyName { Id = "BOV_FR", LanguageId = "FR", CurrencyId = "BOV", Name = "Mvdol bolivien" },
                new CurrencyName { Id = "BOV_AR", LanguageId = "AR", CurrencyId = "BOV", Name = "إمفيدول بوليفي" },

                new CurrencyName { Id = "BRL_EN", LanguageId = "EN", CurrencyId = "BRL", Name = "Brazilian real" },
                new CurrencyName { Id = "BRL_FR", LanguageId = "FR", CurrencyId = "BRL", Name = "Réal brésilien" },
                new CurrencyName { Id = "BRL_AR", LanguageId = "AR", CurrencyId = "BRL", Name = "ريال برازيلي" },

                new CurrencyName { Id = "BSD_EN", LanguageId = "EN", CurrencyId = "BSD", Name = "Bahamian dollar" },
                new CurrencyName { Id = "BSD_FR", LanguageId = "FR", CurrencyId = "BSD", Name = "Dollar bahaméen" },
                new CurrencyName { Id = "BSD_AR", LanguageId = "AR", CurrencyId = "BSD", Name = "دولار باهامي" },

                new CurrencyName { Id = "BTN_EN", LanguageId = "EN", CurrencyId = "BTN", Name = "Bhutanese ngultrum" },
                new CurrencyName { Id = "BTN_FR", LanguageId = "FR", CurrencyId = "BTN", Name = "Ngultrum bhoutanais" },
                new CurrencyName { Id = "BTN_AR", LanguageId = "AR", CurrencyId = "BTN", Name = "نولتورم بوتاني" },

                new CurrencyName { Id = "BWP_EN", LanguageId = "EN", CurrencyId = "BWP", Name = "Botswana pula" },
                new CurrencyName { Id = "BWP_FR", LanguageId = "FR", CurrencyId = "BWP", Name = "Pula botswanais" },
                new CurrencyName { Id = "BWP_AR", LanguageId = "AR", CurrencyId = "BWP", Name = "بولا بوتسواني" },

                new CurrencyName { Id = "BYN_EN", LanguageId = "EN", CurrencyId = "BYN", Name = "Belarusian ruble" },
                new CurrencyName { Id = "BYN_FR", LanguageId = "FR", CurrencyId = "BYN", Name = "Rouble biélorusse" },
                new CurrencyName { Id = "BYN_AR", LanguageId = "AR", CurrencyId = "BYN", Name = "روبل بيلاروسي" },

                new CurrencyName { Id = "BZD_EN", LanguageId = "EN", CurrencyId = "BZD", Name = "Belize dollar" },
                new CurrencyName { Id = "BZD_FR", LanguageId = "FR", CurrencyId = "BZD", Name = "Dollar bélizien" },
                new CurrencyName { Id = "BZD_AR", LanguageId = "AR", CurrencyId = "BZD", Name = "دولار بليزي" },

                // ===== C =====
                new CurrencyName { Id = "CAD_EN", LanguageId = "EN", CurrencyId = "CAD", Name = "Canadian dollar" },
                new CurrencyName { Id = "CAD_FR", LanguageId = "FR", CurrencyId = "CAD", Name = "Dollar canadien" },
                new CurrencyName { Id = "CAD_AR", LanguageId = "AR", CurrencyId = "CAD", Name = "دولار كندي" },

                new CurrencyName { Id = "CDF_EN", LanguageId = "EN", CurrencyId = "CDF", Name = "Congolese franc" },
                new CurrencyName { Id = "CDF_FR", LanguageId = "FR", CurrencyId = "CDF", Name = "Franc congolais" },
                new CurrencyName { Id = "CDF_AR", LanguageId = "AR", CurrencyId = "CDF", Name = "فرنك كونغولي" },

                new CurrencyName { Id = "CHE_EN", LanguageId = "EN", CurrencyId = "CHE", Name = "WIR euro" },
                new CurrencyName { Id = "CHE_FR", LanguageId = "FR", CurrencyId = "CHE", Name = "WIR euro" },
                new CurrencyName { Id = "CHE_AR", LanguageId = "AR", CurrencyId = "CHE", Name = "يوروالـ WIR" },

                new CurrencyName { Id = "CHF_EN", LanguageId = "EN", CurrencyId = "CHF", Name = "Swiss franc" },
                new CurrencyName { Id = "CHF_FR", LanguageId = "FR", CurrencyId = "CHF", Name = "Franc suisse" },
                new CurrencyName { Id = "CHF_AR", LanguageId = "AR", CurrencyId = "CHF", Name = "فرنك سويسري" },

                new CurrencyName { Id = "CHW_EN", LanguageId = "EN", CurrencyId = "CHW", Name = "WIR franc" },
                new CurrencyName { Id = "CHW_FR", LanguageId = "FR", CurrencyId = "CHW", Name = "WIR franc" },
                new CurrencyName { Id = "CHW_AR", LanguageId = "AR", CurrencyId = "CHW", Name = "فرنك WIR" },

                new CurrencyName { Id = "CLF_EN", LanguageId = "EN", CurrencyId = "CLF", Name = "Unidad de Fomento" },
                new CurrencyName { Id = "CLF_FR", LanguageId = "FR", CurrencyId = "CLF", Name = "Unité de fomento" },
                new CurrencyName { Id = "CLF_AR", LanguageId = "AR", CurrencyId = "CLF", Name = "وحدة التنمية (UF)" },

                new CurrencyName { Id = "CLP_EN", LanguageId = "EN", CurrencyId = "CLP", Name = "Chilean peso" },
                new CurrencyName { Id = "CLP_FR", LanguageId = "FR", CurrencyId = "CLP", Name = "Peso chilien" },
                new CurrencyName { Id = "CLP_AR", LanguageId = "AR", CurrencyId = "CLP", Name = "بيزو تشيلي" },

                new CurrencyName { Id = "CNY_EN", LanguageId = "EN", CurrencyId = "CNY", Name = "Chinese yuan" },
                new CurrencyName { Id = "CNY_FR", LanguageId = "FR", CurrencyId = "CNY", Name = "Yuan chinois" },
                new CurrencyName { Id = "CNY_AR", LanguageId = "AR", CurrencyId = "CNY", Name = "يوان صيني" },

                new CurrencyName { Id = "COP_EN", LanguageId = "EN", CurrencyId = "COP", Name = "Colombian peso" },
                new CurrencyName { Id = "COP_FR", LanguageId = "FR", CurrencyId = "COP", Name = "Peso colombien" },
                new CurrencyName { Id = "COP_AR", LanguageId = "AR", CurrencyId = "COP", Name = "بيزو كولومبي" },

                new CurrencyName { Id = "COU_EN", LanguageId = "EN", CurrencyId = "COU", Name = "Unidad de Valor Real" },
                new CurrencyName { Id = "COU_FR", LanguageId = "FR", CurrencyId = "COU", Name = "Unité de valeur réelle" },
                new CurrencyName { Id = "COU_AR", LanguageId = "AR", CurrencyId = "COU", Name = "وحدة القيمة الحقيقية" },

                new CurrencyName { Id = "CRC_EN", LanguageId = "EN", CurrencyId = "CRC", Name = "Costa Rican colón" },
                new CurrencyName { Id = "CRC_FR", LanguageId = "FR", CurrencyId = "CRC", Name = "Colón costaricien" },
                new CurrencyName { Id = "CRC_AR", LanguageId = "AR", CurrencyId = "CRC", Name = "كولون كوستاريكي" },

                new CurrencyName { Id = "CUP_EN", LanguageId = "EN", CurrencyId = "CUP", Name = "Cuban peso" },
                new CurrencyName { Id = "CUP_FR", LanguageId = "FR", CurrencyId = "CUP", Name = "Peso cubain" },
                new CurrencyName { Id = "CUP_AR", LanguageId = "AR", CurrencyId = "CUP", Name = "بيزو كوبي" },

                new CurrencyName { Id = "CVE_EN", LanguageId = "EN", CurrencyId = "CVE", Name = "Cabo Verde escudo" },
                new CurrencyName { Id = "CVE_FR", LanguageId = "FR", CurrencyId = "CVE", Name = "Escudo cap-verdien" },
                new CurrencyName { Id = "CVE_AR", LanguageId = "AR", CurrencyId = "CVE", Name = "إسكودو الرأس الأخضر" },

                new CurrencyName { Id = "CZK_EN", LanguageId = "EN", CurrencyId = "CZK", Name = "Czech koruna" },
                new CurrencyName { Id = "CZK_FR", LanguageId = "FR", CurrencyId = "CZK", Name = "Couronne tchèque" },
                new CurrencyName { Id = "CZK_AR", LanguageId = "AR", CurrencyId = "CZK", Name = "كورونا تشيكية" },

                // ===== D =====
                new CurrencyName { Id = "DJF_EN", LanguageId = "EN", CurrencyId = "DJF", Name = "Djiboutian franc" },
                new CurrencyName { Id = "DJF_FR", LanguageId = "FR", CurrencyId = "DJF", Name = "Franc djiboutien" },
                new CurrencyName { Id = "DJF_AR", LanguageId = "AR", CurrencyId = "DJF", Name = "فرنك جيبوتي" },

                new CurrencyName { Id = "DKK_EN", LanguageId = "EN", CurrencyId = "DKK", Name = "Danish krone" },
                new CurrencyName { Id = "DKK_FR", LanguageId = "FR", CurrencyId = "DKK", Name = "Couronne danoise" },
                new CurrencyName { Id = "DKK_AR", LanguageId = "AR", CurrencyId = "DKK", Name = "كرونة دانمركية" },

                new CurrencyName { Id = "DOP_EN", LanguageId = "EN", CurrencyId = "DOP", Name = "Dominican peso" },
                new CurrencyName { Id = "DOP_FR", LanguageId = "FR", CurrencyId = "DOP", Name = "Peso dominicain" },
                new CurrencyName { Id = "DOP_AR", LanguageId = "AR", CurrencyId = "DOP", Name = "بيزو دومينيكاني" },

                new CurrencyName { Id = "DZD_EN", LanguageId = "EN", CurrencyId = "DZD", Name = "Algerian dinar" },
                new CurrencyName { Id = "DZD_FR", LanguageId = "FR", CurrencyId = "DZD", Name = "Dinar algérien" },
                new CurrencyName { Id = "DZD_AR", LanguageId = "AR", CurrencyId = "DZD", Name = "دينار جزائري" },

                // ===== E =====
                new CurrencyName { Id = "EGP_EN", LanguageId = "EN", CurrencyId = "EGP", Name = "Egyptian pound" },
                new CurrencyName { Id = "EGP_FR", LanguageId = "FR", CurrencyId = "EGP", Name = "Livre égyptienne" },
                new CurrencyName { Id = "EGP_AR", LanguageId = "AR", CurrencyId = "EGP", Name = "جنيه مصري" },

                new CurrencyName { Id = "ERN_EN", LanguageId = "EN", CurrencyId = "ERN", Name = "Eritrean nakfa" },
                new CurrencyName { Id = "ERN_FR", LanguageId = "FR", CurrencyId = "ERN", Name = "Nakfa érythréen" },
                new CurrencyName { Id = "ERN_AR", LanguageId = "AR", CurrencyId = "ERN", Name = "نكفة إريترية" },

                new CurrencyName { Id = "ETB_EN", LanguageId = "EN", CurrencyId = "ETB", Name = "Ethiopian birr" },
                new CurrencyName { Id = "ETB_FR", LanguageId = "FR", CurrencyId = "ETB", Name = "Birr éthiopien" },
                new CurrencyName { Id = "ETB_AR", LanguageId = "AR", CurrencyId = "ETB", Name = "بر إثيوبي" },

                new CurrencyName { Id = "EUR_EN", LanguageId = "EN", CurrencyId = "EUR", Name = "Euro" },
                new CurrencyName { Id = "EUR_FR", LanguageId = "FR", CurrencyId = "EUR", Name = "Euro" },
                new CurrencyName { Id = "EUR_AR", LanguageId = "AR", CurrencyId = "EUR", Name = "يورو" },

                // ===== F =====
                new CurrencyName { Id = "FJD_EN", LanguageId = "EN", CurrencyId = "FJD", Name = "Fiji dollar" },
                new CurrencyName { Id = "FJD_FR", LanguageId = "FR", CurrencyId = "FJD", Name = "Dollar fidjien" },
                new CurrencyName { Id = "FJD_AR", LanguageId = "AR", CurrencyId = "FJD", Name = "دولار فيجي" },

                new CurrencyName { Id = "FKP_EN", LanguageId = "EN", CurrencyId = "FKP", Name = "Falkland Islands pound" },
                new CurrencyName { Id = "FKP_FR", LanguageId = "FR", CurrencyId = "FKP", Name = "Livre des îles Falkland" },
                new CurrencyName { Id = "FKP_AR", LanguageId = "AR", CurrencyId = "FKP", Name = "جنيه جزر فوكلاند" },

                // ===== G =====
                new CurrencyName { Id = "GBP_EN", LanguageId = "EN", CurrencyId = "GBP", Name = "Great British Pound" },
                new CurrencyName { Id = "GBP_FR", LanguageId = "FR", CurrencyId = "GBP", Name = "Livre sterling" },
                new CurrencyName { Id = "GBP_AR", LanguageId = "AR", CurrencyId = "GBP", Name = "الجنيه الإسترليني" },

                new CurrencyName { Id = "GEL_EN", LanguageId = "EN", CurrencyId = "GEL", Name = "Georgian lari" },
                new CurrencyName { Id = "GEL_FR", LanguageId = "FR", CurrencyId = "GEL", Name = "Lari géorgien" },
                new CurrencyName { Id = "GEL_AR", LanguageId = "AR", CurrencyId = "GEL", Name = "لاري جورجي" },

                new CurrencyName { Id = "GHS_EN", LanguageId = "EN", CurrencyId = "GHS", Name = "Ghanaian cedi" },
                new CurrencyName { Id = "GHS_FR", LanguageId = "FR", CurrencyId = "GHS", Name = "Cedi ghanéen" },
                new CurrencyName { Id = "GHS_AR", LanguageId = "AR", CurrencyId = "GHS", Name = "سيدي غاني" },

                new CurrencyName { Id = "GIP_EN", LanguageId = "EN", CurrencyId = "GIP", Name = "Gibraltar pound" },
                new CurrencyName { Id = "GIP_FR", LanguageId = "FR", CurrencyId = "GIP", Name = "Livre de Gibraltar" },
                new CurrencyName { Id = "GIP_AR", LanguageId = "AR", CurrencyId = "GIP", Name = "جنيه جبل طارق" },

                new CurrencyName { Id = "GMD_EN", LanguageId = "EN", CurrencyId = "GMD", Name = "Gambian dalasi" },
                new CurrencyName { Id = "GMD_FR", LanguageId = "FR", CurrencyId = "GMD", Name = "Dalasi gambien" },
                new CurrencyName { Id = "GMD_AR", LanguageId = "AR", CurrencyId = "GMD", Name = "دلاسي غامبي" },

                new CurrencyName { Id = "GNF_EN", LanguageId = "EN", CurrencyId = "GNF", Name = "Guinean franc" },
                new CurrencyName { Id = "GNF_FR", LanguageId = "FR", CurrencyId = "GNF", Name = "Franc guinéen" },
                new CurrencyName { Id = "GNF_AR", LanguageId = "AR", CurrencyId = "GNF", Name = "فرنك غيني" },

                new CurrencyName { Id = "GTQ_EN", LanguageId = "EN", CurrencyId = "GTQ", Name = "Guatemalan quetzal" },
                new CurrencyName { Id = "GTQ_FR", LanguageId = "FR", CurrencyId = "GTQ", Name = "Quetzal guatémaltèque" },
                new CurrencyName { Id = "GTQ_AR", LanguageId = "AR", CurrencyId = "GTQ", Name = "كتزال غواتيمالي" },

                new CurrencyName { Id = "GYD_EN", LanguageId = "EN", CurrencyId = "GYD", Name = "Guyanese dollar" },
                new CurrencyName { Id = "GYD_FR", LanguageId = "FR", CurrencyId = "GYD", Name = "Dollar guyanais" },
                new CurrencyName { Id = "GYD_AR", LanguageId = "AR", CurrencyId = "GYD", Name = "دولار غياني" },

                // ===== H =====
                new CurrencyName { Id = "HKD_EN", LanguageId = "EN", CurrencyId = "HKD", Name = "Hong Kong dollar" },
                new CurrencyName { Id = "HKD_FR", LanguageId = "FR", CurrencyId = "HKD", Name = "Dollar de Hong Kong" },
                new CurrencyName { Id = "HKD_AR", LanguageId = "AR", CurrencyId = "HKD", Name = "دولار هونغ كونغ" },

                new CurrencyName { Id = "HNL_EN", LanguageId = "EN", CurrencyId = "HNL", Name = "Honduran lempira" },
                new CurrencyName { Id = "HNL_FR", LanguageId = "FR", CurrencyId = "HNL", Name = "Lempira hondurien" },
                new CurrencyName { Id = "HNL_AR", LanguageId = "AR", CurrencyId = "HNL", Name = "ليمبيرا هندوراسية" },

                new CurrencyName { Id = "HTG_EN", LanguageId = "EN", CurrencyId = "HTG", Name = "Haitian gourde" },
                new CurrencyName { Id = "HTG_FR", LanguageId = "FR", CurrencyId = "HTG", Name = "Gourde haïtienne" },
                new CurrencyName { Id = "HTG_AR", LanguageId = "AR", CurrencyId = "HTG", Name = "غورد هايتي" },

                new CurrencyName { Id = "HUF_EN", LanguageId = "EN", CurrencyId = "HUF", Name = "Hungarian forint" },
                new CurrencyName { Id = "HUF_FR", LanguageId = "FR", CurrencyId = "HUF", Name = "Forint hongrois" },
                new CurrencyName { Id = "HUF_AR", LanguageId = "AR", CurrencyId = "HUF", Name = "فورينت هنغاري" },

                // ===== I =====
                new CurrencyName { Id = "IDR_EN", LanguageId = "EN", CurrencyId = "IDR", Name = "Indonesian rupiah" },
                new CurrencyName { Id = "IDR_FR", LanguageId = "FR", CurrencyId = "IDR", Name = "Roupie indonésienne" },
                new CurrencyName { Id = "IDR_AR", LanguageId = "AR", CurrencyId = "IDR", Name = "روبية إندونيسية" },

                new CurrencyName { Id = "INR_EN", LanguageId = "EN", CurrencyId = "INR", Name = "Indian rupee" },
                new CurrencyName { Id = "INR_FR", LanguageId = "FR", CurrencyId = "INR", Name = "Roupie indienne" },
                new CurrencyName { Id = "INR_AR", LanguageId = "AR", CurrencyId = "INR", Name = "روبية هندية" },

                new CurrencyName { Id = "IQD_EN", LanguageId = "EN", CurrencyId = "IQD", Name = "Iraqi dinar" },
                new CurrencyName { Id = "IQD_FR", LanguageId = "FR", CurrencyId = "IQD", Name = "Dinar irakien" },
                new CurrencyName { Id = "IQD_AR", LanguageId = "AR", CurrencyId = "IQD", Name = "دينار عراقي" },

                new CurrencyName { Id = "IRR_EN", LanguageId = "EN", CurrencyId = "IRR", Name = "Iranian rial" },
                new CurrencyName { Id = "IRR_FR", LanguageId = "FR", CurrencyId = "IRR", Name = "Rial iranien" },
                new CurrencyName { Id = "IRR_AR", LanguageId = "AR", CurrencyId = "IRR", Name = "ريال إيراني" },

                new CurrencyName { Id = "ISK_EN", LanguageId = "EN", CurrencyId = "ISK", Name = "Icelandic króna" },
                new CurrencyName { Id = "ISK_FR", LanguageId = "FR", CurrencyId = "ISK", Name = "Couronne islandaise" },
                new CurrencyName { Id = "ISK_AR", LanguageId = "AR", CurrencyId = "ISK", Name = "كرونة آيسلندية" },

                // ===== J =====
                new CurrencyName { Id = "JMD_EN", LanguageId = "EN", CurrencyId = "JMD", Name = "Jamaican dollar" },
                new CurrencyName { Id = "JMD_FR", LanguageId = "FR", CurrencyId = "JMD", Name = "Dollar jamaïcain" },
                new CurrencyName { Id = "JMD_AR", LanguageId = "AR", CurrencyId = "JMD", Name = "دولار جامايكي" },

                new CurrencyName { Id = "JOD_EN", LanguageId = "EN", CurrencyId = "JOD", Name = "Jordanian dinar" },
                new CurrencyName { Id = "JOD_FR", LanguageId = "FR", CurrencyId = "JOD", Name = "Dinar jordanien" },
                new CurrencyName { Id = "JOD_AR", LanguageId = "AR", CurrencyId = "JOD", Name = "دينار أردني" },

                new CurrencyName { Id = "JPY_EN", LanguageId = "EN", CurrencyId = "JPY", Name = "Japanese yen" },
                new CurrencyName { Id = "JPY_FR", LanguageId = "FR", CurrencyId = "JPY", Name = "Yen japonais" },
                new CurrencyName { Id = "JPY_AR", LanguageId = "AR", CurrencyId = "JPY", Name = "ين ياباني" },

                // ===== K =====
                new CurrencyName { Id = "KES_EN", LanguageId = "EN", CurrencyId = "KES", Name = "Kenyan shilling" },
                new CurrencyName { Id = "KES_FR", LanguageId = "FR", CurrencyId = "KES", Name = "Shilling kényan" },
                new CurrencyName { Id = "KES_AR", LanguageId = "AR", CurrencyId = "KES", Name = "شلن كيني" },

                new CurrencyName { Id = "KGS_EN", LanguageId = "EN", CurrencyId = "KGS", Name = "Kyrgyzstani som" },
                new CurrencyName { Id = "KGS_FR", LanguageId = "FR", CurrencyId = "KGS", Name = "Som kirghize" },
                new CurrencyName { Id = "KGS_AR", LanguageId = "AR", CurrencyId = "KGS", Name = "سوم قيرغيزي" },

                new CurrencyName { Id = "KHR_EN", LanguageId = "EN", CurrencyId = "KHR", Name = "Cambodian riel" },
                new CurrencyName { Id = "KHR_FR", LanguageId = "FR", CurrencyId = "KHR", Name = "Riel cambodgien" },
                new CurrencyName { Id = "KHR_AR", LanguageId = "AR", CurrencyId = "KHR", Name = "ريال كمبودي" },

                new CurrencyName { Id = "KMF_EN", LanguageId = "EN", CurrencyId = "KMF", Name = "Comorian franc" },
                new CurrencyName { Id = "KMF_FR", LanguageId = "FR", CurrencyId = "KMF", Name = "Franc comorien" },
                new CurrencyName { Id = "KMF_AR", LanguageId = "AR", CurrencyId = "KMF", Name = "فرنك قمري" },

                new CurrencyName { Id = "KRW_EN", LanguageId = "EN", CurrencyId = "KRW", Name = "South Korean Won" },
                new CurrencyName { Id = "KRW_FR", LanguageId = "FR", CurrencyId = "KRW", Name = "Won sud-coréen" },
                new CurrencyName { Id = "KRW_AR", LanguageId = "AR", CurrencyId = "KRW", Name = "وون كوري جنوبي" },

                new CurrencyName { Id = "KPW_EN", LanguageId = "EN", CurrencyId = "KPW", Name = "North Korean Won" },
                new CurrencyName { Id = "KPW_FR", LanguageId = "FR", CurrencyId = "KPW", Name = "Won nord-coréen" },
                new CurrencyName { Id = "KPW_AR", LanguageId = "AR", CurrencyId = "KPW", Name = "وون كوري شمالي" },

                new CurrencyName { Id = "KWD_EN", LanguageId = "EN", CurrencyId = "KWD", Name = "Kuwaiti dinar" },
                new CurrencyName { Id = "KWD_FR", LanguageId = "FR", CurrencyId = "KWD", Name = "Dinar koweïtien" },
                new CurrencyName { Id = "KWD_AR", LanguageId = "AR", CurrencyId = "KWD", Name = "دينار كويتي" },

                new CurrencyName { Id = "KYD_EN", LanguageId = "EN", CurrencyId = "KYD", Name = "Cayman Islands dollar" },
                new CurrencyName { Id = "KYD_FR", LanguageId = "FR", CurrencyId = "KYD", Name = "Dollar des îles Caïmans" },
                new CurrencyName { Id = "KYD_AR", LanguageId = "AR", CurrencyId = "KYD", Name = "دولار جزر كايمان" },

                new CurrencyName { Id = "KZT_EN", LanguageId = "EN", CurrencyId = "KZT", Name = "Kazakhstani tenge" },
                new CurrencyName { Id = "KZT_FR", LanguageId = "FR", CurrencyId = "KZT", Name = "Tenge kazakh" },
                new CurrencyName { Id = "KZT_AR", LanguageId = "AR", CurrencyId = "KZT", Name = "تينغي كازاخستاني" },

                // ===== L =====
                new CurrencyName { Id = "LAK_EN", LanguageId = "EN", CurrencyId = "LAK", Name = "Lao kip" },
                new CurrencyName { Id = "LAK_FR", LanguageId = "FR", CurrencyId = "LAK", Name = "Kip laotien" },
                new CurrencyName { Id = "LAK_AR", LanguageId = "AR", CurrencyId = "LAK", Name = "كيب لاوسي" },

                new CurrencyName { Id = "LBP_EN", LanguageId = "EN", CurrencyId = "LBP", Name = "Lebanese pound" },
                new CurrencyName { Id = "LBP_FR", LanguageId = "FR", CurrencyId = "LBP", Name = "Livre libanaise" },
                new CurrencyName { Id = "LBP_AR", LanguageId = "AR", CurrencyId = "LBP", Name = "جنيه لبناني" },

                new CurrencyName { Id = "LKR_EN", LanguageId = "EN", CurrencyId = "LKR", Name = "Sri Lankan rupee" },
                new CurrencyName { Id = "LKR_FR", LanguageId = "FR", CurrencyId = "LKR", Name = "Roupie srilankaise" },
                new CurrencyName { Id = "LKR_AR", LanguageId = "AR", CurrencyId = "LKR", Name = "روبية سريلانكية" },

                new CurrencyName { Id = "LRD_EN", LanguageId = "EN", CurrencyId = "LRD", Name = "Liberian dollar" },
                new CurrencyName { Id = "LRD_FR", LanguageId = "FR", CurrencyId = "LRD", Name = "Dollar libérien" },
                new CurrencyName { Id = "LRD_AR", LanguageId = "AR", CurrencyId = "LRD", Name = "دولار ليبيري" },

                new CurrencyName { Id = "LSL_EN", LanguageId = "EN", CurrencyId = "LSL", Name = "Lesotho loti" },
                new CurrencyName { Id = "LSL_FR", LanguageId = "FR", CurrencyId = "LSL", Name = "Loti lesothan" },
                new CurrencyName { Id = "LSL_AR", LanguageId = "AR", CurrencyId = "LSL", Name = "لوتي ليسوتي" },

                new CurrencyName { Id = "LYD_EN", LanguageId = "EN", CurrencyId = "LYD", Name = "Libyan dinar" },
                new CurrencyName { Id = "LYD_FR", LanguageId = "FR", CurrencyId = "LYD", Name = "Dinar libyen" },
                new CurrencyName { Id = "LYD_AR", LanguageId = "AR", CurrencyId = "LYD", Name = "دينار ليبي" },

                // ===== M =====
                new CurrencyName { Id = "MAD_EN", LanguageId = "EN", CurrencyId = "MAD", Name = "Moroccan dirham" },
                new CurrencyName { Id = "MAD_FR", LanguageId = "FR", CurrencyId = "MAD", Name = "Dirham marocain" },
                new CurrencyName { Id = "MAD_AR", LanguageId = "AR", CurrencyId = "MAD", Name = "درهم مغربي" },

                new CurrencyName { Id = "MDL_EN", LanguageId = "EN", CurrencyId = "MDL", Name = "Moldovan leu" },
                new CurrencyName { Id = "MDL_FR", LanguageId = "FR", CurrencyId = "MDL", Name = "Leu moldave" },
                new CurrencyName { Id = "MDL_AR", LanguageId = "AR", CurrencyId = "MDL", Name = "ليو مولدوفي" },

                new CurrencyName { Id = "MGA_EN", LanguageId = "EN", CurrencyId = "MGA", Name = "Malagasy ariary" },
                new CurrencyName { Id = "MGA_FR", LanguageId = "FR", CurrencyId = "MGA", Name = "Ariary malgache" },
                new CurrencyName { Id = "MGA_AR", LanguageId = "AR", CurrencyId = "MGA", Name = "أرياري ملغاشي" },

                new CurrencyName { Id = "MKD_EN", LanguageId = "EN", CurrencyId = "MKD", Name = "Macedonian denar" },
                new CurrencyName { Id = "MKD_FR", LanguageId = "FR", CurrencyId = "MKD", Name = "Denar macédonien" },
                new CurrencyName { Id = "MKD_AR", LanguageId = "AR", CurrencyId = "MKD", Name = "دينار مقدوني" },

                new CurrencyName { Id = "MMK_EN", LanguageId = "EN", CurrencyId = "MMK", Name = "Myanmar kyat" },
                new CurrencyName { Id = "MMK_FR", LanguageId = "FR", CurrencyId = "MMK", Name = "Kyat myanmarais" },
                new CurrencyName { Id = "MMK_AR", LanguageId = "AR", CurrencyId = "MMK", Name = "كيات ميانماري" },

                new CurrencyName { Id = "MNT_EN", LanguageId = "EN", CurrencyId = "MNT", Name = "Mongolian tögrög" },
                new CurrencyName { Id = "MNT_FR", LanguageId = "FR", CurrencyId = "MNT", Name = "Tögrög mongol" },
                new CurrencyName { Id = "MNT_AR", LanguageId = "AR", CurrencyId = "MNT", Name = "توغروغ منغولي" },

                new CurrencyName { Id = "MOP_EN", LanguageId = "EN", CurrencyId = "MOP", Name = "Macanese pataca" },
                new CurrencyName { Id = "MOP_FR", LanguageId = "FR", CurrencyId = "MOP", Name = "Pataca macanaise" },
                new CurrencyName { Id = "MOP_AR", LanguageId = "AR", CurrencyId = "MOP", Name = "باتاكا ماكاوية" },

                new CurrencyName { Id = "MRU_EN", LanguageId = "EN", CurrencyId = "MRU", Name = "Mauritanian ouguiya" },
                new CurrencyName { Id = "MRU_FR", LanguageId = "FR", CurrencyId = "MRU", Name = "Ouguiya mauritanienne" },
                new CurrencyName { Id = "MRU_AR", LanguageId = "AR", CurrencyId = "MRU", Name = "أوقية موريتانية" },

                new CurrencyName { Id = "MUR_EN", LanguageId = "EN", CurrencyId = "MUR", Name = "Mauritian rupee" },
                new CurrencyName { Id = "MUR_FR", LanguageId = "FR", CurrencyId = "MUR", Name = "Roupie mauricienne" },
                new CurrencyName { Id = "MUR_AR", LanguageId = "AR", CurrencyId = "MUR", Name = "روبية موريشيوسية" },

                new CurrencyName { Id = "MVR_EN", LanguageId = "EN", CurrencyId = "MVR", Name = "Maldivian rufiyaa" },
                new CurrencyName { Id = "MVR_FR", LanguageId = "FR", CurrencyId = "MVR", Name = "Rufiyaa maldivienne" },
                new CurrencyName { Id = "MVR_AR", LanguageId = "AR", CurrencyId = "MVR", Name = "روفيه مالديفية" },

                new CurrencyName { Id = "MWK_EN", LanguageId = "EN", CurrencyId = "MWK", Name = "Malawian kwacha" },
                new CurrencyName { Id = "MWK_FR", LanguageId = "FR", CurrencyId = "MWK", Name = "Kwacha malawien" },
                new CurrencyName { Id = "MWK_AR", LanguageId = "AR", CurrencyId = "MWK", Name = "كواشا مالاوي" },

                new CurrencyName { Id = "MXN_EN", LanguageId = "EN", CurrencyId = "MXN", Name = "Mexican peso" },
                new CurrencyName { Id = "MXN_FR", LanguageId = "FR", CurrencyId = "MXN", Name = "Peso mexicain" },
                new CurrencyName { Id = "MXN_AR", LanguageId = "AR", CurrencyId = "MXN", Name = "بيزو مكسيكي" },

                new CurrencyName { Id = "MXV_EN", LanguageId = "EN", CurrencyId = "MXV", Name = "Mexican UDI" },
                new CurrencyName { Id = "MXV_FR", LanguageId = "FR", CurrencyId = "MXV", Name = "UDI mexicaine" },
                new CurrencyName { Id = "MXV_AR", LanguageId = "AR", CurrencyId = "MXV", Name = "وحدة UDI المكسيكية" },

                new CurrencyName { Id = "MYR_EN", LanguageId = "EN", CurrencyId = "MYR", Name = "Malaysian ringgit" },
                new CurrencyName { Id = "MYR_FR", LanguageId = "FR", CurrencyId = "MYR", Name = "Ringgit malaisien" },
                new CurrencyName { Id = "MYR_AR", LanguageId = "AR", CurrencyId = "MYR", Name = "رينغيت ماليزي" },

                new CurrencyName { Id = "MZN_EN", LanguageId = "EN", CurrencyId = "MZN", Name = "Mozambican metical" },
                new CurrencyName { Id = "MZN_FR", LanguageId = "FR", CurrencyId = "MZN", Name = "Metical mozambicain" },
                new CurrencyName { Id = "MZN_AR", LanguageId = "AR", CurrencyId = "MZN", Name = "متكال موزمبيقي" },

                // ===== N =====
                new CurrencyName { Id = "NAD_EN", LanguageId = "EN", CurrencyId = "NAD", Name = "Namibian dollar" },
                new CurrencyName { Id = "NAD_FR", LanguageId = "FR", CurrencyId = "NAD", Name = "Dollar namibien" },
                new CurrencyName { Id = "NAD_AR", LanguageId = "AR", CurrencyId = "NAD", Name = "دولار ناميبي" },

                new CurrencyName { Id = "NGN_EN", LanguageId = "EN", CurrencyId = "NGN", Name = "Nigerian naira" },
                new CurrencyName { Id = "NGN_FR", LanguageId = "FR", CurrencyId = "NGN", Name = "Naira nigérian" },
                new CurrencyName { Id = "NGN_AR", LanguageId = "AR", CurrencyId = "NGN", Name = "نايرا نيجيري" },

                new CurrencyName { Id = "NIO_EN", LanguageId = "EN", CurrencyId = "NIO", Name = "Nicaraguan córdoba" },
                new CurrencyName { Id = "NIO_FR", LanguageId = "FR", CurrencyId = "NIO", Name = "Córdoba nicaraguayen" },
                new CurrencyName { Id = "NIO_AR", LanguageId = "AR", CurrencyId = "NIO", Name = "كوردوبا نيكاراغوا" },

                new CurrencyName { Id = "NIK_EN", LanguageId = "EN", CurrencyId = "NOK", Name = "Norwegian krone" },
                new CurrencyName { Id = "NIK_FR", LanguageId = "FR", CurrencyId = "NOK", Name = "Couronne norvégienne" },
                new CurrencyName { Id = "NIK_AR", LanguageId = "AR", CurrencyId = "NOK", Name = "كرونة نرويجية" },

                new CurrencyName { Id = "NPR_EN", LanguageId = "EN", CurrencyId = "NPR", Name = "Nepalese rupee" },
                new CurrencyName { Id = "NPR_FR", LanguageId = "FR", CurrencyId = "NPR", Name = "Roupie népalaise" },
                new CurrencyName { Id = "NPR_AR", LanguageId = "AR", CurrencyId = "NPR", Name = "روبية نيبالية" },

                new CurrencyName { Id = "NZD_EN", LanguageId = "EN", CurrencyId = "NZD", Name = "New Zealand dollar" },
                new CurrencyName { Id = "NZD_FR", LanguageId = "FR", CurrencyId = "NZD", Name = "Dollar néo-zélandais" },
                new CurrencyName { Id = "NZD_AR", LanguageId = "AR", CurrencyId = "NZD", Name = "دولار نيوزيلندي" },

                // ===== O =====
                new CurrencyName { Id = "OMR_EN", LanguageId = "EN", CurrencyId = "OMR", Name = "Omani rial" },
                new CurrencyName { Id = "OMR_FR", LanguageId = "FR", CurrencyId = "OMR", Name = "Rial omanais" },
                new CurrencyName { Id = "OMR_AR", LanguageId = "AR", CurrencyId = "OMR", Name = "ريال عُماني" },

                 // ===== P =====
                new CurrencyName { Id = "PAB_EN", LanguageId = "EN", CurrencyId = "PAB", Name = "Panamanian balboa" },
                new CurrencyName { Id = "PAB_FR", LanguageId = "FR", CurrencyId = "PAB", Name = "Balboa panaméen" },
                new CurrencyName { Id = "PAB_AR", LanguageId = "AR", CurrencyId = "PAB", Name = "بالبوا بنمي" },

                new CurrencyName { Id = "PEN_EN", LanguageId = "EN", CurrencyId = "PEN", Name = "Peruvian sol" },
                new CurrencyName { Id = "PEN_FR", LanguageId = "FR", CurrencyId = "PEN", Name = "Sol péruvien" },
                new CurrencyName { Id = "PEN_AR", LanguageId = "AR", CurrencyId = "PEN", Name = "سول بيروفي" },

                new CurrencyName { Id = "PGK_EN", LanguageId = "EN", CurrencyId = "PGK", Name = "Papua New Guinean kina" },
                new CurrencyName { Id = "PGK_FR", LanguageId = "FR", CurrencyId = "PGK", Name = "Kina papou-néo-guinéen" },
                new CurrencyName { Id = "PGK_AR", LanguageId = "AR", CurrencyId = "PGK", Name = "كينا بابوا غينيا الجديدة" },

                new CurrencyName { Id = "PHP_EN", LanguageId = "EN", CurrencyId = "PHP", Name = "Philippine peso" },
                new CurrencyName { Id = "PHP_FR", LanguageId = "FR", CurrencyId = "PHP", Name = "Peso philippin" },
                new CurrencyName { Id = "PHP_AR", LanguageId = "AR", CurrencyId = "PHP", Name = "بيزو فلبيني" },

                new CurrencyName { Id = "PKR_EN", LanguageId = "EN", CurrencyId = "PKR", Name = "Pakistani rupee" },
                new CurrencyName { Id = "PKR_FR", LanguageId = "FR", CurrencyId = "PKR", Name = "Roupie pakistanaise" },
                new CurrencyName { Id = "PKR_AR", LanguageId = "AR", CurrencyId = "PKR", Name = "روبية باكستانية" },

                new CurrencyName { Id = "PLN_EN", LanguageId = "EN", CurrencyId = "PLN", Name = "Polish zloty" },
                new CurrencyName { Id = "PLN_FR", LanguageId = "FR", CurrencyId = "PLN", Name = "Zloty polonais" },
                new CurrencyName { Id = "PLN_AR", LanguageId = "AR", CurrencyId = "PLN", Name = "زلوتي بولندي" },

                new CurrencyName { Id = "PYG_EN", LanguageId = "EN", CurrencyId = "PYG", Name = "Paraguayan guarani" },
                new CurrencyName { Id = "PYG_FR", LanguageId = "FR", CurrencyId = "PYG", Name = "Guarani paraguayen" },
                new CurrencyName { Id = "PYG_AR", LanguageId = "AR", CurrencyId = "PYG", Name = "غواراني باراغواي" },

                // ===== Q =====
                new CurrencyName { Id = "QAR_EN", LanguageId = "EN", CurrencyId = "QAR", Name = "Qatari riyal" },
                new CurrencyName { Id = "QAR_FR", LanguageId = "FR", CurrencyId = "QAR", Name = "Riyal qatari" },
                new CurrencyName { Id = "QAR_AR", LanguageId = "AR", CurrencyId = "QAR", Name = "ريال قطري" },

                // ===== R =====
                new CurrencyName { Id = "RON_EN", LanguageId = "EN", CurrencyId = "RON", Name = "Romanian leu" },
                new CurrencyName { Id = "RON_FR", LanguageId = "FR", CurrencyId = "RON", Name = "Leu roumain" },
                new CurrencyName { Id = "RON_AR", LanguageId = "AR", CurrencyId = "RON", Name = "ليو روماني" },

                new CurrencyName { Id = "RSD_EN", LanguageId = "EN", CurrencyId = "RSD", Name = "Serbian dinar" },
                new CurrencyName { Id = "RSD_FR", LanguageId = "FR", CurrencyId = "RSD", Name = "Dinar serbe" },
                new CurrencyName { Id = "RSD_AR", LanguageId = "AR", CurrencyId = "RSD", Name = "دينار صربي" },

                new CurrencyName { Id = "RUB_EN", LanguageId = "EN", CurrencyId = "RUB", Name = "Russian ruble" },
                new CurrencyName { Id = "RUB_FR", LanguageId = "FR", CurrencyId = "RUB", Name = "Rouble russe" },
                new CurrencyName { Id = "RUB_AR", LanguageId = "AR", CurrencyId = "RUB", Name = "روبل روسي" },

                new CurrencyName { Id = "RWF_EN", LanguageId = "EN", CurrencyId = "RWF", Name = "Rwandan franc" },
                new CurrencyName { Id = "RWF_FR", LanguageId = "FR", CurrencyId = "RWF", Name = "Franc rwandais" },
                new CurrencyName { Id = "RWF_AR", LanguageId = "AR", CurrencyId = "RWF", Name = "فرنك رواندي" },

                // ===== S =====
                new CurrencyName { Id = "SAR_EN", LanguageId = "EN", CurrencyId = "SAR", Name = "Saudi riyal" },
                new CurrencyName { Id = "SAR_FR", LanguageId = "FR", CurrencyId = "SAR", Name = "Riyal saoudien" },
                new CurrencyName { Id = "SAR_AR", LanguageId = "AR", CurrencyId = "SAR", Name = "ريال سعودي" },

                new CurrencyName { Id = "SBD_EN", LanguageId = "EN", CurrencyId = "SBD", Name = "Solomon Islands dollar" },
                new CurrencyName { Id = "SBD_FR", LanguageId = "FR", CurrencyId = "SBD", Name = "Dollar des Îles Salomon" },
                new CurrencyName { Id = "SBD_AR", LanguageId = "AR", CurrencyId = "SBD", Name = "دولار جزر سليمان" },

                new CurrencyName { Id = "SCR_EN", LanguageId = "EN", CurrencyId = "SCR", Name = "Seychellois rupee" },
                new CurrencyName { Id = "SCR_FR", LanguageId = "FR", CurrencyId = "SCR", Name = "Roupie seychelloise" },
                new CurrencyName { Id = "SCR_AR", LanguageId = "AR", CurrencyId = "SCR", Name = "روبية سيشيلية" },

                new CurrencyName { Id = "SDG_EN", LanguageId = "EN", CurrencyId = "SDG", Name = "Sudanese pound" },
                new CurrencyName { Id = "SDG_FR", LanguageId = "FR", CurrencyId = "SDG", Name = "Livre soudanaise" },
                new CurrencyName { Id = "SDG_AR", LanguageId = "AR", CurrencyId = "SDG", Name = "جنيه سوداني" },

                new CurrencyName { Id = "SEK_EN", LanguageId = "EN", CurrencyId = "SEK", Name = "Swedish krona" },
                new CurrencyName { Id = "SEK_FR", LanguageId = "FR", CurrencyId = "SEK", Name = "Couronne suédoise" },
                new CurrencyName { Id = "SEK_AR", LanguageId = "AR", CurrencyId = "SEK", Name = "كرونة سويدية" },

                new CurrencyName { Id = "SGD_EN", LanguageId = "EN", CurrencyId = "SGD", Name = "Singapore dollar" },
                new CurrencyName { Id = "SGD_FR", LanguageId = "FR", CurrencyId = "SGD", Name = "Dollar de Singapour" },
                new CurrencyName { Id = "SGD_AR", LanguageId = "AR", CurrencyId = "SGD", Name = "دولار سنغافوري" },

                new CurrencyName { Id = "SHP_EN", LanguageId = "EN", CurrencyId = "SHP", Name = "Saint Helena pound" },
                new CurrencyName { Id = "SHP_FR", LanguageId = "FR", CurrencyId = "SHP", Name = "Livre de Sainte-Hélène" },
                new CurrencyName { Id = "SHP_AR", LanguageId = "AR", CurrencyId = "SHP", Name = "جنيه سانت هيليني" },

                new CurrencyName { Id = "SLE_EN", LanguageId = "EN", CurrencyId = "SLE", Name = "Sierra Leone leone" },
                new CurrencyName { Id = "SLE_FR", LanguageId = "FR", CurrencyId = "SLE", Name = "Leone sierra-léonais" },
                new CurrencyName { Id = "SLE_AR", LanguageId = "AR", CurrencyId = "SLE", Name = "ليون سيراليوني" },

                new CurrencyName { Id = "SOS_EN", LanguageId = "EN", CurrencyId = "SOS", Name = "Somali shilling" },
                new CurrencyName { Id = "SOS_FR", LanguageId = "FR", CurrencyId = "SOS", Name = "Shilling somalien" },
                new CurrencyName { Id = "SOS_AR", LanguageId = "AR", CurrencyId = "SOS", Name = "شلن صومالي" },

                new CurrencyName { Id = "SRD_EN", LanguageId = "EN", CurrencyId = "SRD", Name = "Surinamese dollar" },
                new CurrencyName { Id = "SRD_FR", LanguageId = "FR", CurrencyId = "SRD", Name = "Dollar surinamais" },
                new CurrencyName { Id = "SRD_AR", LanguageId = "AR", CurrencyId = "SRD", Name = "دولار سورينامي" },

                new CurrencyName { Id = "SSP_EN", LanguageId = "EN", CurrencyId = "SSP", Name = "South Sudanese pound" },
                new CurrencyName { Id = "SSP_FR", LanguageId = "FR", CurrencyId = "SSP", Name = "Livre sud-soudanaise" },
                new CurrencyName { Id = "SSP_AR", LanguageId = "AR", CurrencyId = "SSP", Name = "جنيه جنوب سوداني" },

                new CurrencyName { Id = "STN_EN", LanguageId = "EN", CurrencyId = "STN", Name = "São Tomé and Príncipe dobra" },
                new CurrencyName { Id = "STN_FR", LanguageId = "FR", CurrencyId = "STN", Name = "Dobra de Sao Tomé-et-Principe" },
                new CurrencyName { Id = "STN_AR", LanguageId = "AR", CurrencyId = "STN", Name = "دوبرا ساو تومي وبرينسيب" },

                new CurrencyName { Id = "SYP_EN", LanguageId = "EN", CurrencyId = "SYP", Name = "Syrian pound" },
                new CurrencyName { Id = "SYP_FR", LanguageId = "FR", CurrencyId = "SYP", Name = "Livre syrienne" },
                new CurrencyName { Id = "SYP_AR", LanguageId = "AR", CurrencyId = "SYP", Name = "ليرة سورية" },

                new CurrencyName { Id = "SZL_EN", LanguageId = "EN", CurrencyId = "SZL", Name = "Eswatini lilangeni" },
                new CurrencyName { Id = "SZL_FR", LanguageId = "FR", CurrencyId = "SZL", Name = "Lilangeni eswatinien" },
                new CurrencyName { Id = "SZL_AR", LanguageId = "AR", CurrencyId = "SZL", Name = "ليلانغيني إسواتيني" },

                // ===== T =====
                new CurrencyName { Id = "THB_EN", LanguageId = "EN", CurrencyId = "THB", Name = "Thai baht" },
                new CurrencyName { Id = "THB_FR", LanguageId = "FR", CurrencyId = "THB", Name = "Baht thaïlandais" },
                new CurrencyName { Id = "THB_AR", LanguageId = "AR", CurrencyId = "THB", Name = "بات تايلندي" },

                new CurrencyName { Id = "TJS_EN", LanguageId = "EN", CurrencyId = "TJS", Name = "Tajikistani somoni" },
                new CurrencyName { Id = "TJS_FR", LanguageId = "FR", CurrencyId = "TJS", Name = "Somoni tadjik" },
                new CurrencyName { Id = "TJS_AR", LanguageId = "AR", CurrencyId = "TJS", Name = "سوموني طاجيكي" },

                new CurrencyName { Id = "TMT_EN", LanguageId = "EN", CurrencyId = "TMT", Name = "Turkmenistan manat" },
                new CurrencyName { Id = "TMT_FR", LanguageId = "FR", CurrencyId = "TMT", Name = "Manat turkmène" },
                new CurrencyName { Id = "TMT_AR", LanguageId = "AR", CurrencyId = "TMT", Name = "مانات تركماني" },

                new CurrencyName { Id = "TND_EN", LanguageId = "EN", CurrencyId = "TND", Name = "Tunisian dinar" },
                new CurrencyName { Id = "TND_FR", LanguageId = "FR", CurrencyId = "TND", Name = "Dinar tunisien" },
                new CurrencyName { Id = "TND_AR", LanguageId = "AR", CurrencyId = "TND", Name = "دينار تونسي" },

                new CurrencyName { Id = "TOP_EN", LanguageId = "EN", CurrencyId = "TOP", Name = "Tongan paʻanga" },
                new CurrencyName { Id = "TOP_FR", LanguageId = "FR", CurrencyId = "TOP", Name = "Pa’anga tongien" },
                new CurrencyName { Id = "TOP_AR", LanguageId = "AR", CurrencyId = "TOP", Name = "بانغا تونغي" },

                new CurrencyName { Id = "TRY_EN", LanguageId = "EN", CurrencyId = "TRY", Name = "Turkish lira" },
                new CurrencyName { Id = "TRY_FR", LanguageId = "FR", CurrencyId = "TRY", Name = "Livre turque" },
                new CurrencyName { Id = "TRY_AR", LanguageId = "AR", CurrencyId = "TRY", Name = "ليرة تركية" },

                new CurrencyName { Id = "TTD_EN", LanguageId = "EN", CurrencyId = "TTD", Name = "Trinidad and Tobago dollar" },
                new CurrencyName { Id = "TTD_FR", LanguageId = "FR", CurrencyId = "TTD", Name = "Dollar trinidadien" },
                new CurrencyName { Id = "TTD_AR", LanguageId = "AR", CurrencyId = "TTD", Name = "دولار ترينيداد وتوباغو" },

                new CurrencyName { Id = "TWD_EN", LanguageId = "EN", CurrencyId = "TWD", Name = "New Taiwan dollar" },
                new CurrencyName { Id = "TWD_FR", LanguageId = "FR", CurrencyId = "TWD", Name = "Nouveau dollar taïwanais" },
                new CurrencyName { Id = "TWD_AR", LanguageId = "AR", CurrencyId = "TWD", Name = "دولار تايواني جديد" },

                new CurrencyName { Id = "TZS_EN", LanguageId = "EN", CurrencyId = "TZS", Name = "Tanzanian shilling" },
                new CurrencyName { Id = "TZS_FR", LanguageId = "FR", CurrencyId = "TZS", Name = "Shilling tanzanien" },
                new CurrencyName { Id = "TZS_AR", LanguageId = "AR", CurrencyId = "TZS", Name = "شلن تنزاني" },

                // ===== U =====
                new CurrencyName { Id = "UAH_EN", LanguageId = "EN", CurrencyId = "UAH", Name = "Ukrainian hryvnia" },
                new CurrencyName { Id = "UAH_FR", LanguageId = "FR", CurrencyId = "UAH", Name = "Hryvnia ukrainienne" },
                new CurrencyName { Id = "UAH_AR", LanguageId = "AR", CurrencyId = "UAH", Name = "هريفنيا أوكرانية" },

                new CurrencyName { Id = "UGX_EN", LanguageId = "EN", CurrencyId = "UGX", Name = "Ugandan shilling" },
                new CurrencyName { Id = "UGX_FR", LanguageId = "FR", CurrencyId = "UGX", Name = "Shilling ougandais" },
                new CurrencyName { Id = "UGX_AR", LanguageId = "AR", CurrencyId = "UGX", Name = "شلن أوغندي" },

                new CurrencyName { Id = "USD_EN", LanguageId = "EN", CurrencyId = "USD", Name = "United States dollar" },
                new CurrencyName { Id = "USD_FR", LanguageId = "FR", CurrencyId = "USD", Name = "Dollar des États-Unis" },
                new CurrencyName { Id = "USD_AR", LanguageId = "AR", CurrencyId = "USD", Name = "دولار أمريكي" },

                new CurrencyName { Id = "UYU_EN", LanguageId = "EN", CurrencyId = "UYU", Name = "Uruguayan peso" },
                new CurrencyName { Id = "UYU_FR", LanguageId = "FR", CurrencyId = "UYU", Name = "Peso uruguayen" },
                new CurrencyName { Id = "UYU_AR", LanguageId = "AR", CurrencyId = "UYU", Name = "بيزو أوروغواياني" },

                new CurrencyName { Id = "UZS_EN", LanguageId = "EN", CurrencyId = "UZS", Name = "Uzbekistani soʻm" },
                new CurrencyName { Id = "UZS_FR", LanguageId = "FR", CurrencyId = "UZS", Name = "Soum ouzbek" },
                new CurrencyName { Id = "UZS_AR", LanguageId = "AR", CurrencyId = "UZS", Name = "سوم أوزبكي" },

                // ===== V =====
                new CurrencyName { Id = "VES_EN", LanguageId = "EN", CurrencyId = "VES", Name = "Venezuelan bolívar" },
                new CurrencyName { Id = "VES_FR", LanguageId = "FR", CurrencyId = "VES", Name = "Bolívar vénézuélien" },
                new CurrencyName { Id = "VES_AR", LanguageId = "AR", CurrencyId = "VES", Name = "بوليفار فنزويلي" },

                new CurrencyName { Id = "VND_EN", LanguageId = "EN", CurrencyId = "VND", Name = "Vietnamese đồng" },
                new CurrencyName { Id = "VND_FR", LanguageId = "FR", CurrencyId = "VND", Name = "Dong vietnamien" },
                new CurrencyName { Id = "VND_AR", LanguageId = "AR", CurrencyId = "VND", Name = "دونغ فيتنامي" },

                new CurrencyName { Id = "VUV_EN", LanguageId = "EN", CurrencyId = "VUV", Name = "Vanuatu vatu" },
                new CurrencyName { Id = "VUV_FR", LanguageId = "FR", CurrencyId = "VUV", Name = "Vatu vanuatais" },
                new CurrencyName { Id = "VUV_AR", LanguageId = "AR", CurrencyId = "VUV", Name = "فاتو فانواتو" },

                // ===== W =====
                new CurrencyName { Id = "WST_EN", LanguageId = "EN", CurrencyId = "WST", Name = "Samoan tālā" },
                new CurrencyName { Id = "WST_FR", LanguageId = "FR", CurrencyId = "WST", Name = "Tālā samoan" },
                new CurrencyName { Id = "WST_AR", LanguageId = "AR", CurrencyId = "WST", Name = "تالا ساموا" },

                // ===== X (funds/units/metals + XCD) =====
                new CurrencyName { Id = "XAF_EN", LanguageId = "EN", CurrencyId = "XAF", Name = "CFA franc BEAC" },
                new CurrencyName { Id = "XAF_FR", LanguageId = "FR", CurrencyId = "XAF", Name = "Franc CFA BEAC" },
                new CurrencyName { Id = "XAF_AR", LanguageId = "AR", CurrencyId = "XAF", Name = "فرنك إفريقي (BEAC)" },

                new CurrencyName { Id = "XAG_EN", LanguageId = "EN", CurrencyId = "XAG", Name = "Silver (troy ounce)" },
                new CurrencyName { Id = "XAG_FR", LanguageId = "FR", CurrencyId = "XAG", Name = "Argent (once troy)" },
                new CurrencyName { Id = "XAG_AR", LanguageId = "AR", CurrencyId = "XAG", Name = "فضة (أونصة تروي)" },

                new CurrencyName { Id = "XAU_EN", LanguageId = "EN", CurrencyId = "XAU", Name = "Gold (troy ounce)" },
                new CurrencyName { Id = "XAU_FR", LanguageId = "FR", CurrencyId = "XAU", Name = "Or (once troy)" },
                new CurrencyName { Id = "XAU_AR", LanguageId = "AR", CurrencyId = "XAU", Name = "ذهب (أونصة تروي)" },

                new CurrencyName { Id = "XBA_EN", LanguageId = "EN", CurrencyId = "XBA", Name = "European composite unit" },
                new CurrencyName { Id = "XBA_FR", LanguageId = "FR", CurrencyId = "XBA", Name = "Unité composite européenne" },
                new CurrencyName { Id = "XBA_AR", LanguageId = "AR", CurrencyId = "XBA", Name = "وحدة مركبة أوروبية" },

                new CurrencyName { Id = "XBB_EN", LanguageId = "EN", CurrencyId = "XBB", Name = "European monetary unit" },
                new CurrencyName { Id = "XBB_FR", LanguageId = "FR", CurrencyId = "XBB", Name = "Unité monétaire européenne" },
                new CurrencyName { Id = "XBB_AR", LanguageId = "AR", CurrencyId = "XBB", Name = "وحدة نقدية أوروبية" },

                new CurrencyName { Id = "XBC_EN", LanguageId = "EN", CurrencyId = "XBC", Name = "European unit of account 9" },
                new CurrencyName { Id = "XBC_FR", LanguageId = "FR", CurrencyId = "XBC", Name = "Unité de compte européenne 9" },
                new CurrencyName { Id = "XBC_AR", LanguageId = "AR", CurrencyId = "XBC", Name = "وحدة حساب أوروبية 9" },

                new CurrencyName { Id = "XBD_EN", LanguageId = "EN", CurrencyId = "XBD", Name = "European unit of account 17" },
                new CurrencyName { Id = "XBD_FR", LanguageId = "FR", CurrencyId = "XBD", Name = "Unité de compte européenne 17" },
                new CurrencyName { Id = "XBD_AR", LanguageId = "AR", CurrencyId = "XBD", Name = "وحدة حساب أوروبية 17" },

                new CurrencyName { Id = "XCD_EN", LanguageId = "EN", CurrencyId = "XCD", Name = "East Caribbean dollar" },
                new CurrencyName { Id = "XCD_FR", LanguageId = "FR", CurrencyId = "XCD", Name = "Dollar des Caraïbes orientales" },
                new CurrencyName { Id = "XCD_AR", LanguageId = "AR", CurrencyId = "XCD", Name = "دولار شرق الكاريبي" },

                new CurrencyName { Id = "XCG_EN", LanguageId = "EN", CurrencyId = "XCG", Name = "CFA Franc BEAC" },
                new CurrencyName { Id = "XCG_FR", LanguageId = "FR", CurrencyId = "XCG", Name = "Franc CFA BEAC" },
                new CurrencyName { Id = "XCG_AR", LanguageId = "AR", CurrencyId = "XCG", Name = "فرنك إفريقي منطقة وسط أفريقيا" },

                new CurrencyName { Id = "XDR_EN", LanguageId = "EN", CurrencyId = "XDR", Name = "SDR (IMF)" },
                new CurrencyName { Id = "XDR_FR", LanguageId = "FR", CurrencyId = "XDR", Name = "DTS (FMI)" },
                new CurrencyName { Id = "XDR_AR", LanguageId = "AR", CurrencyId = "XDR", Name = "حقوق سحب خاصة (IMF)" },

                new CurrencyName { Id = "XOF_EN", LanguageId = "EN", CurrencyId = "XOF", Name = "CFA franc BCEAO" },
                new CurrencyName { Id = "XOF_FR", LanguageId = "FR", CurrencyId = "XOF", Name = "Franc CFA BCEAO" },
                new CurrencyName { Id = "XOF_AR", LanguageId = "AR", CurrencyId = "XOF", Name = "فرنك إفريقي (BCEAO)" },

                new CurrencyName { Id = "XPD_EN", LanguageId = "EN", CurrencyId = "XPD", Name = "Palladium (troy ounce)" },
                new CurrencyName { Id = "XPD_FR", LanguageId = "FR", CurrencyId = "XPD", Name = "Palladium (once troy)" },
                new CurrencyName { Id = "XPD_AR", LanguageId = "AR", CurrencyId = "XPD", Name = "بالاديوم (أونصة تروي)" },

                new CurrencyName { Id = "XPF_EN", LanguageId = "EN", CurrencyId = "XPF", Name = "CFP franc" },
                new CurrencyName { Id = "XPF_FR", LanguageId = "FR", CurrencyId = "XPF", Name = "Franc CFP" },
                new CurrencyName { Id = "XPF_AR", LanguageId = "AR", CurrencyId = "XPF", Name = "فرنك CFP" },

                new CurrencyName { Id = "XPT_EN", LanguageId = "EN", CurrencyId = "XPT", Name = "Platinum (troy ounce)" },
                new CurrencyName { Id = "XPT_FR", LanguageId = "FR", CurrencyId = "XPT", Name = "Platine (once troy)" },
                new CurrencyName { Id = "XPT_AR", LanguageId = "AR", CurrencyId = "XPT", Name = "بلاتين (أونصة تروي)" },

                new CurrencyName { Id = "XSU_EN", LanguageId = "EN", CurrencyId = "XSU", Name = "Sucre (ALBA unit)" },
                new CurrencyName { Id = "XSU_FR", LanguageId = "FR", CurrencyId = "XSU", Name = "Sucre (unité ALBA)" },
                new CurrencyName { Id = "XSU_AR", LanguageId = "AR", CurrencyId = "XSU", Name = "سوكر (وحدة ألبا)" },

                new CurrencyName { Id = "XTS_EN", LanguageId = "EN", CurrencyId = "XTS", Name = "Code for testing" },
                new CurrencyName { Id = "XTS_FR", LanguageId = "FR", CurrencyId = "XTS", Name = "Code pour tests" },
                new CurrencyName { Id = "XTS_AR", LanguageId = "AR", CurrencyId = "XTS", Name = "رمز للاختبار" },

                new CurrencyName { Id = "XUA_EN", LanguageId = "EN", CurrencyId = "XUA", Name = "ADB unit of account" },
                new CurrencyName { Id = "XUA_FR", LanguageId = "FR", CurrencyId = "XUA", Name = "Unité de compte BAD" },
                new CurrencyName { Id = "XUA_AR", LanguageId = "AR", CurrencyId = "XUA", Name = "وحدة حساب بنك التنمية الأفريقي" },

                new CurrencyName { Id = "XXX_EN", LanguageId = "EN", CurrencyId = "XXX", Name = "No currency" },
                new CurrencyName { Id = "XXX_FR", LanguageId = "FR", CurrencyId = "XXX", Name = "Aucune monnaie" },
                new CurrencyName { Id = "XXX_AR", LanguageId = "AR", CurrencyId = "XXX", Name = "بدون عملة" },

                // ===== Y =====
                new CurrencyName { Id = "YER_EN", LanguageId = "EN", CurrencyId = "YER", Name = "Yemeni rial" },
                new CurrencyName { Id = "YER_FR", LanguageId = "FR", CurrencyId = "YER", Name = "Rial yéménite" },
                new CurrencyName { Id = "YER_AR", LanguageId = "AR", CurrencyId = "YER", Name = "ريال يمني" },

                // ===== Z =====
                new CurrencyName { Id = "ZAR_EN", LanguageId = "EN", CurrencyId = "ZAR", Name = "South African rand" },
                new CurrencyName { Id = "ZAR_FR", LanguageId = "FR", CurrencyId = "ZAR", Name = "Rand sud-africain" },
                new CurrencyName { Id = "ZAR_AR", LanguageId = "AR", CurrencyId = "ZAR", Name = "راند جنوب أفريقي" },

                new CurrencyName { Id = "ZMW_EN", LanguageId = "EN", CurrencyId = "ZMW", Name = "Zambian kwacha" },
                new CurrencyName { Id = "ZMW_FR", LanguageId = "FR", CurrencyId = "ZMW", Name = "Kwacha zambien" },
                new CurrencyName { Id = "ZMW_AR", LanguageId = "AR", CurrencyId = "ZMW", Name = "كواشا زامبي" },

                new CurrencyName { Id = "ZWL_EN", LanguageId = "EN", CurrencyId = "ZWL", Name = "Zimbabwean dollar" },
                new CurrencyName { Id = "ZWL_FR", LanguageId = "FR", CurrencyId = "ZWL", Name = "Dollar zimbabwéen" },
                new CurrencyName { Id = "ZWL_AR", LanguageId = "AR", CurrencyId = "ZWL", Name = "دولار زيمبابوي" },
            };

        }
        public void Configure(EntityTypeBuilder<CurrencyName> builder)
        {
            builder.HasIndex(i => new { i.CurrencyId, i.LanguageId }).IsUnique();
            builder.HasData(GetWorldCurrencyNames());
        }
    }
}
