using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerializationBlockChain.Models
{
    public class BlockChain
    {
        public string Serialnumber { get; set; } = "";
        public List<Block> Chain { get; set; }

        public BlockChain(string SN)
        {
            Serialnumber = SN;
            Chain = new List<Block> { CreateGenesisBlock(SN) };
        }

        public void CreateSerialNumber(string SN)
        {
            Chain = new List<Block> { CreateGenesisBlock(SN) };
        }

        public bool IsValidChain()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block previousBlock = Chain[i - 1];
                Block currentBlock = Chain[i];
                if (currentBlock.Hash != currentBlock.CreateHash())
                    return false;
                if (currentBlock.PreviousHash != previousBlock.Hash)
                    return false;
            }
            return true;
        }

        private Block CreateGenesisBlock(string SN)
        {
            return new Block(SN, "Download", "0");
        }
    }
}
