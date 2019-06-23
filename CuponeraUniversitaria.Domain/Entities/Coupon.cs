using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using CuponeraUniversitaria.Infrastructure;

namespace CuponeraUniversitaria.Domain.Entities
{
    [BsonIgnoreExtraElements]
    [MongoCollection(Name = "Coupons")]
    public class Coupon : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("advertisement_id")]
        public string AdvertisementId { get; set; }

        [BsonElement("code")]
        public string Code { get; set; }

        [BsonElement("user_id")]
        public string UserId { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("expired_at")]
        public DateTime ExpiredAt { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        public Coupon(string advertisementId, string userId)
        {
            this.AdvertisementId = advertisementId;
            this.UserId = userId;
            this.CreatedAt = DateTime.UtcNow;
            this.Status = "GENERATED";
        }
    }
}
