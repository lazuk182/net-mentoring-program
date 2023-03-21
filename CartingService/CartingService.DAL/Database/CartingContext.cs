using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CartingService.DAL.Database
{
    public class CartingContext : DbContext
    {
        public CartingContext(DbContextOptions<CartingContext> options) : base(options)
        {

        }
        public DbSet<Models.Cart> Carts { get; set; }
        public DbSet<Models.Item> Items { get; set; }
    }
}
