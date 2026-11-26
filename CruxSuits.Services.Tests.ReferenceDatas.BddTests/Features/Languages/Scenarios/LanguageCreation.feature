# Assumption: a future Language controller will use the @languages_controller tag.
# Assumption: language routes and request/response formats will follow the existing Country/Currency API conventions; no Language controller, service, or DTO currently defines this contract.
@languages_controller
Feature: Language creation
  Languages can be added to the reference data catalog.

  # Assumption: create accepts the fields shown, returns HTTP 201, and the created language can be retrieved.
  Scenario: Create a language
    When the client creates language "QX" with English name "Test language", native name "Test", and right-to-left set to false
    Then the language API response status is 201
    And the created language can be retrieved

  # Assumption: duplicate language IDs return HTTP 409, following the Country/Currency API convention.
  Scenario: Reject creation with an existing language ID
    Given the test language "en" exists
    When the client creates language "en" with English name "English", native name "English", and right-to-left set to false
    Then the language API response status is 409
