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
    internal class CountryNameConfiguration : IEntityTypeConfiguration<CountryName>
    {
        private static IEnumerable<CountryName> GetWorldCountryNames()
        {
            return new List<CountryName>
            {
                // ===== A =====
                new CountryName { Id = "AF_EN", LanguageId = "EN", CountryId = "AF", Name = "Afghanistan", Nationality = "Afghan" },
                new CountryName { Id = "AF_FR", LanguageId = "FR", CountryId = "AF", Name = "Afghanistan", Nationality = "Afghan" },
                new CountryName { Id = "AF_AR", LanguageId = "AR", CountryId = "AF", Name = "أفغانستان", Nationality = "أفغاني" },

                new CountryName { Id = "AL_EN", LanguageId = "EN", CountryId = "AL", Name = "Albania", Nationality = "Albanian" },
                new CountryName { Id = "AL_FR", LanguageId = "FR", CountryId = "AL", Name = "Albanie", Nationality = "Albanais" },
                new CountryName { Id = "AL_AR", LanguageId = "AR", CountryId = "AL", Name = "ألبانيا", Nationality = "ألباني" },

                new CountryName { Id = "AD_EN", LanguageId = "EN", CountryId = "AD", Name = "Andorra", Nationality = "Andorran" },
                new CountryName { Id = "AD_FR", LanguageId = "FR", CountryId = "AD", Name = "Andorre", Nationality = "Andorran" },
                new CountryName { Id = "AD_AR", LanguageId = "AR", CountryId = "AD", Name = "أندورا", Nationality = "أندوري" },

                new CountryName { Id = "AO_EN", LanguageId = "EN", CountryId = "AO", Name = "Angola", Nationality = "Angolan" },
                new CountryName { Id = "AO_FR", LanguageId = "FR", CountryId = "AO", Name = "Angola", Nationality = "Angolais" },
                new CountryName { Id = "AO_AR", LanguageId = "AR", CountryId = "AO", Name = "أنغولا", Nationality = "أنغولي" },

                new CountryName { Id = "AR_EN", LanguageId = "EN", CountryId = "AR", Name = "Argentina", Nationality = "Argentine" },
                new CountryName { Id = "AR_FR", LanguageId = "FR", CountryId = "AR", Name = "Argentine", Nationality = "Argentin" },
                new CountryName { Id = "AR_AR", LanguageId = "AR", CountryId = "AR", Name = "الأرجنتين", Nationality = "أرجنتيني" },

                new CountryName { Id = "AM_EN", LanguageId = "EN", CountryId = "AM", Name = "Armenia", Nationality = "Armenian" },
                new CountryName { Id = "AM_FR", LanguageId = "FR", CountryId = "AM", Name = "Arménie", Nationality = "Arménien" },
                new CountryName { Id = "AM_AR", LanguageId = "AR", CountryId = "AM", Name = "أرمينيا", Nationality = "أرميني" },

                new CountryName { Id = "AU_EN", LanguageId = "EN", CountryId = "AU", Name = "Australia", Nationality = "Australian" },
                new CountryName { Id = "AU_FR", LanguageId = "FR", CountryId = "AU", Name = "Australie", Nationality = "Australien" },
                new CountryName { Id = "AU_AR", LanguageId = "AR", CountryId = "AU", Name = "أستراليا", Nationality = "أسترالي" },

                new CountryName { Id = "AT_EN", LanguageId = "EN", CountryId = "AT", Name = "Austria", Nationality = "Austrian" },
                new CountryName { Id = "AT_FR", LanguageId = "FR", CountryId = "AT", Name = "Autriche", Nationality = "Autrichien" },
                new CountryName { Id = "AT_AR", LanguageId = "AR", CountryId = "AT", Name = "النمسا", Nationality = "نمساوي" },

                new CountryName { Id = "AZ_EN", LanguageId = "EN", CountryId = "AZ", Name = "Azerbaijan", Nationality = "Azerbaijani" },
                new CountryName { Id = "AZ_FR", LanguageId = "FR", CountryId = "AZ", Name = "Azerbaïdjan", Nationality = "Azerbaïdjanais" },
                new CountryName { Id = "AZ_AR", LanguageId = "AR", CountryId = "AZ", Name = "أذربيجان", Nationality = "أذربيجاني" },

                // ===== B =====
                new CountryName { Id = "BH_EN", LanguageId = "EN", CountryId = "BH", Name = "Bahrain", Nationality = "Bahraini" },
                new CountryName { Id = "BH_FR", LanguageId = "FR", CountryId = "BH", Name = "Bahreïn", Nationality = "Bahreïni" },
                new CountryName { Id = "BH_AR", LanguageId = "AR", CountryId = "BH", Name = "البحرين", Nationality = "بحريني" },

                new CountryName { Id = "BD_EN", LanguageId = "EN", CountryId = "BD", Name = "Bangladesh", Nationality = "Bangladeshi" },
                new CountryName { Id = "BD_FR", LanguageId = "FR", CountryId = "BD", Name = "Bangladesh", Nationality = "Bangladais" },
                new CountryName { Id = "BD_AR", LanguageId = "AR", CountryId = "BD", Name = "بنغلاديش", Nationality = "بنغلاديشي" },

                new CountryName { Id = "BE_EN", LanguageId = "EN", CountryId = "BE", Name = "Belgium", Nationality = "Belgian" },
                new CountryName { Id = "BE_FR", LanguageId = "FR", CountryId = "BE", Name = "Belgique", Nationality = "Belge" },
                new CountryName { Id = "BE_AR", LanguageId = "AR", CountryId = "BE", Name = "بلجيكا", Nationality = "بلجيكي" },

                new CountryName { Id = "BR_EN", LanguageId = "EN", CountryId = "BR", Name = "Brazil", Nationality = "Brazilian" },
                new CountryName { Id = "BR_FR", LanguageId = "FR", CountryId = "BR", Name = "Brésil", Nationality = "Brésilien" },
                new CountryName { Id = "BR_AR", LanguageId = "AR", CountryId = "BR", Name = "البرازيل", Nationality = "برازيلي" },

                new CountryName { Id = "BG_EN", LanguageId = "EN", CountryId = "BG", Name = "Bulgaria", Nationality = "Bulgarian" },
                new CountryName { Id = "BG_FR", LanguageId = "FR", CountryId = "BG", Name = "Bulgarie", Nationality = "Bulgare" },
                new CountryName { Id = "BG_AR", LanguageId = "AR", CountryId = "BG", Name = "بلغاريا", Nationality = "بلغاري" },

                // ===== C =====
                new CountryName { Id = "CA_EN", LanguageId = "EN", CountryId = "CA", Name = "Canada", Nationality = "Canadian" },
                new CountryName { Id = "CA_FR", LanguageId = "FR", CountryId = "CA", Name = "Canada", Nationality = "Canadien" },
                new CountryName { Id = "CA_AR", LanguageId = "AR", CountryId = "CA", Name = "كندا", Nationality = "كندي" },

                new CountryName { Id = "CN_EN", LanguageId = "EN", CountryId = "CN", Name = "China", Nationality = "Chinese" },
                new CountryName { Id = "CN_FR", LanguageId = "FR", CountryId = "CN", Name = "Chine", Nationality = "Chinois" },
                new CountryName { Id = "CN_AR", LanguageId = "AR", CountryId = "CN", Name = "الصين", Nationality = "صيني" },

                new CountryName { Id = "CL_EN", LanguageId = "EN", CountryId = "CL", Name = "Chile", Nationality = "Chilean" },
                new CountryName { Id = "CL_FR", LanguageId = "FR", CountryId = "CL", Name = "Chili", Nationality = "Chilien" },
                new CountryName { Id = "CL_AR", LanguageId = "AR", CountryId = "CL", Name = "تشيلي", Nationality = "تشيلي" },

                new CountryName { Id = "CO_EN", LanguageId = "EN", CountryId = "CO", Name = "Colombia", Nationality = "Colombian" },
                new CountryName { Id = "CO_FR", LanguageId = "FR", CountryId = "CO", Name = "Colombie", Nationality = "Colombien" },
                new CountryName { Id = "CO_AR", LanguageId = "AR", CountryId = "CO", Name = "كولومبيا", Nationality = "كولومبي" },

                new CountryName { Id = "CR_EN", LanguageId = "EN", CountryId = "CR", Name = "Costa Rica", Nationality = "Costa Rican" },
                new CountryName { Id = "CR_FR", LanguageId = "FR", CountryId = "CR", Name = "Costa Rica", Nationality = "Costaricien" },
                new CountryName { Id = "CR_AR", LanguageId = "AR", CountryId = "CR", Name = "كوستاريكا", Nationality = "كوستاريكي" },

                new CountryName { Id = "CU_EN", LanguageId = "EN", CountryId = "CU", Name = "Cuba", Nationality = "Cuban" },
                new CountryName { Id = "CU_FR", LanguageId = "FR", CountryId = "CU", Name = "Cuba", Nationality = "Cubain" },
                new CountryName { Id = "CU_AR", LanguageId = "AR", CountryId = "CU", Name = "كوبا", Nationality = "كوبي" },

                // ===== D =====
                new CountryName { Id = "DK_EN", LanguageId = "EN", CountryId = "DK", Name = "Denmark", Nationality = "Danish" },
                new CountryName { Id = "DK_FR", LanguageId = "FR", CountryId = "DK", Name = "Danemark", Nationality = "Danois" },
                new CountryName { Id = "DK_AR", LanguageId = "AR", CountryId = "DK", Name = "الدنمارك", Nationality = "دنماركي" },

                new CountryName { Id = "DJ_EN", LanguageId = "EN", CountryId = "DJ", Name = "Djibouti", Nationality = "Djiboutian" },
                new CountryName { Id = "DJ_FR", LanguageId = "FR", CountryId = "DJ", Name = "Djibouti", Nationality = "Djiboutien" },
                new CountryName { Id = "DJ_AR", LanguageId = "AR", CountryId = "DJ", Name = "جيبوتي", Nationality = "جيبوتي" },

                new CountryName { Id = "DO_EN", LanguageId = "EN", CountryId = "DO", Name = "Dominican Republic", Nationality = "Dominican" },
                new CountryName { Id = "DO_FR", LanguageId = "FR", CountryId = "DO", Name = "République dominicaine", Nationality = "Dominicain" },
                new CountryName { Id = "DO_AR", LanguageId = "AR", CountryId = "DO", Name = "جمهورية الدومينيكان", Nationality = "دومينيكاني" },

                new CountryName { Id = "DZ_EN", LanguageId = "EN", CountryId = "DZ", Name = "Algeria", Nationality = "Algerian" },
                new CountryName { Id = "DZ_FR", LanguageId = "FR", CountryId = "DZ", Name = "Algérie", Nationality = "Algérien" },
                new CountryName { Id = "DZ_AR", LanguageId = "AR", CountryId = "DZ", Name = "الجزائر", Nationality = "جزائري" },

                // ===== E =====
                new CountryName { Id = "EG_EN", LanguageId = "EN", CountryId = "EG", Name = "Egypt", Nationality = "Egyptian" },
                new CountryName { Id = "EG_FR", LanguageId = "FR", CountryId = "EG", Name = "Égypte", Nationality = "Égyptien" },
                new CountryName { Id = "EG_AR", LanguageId = "AR", CountryId = "EG", Name = "مصر", Nationality = "مصري" },

                new CountryName { Id = "EE_EN", LanguageId = "EN", CountryId = "EE", Name = "Estonia", Nationality = "Estonian" },
                new CountryName { Id = "EE_FR", LanguageId = "FR", CountryId = "EE", Name = "Estonie", Nationality = "Estonien" },
                new CountryName { Id = "EE_AR", LanguageId = "AR", CountryId = "EE", Name = "إستونيا", Nationality = "إستوني" },

                new CountryName { Id = "ET_EN", LanguageId = "EN", CountryId = "ET", Name = "Ethiopia", Nationality = "Ethiopian" },
                new CountryName { Id = "ET_FR", LanguageId = "FR", CountryId = "ET", Name = "Éthiopie", Nationality = "Éthiopien" },
                new CountryName { Id = "ET_AR", LanguageId = "AR", CountryId = "ET", Name = "إثيوبيا", Nationality = "إثيوبي" },

                // ===== F =====
                new CountryName { Id = "FI_EN", LanguageId = "EN", CountryId = "FI", Name = "Finland", Nationality = "Finnish" },
                new CountryName { Id = "FI_FR", LanguageId = "FR", CountryId = "FI", Name = "Finlande", Nationality = "Finlandais" },
                new CountryName { Id = "FI_AR", LanguageId = "AR", CountryId = "FI", Name = "فنلندا", Nationality = "فنلندي" },

                new CountryName { Id = "FR_EN", LanguageId = "EN", CountryId = "FR", Name = "France", Nationality = "French" },
                new CountryName { Id = "FR_FR", LanguageId = "FR", CountryId = "FR", Name = "France", Nationality = "Français" },
                new CountryName { Id = "FR_AR", LanguageId = "AR", CountryId = "FR", Name = "فرنسا", Nationality = "فرنسي" },

                new CountryName { Id = "FJ_EN", LanguageId = "EN", CountryId = "FJ", Name = "Fiji", Nationality = "Fijian" },
                new CountryName { Id = "FJ_FR", LanguageId = "FR", CountryId = "FJ", Name = "Fidji", Nationality = "Fidjien" },
                new CountryName { Id = "FJ_AR", LanguageId = "AR", CountryId = "FJ", Name = "فيجي", Nationality = "فيجي" },

                new CountryName { Id = "FO_EN", LanguageId = "EN", CountryId = "FO", Name = "Faroe Islands", Nationality = "Faroese" },
                new CountryName { Id = "FO_FR", LanguageId = "FR", CountryId = "FO", Name = "Îles Féroé", Nationality = "Féringien" },
                new CountryName { Id = "FO_AR", LanguageId = "AR", CountryId = "FO", Name = "جزر فارو", Nationality = "فاروي" },

                // ===== G =====
                new CountryName { Id = "GA_EN", LanguageId = "EN", CountryId = "GA", Name = "Gabon", Nationality = "Gabonese" },
                new CountryName { Id = "GA_FR", LanguageId = "FR", CountryId = "GA", Name = "Gabon", Nationality = "Gabonais" },
                new CountryName { Id = "GA_AR", LanguageId = "AR", CountryId = "GA", Name = "الغابون", Nationality = "غابوني" },

                new CountryName { Id = "GM_EN", LanguageId = "EN", CountryId = "GM", Name = "Gambia", Nationality = "Gambian" },
                new CountryName { Id = "GM_FR", LanguageId = "FR", CountryId = "GM", Name = "Gambie", Nationality = "Gambien" },
                new CountryName { Id = "GM_AR", LanguageId = "AR", CountryId = "GM", Name = "غامبيا", Nationality = "غامبي" },

                new CountryName { Id = "GE_EN", LanguageId = "EN", CountryId = "GE", Name = "Georgia", Nationality = "Georgian" },
                new CountryName { Id = "GE_FR", LanguageId = "FR", CountryId = "GE", Name = "Géorgie", Nationality = "Géorgien" },
                new CountryName { Id = "GE_AR", LanguageId = "AR", CountryId = "GE", Name = "جورجيا", Nationality = "جورجي" },

                new CountryName { Id = "DE_EN", LanguageId = "EN", CountryId = "DE", Name = "Germany", Nationality = "German" },
                new CountryName { Id = "DE_FR", LanguageId = "FR", CountryId = "DE", Name = "Allemagne", Nationality = "Allemand" },
                new CountryName { Id = "DE_AR", LanguageId = "AR", CountryId = "DE", Name = "ألمانيا", Nationality = "ألماني" },

                new CountryName { Id = "GH_EN", LanguageId = "EN", CountryId = "GH", Name = "Ghana", Nationality = "Ghanaian" },
                new CountryName { Id = "GH_FR", LanguageId = "FR", CountryId = "GH", Name = "Ghana", Nationality = "Ghanéen" },
                new CountryName { Id = "GH_AR", LanguageId = "AR", CountryId = "GH", Name = "غانا", Nationality = "غاني" },

                new CountryName { Id = "GR_EN", LanguageId = "EN", CountryId = "GR", Name = "Greece", Nationality = "Greek" },
                new CountryName { Id = "GR_FR", LanguageId = "FR", CountryId = "GR", Name = "Grèce", Nationality = "Grec" },
                new CountryName { Id = "GR_AR", LanguageId = "AR", CountryId = "GR", Name = "اليونان", Nationality = "يوناني" },

                new CountryName { Id = "GT_EN", LanguageId = "EN", CountryId = "GT", Name = "Guatemala", Nationality = "Guatemalan" },
                new CountryName { Id = "GT_FR", LanguageId = "FR", CountryId = "GT", Name = "Guatemala", Nationality = "Guatémaltèque" },
                new CountryName { Id = "GT_AR", LanguageId = "AR", CountryId = "GT", Name = "غواتيمالا", Nationality = "غواتيمالي" },

                new CountryName { Id = "GN_EN", LanguageId = "EN", CountryId = "GN", Name = "Guinea", Nationality = "Guinean" },
                new CountryName { Id = "GN_FR", LanguageId = "FR", CountryId = "GN", Name = "Guinée", Nationality = "Guinéen" },
                new CountryName { Id = "GN_AR", LanguageId = "AR", CountryId = "GN", Name = "غينيا", Nationality = "غيني" },

                new CountryName { Id = "GW_EN", LanguageId = "EN", CountryId = "GW", Name = "Guinea-Bissau", Nationality = "Bissau-Guinean" },
                new CountryName { Id = "GW_FR", LanguageId = "FR", CountryId = "GW", Name = "Guinée-Bissau", Nationality = "Bissau-Guinéen" },
                new CountryName { Id = "GW_AR", LanguageId = "AR", CountryId = "GW", Name = "غينيا بيساو", Nationality = "غيني بيساوي" },

                new CountryName { Id = "GY_EN", LanguageId = "EN", CountryId = "GY", Name = "Guyana", Nationality = "Guyanese" },
                new CountryName { Id = "GY_FR", LanguageId = "FR", CountryId = "GY", Name = "Guyana", Nationality = "Guyanien" },
                new CountryName { Id = "GY_AR", LanguageId = "AR", CountryId = "GY", Name = "غيانا", Nationality = "غياني" },

                // ===== H =====
                new CountryName { Id = "HT_EN", LanguageId = "EN", CountryId = "HT", Name = "Haiti", Nationality = "Haitian" },
                new CountryName { Id = "HT_FR", LanguageId = "FR", CountryId = "HT", Name = "Haïti", Nationality = "Haïtien" },
                new CountryName { Id = "HT_AR", LanguageId = "AR", CountryId = "HT", Name = "هايتي", Nationality = "هايتي" },

                new CountryName { Id = "HN_EN", LanguageId = "EN", CountryId = "HN", Name = "Honduras", Nationality = "Honduran" },
                new CountryName { Id = "HN_FR", LanguageId = "FR", CountryId = "HN", Name = "Honduras", Nationality = "Hondurien" },
                new CountryName { Id = "HN_AR", LanguageId = "AR", CountryId = "HN", Name = "هندوراس", Nationality = "هندوراسي" },

                new CountryName { Id = "HU_EN", LanguageId = "EN", CountryId = "HU", Name = "Hungary", Nationality = "Hungarian" },
                new CountryName { Id = "HU_FR", LanguageId = "FR", CountryId = "HU", Name = "Hongrie", Nationality = "Hongrois" },
                new CountryName { Id = "HU_AR", LanguageId = "AR", CountryId = "HU", Name = "المجر", Nationality = "مجري" },

                // ===== I =====
                new CountryName { Id = "IS_EN", LanguageId = "EN", CountryId = "IS", Name = "Iceland", Nationality = "Icelandic" },
                new CountryName { Id = "IS_FR", LanguageId = "FR", CountryId = "IS", Name = "Islande", Nationality = "Islandais" },
                new CountryName { Id = "IS_AR", LanguageId = "AR", CountryId = "IS", Name = "آيسلندا", Nationality = "آيسلندي" },

                new CountryName { Id = "IN_EN", LanguageId = "EN", CountryId = "IN", Name = "India", Nationality = "Indian" },
                new CountryName { Id = "IN_FR", LanguageId = "FR", CountryId = "IN", Name = "Inde", Nationality = "Indien" },
                new CountryName { Id = "IN_AR", LanguageId = "AR", CountryId = "IN", Name = "الهند", Nationality = "هندي" },

                new CountryName { Id = "ID_EN", LanguageId = "EN", CountryId = "ID", Name = "Indonesia", Nationality = "Indonesian" },
                new CountryName { Id = "ID_FR", LanguageId = "FR", CountryId = "ID", Name = "Indonésie", Nationality = "Indonésien" },
                new CountryName { Id = "ID_AR", LanguageId = "AR", CountryId = "ID", Name = "إندونيسيا", Nationality = "إندونيسي" },

                new CountryName { Id = "IR_EN", LanguageId = "EN", CountryId = "IR", Name = "Iran", Nationality = "Iranian" },
                new CountryName { Id = "IR_FR", LanguageId = "FR", CountryId = "IR", Name = "Iran", Nationality = "Iranien" },
                new CountryName { Id = "IR_AR", LanguageId = "AR", CountryId = "IR", Name = "إيران", Nationality = "إيراني" },

                new CountryName { Id = "IQ_EN", LanguageId = "EN", CountryId = "IQ", Name = "Iraq", Nationality = "Iraqi" },
                new CountryName { Id = "IQ_FR", LanguageId = "FR", CountryId = "IQ", Name = "Irak", Nationality = "Irakien" },
                new CountryName { Id = "IQ_AR", LanguageId = "AR", CountryId = "IQ", Name = "العراق", Nationality = "عراقي" },

                new CountryName { Id = "IE_EN", LanguageId = "EN", CountryId = "IE", Name = "Ireland", Nationality = "Irish" },
                new CountryName { Id = "IE_FR", LanguageId = "FR", CountryId = "IE", Name = "Irlande", Nationality = "Irlandais" },
                new CountryName { Id = "IE_AR", LanguageId = "AR", CountryId = "IE", Name = "أيرلندا", Nationality = "أيرلندي" },

                new CountryName { Id = "IT_EN", LanguageId = "EN", CountryId = "IT", Name = "Italy", Nationality = "Italian" },
                new CountryName { Id = "IT_FR", LanguageId = "FR", CountryId = "IT", Name = "Italie", Nationality = "Italien" },
                new CountryName { Id = "IT_AR", LanguageId = "AR", CountryId = "IT", Name = "إيطاليا", Nationality = "إيطالي" },

                // ===== J =====
                new CountryName { Id = "JM_EN", LanguageId = "EN", CountryId = "JM", Name = "Jamaica", Nationality = "Jamaican" },
                new CountryName { Id = "JM_FR", LanguageId = "FR", CountryId = "JM", Name = "Jamaïque", Nationality = "Jamaïcain" },
                new CountryName { Id = "JM_AR", LanguageId = "AR", CountryId = "JM", Name = "جامايكا", Nationality = "جامايكي" },

                new CountryName { Id = "JP_EN", LanguageId = "EN", CountryId = "JP", Name = "Japan", Nationality = "Japanese" },
                new CountryName { Id = "JP_FR", LanguageId = "FR", CountryId = "JP", Name = "Japon", Nationality = "Japonais" },
                new CountryName { Id = "JP_AR", LanguageId = "AR", CountryId = "JP", Name = "اليابان", Nationality = "ياباني" },

                new CountryName { Id = "JO_EN", LanguageId = "EN", CountryId = "JO", Name = "Jordan", Nationality = "Jordanian" },
                new CountryName { Id = "JO_FR", LanguageId = "FR", CountryId = "JO", Name = "Jordanie", Nationality = "Jordanien" },
                new CountryName { Id = "JO_AR", LanguageId = "AR", CountryId = "JO", Name = "الأردن", Nationality = "أردني" },

                // ===== K =====
                new CountryName { Id = "KZ_EN", LanguageId = "EN", CountryId = "KZ", Name = "Kazakhstan", Nationality = "Kazakh" },
                new CountryName { Id = "KZ_FR", LanguageId = "FR", CountryId = "KZ", Name = "Kazakhstan", Nationality = "Kazakh" },
                new CountryName { Id = "KZ_AR", LanguageId = "AR", CountryId = "KZ", Name = "كازاخستان", Nationality = "كازاخستاني" },

                new CountryName { Id = "KE_EN", LanguageId = "EN", CountryId = "KE", Name = "Kenya", Nationality = "Kenyan" },
                new CountryName { Id = "KE_FR", LanguageId = "FR", CountryId = "KE", Name = "Kenya", Nationality = "Kényan" },
                new CountryName { Id = "KE_AR", LanguageId = "AR", CountryId = "KE", Name = "كينيا", Nationality = "كيني" },

                new CountryName { Id = "KI_EN", LanguageId = "EN", CountryId = "KI", Name = "Kiribati", Nationality = "I-Kiribati" },
                new CountryName { Id = "KI_FR", LanguageId = "FR", CountryId = "KI", Name = "Kiribati", Nationality = "Kiribatien" },
                new CountryName { Id = "KI_AR", LanguageId = "AR", CountryId = "KI", Name = "كيريباس", Nationality = "كيريباسي" },

                new CountryName { Id = "KW_EN", LanguageId = "EN", CountryId = "KW", Name = "Kuwait", Nationality = "Kuwaiti" },
                new CountryName { Id = "KW_FR", LanguageId = "FR", CountryId = "KW", Name = "Koweït", Nationality = "Koweïtien" },
                new CountryName { Id = "KW_AR", LanguageId = "AR", CountryId = "KW", Name = "الكويت", Nationality = "كويتي" },

                new CountryName { Id = "KG_EN", LanguageId = "EN", CountryId = "KG", Name = "Kyrgyzstan", Nationality = "Kyrgyz" },
                new CountryName { Id = "KG_FR", LanguageId = "FR", CountryId = "KG", Name = "Kirghizistan", Nationality = "Kirghize" },
                new CountryName { Id = "KG_AR", LanguageId = "AR", CountryId = "KG", Name = "قرغيزستان", Nationality = "قرغيزي" },

                // ===== L =====
                new CountryName { Id = "LA_EN", LanguageId = "EN", CountryId = "LA", Name = "Laos", Nationality = "Lao" },
                new CountryName { Id = "LA_FR", LanguageId = "FR", CountryId = "LA", Name = "Laos", Nationality = "Laotien" },
                new CountryName { Id = "LA_AR", LanguageId = "AR", CountryId = "LA", Name = "لاوس", Nationality = "لاوسي" },

                new CountryName { Id = "LV_EN", LanguageId = "EN", CountryId = "LV", Name = "Latvia", Nationality = "Latvian" },
                new CountryName { Id = "LV_FR", LanguageId = "FR", CountryId = "LV", Name = "Lettonie", Nationality = "Letton" },
                new CountryName { Id = "LV_AR", LanguageId = "AR", CountryId = "LV", Name = "لاتفيا", Nationality = "لاتفي" },

                new CountryName { Id = "LB_EN", LanguageId = "EN", CountryId = "LB", Name = "Lebanon", Nationality = "Lebanese" },
                new CountryName { Id = "LB_FR", LanguageId = "FR", CountryId = "LB", Name = "Liban", Nationality = "Libanais" },
                new CountryName { Id = "LB_AR", LanguageId = "AR", CountryId = "LB", Name = "لبنان", Nationality = "لبناني" },

                new CountryName { Id = "LS_EN", LanguageId = "EN", CountryId = "LS", Name = "Lesotho", Nationality = "Basotho" },
                new CountryName { Id = "LS_FR", LanguageId = "FR", CountryId = "LS", Name = "Lesotho", Nationality = "Lésothien" },
                new CountryName { Id = "LS_AR", LanguageId = "AR", CountryId = "LS", Name = "ليسوتو", Nationality = "ليسوتي" },

                new CountryName { Id = "LR_EN", LanguageId = "EN", CountryId = "LR", Name = "Liberia", Nationality = "Liberian" },
                new CountryName { Id = "LR_FR", LanguageId = "FR", CountryId = "LR", Name = "Libéria", Nationality = "Libérien" },
                new CountryName { Id = "LR_AR", LanguageId = "AR", CountryId = "LR", Name = "ليبيريا", Nationality = "ليبيري" },

                new CountryName { Id = "LY_EN", LanguageId = "EN", CountryId = "LY", Name = "Libya", Nationality = "Libyan" },
                new CountryName { Id = "LY_FR", LanguageId = "FR", CountryId = "LY", Name = "Libye", Nationality = "Libyen" },
                new CountryName { Id = "LY_AR", LanguageId = "AR", CountryId = "LY", Name = "ليبيا", Nationality = "ليبي" },

                new CountryName { Id = "LI_EN", LanguageId = "EN", CountryId = "LI", Name = "Liechtenstein", Nationality = "Liechtensteiner" },
                new CountryName { Id = "LI_FR", LanguageId = "FR", CountryId = "LI", Name = "Liechtenstein", Nationality = "Liechtensteinois" },
                new CountryName { Id = "LI_AR", LanguageId = "AR", CountryId = "LI", Name = "ليختنشتاين", Nationality = "ليختنشتايني" },

                new CountryName { Id = "LT_EN", LanguageId = "EN", CountryId = "LT", Name = "Lithuania", Nationality = "Lithuanian" },
                new CountryName { Id = "LT_FR", LanguageId = "FR", CountryId = "LT", Name = "Lituanie", Nationality = "Lituanien" },
                new CountryName { Id = "LT_AR", LanguageId = "AR", CountryId = "LT", Name = "ليتوانيا", Nationality = "ليتواني" },

                new CountryName { Id = "LU_EN", LanguageId = "EN", CountryId = "LU", Name = "Luxembourg", Nationality = "Luxembourgish" },
                new CountryName { Id = "LU_FR", LanguageId = "FR", CountryId = "LU", Name = "Luxembourg", Nationality = "Luxembourgeois" },
                new CountryName { Id = "LU_AR", LanguageId = "AR", CountryId = "LU", Name = "لوكسمبورغ", Nationality = "لوكسمبورغي" },

                // ===== M =====
                new CountryName { Id = "MG_EN", LanguageId = "EN", CountryId = "MG", Name = "Madagascar", Nationality = "Malagasy" },
                new CountryName { Id = "MG_FR", LanguageId = "FR", CountryId = "MG", Name = "Madagascar", Nationality = "Malgache" },
                new CountryName { Id = "MG_AR", LanguageId = "AR", CountryId = "MG", Name = "مدغشقر", Nationality = "مدغشقري" },

                new CountryName { Id = "MW_EN", LanguageId = "EN", CountryId = "MW", Name = "Malawi", Nationality = "Malawian" },
                new CountryName { Id = "MW_FR", LanguageId = "FR", CountryId = "MW", Name = "Malawi", Nationality = "Malawien" },
                new CountryName { Id = "MW_AR", LanguageId = "AR", CountryId = "MW", Name = "ملاوي", Nationality = "ملاوي" },

                new CountryName { Id = "MY_EN", LanguageId = "EN", CountryId = "MY", Name = "Malaysia", Nationality = "Malaysian" },
                new CountryName { Id = "MY_FR", LanguageId = "FR", CountryId = "MY", Name = "Malaisie", Nationality = "Malaisien" },
                new CountryName { Id = "MY_AR", LanguageId = "AR", CountryId = "MY", Name = "ماليزيا", Nationality = "ماليزي" },

                new CountryName { Id = "MV_EN", LanguageId = "EN", CountryId = "MV", Name = "Maldives", Nationality = "Maldivian" },
                new CountryName { Id = "MV_FR", LanguageId = "FR", CountryId = "MV", Name = "Maldives", Nationality = "Maldivien" },
                new CountryName { Id = "MV_AR", LanguageId = "AR", CountryId = "MV", Name = "المالديف", Nationality = "مالديفي" },

                new CountryName { Id = "ML_EN", LanguageId = "EN", CountryId = "ML", Name = "Mali", Nationality = "Malian" },
                new CountryName { Id = "ML_FR", LanguageId = "FR", CountryId = "ML", Name = "Mali", Nationality = "Malien" },
                new CountryName { Id = "ML_AR", LanguageId = "AR", CountryId = "ML", Name = "مالي", Nationality = "مالي" },

                new CountryName { Id = "MT_EN", LanguageId = "EN", CountryId = "MT", Name = "Malta", Nationality = "Maltese" },
                new CountryName { Id = "MT_FR", LanguageId = "FR", CountryId = "MT", Name = "Malte", Nationality = "Maltais" },
                new CountryName { Id = "MT_AR", LanguageId = "AR", CountryId = "MT", Name = "مالطا", Nationality = "مالطي" },

                new CountryName { Id = "MH_EN", LanguageId = "EN", CountryId = "MH", Name = "Marshall Islands", Nationality = "Marshallese" },
                new CountryName { Id = "MH_FR", LanguageId = "FR", CountryId = "MH", Name = "Îles Marshall", Nationality = "Marshallais" },
                new CountryName { Id = "MH_AR", LanguageId = "AR", CountryId = "MH", Name = "جزر مارشال", Nationality = "مارشالي" },

                new CountryName { Id = "MR_EN", LanguageId = "EN", CountryId = "MR", Name = "Mauritania", Nationality = "Mauritanian" },
                new CountryName { Id = "MR_FR", LanguageId = "FR", CountryId = "MR", Name = "Mauritanie", Nationality = "Mauritanien" },
                new CountryName { Id = "MR_AR", LanguageId = "AR", CountryId = "MR", Name = "موريتانيا", Nationality = "موريتاني" },

                new CountryName { Id = "MU_EN", LanguageId = "EN", CountryId = "MU", Name = "Mauritius", Nationality = "Mauritian" },
                new CountryName { Id = "MU_FR", LanguageId = "FR", CountryId = "MU", Name = "Maurice", Nationality = "Mauricien" },
                new CountryName { Id = "MU_AR", LanguageId = "AR", CountryId = "MU", Name = "موريشيوس", Nationality = "موريشي" },

                new CountryName { Id = "MX_EN", LanguageId = "EN", CountryId = "MX", Name = "Mexico", Nationality = "Mexican" },
                new CountryName { Id = "MX_FR", LanguageId = "FR", CountryId = "MX", Name = "Mexique", Nationality = "Mexicain" },
                new CountryName { Id = "MX_AR", LanguageId = "AR", CountryId = "MX", Name = "المكسيك", Nationality = "مكسيكي" },

                new CountryName { Id = "FM_EN", LanguageId = "EN", CountryId = "FM", Name = "Micronesia", Nationality = "Micronesian" },
                new CountryName { Id = "FM_FR", LanguageId = "FR", CountryId = "FM", Name = "Micronésie", Nationality = "Micronésien" },
                new CountryName { Id = "FM_AR", LanguageId = "AR", CountryId = "FM", Name = "ميكرونيزيا", Nationality = "ميكرونيزي" },

                new CountryName { Id = "MD_EN", LanguageId = "EN", CountryId = "MD", Name = "Moldova", Nationality = "Moldovan" },
                new CountryName { Id = "MD_FR", LanguageId = "FR", CountryId = "MD", Name = "Moldavie", Nationality = "Moldave" },
                new CountryName { Id = "MD_AR", LanguageId = "AR", CountryId = "MD", Name = "مولدوفا", Nationality = "مولدوفي" },

                new CountryName { Id = "MC_EN", LanguageId = "EN", CountryId = "MC", Name = "Monaco", Nationality = "Monegasque" },
                new CountryName { Id = "MC_FR", LanguageId = "FR", CountryId = "MC", Name = "Monaco", Nationality = "Monégasque" },
                new CountryName { Id = "MC_AR", LanguageId = "AR", CountryId = "MC", Name = "موناكو", Nationality = "موناكي" },

                new CountryName { Id = "MN_EN", LanguageId = "EN", CountryId = "MN", Name = "Mongolia", Nationality = "Mongolian" },
                new CountryName { Id = "MN_FR", LanguageId = "FR", CountryId = "MN", Name = "Mongolie", Nationality = "Mongol" },
                new CountryName { Id = "MN_AR", LanguageId = "AR", CountryId = "MN", Name = "منغوليا", Nationality = "منغولي" },

                new CountryName { Id = "ME_EN", LanguageId = "EN", CountryId = "ME", Name = "Montenegro", Nationality = "Montenegrin" },
                new CountryName { Id = "ME_FR", LanguageId = "FR", CountryId = "ME", Name = "Monténégro", Nationality = "Monténégrin" },
                new CountryName { Id = "ME_AR", LanguageId = "AR", CountryId = "ME", Name = "الجبل الأسود", Nationality = "مونتينيغري" },

                new CountryName { Id = "MA_EN", LanguageId = "EN", CountryId = "MA", Name = "Morocco", Nationality = "Moroccan" },
                new CountryName { Id = "MA_FR", LanguageId = "FR", CountryId = "MA", Name = "Maroc", Nationality = "Marocain" },
                new CountryName { Id = "MA_AR", LanguageId = "AR", CountryId = "MA", Name = "المغرب", Nationality = "مغربي" },

                new CountryName { Id = "MZ_EN", LanguageId = "EN", CountryId = "MZ", Name = "Mozambique", Nationality = "Mozambican" },
                new CountryName { Id = "MZ_FR", LanguageId = "FR", CountryId = "MZ", Name = "Mozambique", Nationality = "Mozambicain" },
                new CountryName { Id = "MZ_AR", LanguageId = "AR", CountryId = "MZ", Name = "موزمبيق", Nationality = "موزمبيقي" },

                new CountryName { Id = "MM_EN", LanguageId = "EN", CountryId = "MM", Name = "Myanmar", Nationality = "Burmese" },
                new CountryName { Id = "MM_FR", LanguageId = "FR", CountryId = "MM", Name = "Myanmar", Nationality = "Birman" },
                new CountryName { Id = "MM_AR", LanguageId = "AR", CountryId = "MM", Name = "ميانمار", Nationality = "بورمي" },

                // ===== N =====
                new CountryName { Id = "NA_EN", LanguageId = "EN", CountryId = "NA", Name = "Namibia", Nationality = "Namibian" },
                new CountryName { Id = "NA_FR", LanguageId = "FR", CountryId = "NA", Name = "Namibie", Nationality = "Namibien" },
                new CountryName { Id = "NA_AR", LanguageId = "AR", CountryId = "NA", Name = "ناميبيا", Nationality = "ناميبي" },

                new CountryName { Id = "NP_EN", LanguageId = "EN", CountryId = "NP", Name = "Nepal", Nationality = "Nepali" },
                new CountryName { Id = "NP_FR", LanguageId = "FR", CountryId = "NP", Name = "Népal", Nationality = "Népalais" },
                new CountryName { Id = "NP_AR", LanguageId = "AR", CountryId = "NP", Name = "نيبال", Nationality = "نيبالي" },

                new CountryName { Id = "NL_EN", LanguageId = "EN", CountryId = "NL", Name = "Netherlands", Nationality = "Dutch" },
                new CountryName { Id = "NL_FR", LanguageId = "FR", CountryId = "NL", Name = "Pays-Bas", Nationality = "Néerlandais" },
                new CountryName { Id = "NL_AR", LanguageId = "AR", CountryId = "NL", Name = "هولندا", Nationality = "هولندي" },

                new CountryName { Id = "NZ_EN", LanguageId = "EN", CountryId = "NZ", Name = "New Zealand", Nationality = "New Zealander" },
                new CountryName { Id = "NZ_FR", LanguageId = "FR", CountryId = "NZ", Name = "Nouvelle-Zélande", Nationality = "Néo-Zélandais" },
                new CountryName { Id = "NZ_AR", LanguageId = "AR", CountryId = "NZ", Name = "نيوزيلندا", Nationality = "نيوزيلندي" },

                new CountryName { Id = "NI_EN", LanguageId = "EN", CountryId = "NI", Name = "Nicaragua", Nationality = "Nicaraguan" },
                new CountryName { Id = "NI_FR", LanguageId = "FR", CountryId = "NI", Name = "Nicaragua", Nationality = "Nicaraguayen" },
                new CountryName { Id = "NI_AR", LanguageId = "AR", CountryId = "NI", Name = "نيكاراغوا", Nationality = "نيكاراغوي" },

                new CountryName { Id = "NG_EN", LanguageId = "EN", CountryId = "NG", Name = "Nigeria", Nationality = "Nigerian" },
                new CountryName { Id = "NG_FR", LanguageId = "FR", CountryId = "NG", Name = "Nigéria", Nationality = "Nigérian" },
                new CountryName { Id = "NG_AR", LanguageId = "AR", CountryId = "NG", Name = "نيجيريا", Nationality = "نيجيري" },

                new CountryName { Id = "NO_EN", LanguageId = "EN", CountryId = "NO", Name = "Norway", Nationality = "Norwegian" },
                new CountryName { Id = "NO_FR", LanguageId = "FR", CountryId = "NO", Name = "Norvège", Nationality = "Norvégien" },
                new CountryName { Id = "NO_AR", LanguageId = "AR", CountryId = "NO", Name = "النرويج", Nationality = "نرويجي" },

                // ===== O =====
                new CountryName { Id = "OM_EN", LanguageId = "EN", CountryId = "OM", Name = "Oman", Nationality = "Omani" },
                new CountryName { Id = "OM_FR", LanguageId = "FR", CountryId = "OM", Name = "Oman", Nationality = "Omanais" },
                new CountryName { Id = "OM_AR", LanguageId = "AR", CountryId = "OM", Name = "عمان", Nationality = "عماني" },

                // ===== P =====
                new CountryName { Id = "PK_EN", LanguageId = "EN", CountryId = "PK", Name = "Pakistan", Nationality = "Pakistani" },
                new CountryName { Id = "PK_FR", LanguageId = "FR", CountryId = "PK", Name = "Pakistan", Nationality = "Pakistanais" },
                new CountryName { Id = "PK_AR", LanguageId = "AR", CountryId = "PK", Name = "باكستان", Nationality = "باكستاني" },

                new CountryName { Id = "PW_EN", LanguageId = "EN", CountryId = "PW", Name = "Palau", Nationality = "Palauan" },
                new CountryName { Id = "PW_FR", LanguageId = "FR", CountryId = "PW", Name = "Palaos", Nationality = "Paluan" },
                new CountryName { Id = "PW_AR", LanguageId = "AR", CountryId = "PW", Name = "بالاو", Nationality = "بالاوي" },

                new CountryName { Id = "PS_EN", LanguageId = "EN", CountryId = "PS", Name = "Palestine", Nationality = "Palestinian" },
                new CountryName { Id = "PS_FR", LanguageId = "FR", CountryId = "PS", Name = "Palestine", Nationality = "Palestinien" },
                new CountryName { Id = "PS_AR", LanguageId = "AR", CountryId = "PS", Name = "فلسطين", Nationality = "فلسطيني" },

                new CountryName { Id = "PA_EN", LanguageId = "EN", CountryId = "PA", Name = "Panama", Nationality = "Panamanian" },
                new CountryName { Id = "PA_FR", LanguageId = "FR", CountryId = "PA", Name = "Panama", Nationality = "Panaméen" },
                new CountryName { Id = "PA_AR", LanguageId = "AR", CountryId = "PA", Name = "بنما", Nationality = "بنمي" },

                new CountryName { Id = "PG_EN", LanguageId = "EN", CountryId = "PG", Name = "Papua New Guinea", Nationality = "Papua New Guinean" },
                new CountryName { Id = "PG_FR", LanguageId = "FR", CountryId = "PG", Name = "Papouasie-Nouvelle-Guinée", Nationality = "Papou" },
                new CountryName { Id = "PG_AR", LanguageId = "AR", CountryId = "PG", Name = "بابوا غينيا الجديدة", Nationality = "بابوي" },

                new CountryName { Id = "PY_EN", LanguageId = "EN", CountryId = "PY", Name = "Paraguay", Nationality = "Paraguayan" },
                new CountryName { Id = "PY_FR", LanguageId = "FR", CountryId = "PY", Name = "Paraguay", Nationality = "Paraguayen" },
                new CountryName { Id = "PY_AR", LanguageId = "AR", CountryId = "PY", Name = "باراغواي", Nationality = "باراغواياني" },

                new CountryName { Id = "PE_EN", LanguageId = "EN", CountryId = "PE", Name = "Peru", Nationality = "Peruvian" },
                new CountryName { Id = "PE_FR", LanguageId = "FR", CountryId = "PE", Name = "Pérou", Nationality = "Péruvien" },
                new CountryName { Id = "PE_AR", LanguageId = "AR", CountryId = "PE", Name = "بيرو", Nationality = "بيروفي" },

                new CountryName { Id = "PH_EN", LanguageId = "EN", CountryId = "PH", Name = "Philippines", Nationality = "Filipino" },
                new CountryName { Id = "PH_FR", LanguageId = "FR", CountryId = "PH", Name = "Philippines", Nationality = "Philippin" },
                new CountryName { Id = "PH_AR", LanguageId = "AR", CountryId = "PH", Name = "الفلبين", Nationality = "فلبيني" },

                new CountryName { Id = "PL_EN", LanguageId = "EN", CountryId = "PL", Name = "Poland", Nationality = "Polish" },
                new CountryName { Id = "PL_FR", LanguageId = "FR", CountryId = "PL", Name = "Pologne", Nationality = "Polonais" },
                new CountryName { Id = "PL_AR", LanguageId = "AR", CountryId = "PL", Name = "بولندا", Nationality = "بولندي" },

                new CountryName { Id = "PT_EN", LanguageId = "EN", CountryId = "PT", Name = "Portugal", Nationality = "Portuguese" },
                new CountryName { Id = "PT_FR", LanguageId = "FR", CountryId = "PT", Name = "Portugal", Nationality = "Portugais" },
                new CountryName { Id = "PT_AR", LanguageId = "AR", CountryId = "PT", Name = "البرتغال", Nationality = "برتغالي" },

                // ===== Q =====
                new CountryName { Id = "QA_EN", LanguageId = "EN", CountryId = "QA", Name = "Qatar", Nationality = "Qatari" },
                new CountryName { Id = "QA_FR", LanguageId = "FR", CountryId = "QA", Name = "Qatar", Nationality = "Qatari" },
                new CountryName { Id = "QA_AR", LanguageId = "AR", CountryId = "QA", Name = "قطر", Nationality = "قطري" },

                // ===== R =====
                new CountryName { Id = "RO_EN", LanguageId = "EN", CountryId = "RO", Name = "Romania", Nationality = "Romanian" },
                new CountryName { Id = "RO_FR", LanguageId = "FR", CountryId = "RO", Name = "Roumanie", Nationality = "Roumain" },
                new CountryName { Id = "RO_AR", LanguageId = "AR", CountryId = "RO", Name = "رومانيا", Nationality = "روماني" },

                new CountryName { Id = "RU_EN", LanguageId = "EN", CountryId = "RU", Name = "Russia", Nationality = "Russian" },
                new CountryName { Id = "RU_FR", LanguageId = "FR", CountryId = "RU", Name = "Russie", Nationality = "Russe" },
                new CountryName { Id = "RU_AR", LanguageId = "AR", CountryId = "RU", Name = "روسيا", Nationality = "روسي" },

                new CountryName { Id = "RW_EN", LanguageId = "EN", CountryId = "RW", Name = "Rwanda", Nationality = "Rwandan" },
                new CountryName { Id = "RW_FR", LanguageId = "FR", CountryId = "RW", Name = "Rwanda", Nationality = "Rwandais" },
                new CountryName { Id = "RW_AR", LanguageId = "AR", CountryId = "RW", Name = "رواندا", Nationality = "رواندي" },

                // ===== S =====
                new CountryName { Id = "KN_EN", LanguageId = "EN", CountryId = "KN", Name = "Saint Kitts and Nevis", Nationality = "Kittitian / Nevisian" },
                new CountryName { Id = "KN_FR", LanguageId = "FR", CountryId = "KN", Name = "Saint-Christophe-et-Niévès", Nationality = "Kittitien / Névicien" },
                new CountryName { Id = "KN_AR", LanguageId = "AR", CountryId = "KN", Name = "سانت كيتس ونيفيس", Nationality = "نيفيسي" },

                new CountryName { Id = "LC_EN", LanguageId = "EN", CountryId = "LC", Name = "Saint Lucia", Nationality = "Saint Lucian" },
                new CountryName { Id = "LC_FR", LanguageId = "FR", CountryId = "LC", Name = "Sainte-Lucie", Nationality = "Saint-Lucien" },
                new CountryName { Id = "LC_AR", LanguageId = "AR", CountryId = "LC", Name = "سانت لوسيا", Nationality = "لوسياني" },

                new CountryName { Id = "VC_EN", LanguageId = "EN", CountryId = "VC", Name = "Saint Vincent and the Grenadines", Nationality = "Vincentian" },
                new CountryName { Id = "VC_FR", LanguageId = "FR", CountryId = "VC", Name = "Saint-Vincent-et-les-Grenadines", Nationality = "Saint-Vincentais" },
                new CountryName { Id = "VC_AR", LanguageId = "AR", CountryId = "VC", Name = "سانت فنسنت والغرينادين", Nationality = "فنسنتي" },

                new CountryName { Id = "WS_EN", LanguageId = "EN", CountryId = "WS", Name = "Samoa", Nationality = "Samoan" },
                new CountryName { Id = "WS_FR", LanguageId = "FR", CountryId = "WS", Name = "Samoa", Nationality = "Samoan" },
                new CountryName { Id = "WS_AR", LanguageId = "AR", CountryId = "WS", Name = "ساموا", Nationality = "ساموي" },

                new CountryName { Id = "SM_EN", LanguageId = "EN", CountryId = "SM", Name = "San Marino", Nationality = "Sammarinese" },
                new CountryName { Id = "SM_FR", LanguageId = "FR", CountryId = "SM", Name = "Saint-Marin", Nationality = "Saint-Marinais" },
                new CountryName { Id = "SM_AR", LanguageId = "AR", CountryId = "SM", Name = "سان مارينو", Nationality = "ماريني" },

                new CountryName { Id = "ST_EN", LanguageId = "EN", CountryId = "ST", Name = "São Tomé and Príncipe", Nationality = "São Toméan" },
                new CountryName { Id = "ST_FR", LanguageId = "FR", CountryId = "ST", Name = "Sao Tomé-et-Principe", Nationality = "Santoméen" },
                new CountryName { Id = "ST_AR", LanguageId = "AR", CountryId = "ST", Name = "ساو تومي وبرينسيب", Nationality = "ساو تومي" },

                new CountryName { Id = "SA_EN", LanguageId = "EN", CountryId = "SA", Name = "Saudi Arabia", Nationality = "Saudi" },
                new CountryName { Id = "SA_FR", LanguageId = "FR", CountryId = "SA", Name = "Arabie saoudite", Nationality = "Saoudien" },
                new CountryName { Id = "SA_AR", LanguageId = "AR", CountryId = "SA", Name = "السعودية", Nationality = "سعودي" },

                new CountryName { Id = "SN_EN", LanguageId = "EN", CountryId = "SN", Name = "Senegal", Nationality = "Senegalese" },
                new CountryName { Id = "SN_FR", LanguageId = "FR", CountryId = "SN", Name = "Sénégal", Nationality = "Sénégalais" },
                new CountryName { Id = "SN_AR", LanguageId = "AR", CountryId = "SN", Name = "السنغال", Nationality = "سنغالي" },

                new CountryName { Id = "RS_EN", LanguageId = "EN", CountryId = "RS", Name = "Serbia", Nationality = "Serbian" },
                new CountryName { Id = "RS_FR", LanguageId = "FR", CountryId = "RS", Name = "Serbie", Nationality = "Serbe" },
                new CountryName { Id = "RS_AR", LanguageId = "AR", CountryId = "RS", Name = "صربيا", Nationality = "صربي" },

                new CountryName { Id = "SC_EN", LanguageId = "EN", CountryId = "SC", Name = "Seychelles", Nationality = "Seychellois" },
                new CountryName { Id = "SC_FR", LanguageId = "FR", CountryId = "SC", Name = "Seychelles", Nationality = "Seychellois" },
                new CountryName { Id = "SC_AR", LanguageId = "AR", CountryId = "SC", Name = "سيشل", Nationality = "سيشلي" },

                new CountryName { Id = "SL_EN", LanguageId = "EN", CountryId = "SL", Name = "Sierra Leone", Nationality = "Sierra Leonean" },
                new CountryName { Id = "SL_FR", LanguageId = "FR", CountryId = "SL", Name = "Sierra Leone", Nationality = "Sierra-Léonais" },
                new CountryName { Id = "SL_AR", LanguageId = "AR", CountryId = "SL", Name = "سيراليون", Nationality = "سيراليوني" },

                new CountryName { Id = "SG_EN", LanguageId = "EN", CountryId = "SG", Name = "Singapore", Nationality = "Singaporean" },
                new CountryName { Id = "SG_FR", LanguageId = "FR", CountryId = "SG", Name = "Singapour", Nationality = "Singapourien" },
                new CountryName { Id = "SG_AR", LanguageId = "AR", CountryId = "SG", Name = "سنغافورة", Nationality = "سنغافوري" },

                new CountryName { Id = "SK_EN", LanguageId = "EN", CountryId = "SK", Name = "Slovakia", Nationality = "Slovak" },
                new CountryName { Id = "SK_FR", LanguageId = "FR", CountryId = "SK", Name = "Slovaquie", Nationality = "Slovaque" },
                new CountryName { Id = "SK_AR", LanguageId = "AR", CountryId = "SK", Name = "سلوفاكيا", Nationality = "سلوفاكي" },

                new CountryName { Id = "SI_EN", LanguageId = "EN", CountryId = "SI", Name = "Slovenia", Nationality = "Slovenian" },
                new CountryName { Id = "SI_FR", LanguageId = "FR", CountryId = "SI", Name = "Slovénie", Nationality = "Slovène" },
                new CountryName { Id = "SI_AR", LanguageId = "AR", CountryId = "SI", Name = "سلوفينيا", Nationality = "سلوفيني" },

                new CountryName { Id = "SO_EN", LanguageId = "EN", CountryId = "SO", Name = "Somalia", Nationality = "Somali" },
                new CountryName { Id = "SO_FR", LanguageId = "FR", CountryId = "SO", Name = "Somalie", Nationality = "Somalien" },
                new CountryName { Id = "SO_AR", LanguageId = "AR", CountryId = "SO", Name = "الصومال", Nationality = "صومالي" },

                new CountryName { Id = "ZA_EN", LanguageId = "EN", CountryId = "ZA", Name = "South Africa", Nationality = "South African" },
                new CountryName { Id = "ZA_FR", LanguageId = "FR", CountryId = "ZA", Name = "Afrique du Sud", Nationality = "Sud-Africain" },
                new CountryName { Id = "ZA_AR", LanguageId = "AR", CountryId = "ZA", Name = "جنوب أفريقيا", Nationality = "جنوب أفريقي" },

                new CountryName { Id = "KR_EN", LanguageId = "EN", CountryId = "KR", Name = "South Korea", Nationality = "South Korean" },
                new CountryName { Id = "KR_FR", LanguageId = "FR", CountryId = "KR", Name = "Corée du Sud", Nationality = "Sud-Coréen" },
                new CountryName { Id = "KR_AR", LanguageId = "AR", CountryId = "KR", Name = "كوريا الجنوبية", Nationality = "كوري جنوبي" },

                new CountryName { Id = "ES_EN", LanguageId = "EN", CountryId = "ES", Name = "Spain", Nationality = "Spanish" },
                new CountryName { Id = "ES_FR", LanguageId = "FR", CountryId = "ES", Name = "Espagne", Nationality = "Espagnol" },
                new CountryName { Id = "ES_AR", LanguageId = "AR", CountryId = "ES", Name = "إسبانيا", Nationality = "إسباني" },

                new CountryName { Id = "LK_EN", LanguageId = "EN", CountryId = "LK", Name = "Sri Lanka", Nationality = "Sri Lankan" },
                new CountryName { Id = "LK_FR", LanguageId = "FR", CountryId = "LK", Name = "Sri Lanka", Nationality = "Sri-Lankais" },
                new CountryName { Id = "LK_AR", LanguageId = "AR", CountryId = "LK", Name = "سريلانكا", Nationality = "سريلانكي" },

                new CountryName { Id = "SD_EN", LanguageId = "EN", CountryId = "SD", Name = "Sudan", Nationality = "Sudanese" },
                new CountryName { Id = "SD_FR", LanguageId = "FR", CountryId = "SD", Name = "Soudan", Nationality = "Soudanais" },
                new CountryName { Id = "SD_AR", LanguageId = "AR", CountryId = "SD", Name = "السودان", Nationality = "سوداني" },

                new CountryName { Id = "SR_EN", LanguageId = "EN", CountryId = "SR", Name = "Suriname", Nationality = "Surinamese" },
                new CountryName { Id = "SR_FR", LanguageId = "FR", CountryId = "SR", Name = "Suriname", Nationality = "Surinamien" },
                new CountryName { Id = "SR_AR", LanguageId = "AR", CountryId = "SR", Name = "سورينام", Nationality = "سورينامي" },

                new CountryName { Id = "SE_EN", LanguageId = "EN", CountryId = "SE", Name = "Sweden", Nationality = "Swedish" },
                new CountryName { Id = "SE_FR", LanguageId = "FR", CountryId = "SE", Name = "Suède", Nationality = "Suédois" },
                new CountryName { Id = "SE_AR", LanguageId = "AR", CountryId = "SE", Name = "السويد", Nationality = "سويدي" },

                new CountryName { Id = "CH_EN", LanguageId = "EN", CountryId = "CH", Name = "Switzerland", Nationality = "Swiss" },
                new CountryName { Id = "CH_FR", LanguageId = "FR", CountryId = "CH", Name = "Suisse", Nationality = "Suisse" },
                new CountryName { Id = "CH_AR", LanguageId = "AR", CountryId = "CH", Name = "سويسرا", Nationality = "سويسري" },

                new CountryName { Id = "SY_EN", LanguageId = "EN", CountryId = "SY", Name = "Syria", Nationality = "Syrian" },
                new CountryName { Id = "SY_FR", LanguageId = "FR", CountryId = "SY", Name = "Syrie", Nationality = "Syrien" },
                new CountryName { Id = "SY_AR", LanguageId = "AR", CountryId = "SY", Name = "سوريا", Nationality = "سوري" },

                // ===== T =====
                new CountryName { Id = "TW_EN", LanguageId = "EN", CountryId = "TW", Name = "Taiwan", Nationality = "Taiwanese" },
                new CountryName { Id = "TW_FR", LanguageId = "FR", CountryId = "TW", Name = "Taïwan", Nationality = "Taïwanais" },
                new CountryName { Id = "TW_AR", LanguageId = "AR", CountryId = "TW", Name = "تايوان", Nationality = "تايواني" },

                new CountryName { Id = "TJ_EN", LanguageId = "EN", CountryId = "TJ", Name = "Tajikistan", Nationality = "Tajik" },
                new CountryName { Id = "TJ_FR", LanguageId = "FR", CountryId = "TJ", Name = "Tadjikistan", Nationality = "Tadjik" },
                new CountryName { Id = "TJ_AR", LanguageId = "AR", CountryId = "TJ", Name = "طاجيكستان", Nationality = "طاجيكي" },

                new CountryName { Id = "TZ_EN", LanguageId = "EN", CountryId = "TZ", Name = "Tanzania", Nationality = "Tanzanian" },
                new CountryName { Id = "TZ_FR", LanguageId = "FR", CountryId = "TZ", Name = "Tanzanie", Nationality = "Tanzanien" },
                new CountryName { Id = "TZ_AR", LanguageId = "AR", CountryId = "TZ", Name = "تنزانيا", Nationality = "تنزاني" },

                new CountryName { Id = "TH_EN", LanguageId = "EN", CountryId = "TH", Name = "Thailand", Nationality = "Thai" },
                new CountryName { Id = "TH_FR", LanguageId = "FR", CountryId = "TH", Name = "Thaïlande", Nationality = "Thaïlandais" },
                new CountryName { Id = "TH_AR", LanguageId = "AR", CountryId = "TH", Name = "تايلاند", Nationality = "تايلاندي" },

                new CountryName { Id = "TL_EN", LanguageId = "EN", CountryId = "TL", Name = "Timor-Leste", Nationality = "Timorese" },
                new CountryName { Id = "TL_FR", LanguageId = "FR", CountryId = "TL", Name = "Timor oriental", Nationality = "Timorais" },
                new CountryName { Id = "TL_AR", LanguageId = "AR", CountryId = "TL", Name = "تيمور الشرقية", Nationality = "تيموري" },

                new CountryName { Id = "TG_EN", LanguageId = "EN", CountryId = "TG", Name = "Togo", Nationality = "Togolese" },
                new CountryName { Id = "TG_FR", LanguageId = "FR", CountryId = "TG", Name = "Togo", Nationality = "Togolais" },
                new CountryName { Id = "TG_AR", LanguageId = "AR", CountryId = "TG", Name = "توجو", Nationality = "توغولي" },

                new CountryName { Id = "TO_EN", LanguageId = "EN", CountryId = "TO", Name = "Tonga", Nationality = "Tongan" },
                new CountryName { Id = "TO_FR", LanguageId = "FR", CountryId = "TO", Name = "Tonga", Nationality = "Tongien" },
                new CountryName { Id = "TO_AR", LanguageId = "AR", CountryId = "TO", Name = "تونغا", Nationality = "تونغي" },

                new CountryName { Id = "TT_EN", LanguageId = "EN", CountryId = "TT", Name = "Trinidad and Tobago", Nationality = "Trinidadian / Tobagonian" },
                new CountryName { Id = "TT_FR", LanguageId = "FR", CountryId = "TT", Name = "Trinité-et-Tobago", Nationality = "Trinidadien / Tobagonien" },
                new CountryName { Id = "TT_AR", LanguageId = "AR", CountryId = "TT", Name = "ترينيداد وتوباغو", Nationality = "ترينيدادي" },

                new CountryName { Id = "TN_EN", LanguageId = "EN", CountryId = "TN", Name = "Tunisia", Nationality = "Tunisian" },
                new CountryName { Id = "TN_FR", LanguageId = "FR", CountryId = "TN", Name = "Tunisie", Nationality = "Tunisien" },
                new CountryName { Id = "TN_AR", LanguageId = "AR", CountryId = "TN", Name = "تونس", Nationality = "تونسي" },

                new CountryName { Id = "TR_EN", LanguageId = "EN", CountryId = "TR", Name = "Turkey", Nationality = "Turkish" },
                new CountryName { Id = "TR_FR", LanguageId = "FR", CountryId = "TR", Name = "Turquie", Nationality = "Turc" },
                new CountryName { Id = "TR_AR", LanguageId = "AR", CountryId = "TR", Name = "تركيا", Nationality = "تركي" },

                // ===== U =====
                new CountryName { Id = "UG_EN", LanguageId = "EN", CountryId = "UG", Name = "Uganda", Nationality = "Ugandan" },
                new CountryName { Id = "UG_FR", LanguageId = "FR", CountryId = "UG", Name = "Ouganda", Nationality = "Ougandais" },
                new CountryName { Id = "UG_AR", LanguageId = "AR", CountryId = "UG", Name = "أوغندا", Nationality = "أوغندي" },

                new CountryName { Id = "UA_EN", LanguageId = "EN", CountryId = "UA", Name = "Ukraine", Nationality = "Ukrainian" },
                new CountryName { Id = "UA_FR", LanguageId = "FR", CountryId = "UA", Name = "Ukraine", Nationality = "Ukrainien" },
                new CountryName { Id = "UA_AR", LanguageId = "AR", CountryId = "UA", Name = "أوكرانيا", Nationality = "أوكراني" },

                new CountryName { Id = "AE_EN", LanguageId = "EN", CountryId = "AE", Name = "United Arab Emirates", Nationality = "Emirati" },
                new CountryName { Id = "AE_FR", LanguageId = "FR", CountryId = "AE", Name = "Émirats arabes unis", Nationality = "Émirati" },
                new CountryName { Id = "AE_AR", LanguageId = "AR", CountryId = "AE", Name = "الإمارات العربية المتحدة", Nationality = "إماراتي" },

                new CountryName { Id = "GB_EN", LanguageId = "EN", CountryId = "GB", Name = "United Kingdom", Nationality = "British" },
                new CountryName { Id = "GB_FR", LanguageId = "FR", CountryId = "GB", Name = "Royaume-Uni", Nationality = "Britannique" },
                new CountryName { Id = "GB_AR", LanguageId = "AR", CountryId = "GB", Name = "المملكة المتحدة", Nationality = "بريطاني" },

                new CountryName { Id = "US_EN", LanguageId = "EN", CountryId = "US", Name = "United States", Nationality = "American" },
                new CountryName { Id = "US_FR", LanguageId = "FR", CountryId = "US", Name = "États-Unis", Nationality = "Américain" },
                new CountryName { Id = "US_AR", LanguageId = "AR", CountryId = "US", Name = "الولايات المتحدة الأمريكية", Nationality = "أمريكي" },

                new CountryName { Id = "UY_EN", LanguageId = "EN", CountryId = "UY", Name = "Uruguay", Nationality = "Uruguayan" },
                new CountryName { Id = "UY_FR", LanguageId = "FR", CountryId = "UY", Name = "Uruguay", Nationality = "Uruguayen" },
                new CountryName { Id = "UY_AR", LanguageId = "AR", CountryId = "UY", Name = "أوروغواي", Nationality = "أوروغواياني" },

                new CountryName { Id = "UZ_EN", LanguageId = "EN", CountryId = "UZ", Name = "Uzbekistan", Nationality = "Uzbek" },
                new CountryName { Id = "UZ_FR", LanguageId = "FR", CountryId = "UZ", Name = "Ouzbékistan", Nationality = "Ouzbek" },
                new CountryName { Id = "UZ_AR", LanguageId = "AR", CountryId = "UZ", Name = "أوزبكستان", Nationality = "أوزبكي" },

                // ===== V =====
                new CountryName { Id = "VU_EN", LanguageId = "EN", CountryId = "VU", Name = "Vanuatu", Nationality = "Ni-Vanuatu" },
                new CountryName { Id = "VU_FR", LanguageId = "FR", CountryId = "VU", Name = "Vanuatu", Nationality = "Vanuatais" },
                new CountryName { Id = "VU_AR", LanguageId = "AR", CountryId = "VU", Name = "فانواتو", Nationality = "فانواتي" },

                new CountryName { Id = "VE_EN", LanguageId = "EN", CountryId = "VE", Name = "Venezuela", Nationality = "Venezuelan" },
                new CountryName { Id = "VE_FR", LanguageId = "FR", CountryId = "VE", Name = "Venezuela", Nationality = "Vénézuélien" },
                new CountryName { Id = "VE_AR", LanguageId = "AR", CountryId = "VE", Name = "فنزويلا", Nationality = "فنزويلي" },

                new CountryName { Id = "VN_EN", LanguageId = "EN", CountryId = "VN", Name = "Vietnam", Nationality = "Vietnamese" },
                new CountryName { Id = "VN_FR", LanguageId = "FR", CountryId = "VN", Name = "Viêt Nam", Nationality = "Vietnamien" },
                new CountryName { Id = "VN_AR", LanguageId = "AR", CountryId = "VN", Name = "فيتنام", Nationality = "فيتنامي" },

                // ===== Y =====
                new CountryName { Id = "YE_EN", LanguageId = "EN", CountryId = "YE", Name = "Yemen", Nationality = "Yemeni" },
                new CountryName { Id = "YE_FR", LanguageId = "FR", CountryId = "YE", Name = "Yémen", Nationality = "Yéménite" },
                new CountryName { Id = "YE_AR", LanguageId = "AR", CountryId = "YE", Name = "اليمن", Nationality = "يمني" },

                // ===== Z =====
                new CountryName { Id = "ZM_EN", LanguageId = "EN", CountryId = "ZM", Name = "Zambia", Nationality = "Zambian" },
                new CountryName { Id = "ZM_FR", LanguageId = "FR", CountryId = "ZM", Name = "Zambie", Nationality = "Zambien" },
                new CountryName { Id = "ZM_AR", LanguageId = "AR", CountryId = "ZM", Name = "زامبيا", Nationality = "زامبي" },

                new CountryName { Id = "ZW_EN", LanguageId = "EN", CountryId = "ZW", Name = "Zimbabwe", Nationality = "Zimbabwean" },
                new CountryName { Id = "ZW_FR", LanguageId = "FR", CountryId = "ZW", Name = "Zimbabwe", Nationality = "Zimbabwéen" },
                new CountryName { Id = "ZW_AR", LanguageId = "AR", CountryId = "ZW", Name = "زيمبابوي", Nationality = "زيمبابوي" },
            };
        }
        public void Configure(EntityTypeBuilder<CountryName> builder)
        {
            builder.HasIndex(i => new { i.CountryId, i.LanguageId }).IsUnique();
            builder.HasData(GetWorldCountryNames());
        }
    }
}
