@administrative_areas
Feature: Administrative area deletion

  Scenario: Delete an administrative area
    When the client deletes administrative area "SF" in country "TN"
    Then the administrative area response status is 204
    And administrative area "SF" in country "TN" can no longer be retrieved

  Scenario: Return not found for a missing area
    When the client deletes administrative area "ZZ" in country "TN"
    Then the administrative area response status is 404
