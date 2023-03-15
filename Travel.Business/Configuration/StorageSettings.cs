using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Business.Configuration
{
    public class StorageSettings
    { 
        public string ConnectionString { get; set; }
        public string SafeDataTable { get; set; }
        public string SafePartitionKey { get; set; }
    }
}
