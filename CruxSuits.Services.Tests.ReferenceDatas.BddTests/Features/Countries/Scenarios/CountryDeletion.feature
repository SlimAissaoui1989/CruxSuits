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

  Scenario: Soft delete a country's areas and localities
    Given the test-only country "QX" exists
    And country "QX" has an unrelated administrative area and locality
    When the client deletes country "TN"
    Then the country API response status is 204
    And the deleted country can no longer be retrieved
    And country "TN" and its 24 areas and 350 localities retain their rows with deletion audit metadata
    And country "QX" and its area and locality remain active
