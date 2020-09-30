using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data.Repository.IRepository;
using MyBlog.Models;

namespace MyBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UpSert(int? id)
        {
            Tag tag = new Tag();
            if (id == null)
            {
                return View(tag);
            }
            tag = _unitOfWork.Tag.Get(id.GetValueOrDefault());
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(Tag tag)
        {
            if (ModelState.IsValid)
            {
                if (tag.Id == 0)
                {
                    _unitOfWork.Tag.Add(tag);
                }
                else
                {
                    _unitOfWork.Tag.Update(tag);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var Allobj = _unitOfWork.Tag.GetAll();
            return Json(new { data = Allobj });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.Tag.Get(id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while delete" });
            }
            else
            {
                _unitOfWork.Tag.Remove(obj);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Delete Success" });
            }
        }

        #endregion
    }
}
