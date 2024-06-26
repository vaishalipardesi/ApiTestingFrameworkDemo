using ApiTestingFrameworkDemo.Support;
using Sainsburys.DepotManagement.Depots.Models;

namespace ApiTestingFrameworkDemo.ResourceObjects
{
    public class Depot
    {
        private string resource = Endpoint.Depot;
        private string kafka = Endpoint.Kafka;

        public async Task GetDepotsAsync()
        {
            await TestBase.Instance.m_client.GetFlurClientDataAsync(resource);
        }

        public async Task GetDepotAsync(string id)
        {
            await TestBase.Instance.m_client.GetFlurClientDataAsync(resource + id);
        }

        public async Task<HttpResponseMessage> CreateDepot(DepotSubscriptionModel data) 
        {
            return await  TestBase.Instance.m_client.FlurlClientPostAsync(data, kafka);
        }

    }
}
