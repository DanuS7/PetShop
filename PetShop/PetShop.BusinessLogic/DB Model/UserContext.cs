﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using PetShop.Domain.Entities.Shop;
using PetShop.Domain.Entities.User;


namespace ClassLibrary1BussinesLogic.DBModel
{
    public class UserContext : DbContext
    {
        public UserContext() : base("name=petShop") { }

        public virtual DbSet<UDbTable> Users { get; set; }
        public virtual DbSet<CartDbTable> Carts { get; set; }
    }
}
