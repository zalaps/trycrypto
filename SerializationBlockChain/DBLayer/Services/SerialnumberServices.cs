using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SerializationBlockChain.Context;
using SerializationBlockChain.DBLayer.Repository;
using SerializationBlockChain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerializationBlockChain.DBLayer.Services
{
    public class SerialnumberServices : ISerialnumberRepository
    {
        private readonly DBContext<SerialNumber> contextSN;


        //public PartnerService() { }

        public SerialnumberServices(IOptions<DBConnection> options)
        {
            contextSN = new DBContext<SerialNumber>(options);
        }

        public void CreateSerialNumber(SerialNumber SN)
        {
            try
            {
                if (ValidateSN(SN).Result == null)
                {
                    contextSN.Entity.InsertOne(SN);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<SerialNumber> ValidateSN(SerialNumber SN)
        {
            try
            {
                var result = await contextSN.Entity.FindAsync(u => (u.Serialnumber == SN.Serialnumber)).Result.FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SerialNumber> GetSerialNumbers()
        {
            return contextSN.Entity.Find(new BsonDocument()).ToList();
        }
        public SerialNumber GetSerialNumber(string SN)
        {
            var filter = Builders<SerialNumber>.Filter.Eq(u => u.Serialnumber, SN);
            return contextSN.Entity.Find(filter).FirstOrDefault();
        }

        public void AddBlock(string SN, Block block)
        {
            try
            {
                var update = Builders<SerialNumber>.Update.Push(s=>s.BlockChain,block);
                contextSN.Entity.UpdateOne(n => n.Serialnumber == SN, update);
            }
            catch { }
        }
        //public UpdateResult UpdateUser(string id, User useritem)
        //{
        //    try
        //    {
        //        //var _id = new ObjectId(id);
        //        //var filter = Builders<User>.Filter.Eq(u => u._id, id);
        //        var update = Builders<User>.Update
        //                            .Set(u => u.FirstName, useritem.FirstName)
        //                            .Set(u => u.LastName, useritem.LastName)
        //                            .Set(u => u.MiddleName, useritem.MiddleName)
        //                            .Set(u => u.MobileNo, useritem.MobileNo)
        //                            .Set(u => u.EmailId, useritem.EmailId)
        //                            .Set(u => u.UserImage, useritem.UserImage)
        //                            .Set(u => u.UpdatedOn, DateTime.Now)
        //                            .Set(u => u.UpdatedBy, useritem.UpdatedBy);
        //        var result = _usercontext.Entity.UpdateOne(u => u._id == id, update);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
