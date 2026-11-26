# Assumption: a future Currency controller will use the @currencies_controller tag.
@currencies_controller
Feature: Currency creation
  Currencies can be added to the reference data catalog.

  # Assumptions: the API accepts these fields, returns HTTP 201, and the created currency is available through the detail operation.
  Scenario: Create a currency
    When the client creates currency "QXZ" with numeric code "996", symbol "¤", decimal separator "." and minor unit 2
    Then the currency API response status is 201
    And the created currency can be retrieved with numeric code "996"

  # Assumptions: currency IDs must be unique and a duplicate returns HTTP 409, following the Country API convention.
  Scenario: Reject creation with an existing currency ID
    Given the test currency "USD" exists
    When the client creates currency "USD" with numeric code "996", symbol "$", decimal separator "." and minor unit 2
    Then the currency API response status is 409

  # Assumptions: numeric codes must be unique and a duplicate returns HTTP 409, following the Country API convention.
  Scenario: Reject creation with an existing numeric code
    Given the test currency "USD" exists with numeric code "840"
    When the client creates currency "QXZ" with numeric code "840", symbol "¤", decimal separator "." and minor unit 2
    Then the currency API response status is 409
