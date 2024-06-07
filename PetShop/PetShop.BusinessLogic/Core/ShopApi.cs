using AutoMapper;
using ClassLibrary1BussinesLogic.DBModel;
using Microsoft.Owin.BuilderProperties;
using PetShop.BusinessLogic.Interfaces;
using PetShop.Domain.Entities.Response;
using PetShop.Domain.Entities.Shop;
using PetShop.Domain.Entities.User;
using PetShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;



public class ShopApi
    {

    private UDbTable GetUser(int id)
    {
        using (var userContext = new UserContext())
        {
            return userContext.Users.FirstOrDefault(u => u.Id == id);

        }
    }

    private CartDbTable GetUserCart(int userId)
    {
        using (var cartContext = new CartContext())
        {
            return cartContext.Carts.Include("Items").FirstOrDefault(u => u.UserId == userId);

        }
    }

    private ProdDbTable GetProd(int productId) 
    {
        using(var prodContext = new ProductContext()) 
        {
            return prodContext.Products.FirstOrDefault(p => p.Id == productId);
        }
    }


    public Response AddToCartAction(AddToCartData item)
    {
        var product = GetProd(item.ProductId);
        if(product == null) { return new Response { Status = false, ActionStatusMsg = "Product Not Found" }; }
        if(!product.InStock) { return new Response { Status = false, ActionStatusMsg = "Product Not Available" }; }

        if (item.user != null)
        {
            using (var userContext = new UserContext())
            {
                var currentUser = userContext.Users.FirstOrDefault(u => u.Id == item.user.Id);

                if (currentUser != null)
                {
                    using (var cartContext = new CartContext())
                    {
                        var userCart = cartContext.Carts.Include("Items")
                            .FirstOrDefault(c => c.UserId == currentUser.Id);

                        if (userCart == null)
                        {
                            userCart = new CartDbTable
                            {
                                UserId = currentUser.Id,
                                Items = new List<CartItemData>(),
                                TotalItems = 0,
                                TotalPrice = 0,
                            };
                            cartContext.Carts.Add(userCart);
                        }

                        var existingItem = userCart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
                        if (existingItem != null)
                        {
                            existingItem.Quantity += item.Quantity;
                            existingItem.TotalPrice += item.Quantity * existingItem.ProductPrice;
                        }
                        else
                        {
                            var newItem = new CartItemData
                            {
                                ProductId = item.ProductId,
                                ProductName = product.Title,
                                ProductPrice = product.Price,
                                Quantity = item.Quantity,
                                TotalPrice = product.Price * item.Quantity,
                                DisplayImage = product.DisplayImage,
                                CartId = userCart.Id
                            };
                            userCart.Items.Add(newItem);
                            userCart.TotalItems += 1;
                        }
                        userCart.TotalPrice = userCart.Items.Sum(i => i.TotalPrice);
                        cartContext.SaveChanges();
                    }
                    return new Response { Status = true, ActionStatusMsg = "Item Added" };
                }
                else
                {
                }
            }
        }
        else
        {
        }

        return new Response { Status = false , ActionStatusMsg = "Something Happened"};
    }


    public Response RemoveFromCartAction(RemoveItemCartData item)
    {
        var user = GetUser(item.user.Id);
        if (user == null) return new Response { Status = false, ActionStatusMsg = "User Not Found" };

        using (var cartContext = new CartContext())
        {
            var userCart = cartContext.Carts.Include("Items").FirstOrDefault(c => c.UserId == item.user.Id);
            if (userCart == null) return new Response { Status = false, ActionStatusMsg = "User Cart Not Found" };

            var itemToRemove = userCart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (itemToRemove == null) return new Response { Status = false, ActionStatusMsg = "Item Not Found" };

           
            if(itemToRemove.Quantity < 2)
            {
                userCart.Items.Remove(itemToRemove);
                cartContext.Entry(itemToRemove).State = EntityState.Deleted;
                userCart.TotalItems -= 1;
            }
            else
            {
                itemToRemove.Quantity -= 1;
                itemToRemove.TotalPrice = itemToRemove.ProductPrice * itemToRemove.Quantity;
            }

            userCart.TotalPrice = userCart.Items.Sum(i => i.TotalPrice);


            cartContext.SaveChanges();
        }

        return new Response { Status = true, ActionStatusMsg = "Item removed from cart successfully" };
    }

    public Response ClearCartAction(ClearCartItemData item)
    {
        var user = GetUser(item.user.Id);
        if (user == null) return new Response { Status = false, ActionStatusMsg = "User Not Found" };

        using (var cartContext = new CartContext())
        {
            var userCart = cartContext.Carts.Include("Items").FirstOrDefault(c => c.UserId == item.user.Id);
            if (userCart == null) return new Response { Status = false, ActionStatusMsg = "User Cart Not Found" };

            var itemToRemove = userCart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (itemToRemove == null) return new Response { Status = false, ActionStatusMsg = "Item Not Found" };
            userCart.Items.Remove(itemToRemove);
            cartContext.Entry(itemToRemove).State = EntityState.Deleted;
            userCart.TotalItems -= 1;
            userCart.TotalPrice = userCart.Items.Sum(i => i.TotalPrice);
            cartContext.SaveChanges();
        }

        return new Response { Status = true, ActionStatusMsg = "Item cleared from cart successfully" };
    }

    public Response CreateCartAction(int userId)
    {
        var user = GetUser(userId);
        if (user == null) return new Response { Status = false, ActionStatusMsg = "User Not Found" };
        var cart = GetUserCart(userId);
        if (cart != null) return new Response { Status = false, ActionStatusMsg = "User Already Has a Cart" };
        var newCart = new CartDbTable
        {
            UserId = user.Id,
            Items = new List<CartItemData>(),
            TotalItems = 0,
            TotalPrice = 0,
        };
        using (var cartContext  = new CartContext())
        {
            cartContext.Carts.Add(newCart);
            cartContext.SaveChanges();
        }
        return new Response { Status = true };
    }


    public List<ProdDbTable> SearchProductsAction(QueryData data)
    {
        
        using (var productContext = new ProductContext())
        {
            IQueryable<ProdDbTable> query = productContext.Products;

            if (data != null && !string.IsNullOrWhiteSpace(data.UserQuery))
            {
                var searchTerms = data.UserQuery.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                query = query.Where(p => searchTerms.Any(term =>
                    p.Title.Contains(term) ||
                    p.Description.Contains(term) ||
                    p.Category.ToString().Contains(term)))
                    .OrderByDescending(p => searchTerms.Sum(term =>
                        (p.Category.ToString().Contains(term) ? 3 :
                        (p.Title.Contains(term) ? 2 :
                        (p.Description.Contains(term) ? 1 : 0)))));
            }


            if (data != null && data.MinPrice.HasValue)
                query = query.Where(p => p.Price >= data.MinPrice);

            if (data != null && data.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= data.MaxPrice);

            if (data != null && data.Category.HasValue)
                query = query.Where(p => p.Category == data.Category);

          
            if (data != null && data.SortByType != null)
            {
                switch (data.SortByType)
                {
                    case SortBy.MostPopular:
                        query = query.OrderByDescending(p => p.Orders);
                        break;
                    case SortBy.LeastPopular:
                        query = query.OrderBy(p => p.Title);
                        break;
                    case SortBy.PriceAscending:
                        query = query.OrderByDescending(p => p.Price); 
                        break;
                    case SortBy.PriceDescending:
                        query = query.OrderBy(p => p.Price);
                        break;
                    case SortBy.NewProducts:
                        query = query.OrderByDescending(p => p.DateCreated);
                        break;
                    case SortBy.OldProducts:
                        query = query.OrderBy(p => p.DateCreated);
                        break;
                    default:                  
                        break;
                }
            }

            return query.ToList();
        }
    }

    public Response PlaceOrderAction(UCheckoutData data)
    {
        if (data == null) return new Response { Status = false, ActionStatusMsg = "Checkout data does not exist" };
        var currentUser = GetUser(data.UserId);
        if(currentUser == null) return new Response { Status = false, ActionStatusMsg = "User does not exist" };
        var userCart = GetUserCart(data.UserId);
        if(userCart == null) return new Response { Status = false, ActionStatusMsg = "User Cart does not exist" };

        using (var ordersDb = new OrderContext())
        {
            var newOrder = new OrderDbTable
            {
                UserId = data.UserId,
                Total = userCart.TotalPrice,
                OrderDate = DateTime.Now,
                Address = data.Address,
                Country = data.Country,
                State = data.State,
                City = data.City,
                ZipCode = data.ZipCode,
                Status = OrderStatus.Pending,
                Items = new List<OrderItemDb>()
            };

           

            //Update Products Units
            using (var productsDb = new ProductContext())
            {
                foreach (var cartItem in userCart.Items)
                {

                    var orderItem = new OrderItemDb
                    {
                        ProductId = cartItem.ProductId,
                        OrderId = newOrder.Id,
                        ProductName = cartItem.ProductName,
                        ProductPrice = cartItem.ProductPrice,
                        TotalPrice = cartItem.TotalPrice,
                        Quantity = cartItem.Quantity,
                        DisplayImage = cartItem.DisplayImage,
                    };
                    var currentProduct = productsDb.Products.FirstOrDefault(p => p.Id == cartItem.ProductId);
                    currentProduct.AvailableUnits -= orderItem.Quantity;
                    currentProduct.Orders += 1;
                    productsDb.Entry(currentProduct).State = EntityState.Modified;
                    productsDb.SaveChanges();
                    newOrder.Items.Add(orderItem);
                }

            }
            
            ordersDb.Orders.Add(newOrder);
            ordersDb.SaveChanges();
        }

        //Clear User Cart
        using (var cartsDb = new CartContext())
        {
            var cart = cartsDb.Carts.Include(c => c.Items).FirstOrDefault(c => c.Id == userCart.Id);
            if (cart != null)
            {
                foreach (var item in cart.Items.ToList())
                {
                    cart.Items.Remove(item);
                    cartsDb.Entry(item).State = EntityState.Deleted;
                }
                cart.TotalItems = 0;
                cart.TotalPrice = 0;
                cartsDb.SaveChanges();
            }
        }
        //Update User's Session
        using (var sessionDb = new SessionContext())
        {
            var currentSession = sessionDb.Sessions.FirstOrDefault(s => s.Username == currentUser.Username);
            currentSession.Username = data.Email;
            sessionDb.SaveChanges();
        }
        //Update User Information
        using (var usersDb = new UserContext())
        {
            var user = usersDb.Users.FirstOrDefault(u => u.Id == currentUser.Id);
            user.Username = data.Username;
            user.Surname = data.Surname;
            user.Email = data.Email;
            user.Address = data.Address;
            user.Country = data.Country;
            user.State = data.State;
            user.City = data.City;
            user.ZipCode = data.ZipCode;
            user.PhoneNumber = data.PhoneNumber;
            user.Level = (URole)1;
            usersDb.Entry(user).State = EntityState.Modified;
            usersDb.SaveChanges();

        }
        

        return new Response { Status = true };
    }

    public UCheckoutData GetUserBillinngAction(int userId)
    {
        using ( var db = new UserContext())
        {
            var user = db.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return null;
            var data = new UCheckoutData
            {
                UserId = user.Id,
                Username = user.Username,
                Surname = user.Surname,
                Email = user.Email,
                Address = user.Address,
                Country = user.Country,
                State = user.State,
                City = user.City,
                ZipCode = user.ZipCode,
                PhoneNumber = user.PhoneNumber
            };
            return data;
        }
    }


    public Response AddReviewAction(ReviewData data)
    {
        UDbTable currentUser;
        using (var db = new UserContext())
        {
            currentUser = db.Users.FirstOrDefault(u => u.Id == data.UserId);
            if (currentUser == null) return new Response { Status = false, ActionStatusMsg = "User for Review not Found" };
        }

        ProdDbTable product;
        using (var db = new ProductContext())
        {
            product = db.Products.FirstOrDefault(p => p.Id == data.ProductId);
            if(product == null) return new Response { Status = false, ActionStatusMsg = "Product for Review not Found" };
            if (product.Reviews == null) product.Reviews = 0;
            product.Reviews += 1;
            if(product.Rating == null) product.Rating = 0;
            product.Rating = ((product.Rating * (product.Reviews - 1)) + data.Rating) / product.Reviews;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
        }

        using (var db = new ReviewContext())
        {
            ReviewDbTable newReview = new ReviewDbTable
            {
                UserId = data.UserId,
                ProductId = data.ProductId,
                Username = data.Name,
                Rating = data.Rating,
                Description = data.Description,
                DatePosted = DateTime.Now,
            };
            db.Reviews.Add(newReview);
            db.SaveChanges();
        }


        return new Response { Status = true };
    }

}

