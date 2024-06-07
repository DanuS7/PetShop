using PetShop.BusinessLogic.Interfaces;
using PetShop.Domain.Entities.Response;
using PetShop.Domain.Entities.User;
using System.Threading.Tasks;
using System.Web;

namespace PetShop.BusinessLogic.AppBL
{
    public class SessionBL : UserApi, ISesion
    {
        public Response UserLogin(ULoginData data)
        {
            return UserLoginAction(data);
        }

        public Response UserLogout(UserMinimal profile)
        { 
            return UserLogoutAction(profile); 
        }

        public Response UserRegister(URegisterData data, UserMinimal guestProfile)
        {
            return UserRegisterAction(data, guestProfile);
        }

        public Response GuestRegister(GuestRegisterData data)
        {
            return GuestRegisterAction(data);
        }

        public HttpCookie GenCookie(string loginCredential)
        {
            return Cookie(loginCredential);
        }

        public HttpCookie GenGuestCookie(string guestId)
        {
            return GuestCookie(guestId);
        }
        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }

       public  Task<Response> CleanupGuestUsersAsync()
        {
            return  CleanupGuestUsersAction();
        }
    }
}
