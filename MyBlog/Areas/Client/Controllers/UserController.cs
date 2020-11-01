using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MyBlog.Data.Repository.IRepository;
using MyBlog.Extension;
using MyBlog.Models;
using MyBlog.Models.ViewModels;
using SQLitePCL;

namespace MyBlog.Areas.Client.Controllers
{
    [Area("Client")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            
            return View();
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var u = _unitOfWork.User.GetAll(x=>x.UserName == vm.Username);
                if (u.Count() > 1)
                {
                    return Json(new { success = false, message = "Username đẫ tồn tại" });
                }
                var use = new User() { UserName = vm.Username, Password = vm.Password, Email = vm.Email };
                _unitOfWork.User.Add(use);
                _unitOfWork.Save();
                HttpContext.Session.Set(SD.CurrentUser, use);
                return Json(new { success = true, message = "Tạo tài Khoản thành công" });
            }
            else
            {
                return Json(new { success = false, message = "" });
            }

        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var u = _unitOfWork.User.GetFirstOrDefault(x => x.UserName == vm.UserName && x.Password == vm.Password);
                if (u != null)
                {
                    HttpContext.Session.Set(SD.CurrentUser, u);
                    return Json(new { success = true, message = "Đăng nhập thành công" });
                }
                else
                    return Json(new { success = false, message = "Tài khoản sai hoặc không tồn tại" });
            }

            return Json(new { success = false, message = "" });

        }
        public IActionResult SignOut()
        {
            HttpContext.Session.Remove(SD.CurrentUser);
            return RedirectToAction("index", "home");
            
        }

    }
}
