Feature: File service authorization
  File mutation and internal file routes require service credentials.

  @files_authorization
  Scenario Outline: Require service credentials for protected operations
    Given a file exists
    When a client with "<credential>" credentials performs the protected operation "<operation>"
    Then the response status is <status>

    Examples:
      | credential | operation       | status |
      | anonymous  | upload          | 401    |
      | user       | upload          | 403    |
      | anonymous  | update          | 401    |
      | user       | update          | 403    |
      | anonymous  | delete          | 401    |
      | user       | delete          | 403    |
      | anonymous  | private download| 401    |
      | user       | private download| 403    |
      | anonymous  | private preview | 401    |
      | user       | private preview | 403    |
