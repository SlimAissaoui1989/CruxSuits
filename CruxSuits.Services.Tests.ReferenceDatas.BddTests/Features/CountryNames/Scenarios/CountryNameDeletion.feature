Feature: Country name deletion
  Country name translations can be deleted.

  Background:
    Given the supported languages are "en", "fr" and "ar"

  @country_name_deletion
  Scenario: Delete a country name
    Given country "US" has a translation in language "en"
    When the client deletes the translation for country "US" and language "en"
    Then the response status is 204
    And the translation no longer exists

  @country_name_deletion
  Scenario: Return not found when deleting a missing translation
    Given country "US" has no translation in language "en"
    When the client deletes the translation for country "US" and language "en"
    Then the response status is 404
