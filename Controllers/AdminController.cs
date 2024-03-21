using Microsoft.AspNetCore.Mvc;
using System.Web;
using test.Models;
namespace test.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                var value = HttpContext.Session.GetString("userId");
                if (value != null)
                {
                    var _ctx = new ReservationnContext();
                    var enableUsers = _ctx.EnableUsers.ToList();
                    if (enableUsers.Any())
                    {
                        ViewBag.EnableUsers = enableUsers;
                    }
                    else
                    {
                        ViewBag.EnableUsers = null;
                    }

                    return View();

                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }

            }
            catch (Exception e)
            {
                return View();
            }
            
            
        }

        public  IActionResult AddNewUser()
        {
            var value = HttpContext.Session.GetString("userId");
            if (value != null)
            {
                return View();
            }
            else 
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewUser(EnableUser enableUser)
        {
            try
            {
                var _ctx = new ReservationnContext();
                enableUser.Uid = Guid.NewGuid().ToString().Substring(1, 5);
                _ctx.Add(enableUser);
                _ctx.SaveChanges();
                ViewBag.ok = true;
                ViewBag.error = false;
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ok = false;
                ViewBag.error = true;
                return View();
            }
        }
    }
}
