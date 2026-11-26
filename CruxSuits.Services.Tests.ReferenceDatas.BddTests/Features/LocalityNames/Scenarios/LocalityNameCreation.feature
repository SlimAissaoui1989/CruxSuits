Feature: Locality name creation
  Locality names can be added for supported languages.

  Background:
    Given the supported languages are "en", "fr" and "ar"

  @locality_name_creation
  Scenario Outline: Create a locality name
    Given locality "TN-TU-BRD" exists
    And no translation exists for locality "TN-TU-BRD" in language "<language>"
    When the client creates a locality name "<name>" in language "<language>"
    Then the response status is 201
    And the locality name is stored for locality "TN-TU-BRD" and language "<language>"
    And the stored name is "<name>"

    Examples:
      | language | name                  |
      | en       | Le Bardo             |
      | fr       | Le Bardo     |
      | ar       | باردو          |

  @locality_name_creation
  Scenario: Reject creating a duplicate locality name
    Given locality "TN-TU-BRD" already has a translation in language "en"
    When the client creates another translation for locality "TN-TU-BRD" in language "en"
    Then the response status is 409

  @locality_name_creation
  Scenario: Reject creating a translation for a nonexistent locality
    Given locality "TN-TU-ZZZ" does not exist
    When the client creates a translation for locality "TN-TU-ZZZ"
    Then the response status is 404

  @locality_name_creation
  Scenario: Reject creating a translation for a nonexistent language
    Given locality "TN-TU-BRD" exists
    When the client creates a translation for language "XX"
    Then the response status is 400

  @locality_name_creation
  Scenario Outline: Reject a locality name when the name is blank or whitespace-only
    Given locality "TN-TU-BRD" exists
    When the client creates a locality name with a <condition> name
    Then the response status is 400

    Examples:
      | condition       |
      | blank           |
      | whitespace-only |
