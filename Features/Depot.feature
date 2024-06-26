Feature: Depots

    This is to demostrate the various ways to verify data with bearer ticket authentication

Scenario: Depot Process Kafka Worker
	Given kafka json input file
	| FileName |
	| TestData\Environment\GetKafkaFeed.json   |
    When I send POST request
	Then I receive API response from kafka service
	Then The Status Code should be 200

Scenario: Get All Depots with 2 records
	Given I GET All Depots request
	Then The Status Code should be 200
	And response data must look like '[{"LegacyId":"D0006","TypeId":"Depot","Key":"5662834d-1848-5a0a-943d-2109fe91378e","Name":"Thameside DC","SourceSystem":"RP2008-CPM","MessageEventType":"Update","MessageEventTimeStamp":"2024-04-02T13:28:30.26Z","Id":"663b47a12610e379d278c39e","CreatedDate":"2024-05-08T10:36:33.7166481+01:00","UpdatedDate":"2024-05-29T17:59:43.8083695+01:00"},{"LegacyId":"D0021","TypeId":"Depot","Key":"d80cbfe8-a301-59ed-ae02-09a9b93b6aea","Name":"Belfast DC","SourceSystem":"RP2008-CPM","MessageEventType":"Update","MessageEventTimeStamp":"2024-04-02T13:28:31.26Z","Id":"663b47a12610e379d278c39f","CreatedDate":"2024-05-08T10:36:33.8887222+01:00","UpdatedDate":"2024-05-29T17:59:43.9980476+01:00"}]'

Scenario: Get Single Depot
	Given I make GET Request '5aa9f00b-cdb8-5c65-baa7-42520533f604'
	Then The Status Code should be 200
	And single response data must look like '{"LegacyId":"D0039","TypeId":"Depot","Key":"5aa9f00b-cdb8-5c65-baa7-42520533f604","Name":"Haydock DC","SourceSystem":"RP2008-CPM","MessageEventType":"Update","MessageEventTimeStamp":"2022-10-13T22:02:10.623Z","Id": "663b47a22610e379d278c3a1","CreatedDate":"2024-05-08T10:36:34.4659495+01:00","UpdatedDate":"2024-05-29T17:59:29.5490809+01:00"}'

Scenario: Get All Depots
    Given I retrieve a list of depots	
   Then it should contain any of items
   	  | Name |     
      | Belfast DC |
      | Daventry DC |
      | Emerald Park DC |

Scenario: Simple GET Request
    Given I retrieve a list of depots
    Given json input file
	| FileName |
	| TestData\Environment\GetAllDepots.json   |
	Then I receive API response
	Then The Status Code should be 200
	Then I validate the json response