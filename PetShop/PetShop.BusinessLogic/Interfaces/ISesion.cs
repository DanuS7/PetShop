using PetShop.Domain.Entities.Response;
using PetShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.BusinessLogic.Interfaces
{
    public interface ISesion
    {
        ULoginResp UserLogin(ULoginData data);
    }
}
