Feature: GoRestUsers


Scenario Outline: Get All Users
	Given I want to prepare a request
	When I get all users from the <endpoint> endpoint
	Then The response status code should be <statusCode>
		And the response should contain a list of users

Examples: 
| endpoint | statusCode |
| users    | OK         |
| fake     | NotFound   |

@Authenticate
Scenario Outline: Create a new user
	Given I have the following user data Name:<Name>, Email:<Email>, Gender:<Gender>, Satus:<Status>
	When I send a request to the users endpoint
	Then The response status code should be <statusCode>
		And  The user should be created successfully

Examples: 
| Name   | Email         | Gender | Status | statusCode          |
| asad   | aadad@kek.com | male   | active | Created             |
| asdsad | bademail      | male   | active | UnprocessableEntity |


@Authenticate
Scenario: Update an existing user
	Given I have a created user already with <idstatus>
	And Want to update his first name to Pesho
	When I send a request to the users endpoint to update
	Then The response status code should be <statusCode>
		And The user should be updaates successfully

Examples: 
| idstatus | statusCode |
| existing | OK         |
| null     | NotFound   |
