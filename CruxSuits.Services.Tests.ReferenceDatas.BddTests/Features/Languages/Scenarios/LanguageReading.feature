# Assumption: a future Language controller will use the @languages_controller tag.
# Assumption: language routes and response formats will follow the existing Country/Currency API conventions; no Language controller, service, or DTO currently defines this contract.
@languages_controller
Feature: Language reading
  Clients can read languages in the reference data catalog.

  # Assumption: listing languages returns HTTP 200 and includes the seeded languages en, fr, and ar.
  Scenario: List languages
    When the client lists languages
    Then the language API response status is 200
    And the language list contains "en", "fr" and "ar"

  # Assumption: language details expose ID, English name, native name, and right-to-left status.
  Scenario: Get language details
    Given the test language "QX" exists
    When the client requests language "QX"
    Then the language API response status is 200
    And the language details contain id "QX", English name "Test language", native name "Test", and right-to-left set to false

  Scenario: Return not found for an unknown language
    When the client requests language "ZZ"
    Then the language API response status is 404
