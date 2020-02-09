using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StepShop.CartApi.Notifications.ProductPriceChanged
{
    public class ProductPriceChangedNotificationHandler : INotificationHandler<ProductPriceChangedNotification>
    {
        IMongoDatabase db;

        public ProductPriceChangedNotificationHandler()
        {
            MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
            db = mongoClient.GetDatabase("StepShopCartDb");
        }

        public async Task Handle(ProductPriceChangedNotification notification, CancellationToken cancellationToken)
        {
            await db.DropCollectionAsync("Cart");
        }
    }
}
