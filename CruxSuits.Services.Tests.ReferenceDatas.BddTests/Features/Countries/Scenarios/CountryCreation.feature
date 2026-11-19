@countries_controller
Feature: Country creation
  Countries can be added to the reference data catalog.

  Scenario: Create a country
    When the client creates country "QX" with alpha-3 code "QXZ", numeric code "997", phone code "+999" and currency "USD"
    Then the country API response status is 201
    And the created country can be retrieved with alpha-3 code "QXZ" and numeric code "997"
