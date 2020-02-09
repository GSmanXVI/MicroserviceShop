using Microsoft.EntityFrameworkCore;
using StepShop.CatalogApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StepShop.CatalogApi.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
