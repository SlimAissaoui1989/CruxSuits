Feature: File upload
  Authorized services can upload files with access and storage settings.

  @files_upload_download
  Scenario: Upload a database-stored public image and download it
    Given the Files API is available
    And I have a JPEG image named "bdd-public-image.jpg"
    When I upload the image for database storage and public access
    Then the upload response status is 201
    And the upload response contains the original file name "bdd-public-image.jpg"
    When an anonymous client downloads the uploaded file
    Then the download response status is 200
    And the downloaded bytes match the uploaded image

  @files_upload
  Scenario: Reject an empty uploaded file
    Given the Files API is available
    When I upload an empty file
    Then the upload response status is 400
