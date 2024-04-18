using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cinema.Infrastructure.Data.MongoDb.Documents
{
    public class IngressoDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("SessaoId")]
        public string SessaoId { get; set; } = null!;

        [BsonElement("Preco")]
        public decimal Preco { get; set; }

        [BsonElement("Assento")]
        public string Assento { get; set; } = null!;

        [BsonElement("ClienteId")]
        public string ClienteId { get; set; } = null!;
    }
}