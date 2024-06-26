using ApiTestingFrameworkDemo.Support;
using Xunit;
using TechTalk.SpecFlow;
using BoDi;

namespace ApiTestingFrameworkDemo.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [BeforeScenario]
        public void BeforeScenario()
        {
     
        }


        [AfterScenario]
        public void AfterScenario()
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Test Data used:");
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            foreach (KeyValuePair<object, object> kvp in TestBase.Instance.testData)
            {
                Console.WriteLine($"Key: {kvp.Key.ToString()}, Value: {kvp.Value.ToString()}");
            }
            Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            TestBase.Instance.testData.Clear();
            TestBase.ClearInstance();
        }
    }
}