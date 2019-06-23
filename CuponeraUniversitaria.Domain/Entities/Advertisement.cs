using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using CuponeraUniversitaria.Infrastructure;

namespace CuponeraUniversitaria.Domain.Entities
{
    [BsonIgnoreExtraElements]
    [MongoCollection(Name = "Advertisements")]
    public class Advertisement : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("university_id")]
        public string UniversityId { get; set; }

        [BsonElement("company_id")]
        public string CompanyId { get; set; }

        [BsonElement("max")]
        public int Max { get; set; }

        [BsonElement("unlimited")]
        public bool Unlimited { get; set; }

        [BsonElement("current")]
        public int Current { get; set; }


        public Advertisement(string universityId, string companyId, int max, bool unlimited)
        {
            this.UniversityId = universityId;
            this.CompanyId = companyId;
            this.Max = max;
            this.Unlimited = unlimited;
            this.Current = 0;
        }
    }
}
