Feature: Currency name deletion
  Currency name translations can be deleted.

  Background:
    Given the supported languages are "en", "fr" and "ar"

  @currency_name_deletion
  Scenario: Delete a currency name
    Given currency "USD" has a translation in language "en"
    When the client deletes the translation for currency "USD" and language "en"
    Then the response status is 204
    And the translation no longer exists

  @currency_name_deletion
  Scenario: Return not found when deleting a missing translation
    Given currency "USD" has no translation in language "en"
    When the client deletes the translation for currency "USD" and language "en"
    Then the response status is 404
