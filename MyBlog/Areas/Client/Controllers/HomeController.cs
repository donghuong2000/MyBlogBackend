using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data.Data;
using MyBlog.Models.ViewModels;

namespace MyBlog.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly ApplicationDB _db;
        public HomeController(ApplicationDB db)
        {

            _db = db;
        }

        public IActionResult Index()
        {
            
            var home = new HomeViewModel();
            home.Posts = _db.Posts.Include(x=>x.User).Include(x => x.PostTags).ThenInclude(x => x.Tag)
                .Where(x => x.Confirm == true)
                .Select(x => x)
                .OrderBy(x => x.DateCreate)
                .ToList();
            home.BestPosts = home.Posts.OrderByDescending(x => x.Like).Take(5).ToList();
            return View(home);
        }
        public IActionResult Detail(int? id)
        {
            var p = _db.Posts.Include(x=>x.User).Include(x => x.PostTags).ThenInclude(x => x.Tag).FirstOrDefault(x => x.ID == id);
            if (p == null)
                return NotFound();
            return View(p);
        }
    }
}
