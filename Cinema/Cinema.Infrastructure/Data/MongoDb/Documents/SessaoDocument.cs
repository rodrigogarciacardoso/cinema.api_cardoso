using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cinema.Infrastructure.Data.MongoDb.Documents
{
    public class SessaoDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("FilmeId")]
        public string FilmeId { get; set; } = null!;

        [BsonElement("HorarioInicio")]
        public DateTime HorarioInicio { get; set; }

        [BsonElement("HorarioFim")]
        public DateTime HorarioFim { get; set; }

        [BsonElement("SalaId")]
        public string SalaId { get; set; } = null!;

        [BsonElement("IngressosDisponiveis")]
        public int IngressosDisponiveis { get; set; }
    }
}