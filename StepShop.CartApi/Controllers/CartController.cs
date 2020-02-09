using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using StepShop.CartApi.Models;

namespace StepShop.CartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        IMongoCollection<Cart> collection;

        public CartController()
        {
            MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
            var db = mongoClient.GetDatabase("StepShopCartDb");
            collection = db.GetCollection<Cart>("Cart");
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Cart>> GetCart()
        {
            var username = HttpContext.User.Identity.Name;
            var filter = Builders<Cart>.Filter.Eq(x => x.UserName, username);
            var result = await collection.Find(filter).ToListAsync();
            var cart = result.First();
            if (cart == null)
                return NotFound();

            return cart;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddItemToCart(CartLine item)
        {
            var username = HttpContext.User.Identity.Name;
            var filter = Builders<Cart>.Filter.Eq(x => x.UserName, username);
            var result = await collection.Find(filter).ToListAsync();

            if (!result.Any())
            {
                var userCart = new Cart
                {
                    UserName = username,
                    CartLines = new List<CartLine>()
                };
                userCart.CartLines.Add(item);
                await collection.InsertOneAsync(userCart);
            }
            else
            {
                var userCart = result.First();
                userCart.CartLines.Add(item);
                await collection.ReplaceOneAsync(filter, userCart);
            }

            return Ok();
        }
    }
}