﻿using System.Collections.Generic;
using System.IO;
using CodeLearn.Core.Generator;
using CodeLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeLearn.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _userService;
        private ICourseService _courseService;

        public HomeController(IUserService userService, ICourseService courseService)
        {
            _userService = userService;
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            var popular = _courseService.GetPopularCourse();
            ViewBag.PopularCourse = popular;
            return View(_courseService.GetCourse().Item1);
        }

        [Route("OnlinePayment/{id}")]
        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];

                var wallet = _userService.GetWalletByWalletId(id);

                var payment = new ZarinpalSandbox.Payment(wallet.Amount);

                var res = payment.Verification(authority).Result;

                if (res.Status == 100)
                {
                    ViewBag.Code = res.RefId;
                    ViewBag.IsSuccess = true;
                    wallet.IsPay = true;
                    _userService.UpdateWallet(wallet);
                }
            }
            return View();
        }

        public IActionResult GetSubGroups(int id)
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب کنید", Value = ""}
            };
            list.AddRange(_courseService.GetSubGroupForManageCourse(id));
            return Json(new SelectList(list, "Value", "Text"));
        }

        [HttpPost]
        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(upload.FileName);

            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/MyImages",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }

            var url = $"{"/MyImages/"}{fileName}";

            return Json(new { uploaded = true, url });
        }

        public IActionResult Error404()
        {
            return View();
        }
    }
}