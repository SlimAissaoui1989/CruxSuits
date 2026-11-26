Feature: Locality name deletion
  Locality name translations can be deleted.

  Background:
    Given the supported languages are "en", "fr" and "ar"

  @locality_name_deletion
  Scenario: Delete a locality name
    Given locality "TN-TU-BRD" has a translation in language "en"
    When the client deletes the translation for locality "TN-TU-BRD" and language "en"
    Then the response status is 204
    And the translation no longer exists

  @locality_name_deletion
  Scenario: Return not found when deleting a missing translation
    Given locality "TN-TU-BRD" has no translation in language "en"
    When the client deletes the translation for locality "TN-TU-BRD" and language "en"
    Then the response status is 404
