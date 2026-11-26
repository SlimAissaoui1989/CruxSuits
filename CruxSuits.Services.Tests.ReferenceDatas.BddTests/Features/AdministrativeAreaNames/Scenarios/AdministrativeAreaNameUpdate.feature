Feature: AdministrativeArea name update
  A administrative area translation can be updated.

  Background:
    Given the supported languages are "en", "fr" and "ar"

  @administrative_area_name_update
  Scenario: Update an administrative area name
    Given administrative area "TN-SF" has the following translation:
      | Language | Name      |
      | en       | Sfax |
    When the client updates the translation with:
      | Name                 |
      | Greater Sfax |
    Then the response status is 200
    And the updated administrative area name is returned
    And the administrative area and language remain unchanged

  @administrative_area_name_update
  Scenario: Return not found when updating a missing translation
    Given administrative area "TN-SF" has no translation in language "en"
    When the client updates the translation for administrative area "TN-SF" and language "en"
    Then the response status is 404
