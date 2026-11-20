Feature: Country name reading
  Country names provide localized names and nationalities for each country.

  Background:
    Given the supported languages are "en", "fr" and "ar"

  @country_names_listing
  Scenario: Get all names for a country
    Given country "US" has the following translations:
      | Language | Name              | Nationality |
      | en       | United States     | American    |
      | fr       | États-Unis        | Américaine  |
      | ar       | الولايات المتحدة | أمريكي      |
    When the client requests all names for country "US"
    Then the response status is 200
    And the response contains 3 country names
    And each country name contains its language, name and nationality

  @country_names_listing
  Scenario: Return not found when the country does not exist
    Given country "XX" does not exist
    When the client requests all names for country "XX"
    Then the response status is 404

  @country_names_listing
  Scenario: Return an empty list when the country has no translations
    Given country "QX" exists without translations
    When the client requests all names for country "QX"
    Then the response status is 200
    And the response contains 0 country names

  @country_name_details
  Scenario Outline: Get a country name in a specific language
    Given country "US" has a translation in language "<language>"
    When the client requests the name for country "US" and language "<language>"
    Then the response status is 200
    And the response contains the expected name "<name>" and nationality "<nationality>"

    Examples:
      | language | name              | nationality |
      | en       | United States     | American    |
      | fr       | États-Unis        | Américain   |
      | ar       | الولايات المتحدة | أمريكي      |

  @country_name_details
  Scenario: Return not found when a translation does not exist
    Given country "US" has no translation in language "fr"
    When the client requests the name for country "US" and language "fr"
    Then the response status is 404
