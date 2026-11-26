# Assumption: a future Currency controller will use the @currencies_controller tag.
@currencies_controller
Feature: Currency deletion
  Currencies can be removed from the reference data catalog.

  # Assumptions: deletion returns HTTP 204, and a deleted currency is no longer available through the detail operation.
  Scenario: Delete a currency
    Given the test currency "QXZ" exists with numeric code "996"
    When the client deletes currency "QXZ"
    Then the currency API response status is 204
    And the deleted currency can no longer be retrieved

  # Assumption: deleting a currency ID that does not exist returns HTTP 404.
  Scenario: Return not found for an unknown currency
    When the client deletes currency "ZZZ"
    Then the currency API response status is 404
