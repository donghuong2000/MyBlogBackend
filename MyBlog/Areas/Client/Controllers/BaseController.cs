using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyBlog.Extension;
using MyBlog.Models;

namespace MyBlog.Areas.Client.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.HttpContext.Session.Get<User>(SD.CurrentUser)!= null)
            {
                base.OnActionExecuting(context);
            }    
            else
            {
                context.Result = new RedirectToActionResult("index", "home",new {area = "client" });
                return;
            }    
            
        }
    }
}
