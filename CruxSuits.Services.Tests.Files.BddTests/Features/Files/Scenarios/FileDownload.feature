Feature: File download
  Public and private files are available through their permitted download routes.

  @files_download
  Scenario: Download a public database-stored file
    Given a public database-stored file exists
    When an anonymous client downloads the file
    Then the download response status is 200
    And the response contains the original bytes and file name

  @files_download
  Scenario: Do not expose a private file through the public route
    Given a private file exists
    When an anonymous client downloads it through the public route
    Then the download response status is 404

  @files_download
  Scenario: Download a private file through the internal route
    Given a private file exists
    When an authorized service downloads it through the internal route
    Then the download response status is 200
    And the response contains the original bytes and file name

  @files_download
  Scenario: Do not return a public file through the private route
    Given a public file exists
    When an authorized service downloads it through the internal route
    Then the download response status is 404

  @files_download
  Scenario: Return not found for a missing file
    Given no file exists for the requested identifier
    When a client requests its download
    Then the download response status is 404
