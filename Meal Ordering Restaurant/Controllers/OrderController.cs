using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Restaurant.Models;
using Meal_Ordering_Restaurant.Services;
using Microsoft.AspNetCore.Mvc;

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

            if (getMenuRequest == null)
            {
                TempData["ErrorMessage"] = $"Failed to get Menu from API.";
                return View();
            }

            OrderViewModel orderViewModel = new OrderViewModel()
            {
                Categories = getMenuRequest.Categories
            };
            switch (tabId)
            {
                case "pending":
                    orderViewModel.Orders = getOrdersRequest.Orders.Where(o => o.Status == "Pending").ToList();
                    break;
                case "active":
                    orderViewModel.Orders = getOrdersRequest.Orders.Where(o => o.Status != "Pending" && o.Status != "Out for delivery" && o.Status != "Delivered").ToList();
                    break;
                case "history":
                    orderViewModel.Orders = getOrdersRequest.Orders.Where(o => o.Status == "Out for delivery" || o.Status != "Delivered").ToList();
                    break;
                default:
                    orderViewModel.Orders = getOrdersRequest.Orders;
                    break;
            }
            return View(orderViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> OrderTabContent(string tabId)
        {
            //Need Menu -> make api request
            GetMenuRequest getMenuRequest = await _managementService.GetMenuAsync(HttpContext.Session.GetString("Authorization"));
            //Need Orders -> make api request
            GetOrdersRequest getOrdersRequest = await _orderService.GetOrdersAsync(HttpContext.Session.GetString("Authorization"));

            switch (tabId)
            {
                case "pending":
                    OrderViewModel pendingOrderViewModel = new OrderViewModel()
                    {
                        Orders = getOrdersRequest.Orders.Where(o => o.Status == "Pending").ToList(),
                        Categories = getMenuRequest.Categories
                    };
                    return PartialView("_PendingTabPartial", pendingOrderViewModel);
                case "active":
                    OrderViewModel activeOrderViewModel = new OrderViewModel()
                    {
                        Orders = getOrdersRequest.Orders.Where(o => o.Status != "Pending" && o.Status != "Out for delivery" && o.Status != "Delivered").ToList(),
                        Categories = getMenuRequest.Categories
                    };
                    return PartialView("_OrderCardPartial", activeOrderViewModel);
                case "history":
                    OrderViewModel historyOrderViewModel = new OrderViewModel()
                    {
                        Orders = getOrdersRequest.Orders.Where(o => o.Status == "Out for delivery" || o.Status != "Delivered").ToList(),
                        Categories = getMenuRequest.Categories
                    };
                    return PartialView("_HistoryTabPartial", historyOrderViewModel);
                default:
                    return NotFound();
            }
        }
    }
}
