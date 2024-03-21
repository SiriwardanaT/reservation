using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using test.Models;
using test.ViewModal;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var value = HttpContext.Session.GetString("userId");
                if (value != null)
                {
                    //get data from database and bind to the list
                    var listToDisplay = new List<PropertyViewModal>();
                    var _ctx = new ReservationnContext();
                    var PropertyListfromDb = _ctx.Properties.Where( e => e.Status == 1).ToList();


                    foreach (Property property in PropertyListfromDb)
                    {
                        var viewModal = new PropertyViewModal();
                        var defaultedImage = _ctx.Images.ToList().Find(cImage => cImage.Pid == property.Pid);

                        if (defaultedImage != null)
                        {
                            viewModal.UrlList = defaultedImage.Url;
                        }

                        viewModal.Pid = property.Pid;
                        viewModal.Price = property.Price;
                        viewModal.Facilities = property.Facilities;
                        viewModal.CountRoom = property.CountRoom;
                        viewModal.location = property.Location;
                        Location loc = JsonSerializer.Deserialize<Location>(viewModal.location);
                        viewModal.lat = loc.lat;
                        viewModal.lng = loc.lng;
                        viewModal.status = property.Status;
                        listToDisplay.Add(viewModal);
                    }
                    ViewBag.Locations = listToDisplay;
                    return View(listToDisplay);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public IActionResult Welcome()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("type");
            HttpContext.Session.Remove("name");
            return RedirectToAction("Welcome", "Home");
        }

        [HttpPost]
        public  async Task<IActionResult> Login(EnableUser user)
        {
            var _ctx = new ReservationnContext();
            var enuser = await _ctx.EnableUsers
            .Where(e => e.Phone.Contains(user.Phone) && e.Password.Contains(user.Password))
            .FirstOrDefaultAsync();

            if (enuser != null)
            {
                HttpContext.Session.SetString("userId", enuser.Uid);
                HttpContext.Session.SetString("type", enuser.Type.ToString());
                HttpContext.Session.SetString("name", enuser.FirstName);
                if (enuser.Type == 0)
                {
                    //home student
                    return RedirectToAction("Index", "Home");
                }
                else if (enuser.Type == 1)
                {
                    return RedirectToAction("Index", "Landloard", new { uid = enuser.Uid});
                }
                else if (enuser.Type == 2)
                {
                    return RedirectToAction("Index", "Warden");
                }
                else if (enuser.Type == 5)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return View();
                }

            }
            else {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(EnableUser enableUser)
        {
            try
            {
                var _ctx = new ReservationnContext();
                enableUser.Uid = Guid.NewGuid().ToString().Substring(1, 5);
                enableUser.Type = 1;
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


        public IActionResult Register()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
