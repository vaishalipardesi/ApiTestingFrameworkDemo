using ApiTestingFrameworkDemo.Models.Response;
using ApiTestingFrameworkDemo.ResourceObjects;
using ApiTestingFrameworkDemo.Support;
using ApiTestingFrameworkDemo.Tests.Helper;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sainsburys.DepotManagement.Depots.Models;
using Sainsburys.DepotManagement.Shared.Data.ViewModels;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;
using Depot = ApiTestingFrameworkDemo.ResourceObjects.Depot;
using Table = TechTalk.SpecFlow.Table;

namespace ApiTestingFrameworkDemo.StepDefinitions
{
    [Binding]
    public class DepotStepDefinition :Steps
    {
        private ScenarioContext _scenarioContext;
        private string inputParameters;
        public JsonHelper jsonHelper = new();
        Depot _depot = new();
        DepotSubscriptionModel _depotSubscription = new();
        HttpResponseMessage responseMessage = new();

        public DepotStepDefinition(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"kafka json input file")]
        public void GivenKafkaJsonInputFile(Table table)
        {
            inputParameters = jsonHelper.ReadJsonFile(table);  // Passing table  
            _depotSubscription = inputParameters == null ? null : JsonConvert.DeserializeObject<DepotSubscriptionModel>(inputParameters);
        }

        [When(@"I send POST request")]
        public async void WhenISendPOSTRequest()
        {
            responseMessage = await _depot.CreateDepot(_depotSubscription);
        }

        [Given(@"I GET All Depots request")]
        public async Task WhenIMakeGETAllDepotsRequest()
        {
            await _depot.GetDepotsAsync();
        }

        [Then(@"I receive API response from kafka service")]
        public Task ThenIReceiveAPIResponsefromkafkaserviceAsync()
        {
            var httpResponseMessage = responseMessage;
            TestBase.Instance.testData.AddResponseJson(httpResponseMessage);
            return Task.CompletedTask;
        }

        [Then(@"response data must look like '(.*)'")]
        public void ThenResponseDataMustLookLike(string data)
        {
            var responseJson = TestBase.Instance.testData.GetResponseJson().ToString();
            ODataResponse<DepotODataViewModel>? response = JsonConvert.DeserializeObject<ODataResponse<DepotODataViewModel>>(responseJson);

            // Assert against the received data
            var jsonString = JsonConvert.SerializeObject(response?.Value.Take(2));
            Assert.Equal(JToken.Parse(jsonString), JToken.Parse(data));
        }

        [Given(@"I make GET Request '([^']*)'")]
        public async Task WhenIMakeRequest(string key)
        {
            await _depot.GetDepotAsync(key);
        }

        [Then(@"single response data must look like '(.*)'")]
        public Task ThenSingleResponseDataMustLookLike(string data)
        {
            var responseJson = TestBase.Instance.testData.GetResponseJson().ToString();
            DepotODataViewModel? result1 = null;
            result1 = JsonConvert.DeserializeObject<DepotODataViewModel>(responseJson);

            var jsonString = JsonConvert.SerializeObject(result1);
            Assert.Equal(JToken.Parse(jsonString), JToken.Parse(data));
            return Task.CompletedTask;
        }

        [Given(@"I retrieve a list of depots")]
        public async Task GivenIRetrieveAListOfDepotsAsync()
        {
            await _depot.GetDepotsAsync();
        }
        [Then(@"I should receive the following depots:")]
        public void ThenIShouldReceiveTheFollowingDepots(Table table)
        {
            Assert.Equal(200, TestBase.Instance.testData.GetResponseStatusCode());
            var responseJson = TestBase.Instance.testData.GetResponseJson().ToString();
            ODataResponse<DepotODataViewModel>? response = JsonConvert.DeserializeObject<ODataResponse<DepotODataViewModel>>(responseJson);

            // Assert against the received data
            var resultlist = response?.Value.ToList<DepotODataViewModel>();
            IEnumerable<DepotNames> actualDepotList = resultlist.Select(r => new DepotNames { Name = r.Name });
            table.CompareToSet<DepotNames>(actualDepotList);
        }

        [Then(@"it should contain any of items")]
        public void ThenItShouldContainAnyOfItems(Table table)
        {
            var collection = table.CreateSet<DepotNames>();
            ScenarioContext.Add("Collection", collection);
            Assert.True(table.ToProjection<DepotNames>().Except(collection.ToProjection()).Count() == 0);
        }

        [Given(@"json input file")]
        public void GivenJsonInputFile(Table table)
        {
            inputParameters = jsonHelper.ReadJsonFile(table);  // Passing table  
        }

        [Then(@"I receive API response")]
        public async Task ThenIReceiveAPIResponseAsync()
        {
            TestBase.Instance.testData.GetResponseJson();
        }

        [Then(@"The Status Code should be (.*)")]
        public void ThenTheStatusCodeShouldBe(int statuscode)
        {
            Assert.Equal(statuscode, TestBase.Instance.testData.GetResponseStatusCode());
        }

        [Then(@"I validate the json response")]
        public void ThenIValidateTheJsonResponse()
        {
            var responseJson = TestBase.Instance.testData.GetResponseJson().ToString();
            ODataResponse<DepotODataViewModel>? response = JsonConvert.DeserializeObject<ODataResponse<DepotODataViewModel>>(responseJson);

            var expectedJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(response?.Value.Take(2));
            JToken expected = JToken.Parse(expectedJsonString);
            JToken actual = JToken.Parse(inputParameters);

            actual.Should().BeEquivalentTo(expected);
            JToken.Parse(expectedJsonString).Should().HaveCount(2);

            JArray jsonArray = JArray.Parse(expectedJsonString);
            var legacyID = (string)jsonArray[0].SelectToken("LegacyId"); // Retrieve the character value
            var jsonObjects = jsonArray.OfType<JObject>().ToList();
        }

    }
}
