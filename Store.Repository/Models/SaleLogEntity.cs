using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Store.Repository.Models;

public class SaleLogEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public int SaleId { get; set; }
    public string ClientNif { get; set; }
    public DateTime SellDate { get; set; }
    public List<SaleDetailLog> Items { get; set; }
}