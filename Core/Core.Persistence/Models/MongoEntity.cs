using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Persistence.Models;

public interface IMongoEntity<TId> : IEntity<TId>
{
    [BsonId]
    [JsonConverter(typeof(ObjectIdConverter))]
    [BsonElement("_id", Order = 0)]
    TId Id { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    [BsonElement(Order = 200)]
    DateTime CreatedTime { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    [BsonElement(Order = 201)]

    DateTime? UpdatedTime { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    [BsonElement(Order = 202)]
    DateTime? DeletedTime { get; set; }
}

public class MongoEntity<TId> : IMongoEntity<TId>
{
    [BsonId]
    [JsonConverter(typeof(ObjectIdConverter))]
    [BsonElement("_id",Order = 0)]
    public TId Id { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    [BsonElement(Order = 200)]
    public DateTime CreatedTime { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    [BsonElement(Order = 201)]
  
    public DateTime? UpdatedTime { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    [BsonElement(Order = 202)]
    public DateTime? DeletedTime { get; set; }

}


public static class BsonIdHelper
{
    public static Guid AsGuid(this ObjectId oid)
    {
        var bytes = oid.ToByteArray().Concat(new byte[] { 5, 5, 5, 5 }).ToArray();
        Guid gid = new Guid(bytes);
        return gid;
    }

    /// <summary>
    /// Only Use to convert a Guid that was once an ObjectId
    /// </summary>
    public static ObjectId AsObjectId(this Guid gid)
    {
        var bytes = gid.ToByteArray().Take(12).ToArray();
        var oid = new ObjectId(bytes);
        return oid;
    }
}



public class ObjectIdConverter : JsonConverter
{

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value.ToString());
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JToken token = JToken.Load(reader);
        return new ObjectId(token.ToObject<string>());
    }

    public override bool CanConvert(Type objectType)
    {
        return (objectType == typeof(ObjectId));
    }
}