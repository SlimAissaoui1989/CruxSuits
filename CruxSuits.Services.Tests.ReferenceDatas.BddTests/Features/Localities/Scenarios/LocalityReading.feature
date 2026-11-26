@localities
Feature: Locality reading

  Scenario: List the localities of Tunis
    When the client lists localities in country "TN" and area "TU"
    Then the locality response status is 200
    And the locality response contains 8 localities

  Scenario: Get Le Bardo by its short code
    When the client requests locality "BRD" in country "TN" and area "TU"
    Then the locality response status is 200
    And the locality response identifies "TN-TU-BRD" with code "BRD"

  Scenario: Return not found for an unknown locality
    When the client requests locality "ZZZ" in country "TN" and area "TU"
    Then the locality response status is 404

  Scenario: Return not found when a locality belongs to a different area
    When the client requests locality "BRD" in country "TN" and area "SF"
    Then the locality response status is 404
