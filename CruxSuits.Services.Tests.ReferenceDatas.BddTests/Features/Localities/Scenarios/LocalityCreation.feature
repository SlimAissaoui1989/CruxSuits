@localities
Feature: Locality creation

  Scenario Outline: Create a locality with a supported code length
    When the client creates locality "<code>" in country "TN" and area "TU"
    Then the locality response status is 201
    And the locality response identifies "TN-TU-<code>" with code "<code>"

    Examples:
      | code |
      | ZQA  |
      | ZQAA |

  Scenario: Reject a duplicate locality code
    When the client creates locality "BRD" in country "TN" and area "TU"
    Then the locality response status is 409

  Scenario: Reject creation for an unknown country
    When the client creates locality "ZQA" in country "ZZ" and area "TU"
    Then the locality response status is 404

  Scenario: Reject an invalid locality code
    When the client creates locality "Q1A" in country "TN" and area "TU"
    Then the locality response status is 400
