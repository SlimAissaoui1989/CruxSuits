Feature: Currency name creation
  Currency names can be added for supported languages.

  Background:
    Given the supported languages are "en", "fr" and "ar"

  @currency_name_creation
  Scenario Outline: Create a currency name
    Given currency "USD" exists
    And no translation exists for currency "USD" in language "<language>"
    When the client creates a currency name "<name>" in language "<language>"
    Then the response status is 201
    And the currency name is stored for currency "USD" and language "<language>"
    And the stored name is "<name>"

    Examples:
      | language | name                  |
      | en       | US Dollar             |
      | fr       | dollar américain     |
      | ar       | دولار أمريكي          |

  @currency_name_creation
  Scenario: Reject creating a duplicate currency name
    Given currency "USD" already has a translation in language "en"
    When the client creates another translation for currency "USD" in language "en"
    Then the response status is 409

  @currency_name_creation
  Scenario: Reject creating a translation for a nonexistent currency
    Given currency "ZZZ" does not exist
    When the client creates a translation for currency "ZZZ"
    Then the response status is 404

  @currency_name_creation
  Scenario: Reject creating a translation for a nonexistent language
    Given currency "USD" exists
    When the client creates a translation for language "XX"
    Then the response status is 400

  @currency_name_creation
  Scenario Outline: Reject a currency name when the name is blank or whitespace-only
    Given currency "USD" exists
    When the client creates a currency name with a <condition> name
    Then the response status is 400

    Examples:
      | condition       |
      | blank           |
      | whitespace-only |
