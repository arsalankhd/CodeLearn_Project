﻿using CodeLearn.Core.DTOs.Orders;
using CodeLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class MyOrdersController : Controller
    {
        private IOrderService _orderService;

        public MyOrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View(_orderService.GetUserOrders(User.Identity.Name));
        }

        public IActionResult ShowOrder(int id, bool finaly = false, string type = "")
        {
            var order = _orderService.GetOrderForUserPanel(User.Identity.Name, id);

            if (order == null)
            {
                return NotFound();
            }

            ViewBag.typeDiscount = type;
            ViewBag.finaly = finaly;
            return View(order);
        }

        public IActionResult FinallyOrder(int id)
        {
            if (_orderService.FinallyOrder(User.Identity.Name, id))
            {
                return Redirect("/UserPanel/MyOrders/ShowOrder/" + id + "?finaly=true");
            }

            return BadRequest();
        }

        public IActionResult UseDiscount(int orderId, string code)
        {
            DiscountUseType type = _orderService.UseDiscount(orderId, code);

            return Redirect("/UserPanel/MyOrders/ShowOrder/" + orderId + "?type=" + type.ToString());
        }
    }
}
