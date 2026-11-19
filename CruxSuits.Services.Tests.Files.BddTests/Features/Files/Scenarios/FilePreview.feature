Feature: File preview
  Browser previews are available only for supported image types.

  @files_preview
  Scenario: Preview a public image inline
    Given a public JPEG image exists
    When an anonymous client previews the image
    Then the preview response status is 200
    And the image is returned inline with safe preview headers

  @files_preview
  Scenario: Preview a private image through the internal route
    Given a private JPEG image exists
    When an authorized service previews the image through the internal route
    Then the preview response status is 200
    And the image is returned inline

  @files_preview
  Scenario: Reject previewing a file type that cannot be displayed
    Given a public ZIP archive exists
    When an anonymous client previews the archive
    Then the preview response status is 415

  @files_preview
  Scenario: Reject previewing a private unsupported file type
    Given a private ZIP archive exists
    When an authorized service previews it through the internal route
    Then the preview response status is 415
