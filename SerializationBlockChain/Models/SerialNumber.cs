using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerializationBlockChain.Models
{
    public class SerialNumber
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string Serialnumber { get; set; } = "";
        public List<Block> BlockChain { get; set; }

        public SerialNumber(string SN)
        {
            Serialnumber = SN;
            BlockChain = new List<Block> { CreateGenesisBlock(SN) };
        }

        public bool IsValidChain()
        {
            for (int i = 1; i < BlockChain.Count; i++)
            {
                Block previousBlock = BlockChain[i - 1];
                Block currentBlock = BlockChain[i];
                if (currentBlock.Hash != currentBlock.CreateHash())
                    return false;
                if (currentBlock.PreviousHash != previousBlock.Hash)
                    return false;
            }
            return true;
        }

        private Block CreateGenesisBlock(string SN)
        {
            return new Block(SN, "Created", "0");
        }

    }
}
