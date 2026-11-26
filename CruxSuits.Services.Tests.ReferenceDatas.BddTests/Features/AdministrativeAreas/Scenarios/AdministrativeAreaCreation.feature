@administrative_areas
Feature: Administrative area creation

  Scenario Outline: Create an area with a supported code length
    When the client creates administrative area "<code>" in country "TN"
    Then the administrative area response status is 201
    And the administrative area response identifies "TN-<code>" with code "<code>"

    Examples:
      | code |
      | QZ   |
      | QZA  |

  Scenario: Reject a duplicate area code
    When the client creates administrative area "SF" in country "TN"
    Then the administrative area response status is 409

  Scenario: Reject creation for an unknown country
    When the client creates administrative area "QZ" in country "ZZ"
    Then the administrative area response status is 404

  Scenario: Reject an invalid area code
    When the client creates administrative area "Q1" in country "TN"
    Then the administrative area response status is 400
