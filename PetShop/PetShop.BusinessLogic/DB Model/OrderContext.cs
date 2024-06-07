using PetShop.Domain.Entities.Shop;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1BussinesLogic.DBModel
{
    public class OrderContext : DbContext
    {
        public OrderContext() : base("name=petShop") { }

        public virtual DbSet<OrderDbTable> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDbTable>()
                .HasMany(o => o.Items)
                .WithOptional()
                .HasForeignKey(i => i.OrderId);
        }
    }
}
