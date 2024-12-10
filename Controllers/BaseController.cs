using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineShopping.Controllers
{


    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                ViewBag.IsAuthenticated = true;
                ViewBag.Username = HttpContext.Session.GetString("Username");
            }
            else
            {
                ViewBag.IsAuthenticated = false;
            }
        }
    }

}
