using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Data
{
    public class ProductContext : IdentityDbContext<User,IdentityRole<long>,long> 
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) 
        {
        }

        public DbSet<Product> Products { get; set; }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
