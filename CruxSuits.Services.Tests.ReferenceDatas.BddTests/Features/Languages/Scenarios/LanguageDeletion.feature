# Assumption: a future Language controller will use the @languages_controller tag.
# Assumption: language routes and response formats will follow the existing Country/Currency API conventions; no Language controller, service, or DTO currently defines this contract.
@languages_controller
Feature: Language deletion
  Languages can be removed from the reference data catalog.

  # Assumption: deletion returns HTTP 204 and the deleted language is no longer retrievable.
  Scenario: Delete a language
    Given the test language "QX" exists
    When the client deletes language "QX"
    Then the language API response status is 204
    And the deleted language can no longer be retrieved

  Scenario: Return not found when deleting an unknown language
    When the client deletes language "ZZ"
    Then the language API response status is 404
