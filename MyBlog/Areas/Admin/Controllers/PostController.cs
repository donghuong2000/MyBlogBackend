using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data.Data;

namespace MyBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly ApplicationDB _context;

        public PostController(ApplicationDB context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var Posts = await _context.Posts.ToListAsync();
            return View(Posts);
        }
    }
}
