using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StepShop.CartApi.Models;
using StepShop.CartApi.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StepShop.CartApi.Services
{
    public class CartUnitOfWork
    {
        public CartUnitOfWork(
            MongoClient mongoClient,
            IOptions<MongoDbCartOptions> options)
        {
            Cart = mongoClient
                    .GetDatabase(options.Value.DatabaseName)
                    .GetCollection<Cart>(options.Value.CartCollectionName);
        }

        public IMongoCollection<Cart> Cart { get; set; }
    }
}
