Feature: Country name creation
  Country names can be added for supported languages.

  Background:
    Given the supported languages are "en", "fr" and "ar"

  @country_name_creation
  Scenario Outline: Create a country name
    Given country "US" exists
    And no translation exists for country "US" in language "<language>"
    When the client creates a country name "<name>" with nationality "<nationality>" in language "<language>"
    Then the response status is 201
    And the country name is stored for country "US" and language "<language>"
    And the stored name is "<name>"
    And the stored nationality is "<nationality>"

    Examples:
      | language | name              | nationality |
      | en       | United States     | American    |
      | fr       | États Unis        | Américaine  |
      | ar       | الولايات المتحدة | أمريكي      |

  @country_name_creation
  Scenario: Reject creating a duplicate country name
    Given country "US" already has a translation in language "en"
    When the client creates another translation for country "US" in language "en"
    Then the response status is 409

  @country_name_creation
  Scenario: Reject creating a translation for a nonexistent country
    Given country "XX" does not exist
    When the client creates a translation for country "XX"
    Then the response status is 404

  @country_name_creation
  Scenario: Reject creating a translation for a nonexistent language
    Given country "US" exists
    When the client creates a translation for language "XX"
    Then the response status is 400

  @country_name_creation
  Scenario Outline: Reject a country name when a required designation is blank or whitespace-only
    Given country "US" exists
    When the client creates a country name with a <condition> "<field>"
    Then the response status is 400

    Examples:
      | condition       | field       |
      | blank           | name        |
      | whitespace-only | name        |
      | blank           | nationality |
      | whitespace-only | nationality |
