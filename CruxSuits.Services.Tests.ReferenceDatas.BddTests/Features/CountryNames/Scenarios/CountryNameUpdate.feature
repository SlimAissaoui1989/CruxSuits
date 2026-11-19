Feature: Country name update
  Only a country name's designation (name and nationality) can be changed.

  Background:
    Given the supported languages are "en", "fr" and "ar"

  @ignore @country_name_update
  Scenario: Update a country name and nationality
    Given country "US" has the following translation:
      | Language | Name          | Nationality |
      | en       | United States | American    |
    When the client updates the translation with:
      | Name                     | Nationality |
      | United States of America | American    |
    Then the response status is 200
    And the updated country name and nationality are returned
    And the country and language remain unchanged

  @ignore @country_name_update
  Scenario: Return not found when updating a missing translation
    Given country "US" has no translation in language "en"
    When the client updates the translation for country "US" and language "en"
    Then the response status is 404
