Feature: File deletion
  Deleting a file removes its database record and stored content.

  @files_deletion
  Scenario: Delete a database-stored file
    Given a database-stored file exists
    When an authorized service deletes the file
    Then the delete response status is 204
    And the file can no longer be downloaded

  @files_deletion
  Scenario: Delete a disk-stored file
    Given a disk-stored file exists
    When an authorized service deletes the file
    Then the delete response status is 204
    And the file can no longer be downloaded

  @files_deletion
  Scenario: Return not found when deleting a missing file
    Given no file exists for the requested identifier
    When an authorized service deletes the file
    Then the delete response status is 404
