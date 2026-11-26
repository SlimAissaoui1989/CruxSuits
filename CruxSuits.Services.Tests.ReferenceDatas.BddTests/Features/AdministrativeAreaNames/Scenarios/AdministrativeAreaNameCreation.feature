Feature: AdministrativeArea name creation
  AdministrativeArea names can be added for supported languages.

  Background:
    Given the supported languages are "en", "fr" and "ar"

  @administrative_area_name_creation
  Scenario Outline: Create an administrative area name
    Given administrative area "TN-SF" exists
    And no translation exists for administrative area "TN-SF" in language "<language>"
    When the client creates an administrative area name "<name>" in language "<language>"
    Then the response status is 201
    And the administrative area name is stored for administrative area "TN-SF" and language "<language>"
    And the stored name is "<name>"

    Examples:
      | language | name                  |
      | en       | Sfax             |
      | fr       | Sfax     |
      | ar       | صفاقس          |

  @administrative_area_name_creation
  Scenario: Reject creating a duplicate administrative area name
    Given administrative area "TN-SF" already has a translation in language "en"
    When the client creates another translation for administrative area "TN-SF" in language "en"
    Then the response status is 409

  @administrative_area_name_creation
  Scenario: Reject creating a translation for a nonexistent administrative area
    Given administrative area "TN-ZZ" does not exist
    When the client creates a translation for administrative area "TN-ZZ"
    Then the response status is 404

  @administrative_area_name_creation
  Scenario: Reject creating a translation for a nonexistent language
    Given administrative area "TN-SF" exists
    When the client creates a translation for language "XX"
    Then the response status is 400

  @administrative_area_name_creation
  Scenario Outline: Reject an administrative area name when the name is blank or whitespace-only
    Given administrative area "TN-SF" exists
    When the client creates an administrative area name with a <condition> name
    Then the response status is 400

    Examples:
      | condition       |
      | blank           |
      | whitespace-only |
