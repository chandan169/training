using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace CatalogApi.Models
{
    public class CatalogItem
    {
        public CatalogItem()
        {
            Vendors = new List<Vendor>();
        }
        [BsonId(IdGenerator =typeof(StringObjectIdGenerator))]
        public string Id { get; set; }

        [BsonElement("name")] //this is used to set the name of attribute from Name to name as typescript
        public string Name { get; set; } //uses camel casing format to maintain this consistency we are using this
        
        [BsonElement("price")]
        public double Price { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("reorderLevel")]
        public int ReorderLevel { get; set; }

        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; }

        [BsonElement("manufacturingDate")]
        public DateTime ManufacturingDate { get; set; }

        [BsonElement("vendors")]
        public List<Vendor> Vendors { get; set; }

    }

    public class Vendor
    {
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
    }
}
