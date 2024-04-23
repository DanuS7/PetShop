using PetShop.BusinessLogic.Interfaces;
using PetShop.Domain.Entities.Response;
using PetShop.Domain.Entities.User;
using System.Web;

namespace PetShop.BusinessLogic.AppBL
{
    public class SessionBL : UserApi, ISesion
    {
        public ULoginResp UserLogin(ULoginData data)
        {
            return UserLoginAction(data);
        }

        public URegisterResp UserRegister(URegisterData data)
        {
            return UserRegisterAction(data);
        }

        public HttpCookie GenCookie(string loginCredential)
        {
            return Cookie(loginCredential);
        }
        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }
    }
}
