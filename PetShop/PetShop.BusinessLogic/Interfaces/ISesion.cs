using PetShop.Domain.Entities.Response;
using PetShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PetShop.BusinessLogic.Interfaces
{
    public interface ISesion
    {
        Response UserLogin(ULoginData data);
        Response UserLogout(UserMinimal profile);
        Response UserRegister(URegisterData data, UserMinimal guestProfile);
        Response GuestRegister(GuestRegisterData data);
        HttpCookie GenCookie(string loginCredential);
        HttpCookie GenGuestCookie(string guestId);
        UserMinimal GetUserByCookie(string apiCookieValue);
        Task<Response> CleanupGuestUsersAsync();
    }
}
