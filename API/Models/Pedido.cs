using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Models
{
    [BsonIgnoreExtraElements]
    public class Pedido
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        [BsonElement("Fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;
        [BsonElement("Total")]
        public decimal Total { get; set; } = decimal.Zero;
        [BsonElement("Estado")]
        public string Estado { get; set; } = String.Empty;

        [BsonElement("Usuario")]
        public string Usuario { get; set; } = String.Empty;
        [BsonElement("Direccion")]
        public string Direccion { get; set; } = String.Empty;
        [BsonElement("Clima")]
        public Clima Clima { get; set; } = new Clima();
        [BsonElement("Detalles")]
        public List<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
    }
}
