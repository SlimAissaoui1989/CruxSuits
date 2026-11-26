# Assumption: a future Currency controller will use the @currencies_controller tag.
@currencies_controller
Feature: Currency update
  Currency details can be updated in the reference data catalog.

  # Assumptions: numeric code, symbol, decimal separator, and minor unit are editable; ID is unchanged; success returns HTTP 200 and updated values.
  Scenario: Update a currency
    Given the test currency "QXZ" exists with numeric code "996", symbol "¤", decimal separator "." and minor unit 2
    When the client updates currency "QXZ" with numeric code "998", symbol "Q", decimal separator "," and minor unit 0
    Then the currency API response status is 200
    And the updated currency details contain id "QXZ", numeric code "998", symbol "Q", decimal separator "," and minor unit 0

  # Assumption: updating an unknown currency ID returns HTTP 404.
  Scenario: Return not found when updating an unknown currency
    When the client updates currency "ZZZ" with numeric code "997", symbol "¤", decimal separator "." and minor unit 2
    Then the currency API response status is 404

  # Assumptions: NumericCode uniqueness applies to updates and a conflict returns HTTP 409.
  Scenario: Reject an update that uses another currency's numeric code
    Given the test currency "USD" exists with numeric code "840"
    And the test currency "QXZ" exists with numeric code "996"
    When the client updates currency "QXZ" with numeric code "840", symbol "Q", decimal separator "," and minor unit 0
    Then the currency API response status is 409
