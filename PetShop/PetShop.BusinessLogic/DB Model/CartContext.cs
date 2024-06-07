using PetShop.Domain.Entities.Shop;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1BussinesLogic.DBModel
{
    public class CartContext : DbContext
    {
        public CartContext() : base("name=petShop") { }

        public virtual DbSet<CartDbTable> Carts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartDbTable>()
                .HasMany(c => c.Items)
                .WithOptional()
                .HasForeignKey(c => c.CartId);
        }
    }
}
