using System;
namespace CuponeraUniversitaria.Infrastructure
{
    public class MongoCollectionAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
