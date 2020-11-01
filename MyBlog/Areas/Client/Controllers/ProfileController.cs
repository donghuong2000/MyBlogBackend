using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data.Data;
using MyBlog.Data.Repository.IRepository;
using MyBlog.Extension;
using MyBlog.Models;
using SQLitePCL;

namespace MyBlog.Areas.Client.Controllers
{
    [Area("Client")]
    public class ProfileController : BaseController
    {
        private readonly ApplicationDB _db;

        private readonly IUnitOfWork _unitOfWork;
        public ProfileController(ApplicationDB dB, IUnitOfWork unitOfWork)
        {
            _db = dB;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }




        //api-----------------------------------------------------------------------------------------------------
        public IActionResult GetUserPost()
        {
            var user = HttpContext.Session.Get<User>(SD.CurrentUser);
            var Allobj = _db.Posts.Include(x => x.PostTags).ThenInclude(x => x.Tag).Where(x=>x.UserID == user.Id)
                .Select(x => new
                {
                    title = x.Title,
                    content = x.Content.Length,
                    tags = x.ToString(),
                    Date = x.DateCreate.ToShortDateString(),
                    id = x.ID,
                    confirm = x.Confirm

                });


            return Json(new { data = Allobj });
        }
    }
}
