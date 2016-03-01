Feature: PasswordManager
	In order to avoid remembering passwords
	As a forgetfull person
	I want to get a list of passwords

@mytag
Scenario: Get A List Of Passwords
	Given I have a secret password of 'Test1234'
	And I have a list of passwords
	When I add a new password
	Then the result should be count of passwords plus one
