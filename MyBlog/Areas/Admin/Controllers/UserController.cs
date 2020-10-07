using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data.Repository.IRepository;

namespace MyBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
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

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var Allobj = _unitOfWork.User.GetAll().Select(x=> new {
                id = x.Id,
                username = x.UserName,
                name = x.Name,
                phone = x.PhoneNumber,
                mail = x.Email,
                avatar = x.AvatarUrl ,
                active = x.Active,

            });
            return Json(new { data = Allobj });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.User.Get(id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while delete" });
            }
            else
            {
                _unitOfWork.User.Remove(obj);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Delete Success" });
            }
        }
        [HttpPost]
        public IActionResult Lock([FromBody] string id)
        {
            var obj = _unitOfWork.User.Get(int.Parse(id));
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while lock" });
            }
            else
            {
                obj.Active = !obj.Active;
                _unitOfWork.Save();
                return Json(new { success = true, message = "Lock Success" });
            }
        }

        #endregion
    }
}
