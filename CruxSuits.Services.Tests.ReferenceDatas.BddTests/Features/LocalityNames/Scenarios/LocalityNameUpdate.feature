Feature: Locality name update
  A locality translation can be updated.

  Background:
    Given the supported languages are "en", "fr" and "ar"

  @locality_name_update
  Scenario: Update a locality name
    Given locality "TN-TU-BRD" has the following translation:
      | Language | Name      |
      | en       | Le Bardo |
    When the client updates the translation with:
      | Name                 |
      | Greater Le Bardo |
    Then the response status is 200
    And the updated locality name is returned
    And the locality and language remain unchanged

  @locality_name_update
  Scenario: Return not found when updating a missing translation
    Given locality "TN-TU-BRD" has no translation in language "en"
    When the client updates the translation for locality "TN-TU-BRD" and language "en"
    Then the response status is 404
