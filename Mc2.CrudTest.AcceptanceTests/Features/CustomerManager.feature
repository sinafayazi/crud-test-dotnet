Feature: Customer Manager

As a an operator I wish to be able to Create, Update, Delete customers and list all customers
	
@Create	
Scenario: Add A New Customer
	Given a customer with the following details:
		| FirstName | LastName | DateOfBirth | PhoneNumber | Email                | BankAccountNumber           |
		| Sina      | Fayazi   | 1998-03-08  | 09331236547 | sinafayazi@yahoo.com | IT60X0542811101000000123456 |
	When the user adds the customer
	Then the customer should be successfully added