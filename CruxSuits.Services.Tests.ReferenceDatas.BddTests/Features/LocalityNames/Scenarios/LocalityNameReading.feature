Feature: Locality name reading
  Locality names provide localized names for each locality.

  Background:
    Given the supported languages are "en", "fr" and "ar"

  @locality_names_listing
  Scenario: Get all names for a locality
    Given locality "TN-TU-BRD" has the following translations:
      | Language | Name                  |
      | en       | Le Bardo             |
      | fr       | Le Bardo |
      | ar       | باردو          |
    When the client requests all names for locality "TN-TU-BRD"
    Then the response status is 200
    And the response contains 3 locality names
    And each locality name contains its language and name

  @locality_names_listing
  Scenario: Return not found when the locality does not exist
    Given locality "TN-TU-ZZZ" does not exist
    When the client requests all names for locality "TN-TU-ZZZ"
    Then the response status is 404

  @locality_names_listing
  Scenario: Return an empty list when the locality has no translations
    Given locality "TN-TU-ZQA" exists without translations
    When the client requests all names for locality "TN-TU-ZQA"
    Then the response status is 200
    And the response contains 0 locality names

  @locality_name_details
  Scenario Outline: Get a locality name in a specific language
    Given locality "TN-TU-BRD" has a translation in language "<language>"
    When the client requests the name for locality "TN-TU-BRD" and language "<language>"
    Then the response status is 200
    And the response contains the expected name "<name>"

    Examples:
      | language | name                  |
      | en       | Le Bardo             |
      | fr       | Le Bardo |
      | ar       | باردو          |

  @locality_name_details
  Scenario: Return not found when a translation does not exist
    Given locality "TN-TU-BRD" has no translation in language "fr"
    When the client requests the name for locality "TN-TU-BRD" and language "fr"
    Then the response status is 404
