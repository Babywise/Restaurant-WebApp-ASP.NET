using Meal_Ordering_API.Entities;
using Meal_Ordering_WebApp.Entities;
using Microsoft.AspNetCore.Mvc;
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
            bool check = false;
            string message = "";
            //verify
            if (ApiKey != Guid.Empty)
            {
                check = true;
                message = "Cart Checked-out";
            }
            else
            {
                check = false;
                message = "Guid is empty";
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
        /// Updates an order. Update order status of an existing order to change
        /// Type: PUT
        /// </summary>
        /// <param name="order"></param>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPut("/API/V1/Ordering/update")]
        public string Update(Order order, [FromHeader] Guid ApiKey)
        {
            bool check = false;
            string message = "";
            //verify
            if (order != null && ApiKey != Guid.Empty)
            {
                check = true;
                message = "Updated Order";
            }
            else
            {
                check = false;
                message = "Either Guid is empty or order is null";
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
        /// Gets all available products from the store
        /// Type : GET
        /// </summary>
        /// <returns></returns>
        [HttpGet("/API/V1/Ordering/getAllProducts")]
        public string GetAllProducts()
        {
           List<Product> products = new List<Product>();
            Product p1 = new Product
            {
                Inventory = 3,
                Cost = (float)2.23,
                Name = "Test"
            };

            Product p2 = new Product
            {
                Inventory = 1,
                Name = "Stub",
                Cost = (float)4.33
            };
            products.Add(p1);
            products.Add(p2);
            return JsonSerializer.Serialize(products);
        }


        /// <summary>
        /// Get all available products from the category passed in. Returns an empty list on fail
        /// Type : GET
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet("/API/V1/Ordering/GetAllProducts")]
        public string GetAllProductsCategory(Category category)
        {
            List<Product> products = new List<Product>();
            if (category.Name== "Test")
            {
                Product p1 = new Product
                {
                    Inventory = 3,
                    Cost = (float)2.23,
                    Name = "category1_item1",
                    CategoryId = category.Id
                };

                Product p2 = new Product
                {
                    Inventory = 1,
                    Name = "category1_item2",
                    Cost = (float)4.33,
                    CategoryId = category.Id
                };
                products.Add(p1);
                products.Add(p2);
            }
            else
            {
                Product p1 = new Product
                {
                    Inventory = 3,
                    Cost = (float)2.23,
                    Name = "category2_item1",
                    CategoryId = category.Id
                };

                Product p2 = new Product
                {
                    Inventory = 1,
                    Name = "category2_item2",
                    Cost = (float)4.33,
                    CategoryId = category.Id
                };
                products.Add(p1);
                products.Add(p2);
            }
   
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
          List<Order> orders = new List<Order>();
            List<Product> products = new List<Product>();

            Product p1 = new Product
            {
                Inventory = 3,
                Cost = (float)2.23,
                Name = "category2_item1",
                CategoryId = 1
            };

            Product p2 = new Product
            {
                Inventory = 1,
                Name = "category2_item2",
                Cost = (float)4.33,
                CategoryId =2
            };
            products.Add(p1);
            products.Add(p2);

            Order o1 = new Order()
            {
                Id= 1, 
                CustomerId=1,
                StoreId=2,
                products=products,
                Status = "cart",
                Updated= false
            };

            Order o2 = new Order()
            {
                Id = 2,
                CustomerId = 1,
                StoreId = 2,
                products = products,
                Status = "cart",
                Updated = false
            };
            return JsonSerializer.Serialize(orders);
        }
        

    }
}
