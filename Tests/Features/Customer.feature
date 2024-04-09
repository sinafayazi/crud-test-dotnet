Feature: Customer Manager

As a an operator I wish to be able to Create, Update, Delete customers and list all customers
	
@Create
Scenario: Add A New Customer
    Given a customer with the following details:
      | FirstName | LastName | DateOfBirth | PhoneNumber | Email                 | BankAccountNumber           |
      | Sina      | Fayazi   | 1998-03-08  | 09331236547 | sinafayazi@yahoo.com  | IT60X0542811101000000123456 |
      | John      | Smith    | 1985-02-20  | 01234567891 | johnsmith@example.com | FR5610096000508744899439Y74 |
      | Jane      | Smith    | 1990-12-31  | 09876543210 | janesmith@example.com | NL16RABO1822913977          |
    When the user adds the customer
    Then the customer should be successfully added
    
@CreateValidation
Scenario: Validate New Customer Creation
    Given a customer with the following details:
      | FirstName | LastName | DateOfBirth | PhoneNumber    | Email                 | BankAccountNumber           |
      | Sina      | Fayazi   | 1998-03-08  | +989331236547  | sinafayazi@yahoo.com  | IT60X0542811101000000123456 |
      | John      | Smith    | 1985-02-20  | +4915510686794 | johnsmith@example.com | FR5610096000508744899439Y74 |
      | Jane      | Smith    | 1990-12-31  | +62853555291   | janesmith@example.com | NL16RABO1822913977          |
    When the system validates the customer
    Then the customer validation should be successful

    Given a customer with the following invalid details:
      | FirstName | LastName | DateOfBirth | PhoneNumber | Email                 | BankAccountNumber           | Comment
      | Sina      | Fayazi   | 1998-03-08  | 0933123654  | sinafayazi@yahoo.com  | IT60X0542811101000000123456 | # Invalid phone number
      | John      | Smith    | 1985-02-20  | 01234567891 | johnsmith@example     | FR5610096000508744899439Y74 | # Invalid email
      | Jane      | Smith    | 1990-12-31  | 09876543210 | janesmith@example.com | GB60X0542811101000000123456 | # Invalid bank account number
    When the system validates the customer
    Then the customer validation should fail
    And the system should return an error message