using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PetShop.Domain.Entities.User
{

    public class ULoginData
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserIp { get; set; }
        public DateTime LoginDate { get; set; }

    }
}
