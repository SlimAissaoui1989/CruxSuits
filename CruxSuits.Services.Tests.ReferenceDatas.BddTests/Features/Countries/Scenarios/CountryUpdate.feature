@countries_controller
Feature: Country update
  Existing country details can be updated.

  Scenario: Update a country
    When the client updates country "US" with phone code "+1999"
    Then the country API response status is 200
    And the updated country has phone code "+1999"

  Scenario: Return not found when updating an unknown country
    When the client updates country "ZZ" with phone code "+998"
    Then the country API response status is 404
