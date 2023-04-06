using Meal_Ordering_API.Entities;
using Meal_Ordering_WebApp.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace Meal_Ordering_API.Controllers
{
    public class OrderingController : Controller
    {
        private MealOrderingAPIContext _dbContext;

        /// <summary>
        /// Home controller handles all home related functions with the home page.
        /// </summary>
        /// <param name="dbContext"></param>
        public OrderingController(MealOrderingAPIContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Adds an item to cart. Takes an ApiKey from header. Also takes a product.
        /// Type : POST
        /// </summary>
        /// <param name="product"></param>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPost("/API/V1/Ordering/Add")]
        public string Add(Product product, int Quantity, [FromHeader] Guid ApiKey)
        {

            bool check = false;
            string message = "";
            //verify
            if (ApiKey!= Guid.Empty && product != null && Quantity > 0)
            {
                check = true;
                message = "Add item";
            }
            else
            {
                check = false;
                message = "Either Guid is empty or Product is null or Quantity <= 0";
            }


            // Set Headers
            Response.Headers.UserAgent = "API";
            Response.Headers["Message"] = message;

            if (check)
            {
                Response.StatusCode = 200;
            }
            else
            {
                Response.StatusCode = 400;
            }
            //return
            return "";
        }



        /// <summary>
        /// removes an item from the users cart. Requires an ApiKey and Product
        /// Type : PUT
        /// </summary>
        /// <param name="product"></param>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPut("/API/V1/Ordering/Remove")]
        public string Remove(Product product, int Quantity, [FromHeader] Guid ApiKey)
        {
            bool check = false;
            string message = "";
            //verify
            if (ApiKey != Guid.Empty && product != null && Quantity > 0)
            {
                check = true;
                message = "Remove item";
            }
            else
            {
                check = false;
                message = "Either Guid is empty or Product is null or Quantity <= 0";
            }


            // Set Headers
            Response.Headers.UserAgent = "API";
            Response.Headers["Message"] = message;

            if (check)
            {
                Response.StatusCode = 200;
            }
            else
            {
                Response.StatusCode = 400;
            }
            //return
            return "";
        }


        /// <summary>
        /// Places an order for the current user based on the ApiKey
        /// Type: POST
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPost("/API/V1/Ordering/placeOrder")]
        public string Place([FromHeader] Guid ApiKey)
        {
            bool check = true;
            string message = "";
            Order order = new Order();
            
            List<Account> accounts = _dbContext.account.Where(a => a.ApiKey == ApiKey).ToList();
            if (accounts.Count > 0) // ensure api key is valid
            {
                
                switch (accounts[0].AccountType.ToLower()) // ensure it is the resteraunt adding an item to their inventory
                {
                    case "customer":
                        List<Order> orders = _dbContext.order.OrderByDescending(b => b.Id).ToList();
                        int orderId = 0;

                        if(orders.Count > 0)
                        {
                            orderId = (int)(orders[0].Id + 1);
                        }
                        order.Id = orderId;
                       
                        List<Product>products= _dbContext.product.Where(b => b.status == false && b.customerId == accounts[0].Id).ToList();
                        if (products.Count > 0)
                        {
                            order.StoreId = products[0].StoreId;
                            order.CustomerId= accounts[0].Id;
                            foreach (Product p in products)
                            {
                                p.status = true;
                                p.orderId = orderId;
                            }
                            order.products = products;
                            order.Status = "sending";
                            order.Updated = true;
                            try
                            {
                                _dbContext.Add(order);
                                _dbContext.SaveChanges();
                                order.Status = "sent";
                                _dbContext.SaveChanges();
   
                            }
                            catch(Exception ex)
                            {
                                TextWriterTraceListener logListener = new TextWriterTraceListener("./Log.txt", "Logs");
                                Trace.Listeners.Add(logListener);
                                Trace.WriteLine(ex.Message);
                                Trace.Close();
                                check = false;
                                message = "500 Internal Error";
                            }
                        }
                        else
                        {
                            message = "No Products";
                        }
                        break;
                    default:
                        check = false;
                        message = "Invalid account Type";
                        break;
                }
            }
            else // bad api key
            {
                Response.Headers.UserAgent = "API";
                Response.Headers["Message"] = "Invalid ApiKey";


                //return
                return "";
            }

            // Set Headers
            Response.Headers.UserAgent = "API";
            Response.Headers["Message"] = message;

            if (check)
            {
                Response.StatusCode = 200;
            }
            else
            {
                Response.StatusCode = 200;
            }

            //return
            return "";
        }

            /// <summary>
            /// <summary>
            /// Updates an order. Update order status of an existing order to change
            /// Type: PUT
            /// </summary>
            /// <param name="order"></param>
            /// <param name="ApiKey"></param>
            /// <returns></returns>
            [HttpPut("/API/V1/Ordering/update")]
        public string Update([FromBody]Order order, [FromHeader] Guid ApiKey)
        {
           bool check = true;
           string message = "";
            if (order == null || order.Id == null || order.Status == null)
            {
                message = "order or order Id or order Status is null";
                check = false;
            }
            else
            {
                List<Account> accounts = _dbContext.account.Where(a => a.ApiKey == ApiKey).ToList();
                if (accounts.Count > 0) // ensure api key is valid
                {
                    switch (accounts[0].AccountType) // ensure it is the resteraunt adding an item to their inventory
                    {
                        case "Resteraunt":
                            if(order.StoreId!= accounts[0].Id)
                            {
                                check = false;
                                message = "Order Unavailable";
                            }
                            break;
                        default:
                            check = false;
                            message = "Invalid account Type";
                            break;
                    }

                    if (check)
                    {
                        List<Order> orders = _dbContext.order.Where(d => d.Id == order.Id).ToList();
                        if (orders.Count > 0)
                        {
                            orders[0].Status = order.Status;
                            orders[0].Updated = true;
                            _dbContext.SaveChanges();
                        }
                        else
                        {
                            check = false;
                            message = "No Available Orders";
                        }
                    }
                }
                else // bad api key
                {
                    Response.Headers.UserAgent = "API";
                    Response.Headers["Message"] = "Invalid ApiKey";


                    //return
                    return "";
                }


            }
            // Set Headers
            Response.Headers.UserAgent = "API";
                Response.Headers["Message"] = message;

                if (check)
                {
                    Response.StatusCode = 200;
                }
                else
                {
                    Response.StatusCode = 200;
                }
          
            //return
            return "";
        }


        /// <summary>
        /// Gets all available products from the store
        /// Type : GET
        /// </summary>
        /// <returns></returns>
        [HttpGet("/API/V1/Ordering/getAllProducts")]
        public string GetAllProducts()
        {
            List<Product> products = _dbContext.product.ToList();
            Response.Headers.UserAgent = "API";
            Response.Headers["Message"] = "Get all products";
            return JsonSerializer.Serialize(products);
        }


        /// <summary>
        /// Get all available products from the category passed in. Returns an empty list on fail
        /// Type : GET
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost("/API/V1/Ordering/GetAllProducts")]
        public string GetAllProductsCategory(Category category)
        {
            List<Product> products = _dbContext.product.Where(b => b.CategoryId == category.Id).ToList();
            // Set Headers
            Response.Headers.UserAgent = "API";
            Response.Headers["Message"] = "Get all products from "+products[0].Name;
            return JsonSerializer.Serialize(products);
        }


        /// <summary>
        /// Gets all orders from the database for the store or user.
        /// Type : GET
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpGet("/API/V1/Ordering/getAllOrders")]
        public string GetAllOrders([FromHeader] Guid ApiKey)
        {
            string message = "";
            List<Order> orders = new List<Order>();
            List<Account> accounts = _dbContext.account.Where(a => a.ApiKey == ApiKey).ToList();
            if (accounts.Count > 0) // ensure api key is valid
            {
                switch (accounts[0].AccountType)
                {
                    case "Resteraunt":
                       orders= _dbContext.order.Where(d => d.StoreId == accounts[0].Id).ToList();
                        message = "All Resteraunt Orders";
                        break;
                    case "Customer":
                       orders= _dbContext.order.Where(d => d.CustomerId == accounts[0].Id).ToList();
                        message = "All Customer Orders";
                        break;
                    default:
                        message = "Invalid account Type";
                        break;
                }
                
            }
            else // bad api key
            {
                Response.Headers.UserAgent = "API";
                Response.Headers["Message"] = "Invalid ApiKey";
                //return
                return "";
            }
           
      

            // Set Headers
            Response.Headers.UserAgent = "API";
            Response.Headers["Message"] = message;

            return JsonSerializer.Serialize(orders);
        }
        

    }
}
