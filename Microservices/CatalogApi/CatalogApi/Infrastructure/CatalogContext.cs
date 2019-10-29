using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;


namespace CatalogApi.Infrastructure
{
    public class CatalogContext
    {
        private IConfiguration configuration;
        private IMongoDatabase database;
        public CatalogContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            var connectionString = configuration.GetValue<String>("MongoSettings:ConnectionString");
            MongoClientSettings setting = MongoClientSettings.FromConnectionString(connectionString);
            MongoClient client = new MongoClient(setting);
            if (client != null)
            {
                this.database = client.GetDatabase(configuration.GetValue<String>("MongoSettings:Database"));
            }            
        }
        public IMongoCollection<CatalogItem> Catalog
        {
            get
            {
                return this.database.GetCollection<CatalogItem>("products");
            }
        }

    }
    
}
