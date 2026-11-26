@localities
Feature: Locality deletion

  Scenario: Delete a locality
    When the client deletes locality "BRD" in country "TN" and area "TU"
    Then the locality response status is 204
    And locality "BRD" in country "TN" and area "TU" can no longer be retrieved

  Scenario: Return not found for a missing locality
    When the client deletes locality "ZZZ" in country "TN" and area "TU"
    Then the locality response status is 404
