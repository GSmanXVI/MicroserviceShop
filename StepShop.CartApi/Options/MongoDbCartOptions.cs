using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StepShop.CartApi.Options
{
    public class MongoDbCartOptions
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CartCollectionName { get; set; }
    }
}
