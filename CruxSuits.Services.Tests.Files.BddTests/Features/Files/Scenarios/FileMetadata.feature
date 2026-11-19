Feature: File metadata
  File metadata can be changed while uploaded content remains immutable.

  @files_metadata
  Scenario: Update allowed metadata and preserve the uploaded content
    Given a public database-stored file with description "Before update" exists
    When an authorized service changes the description to "After update" and makes it private
    Then the update response status is 200
    And the new metadata is stored
    And the original content remains downloadable

  @files_metadata
  Scenario: Move a database-stored file to disk after update
    Given a database-stored file exists
    When an authorized service changes its storage to disk
    Then the update response status is 200
    And the original content remains downloadable

  @files_metadata
  Scenario: Move a disk-stored file into the database after update
    Given a disk-stored file exists
    When an authorized service changes its storage to the database
    Then the update response status is 200
    And the original content remains downloadable

  @files_metadata
  Scenario: Reject attempts to change uploaded content
    Given a database-stored file exists
    When an authorized service tries to replace its content during update
    Then the update response status is 400
    And the original content remains downloadable

  @files_metadata
  Scenario: Return not found when updating a missing file
    Given no file exists for the requested identifier
    When an authorized service updates the file
    Then the update response status is 404
