@countries_controller
Feature: Country deletion
  Countries can be removed from the reference data catalog.

  Scenario: Delete a country
    Given the test-only country "QX" exists
    When the client deletes country "QX"
    Then the country API response status is 204
    And the deleted country can no longer be retrieved

  Scenario: Return not found when deleting an unknown country
    When the client deletes country "ZZ"
    Then the country API response status is 404
