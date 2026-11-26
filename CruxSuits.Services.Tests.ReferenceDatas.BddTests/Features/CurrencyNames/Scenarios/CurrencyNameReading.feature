Feature: Currency name reading
  Currency names provide localized names for each currency.

  Background:
    Given the supported languages are "en", "fr" and "ar"

  @currency_names_listing
  Scenario: Get all names for a currency
    Given currency "USD" has the following translations:
      | Language | Name                  |
      | en       | US Dollar             |
      | fr       | dollar des États-Unis |
      | ar       | دولار أمريكي          |
    When the client requests all names for currency "USD"
    Then the response status is 200
    And the response contains 3 currency names
    And each currency name contains its language and name

  @currency_names_listing
  Scenario: Return not found when the currency does not exist
    Given currency "ZZZ" does not exist
    When the client requests all names for currency "ZZZ"
    Then the response status is 404

  @currency_names_listing
  Scenario: Return an empty list when the currency has no translations
    Given currency "QXZ" exists without translations
    When the client requests all names for currency "QXZ"
    Then the response status is 200
    And the response contains 0 currency names

  @currency_name_details
  Scenario Outline: Get a currency name in a specific language
    Given currency "USD" has a translation in language "<language>"
    When the client requests the name for currency "USD" and language "<language>"
    Then the response status is 200
    And the response contains the expected name "<name>"

    Examples:
      | language | name                  |
      | en       | US Dollar             |
      | fr       | dollar des États-Unis |
      | ar       | دولار أمريكي          |

  @currency_name_details
  Scenario: Return not found when a translation does not exist
    Given currency "USD" has no translation in language "fr"
    When the client requests the name for currency "USD" and language "fr"
    Then the response status is 404
