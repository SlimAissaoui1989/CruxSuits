@administrative_areas
Feature: Administrative area reading

  Scenario: List the areas of Tunisia
    When the client lists administrative areas in country "TN"
    Then the administrative area response status is 200
    And the administrative area response contains 24 areas

  Scenario: Get Sfax by its short code
    When the client requests administrative area "SF" in country "TN"
    Then the administrative area response status is 200
    And the administrative area response identifies "TN-SF" with code "SF"

  Scenario: Return not found for an unknown area
    When the client requests administrative area "ZZ" in country "TN"
    Then the administrative area response status is 404
