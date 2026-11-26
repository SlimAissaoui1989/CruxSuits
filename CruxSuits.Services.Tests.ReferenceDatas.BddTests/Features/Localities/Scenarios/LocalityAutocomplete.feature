@localities
Feature: Locality autocomplete

  Scenario Outline: Return the best ranked suggestions for a prefix
    When the client searches localities for "<query>" in language "en"
    Then the locality response status is 200
    And the locality response contains <count> localities
    And the locality suggestions are ranked in this order: "<ids>"

    Examples:
      | query | count | ids |
      | sf    | 10    | TN-SF-SFX, TN-SF-ACH, TN-SF-AGR, TN-SF-AIN, TN-SF-AMR, TN-SF-BRL, TN-SF-CHH, TN-SF-GRB, TN-SF-GRM, TN-SF-HDJ |
      | sfa   | 10    | TN-SF-SFX, TN-SF-ACH, TN-SF-AGR, TN-SF-AIN, TN-SF-AMR, TN-SF-BRL, TN-SF-CHH, TN-SF-GRB, TN-SF-GRM, TN-SF-HDJ |
      | tun   | 10    | TN-TU-TUN, TN-TU-BRD, TN-TU-CRT, TN-TU-GLT, TN-TU-KRM, TN-TU-MRS, TN-TU-SDB, TN-TU-SDH, TN-AR-ARN, TN-AR-ETT |
      | gabes | 10    | TN-GA-BCH, TN-GA-CHN, TN-GA-DKH, TN-GA-GBS, TN-GA-GHN, TN-GA-HBB, TN-GA-HMM, TN-GA-KTT, TN-GA-MET, TN-GA-MNZ |
      | tou   | 4     | TN-GA-DKH, TN-KF-TRF, TN-MS-AMI, TN-MS-TOU |
      | ka    | 10    | TN-AR-KLT, TN-JE-KLM, TN-KF-KAL, TN-KF-KLT, TN-KR-KRN, TN-KS-KSS, TN-MS-MNZ, TN-SS-KLK, TN-SS-KLS, TN-KR-ABD |

  Scenario: Rank a locality name match above an area match
    When the client searches localities for "sfax tun" in language "en"
    Then the locality response status is 200
    And the first locality suggestion is "TN-SF-SFX" with label "Sfax, Sfax Governorate, Tunisia"

  Scenario: Find a locality using its name, area, and country
    When the client searches localities for "bardo, tunis tunisia" in language "ar"
    Then the locality response status is 200
    And the locality suggestions contain "TN-TU-BRD" named "باردو" in area "TN-TU" and country "TN"

  Scenario: Return an empty list when the area does not match
    When the client searches localities for "bardo sfax" in language "en"
    Then the locality response status is 200
    And the locality response contains 0 localities

  Scenario: Limit suggestions for a broad country search
    When the client searches localities for "tunisia" in language "en"
    Then the locality response status is 200
    And the locality response contains 10 localities

  Scenario: Reject a query shorter than two characters
    When the client searches localities for "b" in language "en"
    Then the locality response status is 400
