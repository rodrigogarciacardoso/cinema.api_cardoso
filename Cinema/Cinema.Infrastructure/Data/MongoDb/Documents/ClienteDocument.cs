using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cinema.Infrastructure.Data.MongoDb.Documents
{
    public class ClienteDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Nome")]
        public string Nome { get; set; } = null!;

        [BsonElement("Email")]
        public string Email { get; set; } = null!;

        [BsonElement("Telefone")]
        public string Telefone { get; set; } = null!;

        [BsonElement("IngressosCompradosIds")]
        public List<string> IngressosCompradosIds { get; set; } = [];
    }
}