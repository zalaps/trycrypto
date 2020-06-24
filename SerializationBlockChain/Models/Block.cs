using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SerializationBlockChain.Models
{
    public class Block
    {
        public string SerialNumber { get; set; }
        public string Hash { get; private set; }
        public string Status { get; set; }
        public string PreviousHash { get; set; }

        public Block(string SN, string status, string previousHash = "")
        {
            SerialNumber = SN;
            Status = status;
            PreviousHash = previousHash;
            Hash = CreateHash();
        }

        public string CreateHash()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string rawData = PreviousHash + SerialNumber + Status;
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return Encoding.Default.GetString(bytes);
            }
        }
    }
}
