using AutoMapper;
using ClassLibrary1BussinesLogic.DBModel;
using PetShop.Domain.Entities.Response;
using PetShop.Domain.Entities.User;
using PetShop.Domain.Enums;
using PetShop.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

    public class UserApi
    {
        internal Response UserLoginAction(ULoginData data)
        {
            UDbTable result;
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(data.Email))
            {
                var pass = LoginHelper.HashGen(data.Password);
                using (var db = new UserContext())
                {
                    result = db.Users.FirstOrDefault(u => u.Email == data.Email && u.Password == pass);
                }

                if (result == null)
                {
                    return new Response { Status = false, ActionStatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new UserContext())
                {
                    result.LastIP = data.UserIp;
                    result.LastLogin = data.LoginDate;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
                }
            if (result.Level == URole.admin) return new Response { Status = true, ActionStatusMsg = "admin" };

            

            return new Response { Status = true  };
            }
            else
                return new Response { Status = false };
        }

        internal Response UserLogoutAction(UserMinimal profile)
    {
        using(var db = new UserContext())
        {
            var user = db.Users.FirstOrDefault(u => u.Id == profile.Id);
            if (user == null) return new Response { Status = false, ActionStatusMsg = "User Not Found" };
            if (user.Level == URole.guest) return new Response { Status = false, ActionStatusMsg = "The Profile is a Guest" };
        }
        using(var db = new SessionContext())
        {
            var userSession = db.Sessions.FirstOrDefault(s => s.Username ==  profile.Email );
            db.Sessions.Remove(userSession);
            db.SaveChanges();
        }
        return new Response { Status = true };
    }

        internal Response UserRegisterAction(URegisterData data, UserMinimal guestProfile)
        {
            UDbTable existingUser;
            
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(data.Email))
            {
                using (var db = new UserContext())
                {
                    existingUser = db.Users.FirstOrDefault(u => u.Email == data.Email);

                    if (existingUser != null && existingUser.Password != null)
                    {
                        return new Response { Status = false, ActionStatusMsg = "User With Email Already Exists" };
                    }
                }
            var pass = LoginHelper.HashGen(data.Password);

            if (existingUser != null && existingUser.Password == null)
            {
                using(var db = new UserContext())
                {
                    existingUser = db.Users.FirstOrDefault(u => u.Email == data.Email);
                    existingUser.Password = pass;
                    existingUser.Username = data.Username;
                    db.Entry(existingUser).State = EntityState.Modified;
                    db.SaveChanges();
                    return new Response { Status = true, ActionStatusMsg = "Registered Existing Customer" };
                }
            }
            else if (guestProfile != null)
            {
                using (var db = new UserContext())
                {
                    var currentGuest = db.Users.FirstOrDefault(u => u.Username == guestProfile.Username);
                    if (currentGuest != null)
                    {
                        currentGuest.Email = data.Email;
                        currentGuest.Username = data.Username;
                        currentGuest.Password = pass;
                        currentGuest.LastIP = data.LoginIp;
                        currentGuest.LastLogin = data.LoginDateTime;
                        currentGuest.Level = (URole)1;
                        db.SaveChanges();

                        using (var sessionContext = new SessionContext())
                        {
                            var guestSession = sessionContext.Sessions.FirstOrDefault(s => s.Username == guestProfile.Username);
                            if(guestSession != null)
                            {
                                sessionContext.Sessions.Remove(guestSession);
                                sessionContext.SaveChanges();
                            }
                        }

                        return new Response { Status = true, ActionStatusMsg = "Registered Guest" };
                    }
                    else return new Response { Status = false, ActionStatusMsg = "Gust Profile Not Found" };
                }

                
            }
            else
            {
                var newUser = new UDbTable
                {
                    Email = data.Email,
                    Username = data.Username,
                    Password = pass,
                    LastIP = data.LoginIp,
                    LastLogin = data.LoginDateTime,
                    Level = (URole)1,
                };



                using (var todo = new UserContext())
                {
                    todo.Users.Add(newUser);
                    todo.SaveChanges();
                }
                return new Response { Status = true, ActionStatusMsg = "Registered New User" };
            }
                
            }
            else
                return new Response { Status = false, ActionStatusMsg = "Email Invalid" };
        }


        internal Response GuestRegisterAction(GuestRegisterData data)
        {
        UDbTable existingUser;
        using (var db = new UserContext())
        {
            existingUser = db.Users.FirstOrDefault(u => u.Username == data.Username);
        }
        if (existingUser != null) { return new Response { Status = false, ActionStatusMsg = "Guest Already Exists" }; }

        var newUser = new UDbTable
        {
            Email = null,
            Username = data.Username,
            Password = null,
            LastIP = data.LoginIp,
            LastLogin = data.LoginDateTime,
            Level = (URole)0,
        };
        using (var todo = new UserContext())
        {
            todo.Users.Add(newUser);
            todo.SaveChanges();
        }
        return new Response { Status = true };

    }
  

        internal HttpCookie Cookie(string loginCredential)
        {
            int sessionTime = 60;
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            using (var db = new SessionContext())
            {
                Session curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(loginCredential))
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }
                else
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }

                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(sessionTime);
                    using (var todo = new SessionContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        Username = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(sessionTime)
                    });
                    db.SaveChanges();
                }
            }
            return apiCookie;
        }



    internal HttpCookie GuestCookie(string guestUsername)
    {
        int sessionTime = 120;

        var guestCookie = new HttpCookie("PetShop-Guest")
        {
            Value = CookieGenerator.Create(guestUsername),
        };


        
        using (var db = new SessionContext())
        {
            db.Sessions.Add(new Session
            {
                Username = guestUsername,
                CookieString = guestCookie.Value,
                ExpireTime = DateTime.Now.AddMinutes(sessionTime)
            });
            db.SaveChanges();

        }
            return guestCookie;
    }


    internal UserMinimal UserCookie(string cookie)
        {
            Session session;
            UDbTable curentUser;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new UserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.Username))
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                }
                else
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Username == session.Username);
                }
            }

            if (curentUser == null) return null;
            var userminimal = Mapper.Map<UserMinimal>(curentUser);

            return userminimal;
        }


    public async Task<Response> CleanupGuestUsersAction()
    {
        using (var sessionContext = new SessionContext())
        {
            var expiredSessions = sessionContext.Sessions
                .Where(s => s.ExpireTime < DateTime.Now)
                .AsEnumerable(); 

            
            
            var expiredUsernames = expiredSessions.Select(s => s.Username).ToList();

            using (var userContext = new UserContext())
            {
                var usersToDelete = userContext.Users
                    .Where(u => u.Level == URole.guest && expiredUsernames.Contains(u.Username))
                    .AsEnumerable(); 

                var cartsToDelete = userContext.Carts
                    .Where(c => usersToDelete.Any(u => u.Id == c.UserId))
                    .AsEnumerable(); 

                userContext.Carts.RemoveRange(cartsToDelete);
                userContext.Users.RemoveRange(usersToDelete);
                sessionContext.Sessions.RemoveRange(expiredSessions);

                await userContext.SaveChangesAsync();
                await sessionContext.SaveChangesAsync();

                return new Response { Status = true, ActionStatusMsg = "Users Cleared" };
            }
        }
    }




}
