using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Restaurant.Models;
using Meal_Ordering_Restaurant.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Meal_Ordering_Restaurant.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly ManagementService _managementService;
        public OrderController(OrderService orderService, ManagementService managementService)
        {
            _orderService = orderService;
            _managementService = managementService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync(string? tabId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Authorization")))
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
            //Need Menu -> make api request
            GetMenuRequest getMenuRequest = await _managementService.GetMenuAsync(HttpContext.Session.GetString("Authorization"));
            //Need Orders -> make api request
            GetOrdersRequest getOrdersRequest = await _orderService.GetOrdersAsync(HttpContext.Session.GetString("Authorization"));

            if (getOrdersRequest == null)
            {
                TempData["ErrorMessage"] = $"Failed to get Orders from API.";
                return View();
            }
            else
            {
                getOrdersRequest.Orders = getOrdersRequest.Orders.Where(o => o.Status != "Cart").ToList();
            }

            if (getMenuRequest == null)
            {
                TempData["ErrorMessage"] = $"Failed to get Menu from API.";
                return View();
            }

            OrderViewModel orderViewModel = new OrderViewModel()
            {
                Categories = getMenuRequest.Categories
            };

            var tabIdFromSession = HttpContext.Session.GetString("SelectedTabId");
            if (tabIdFromSession != null)
            {
                orderViewModel.SelectedTab = tabIdFromSession;
            }

            if (tabId != null)
            {
                orderViewModel.SelectedTab = tabId;
                HttpContext.Session.SetString("SelectedTabId", tabId);
            }

            switch (HttpContext.Session.GetString("SelectedTabId"))
            {
                case "pending":
                    orderViewModel.Orders = getOrdersRequest.Orders.Where(o => o.Status == "Pending").ToList();
                    break;
                case "active":
                    orderViewModel.Orders = getOrdersRequest.Orders.Where(o => o.Status != "Pending" && o.Status != "ODelivery" && o.Status != "Delivered").ToList();
                    break;
                case "history":
                    orderViewModel.Orders = getOrdersRequest.Orders.Where(o => o.Status == "ODelivery" || o.Status == "Delivered").Reverse().ToList();
                    break;
                default:
                    orderViewModel.Orders = getOrdersRequest.Orders.Where(o => o.Status != "Pending" && o.Status != "ODelivery" && o.Status != "Delivered").ToList();
                    break;
            }
            return View(orderViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(OrderViewModel orderViewModel)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Authorization")))
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
            
            if (ModelState.IsValid)
            {
                if (orderViewModel.UpdateOrderRequest != null)
                {
                    var response = await _orderService.UpdateOrderAsync(HttpContext.Session.GetString("Authorization"), orderViewModel.UpdateOrderRequest);
                    var responseContent = JObject.Parse(await response.Content.ReadAsStringAsync());

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["LastActionMessage"] = $"({response.StatusCode}) : {responseContent["message"]}";
                        return RedirectToAction("Index", "Order");
                    }
                    TempData["ErrorMessage"] = $"({response.StatusCode}) : {responseContent["message"]}";
                }
                TempData["ErrorMessage"] = $"Invalid Request";
            }
            var tabIdFromSession = HttpContext.Session.GetString("SelectedTabId");
            if (tabIdFromSession != null)
            {
                orderViewModel.SelectedTab = tabIdFromSession;
            }
            return View(orderViewModel);
        }

    }
}
