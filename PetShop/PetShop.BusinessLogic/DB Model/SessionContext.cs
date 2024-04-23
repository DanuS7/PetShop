using PetShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.BusinessLogic.DB_Model
{
    class SessionContext : DbContext
    {
        public SessionContext() : base("name=petshop")
        {
        }
        public virtual DbSet<Session> Sessions { get; set; }
    }
}
