using PetShop.Domain.Entities.Shop;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1BussinesLogic.DBModel
{
    public class KnowledgeContext : DbContext
    {
        public KnowledgeContext() : base("name=petShop") { }

        public virtual DbSet<KnowledgeDbTable> Information { get; set; }
    }
}
