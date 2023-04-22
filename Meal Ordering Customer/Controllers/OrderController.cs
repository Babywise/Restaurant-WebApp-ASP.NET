using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Customer.Models;
using Meal_Ordering_Customer.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Meal_Ordering_Customer.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _customerService;
        private readonly MenuService _menuService;
        public OrderController(OrderService customerService, MenuService menuService)
        {
            _customerService = customerService;
            _menuService = menuService;
        }

        // HTTP GET for the Quick Add, HTTP Post for the form submission of the Menu/DisplayItem Page
        public async Task<IActionResult> AddToCart(int CategoryId, int ProductId, int QuantityToAdd)
        {

            if (QuantityToAdd <= 0) {
                TempData["ErrorMessage"] = "Invalid Quantity";
                return RedirectToAction("DisplayItem", "Menu", new { CategoryId = CategoryId, ProductId = ProductId });
            }

            if (ModelState.IsValid)
            {
                string accessToken = HttpContext.Session.GetString("Authorization");

                if (string.IsNullOrEmpty(accessToken))
                {
                    return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
                }

                string Username = HttpContext.Session.GetString("Username");

                // Create an order product
                OrderProduct orderProduct = new OrderProduct
                {
                    ProductId = ProductId,
                    Quantity = QuantityToAdd
                };

                // Get the list of orders
                GetOrdersRequest orders = await _customerService.GetOrdersByUsernameAsync(HttpContext.Session.GetString("Authorization"), Username);

                // Find the order that has the cart status
                Order cart = orders.Orders.Where(o => o.Status == "Cart").FirstOrDefault();

               
                // If the cart is not null, use it. If it is null, create a new order.
                if (cart != null)
                {
                    orderProduct.OrderId = cart.OrderId;
                    cart.OrderProducts.Add(orderProduct);
                }
                else 
                {
                    cart = new Order
                    {
                        OrderProducts = new List<OrderProduct>(),
                        Status = "Cart",
                        Username = Username,
                    };
                    cart.OrderProducts.Add(orderProduct);
                }

                // Use the update order request to send in the "New Order"
                UpdateOrderRequest uor = new UpdateOrderRequest()
                {
                    Order = cart
                };

                var response = await _customerService.UpdateOrderAsync(HttpContext.Session.GetString("Authorization"), uor);
                var responseContent = JObject.Parse(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    TempData["LastActionMessage"] = $"{responseContent["message"]}";
                }
                else { 
                    TempData["ErrorMessage"] = $"{responseContent["message"]}";
                }

            }
            else
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated  
            }


            // Redirect back to the Menu Display page
            return RedirectToAction("Categories", "Menu");
        }

        // This is the view cart page
        [HttpGet("/Order/Cart")]
        public async Task<IActionResult> Cart()
        {

            if (ModelState.IsValid)
            {
                string accessToken = HttpContext.Session.GetString("Authorization");

                if (string.IsNullOrEmpty(accessToken))
                {
                    return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
                }

                string Username = HttpContext.Session.GetString("Username");

                // Get the list of orders
                GetOrdersRequest orders = await _customerService.GetOrdersByUsernameAsync(HttpContext.Session.GetString("Authorization"), Username);

                // Find the order that has the cart status
                Order cart = orders.Orders.Where(o => o.Status == "Cart").FirstOrDefault();

                if (cart == null) {
                    TempData["ErrorMessage"] = "Cart does not exist. To view cart, first add an item.";
                    return RedirectToAction("Categories", "Menu");
                }

                // Get the menu
                GetMenuRequest gmr = await _menuService.GetMenuAsync(HttpContext.Session.GetString("Authorization"));
                // set the view model
                OrderViewModel ovm = new OrderViewModel
                {
                    Order = cart,
                    Menu = gmr.Categories
                };

                return View("View", ovm);

            }
            else
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
            }

        }

        // This is the order history page
        [HttpGet("/Order/List")]
        public async Task<IActionResult> List()
        {
            if (ModelState.IsValid)
            {
                string accessToken = HttpContext.Session.GetString("Authorization");

                if (string.IsNullOrEmpty(accessToken))
                {
                    return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
                }

                string Username = HttpContext.Session.GetString("Username");
                GetOrdersRequest orders = await _customerService.GetOrdersByUsernameAsync(HttpContext.Session.GetString("Authorization"), Username);

                return View(orders);
            }
            else
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
            }
        }

        // This is the order details page
        [HttpGet("/Order/View/{OrderId}")]
        public async Task<IActionResult> View(int OrderId)
        {

            if (ModelState.IsValid)
            {
                string accessToken = HttpContext.Session.GetString("Authorization");

                if (string.IsNullOrEmpty(accessToken))
                {
                    return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
                }

                string Username = HttpContext.Session.GetString("Username");

                GetOrdersRequest gor = await _customerService.GetOrdersByUsernameAsync(HttpContext.Session.GetString("Authorization"), Username);
                Order order = gor.Orders.Where(o => o.OrderId == OrderId).FirstOrDefault();
                GetMenuRequest gmr = await _menuService.GetMenuAsync(HttpContext.Session.GetString("Authorization"));

                OrderViewModel ovm = new OrderViewModel {
                    Order = order,
                    Menu = gmr.Categories
                };

                return View(ovm);
            }
            else
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
            }

        }

        [HttpPost("/Order/Create")]
        public async Task<IActionResult> Create()
        {
            if (ModelState.IsValid)
            {
                string accessToken = HttpContext.Session.GetString("Authorization");

                if (string.IsNullOrEmpty(accessToken))
                {
                    return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
                }

                string Username = HttpContext.Session.GetString("Username");

                // Get the list of orders
                GetOrdersRequest orders = await _customerService.GetOrdersByUsernameAsync(HttpContext.Session.GetString("Authorization"), Username);

                // Find the order that has the cart status
                Order cart = orders.Orders.Where(o => o.Status == "Cart").FirstOrDefault();

                if (cart != null)
                {
                    cart.Status = "Pending";
                }

                // Use the update order request to send in the "New Order"
                UpdateOrderRequest uor = new UpdateOrderRequest()
                {
                    Order = cart
                };

                var response = await _customerService.UpdateOrderAsync(HttpContext.Session.GetString("Authorization"), uor);
                var responseContent = JObject.Parse(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    TempData["LastActionMessage"] = $"{responseContent["message"]}";
                }
                else
                {
                    TempData["ErrorMessage"] = $"{responseContent["message"]}";
                }

            }
            else
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
            }

            return RedirectToAction("Categories", "Menu");

        }

        [HttpPost("/Order/Delivered/{OrderId}")]
        public async Task<IActionResult> Delivered(int OrderId)
        {
            if (ModelState.IsValid)
            {
                string accessToken = HttpContext.Session.GetString("Authorization");

                if (string.IsNullOrEmpty(accessToken))
                {
                    return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
                }

                string Username = HttpContext.Session.GetString("Username");

                // Get the list of orders
                GetOrdersRequest orders = await _customerService.GetOrdersByUsernameAsync(HttpContext.Session.GetString("Authorization"), Username);

                // Find the order that has the cart status
                Order order = orders.Orders.Where(o => o.OrderId == OrderId).FirstOrDefault();

                if (order != null)
                {
                    order.Status = "Delivered";
                }

                // Use the update order request to send in the "New Order"
                UpdateOrderRequest uor = new UpdateOrderRequest()
                {
                    Order = order
                };

                var response = await _customerService.UpdateOrderAsync(HttpContext.Session.GetString("Authorization"), uor);
                var responseContent = JObject.Parse(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    TempData["LastActionMessage"] = $"{responseContent["message"]}";
                }
                else
                {
                    TempData["ErrorMessage"] = $"{responseContent["message"]}";
                }

            }
            else
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
            }

            return RedirectToAction("List", "Order");

        }

    } 
}
