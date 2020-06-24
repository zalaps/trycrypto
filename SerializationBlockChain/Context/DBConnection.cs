using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerializationBlockChain.Context
{
    public class DBConnection
    {
        public string Name { get; set; }

        public string ConnectionString { get; set; }

        public string Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
