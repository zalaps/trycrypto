using SerializationBlockChain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerializationBlockChain.DBLayer.Repository
{
    public interface ISerialnumberRepository
    {
        void CreateSerialNumber(SerialNumber serialNumber);

        List<SerialNumber> GetSerialNumbers();
    }
}
