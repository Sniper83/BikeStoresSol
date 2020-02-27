Feature: EntityFrameworkTests
	In order to to be sure that Entity framework is working correctly
	As a user of this framework
	I want to test some methods of this framework

Background: 
	Given database BikeStores with table Brands 

@EntityFrameworkTests
Scenario: Check that Get method is working correctly
	When I get elements of Entity framework
	Then I will receive a list of table elements

@EntityFrameworkTests
Scenario: Check that Add method is working correctly
	When I use the Add method of Entity framework
	Then Get method will return a new element

@EntityFrameworkTests
Scenario: Check that Remove method is working correctly
	When I use the Remove method of Entity framework
	Then Get method will not find removed element