using PetShop.BusinessLogic.AppBL;
using PetShop.BusinessLogic.Interfaces;
using PetShop.Domain.Entities.Response;
using PetShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.BusinessLogic.AppBL
{
    public class SessionBL : UserApi, ISesion
    {
        public ULoginResp UserLogin(ULoginData data)
        {
            return UserLoginAction(data);
        }
    }
}
