Feature: AdministrativeArea name reading
  AdministrativeArea names provide localized names for each administrative area.

  Background:
    Given the supported languages are "en", "fr" and "ar"

  @administrative_area_names_listing
  Scenario: Get all names for an administrative area
    Given administrative area "TN-SF" has the following translations:
      | Language | Name                  |
      | en       | Sfax Governorate |
      | fr       | Gouvernorat de Sfax |
      | ar       | ولاية صفاقس         |
    When the client requests all names for administrative area "TN-SF"
    Then the response status is 200
    And the response contains 3 administrative area names
    And each administrative area name contains its language and name

  @administrative_area_names_listing
  Scenario: Return not found when the administrative area does not exist
    Given administrative area "TN-ZZ" does not exist
    When the client requests all names for administrative area "TN-ZZ"
    Then the response status is 404

  @administrative_area_names_listing
  Scenario: Return an empty list when the administrative area has no translations
    Given administrative area "TN-QZ" exists without translations
    When the client requests all names for administrative area "TN-QZ"
    Then the response status is 200
    And the response contains 0 administrative area names

  @administrative_area_name_details
  Scenario Outline: Get an administrative area name in a specific language
    Given administrative area "TN-SF" has a translation in language "<language>"
    When the client requests the name for administrative area "TN-SF" and language "<language>"
    Then the response status is 200
    And the response contains the expected name "<name>"

    Examples:
      | language | name                  |
      | en       | Sfax Governorate |
      | fr       | Gouvernorat de Sfax |
      | ar       | ولاية صفاقس         |

  @administrative_area_name_details
  Scenario: Return not found when a translation does not exist
    Given administrative area "TN-SF" has no translation in language "fr"
    When the client requests the name for administrative area "TN-SF" and language "fr"
    Then the response status is 404
