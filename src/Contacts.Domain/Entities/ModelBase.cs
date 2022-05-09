using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Contacts.Domain.Entities;

public abstract class ModelBase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
