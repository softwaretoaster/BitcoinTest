Feature: SpecFlowFeature1
	
Background: 
	Given User launches the application

Scenario: Testing Delay and Duration Functionalities
	When User provides "2000" ms into Delay and "3000" into Min Duration input
	Then User verifies indicator is not visible for "2000" ms and it will be visible for 3000 ms 
	
Scenario: Testing Dancing wizard
	When User changes from Standard to Templete Url 
	Then User verifies that busy indicator switches from a spinner to a dancing wizard

Scenario: Validating Message Box and busy indicator
	When User provides "0" ms into Delay and "3000" into Min Duration input
	And User verifies that "Please Wait..." is being shown as "Please Wait..." in the busy indicator
	And User verifies that "Waiting" messages shown in the busy indicator as "Waiting."
	Then User sets minimum duration to "1000" ms and press Demo button 
	And User verifies that "Waiting." message is shown in the busy indicator for "1000" ms