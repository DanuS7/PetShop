using PetShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShop.Web.Models
{
    public class CurrentUser
    {
        public UserMinimal user;
        public string status {  get; set; }
    }
}