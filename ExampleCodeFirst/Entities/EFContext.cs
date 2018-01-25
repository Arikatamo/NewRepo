using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCodeFirst.Entities
{
    public class EFContext : DbContext
    {
        public EFContext() : base("ShopConDB")
        {
                
        }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Filters> Filters { get; set; }
        public DbSet<FilterName> FilterName { get; set; }
        public DbSet<FilterValue> FilterValue { get; set; }


    }
}
