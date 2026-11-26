# Assumption: a future Currency controller will use the @currencies_controller tag.
@currencies_controller
Feature: Currency reading
  Clients can read currencies in the reference data catalog.

  # Assumptions: the API provides a detail operation by ID returning HTTP 200 and exposing the ID and numeric code.
  Scenario: Get currency details
    When the client requests currency "USD"
    Then the currency API response status is 200
    And the currency details contain id "USD" and numeric code "840"

  # Assumptions: the API supports language-specific currency listing and uses "Unknown" when a translation is missing, following the Countries API convention.
  Scenario: Include a currency without a translation in the requested language
    Given the test currency "QXZ" has only an English name "Test currency"
    When the client lists currencies in language "fr"
    Then the currency API response status is 200
    And the currency list contains currency "QXZ" named "Unknown" with numeric code "998"

  # Assumption: requesting a currency ID that does not exist returns HTTP 404.
  Scenario: Return not found for an unknown currency
    When the client requests currency "ZZZ"
    Then the currency API response status is 404
