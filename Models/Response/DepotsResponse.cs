using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace ApiTestingFrameworkDemo.Models
{
    public class DepotsResponse
    {
        public DepotPayload[] rooms { get; set; }
    }

    public class DepotPayload
    {
        public string LegacyId { get; set; }

        public string TypeId { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid Key { get; set; }

        public string Name { get; set; }

        public string SourceSystem { get; set; }

        public string MessageEventType { get; set; }

        public DateTime MessageEventTimeStamp { get; set; }
    }
}
