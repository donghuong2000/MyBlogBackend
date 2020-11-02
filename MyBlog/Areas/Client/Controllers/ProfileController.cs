using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MyBlog.Data.Data;
using MyBlog.Data.Repository.IRepository;
using MyBlog.Extension;
using MyBlog.Model;
using MyBlog.Models;
using MyBlog.Models.ViewModels;
using SQLitePCL;

namespace MyBlog.Areas.Client.Controllers
{
    [Area("Client")]
    public class ProfileController : BaseController
    {
        private readonly ApplicationDB _db;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostEnvironment _hostEnvironment;
        
        public ProfileController(ApplicationDB dB, IUnitOfWork unitOfWork,IHostEnvironment hostEnvironment)
        {
            _db = dB;
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            
        }
        public IActionResult Index()
        {
            var user = HttpContext.Session.Get<User>(SD.CurrentUser);
            UpdateProfileViewModel model = new UpdateProfileViewModel();
            model.Email = user.Email;
            model.Name = user.Name;
            model.Phone = user.PhoneNumber;
            ViewBag.UpdateProfilemodel = model;
            ViewBag.user = user;
            return View();
        }


        private void UpdateCurrentUserSession(User user)
        {
            HttpContext.Session.Remove(SD.CurrentUser);
            HttpContext.Session.Set(SD.CurrentUser, user);
        }

        //api-----------------------------------------------------------------------------------------------------
        public IActionResult GetUserPost()
        {
           var user  = HttpContext.Session.Get<User>(SD.CurrentUser);
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
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            PostViewModel postView = new PostViewModel();
            // lấy tất cả tag trong database

            var tagslist = _unitOfWork.Tag.GetAll();
            ViewData["list"] = new SelectList(tagslist, "Id", "Name");
            if (id == null)
            {
                postView.Post = new Post();
                postView.TagSelected = new List<int>();
                return View(postView);
            }
            //lấy post đó
            postView.Post = _unitOfWork.Post.Get(id.GetValueOrDefault());

            //lấy danh sach tag của post đó
            if (postView.Post == null)
            {
                return NotFound();
            }

            var b = _db.PostTags.Where(p => p.PostId == id).Select(tid => tid.TagId).ToList();
            postView.TagSelected = b;
            return View(postView);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(PostViewModel postView)
        {
            if (ModelState.IsValid)
            {
                if (postView.Post.ID == 0)
                {
                    var u = HttpContext.Session.Get<User>(SD.CurrentUser);
                    postView.Post.UserID = u.Id;
                    postView.Post.DateCreate = DateTime.Now;
                    var tags = new List<PostTag>();
                    foreach (var item in postView.TagSelected)
                    {
                        tags.Add(new PostTag() { PostId = postView.Post.ID, TagId = item });
                    }
                    postView.Post.PostTags = tags;
                    _unitOfWork.Post.Add(postView.Post);
                }
                else
                {
                    var p = _db.Posts.Include(x => x.PostTags).FirstOrDefault(x => x.ID == postView.Post.ID);

                    _db.TryUpdateManyToMany(p.PostTags, postView.TagSelected
                        .Select(x => new PostTag
                        {
                            TagId = x,
                            PostId = postView.Post.ID

                        }), x => x.TagId);



                    _unitOfWork.Post.Update(postView.Post);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(postView);
        }


       
        [HttpPost]
        [Produces("application/json")]
        public IActionResult Upload()
        {
            string fileName = "";
            string folderName = "wwwroot\\Media\\";
            IFormFile file;
            try
            {
                file = Request.Form.Files[0];
                string webRootPath = _hostEnvironment.ContentRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);

                    }
                }
                return Json(new { location = $"{fileName}" });
            }
            catch (System.Exception ex)
            {
                return Json("Upload Failed: " + ex.Message);
                //return Json(#"HTTP / 1.1 500 Server Error + ex.Message");
            }
        }
        [HttpPost]
        public IActionResult UpdateProfile([Bind("Name,Phone,Email,AvatarUrl")] UpdateProfileViewModel model)
        {
            if (ModelState.IsValid)
            {

                // upload file
                string fileName = "";
                string folderName = "wwwroot\\Media\\";
                string webRootPath = _hostEnvironment.ContentRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (model.AvatarUrl != null)
                {
                    fileName = ContentDispositionHeaderValue.Parse(model.AvatarUrl.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        model.AvatarUrl.CopyTo(stream);

                    }
                }
                var user = HttpContext.Session.Get<User>(SD.CurrentUser);
                user.AvatarUrl = fileName;
                user.Name = model.Name;
                user.PhoneNumber = model.Phone;
                user.Email = model.Email;
                _unitOfWork.User.Update(user);
                _unitOfWork.Save();
                UpdateCurrentUserSession(user);
                return RedirectToAction(nameof(Index));
            }
            return View("Index");
        
            
        }
    }
}
