using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerializationBlockChain.Context
{
    public class DBContext<T> where T : class
    {
        private readonly IMongoDatabase _database = null;

        private readonly DBConnection dbConnection;


        public DBContext(IOptions<DBConnection> options)
        {
            dbConnection = options.Value;
            var client = new MongoClient(dbConnection.ConnectionString);//Configuration.GetSection("DBConnection").GetSection("ConnectionString").Value);//("mongodb://192.168.1.230:27017");
            if (client != null)
                _database = client.GetDatabase(dbConnection.Name);//Configuration.GetSection("DBConnection").GetSection("DBName").Value);
        }

        public IMongoCollection<T> Entity
        {
            get
            {
                var nameOfEntity = typeof(T).Name;
                return _database.GetCollection<T>(nameOfEntity);
            }
        }

        public IMongoCollection<BsonDocument> GetDynamicEntityCollection(string nameofcollection)
        {
            return _database.GetCollection<BsonDocument>(nameofcollection);
        }
    }
}
