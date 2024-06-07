using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using PetShop.Domain.Entities.Shop;


namespace ClassLibrary1BussinesLogic.DBModel
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("name=petShop") { }

        public virtual DbSet<ProdDbTable> Products { get; set; }
    }

}