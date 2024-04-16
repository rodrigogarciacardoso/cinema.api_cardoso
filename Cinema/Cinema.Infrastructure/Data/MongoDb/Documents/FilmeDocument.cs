using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Cinema.Infrastructure.Data.MongoDb.Documents
{
    public class FilmeDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Titulo")]
        public string Titulo { get; set; } = null!;

        [BsonElement("Diretor")]
        public string Diretor { get; set; } = null!;
    }
}
