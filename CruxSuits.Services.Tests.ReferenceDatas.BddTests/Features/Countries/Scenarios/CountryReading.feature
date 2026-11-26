@countries_controller
Feature: Countries reading
  Clients can read localized country data.

  Scenario Outline: List countries using the requested language
    When the client lists countries in language "<language>"
    Then the country API response status is 200
    And the country list contains country "US" named "<name>" with alpha-3 code "USA" and numeric code "840"

    Examples:
      | language | name                |
      | en       | United States       |
      | fr       | États-Unis          |
      | ar       | الولايات المتحدة    |

  Scenario: Include a country without a translation in the requested language
    Given a test country "QX" has only an English name "Testland"
    When the client lists countries in language "fr"
    Then the country API response status is 200
    And the country list contains country "QX" named "Unknown" with alpha-3 code "QXZ" and numeric code "997"

  Scenario: List phone-code options in a language
    When the client lists phone-code options in language "en"
    Then the country API response status is 200
    And the phone-code options contain country "US" named "United States" with phone code "+1"

  Scenario: Include phone-code options when the requested translation is missing
    Given a test country "QX" has only an English name "Testland"
    When the client lists phone-code options in language "fr"
    Then the country API response status is 200
    And the phone-code options contain country "QX" named "Unknown" with phone code "+999"

  Scenario: Get country details
    When the client requests country "US"
    Then the country API response status is 200
    And the country details contain id "US", alpha-3 code "USA", numeric code "840" and currency "USD"

  Scenario: Return not found for an unknown country
    When the client requests country "ZZ"
    Then the country API response status is 404
