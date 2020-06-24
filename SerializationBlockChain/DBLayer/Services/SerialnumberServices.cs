using Microsoft.Extensions.Options;
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
        private readonly DBContext<SerialNumber> contextPartner;


        //public PartnerService() { }

        public SerialnumberServices(IOptions<DBConnection> options)
        {
            contextPartner = new DBContext<SerialNumber>(options);
        }

        public void CreateSerialNumber(SerialNumber SN)
        {
            try
            {
                if (ValidateUserDetails(SN).Result == null)
                {
                    contextPartner.Entity.InsertOne(SN);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<SerialNumber> ValidateUserDetails(SerialNumber SN)
        {
            try
            {
                var result = await contextPartner.Entity.FindAsync(u => (u.Serialnumber == SN.Serialnumber)).Result.FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public List<SerialNumber> GetSerialNumbers()
        //{
        //    return contextPartner.Entity.Find(s=>s.);
        //}

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
