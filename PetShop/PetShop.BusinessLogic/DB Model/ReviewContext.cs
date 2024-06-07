using PetShop.Domain.Entities.Shop;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1BussinesLogic.DBModel
{
    public class ReviewContext : DbContext
    {
        public ReviewContext() : base("name=petShop") { }

        public virtual DbSet<ReviewDbTable> Reviews { get; set; }
    }
}
