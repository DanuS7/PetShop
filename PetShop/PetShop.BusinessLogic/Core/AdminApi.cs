using System;
using PetShop.Domain.Entities.Response;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShop.Domain.Entities.Shop;
using ClassLibrary1BussinesLogic.DBModel;
using System.Data.Entity.Validation;
using System.Data.Entity;


public class AdminApi
{

        public Response AddProductAction(ProductData product)
   {
        var DefaultPicture = FileHelper.ConvertToByteArray(product.DefaultImageFile);
        bool available = false;
        if(product.AvailableUnits > 0) available = true;
        var newProduct = new ProdDbTable
        {
            Title = product.Title,
            Price = product.Price,
            Description = product.Description,
            Category = product.Category,
            DisplayImage = DefaultPicture,
            DateCreated = product.CreateDate,
            AvailableUnits = product.AvailableUnits,
            InStock = available,
            Orders = 0,
        };

        using (var todo = new ProductContext())
        {
            todo.Products.Add(newProduct);
            todo.SaveChanges();
        }
        return new Response { Status = true };
    }


    public Response EditProductAction (EditProductData product) 
    {
        var DefaultPicture = FileHelper.ConvertToByteArray(product.DefaultImageFile);

        using (var todo = new ProductContext())
        {
            var existingProduct = todo.Products.FirstOrDefault(x => x.Id == product.Id);
            if (existingProduct != null)
            {
                
                existingProduct.Title = product.Title;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                if (DefaultPicture != null) existingProduct.DisplayImage = DefaultPicture;
                existingProduct.Category = product.Category;
                if(existingProduct.AvailableUnits > 0)
                {
                    existingProduct.InStock = true;
                } else existingProduct.InStock = false;
                todo.SaveChanges();
            }
            else { return new Response { Status = false, ActionStatusMsg = "Product not found" }; }

        }


        return new Response { Status = true };
    }

    public void DeleteProductAction (int id)
    {
        using (var todo = new ProductContext())
        {
            var product = todo.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                todo.Products.Remove(product);
                todo.SaveChanges();
            }
        }
    }

    public void DeleteReviewAction(int id)
    {
        using (var todo = new ReviewContext())
        {
            var review = todo.Reviews.FirstOrDefault(x => x.Id == id);
            if (review != null)
            {
                todo.Reviews.Remove(review);
                todo.SaveChanges();
            }
        }
    }

    public Response EditTermsAction(string data)
    {
        if (data == null) return new Response { Status = false, ActionStatusMsg = "Terms and Conditions is Null" };
        using (var db = new KnowledgeContext())
        {
            var information = db.Information.FirstOrDefault(i => i.Title == "Terms and Conditions");
            information.Content = data;
            information.DateUpdated = DateTime.Now;
            db.Entry(information).State = EntityState.Modified;
            db.SaveChanges();
            return new Response { Status = true };
        }    
    }

    public Response ChangeOrderStatusAction(OrderStatusData data)
    {
        if (data == null) return new Response { Status = false, ActionStatusMsg = "Order Status Data does not exist" };
        
        using (var db = new OrderContext())
        {
            var order = db.Orders.FirstOrDefault(o => o.Id == data.OrderId);
            if (order == null) return new Response { Status = false, ActionStatusMsg = "Order was not found" };
            order.Status = data.status;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return new Response { Status = true };
        }
    }

    public DailyRevenueData GetDailyRevenueDataAction() 
    {
        DailyRevenueData data = new DailyRevenueData();
        using( var db = new OrderContext()) 
        {
            DateTime today = DateTime.Today;
            var todayOrders = db.Orders
                     .Where(o => o.OrderDate.Year == today.Year &&
                                 o.OrderDate.Month == today.Month &&
                                 o.OrderDate.Day == today.Day)
                     .ToList();
            for (var i = 0; i < 24; i++)
            {
                var hour = i;
                var hourlyRevenue = todayOrders
                    .Where(o => o.OrderDate.Hour == hour)
                    .Sum(o => o.Total);

                data.revenueByHour.Add(new HourlyRevenueData
                {
                    Hour = hour,
                    Revenue = hourlyRevenue
                });
            }
        }
        return data;
    }

   
}

