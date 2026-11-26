# Assumption: a future Language controller will use the @languages_controller tag.
# Assumption: language routes and request/response formats will follow the existing Country/Currency API conventions; no Language controller, service, or DTO currently defines this contract.
@languages_controller
Feature: Language update
  Existing languages can be updated in the reference data catalog.

  # Assumption: update accepts the fields shown, returns HTTP 200, and the updated values can be retrieved.
  Scenario: Update a language
    Given the test language "QX" exists
    When the client updates language "QX" with English name "Updated language", native name "Updated", and right-to-left set to true
    Then the language API response status is 200
    And the updated language details contain id "QX", English name "Updated language", native name "Updated", and right-to-left set to true

  Scenario: Return not found when updating an unknown language
    When the client updates language "ZZ" with English name "Updated language", native name "Updated", and right-to-left set to true
    Then the language API response status is 404
