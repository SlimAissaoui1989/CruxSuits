Feature: AdministrativeArea name deletion
  AdministrativeArea name translations can be deleted.

  Background:
    Given the supported languages are "en", "fr" and "ar"

  @administrative_area_name_deletion
  Scenario: Delete an administrative area name
    Given administrative area "TN-SF" has a translation in language "en"
    When the client deletes the translation for administrative area "TN-SF" and language "en"
    Then the response status is 204
    And the translation no longer exists

  @administrative_area_name_deletion
  Scenario: Return not found when deleting a missing translation
    Given administrative area "TN-SF" has no translation in language "en"
    When the client deletes the translation for administrative area "TN-SF" and language "en"
    Then the response status is 404
