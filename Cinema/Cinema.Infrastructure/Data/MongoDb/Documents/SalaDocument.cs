using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cinema.Infrastructure.Data.MongoDb.Documents
{
    public class SalaDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Numero")]
        public int Numero { get; set; }

        [BsonElement("Capacidade")]
        public int Capacidade { get; set; }

        public List<string> SessoesIds { get; set; } = [];
    }
}