Feature: Currency name update
  A currency translation can be updated.

  Background:
    Given the supported languages are "en", "fr" and "ar"

  @currency_name_update
  Scenario: Update a currency name
    Given currency "USD" has the following translation:
      | Language | Name      |
      | en       | US Dollar |
    When the client updates the translation with:
      | Name                 |
      | United States dollar |
    Then the response status is 200
    And the updated currency name is returned
    And the currency and language remain unchanged

  @currency_name_update
  Scenario: Return not found when updating a missing translation
    Given currency "USD" has no translation in language "en"
    When the client updates the translation for currency "USD" and language "en"
    Then the response status is 404
