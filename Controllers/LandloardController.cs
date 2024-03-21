using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Reflection;
using test.Models;
using test.ViewModal;
using System.Net.Mail;

namespace test.Controllers
{
    public class LandloardController : Controller
    {
        private readonly IHostEnvironment _env;
        public LandloardController(IHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {   
            try {
                //get data from database and bind to the list
                var value = HttpContext.Session.GetString("userId");

                if (value != null)
                {
                    var listToDisplay = new List<PropertyViewModal>();
                    var _ctx = new ReservationnContext();
                    var PropertyListfromDb = _ctx.Properties.Where(e => e.Landownerid == value).ToList();


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
                        viewModal.status = property.Status;
                        viewModal.reason = property.Reason;
                        listToDisplay.Add(viewModal);
                    }

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

        public IActionResult AddProperty()
        {
            var value = HttpContext.Session.GetString("userId");
            if (value != null)
            {
                ViewBag.uid = value;
                return View();
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public IActionResult DeleteProperty(string pid)
        {
            var _ctx = new ReservationnContext();
            var getpro = _ctx.Properties.Where(e => e.Pid == pid).FirstAsync();
            if (getpro.Result != null)
            {
                _ctx.Remove(getpro.Result);
                _ctx.SaveChanges();
                return RedirectToAction("Index", "Landloard");
            }
            else {
                return RedirectToAction("Login", "Home");
            }

        }

        [HttpPost]
        public IActionResult UpdateProperty(string pid)
        {
            var _ctx = new ReservationnContext();
            var getpro = _ctx.Properties.Where(e => e.Pid == pid).FirstAsync();
            if (getpro.Result != null)
            {
                PropertyViewModal pv = new PropertyViewModal();
                pv.Price = getpro.Result.Price;
                pv.Facilities = getpro.Result.Facilities;
                pv.CountRoom = getpro.Result.CountRoom;
                pv.Pid = getpro.Result.Pid;
                ViewBag.property = pv;
                return View();
            }
            else 
            {
                return RedirectToAction("Welcome", "Home");
            }
        }

        [HttpPost]
        public IActionResult UpdatePropertyId(PropertyViewModal model)
        {
            var _ctx = new ReservationnContext();
            var getpro = _ctx.Properties.Where(e => e.Pid == model.Pid).FirstAsync().Result;
            

            if (getpro != null)
            {
                getpro.Price = model.Price;
                getpro.Facilities = model.Facilities;
                getpro.CountRoom = model.CountRoom;
                _ctx.SaveChanges();
                return RedirectToAction("Index", "Landloard");
            }
            else
            {
                return RedirectToAction("Welcome", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProperty(PropertyViewModal model)
        {
            try
            {
                ReservationnContext _ctx = new ReservationnContext();
                var pathList = new List<string>();
                foreach (IFormFile file in model.Image)
                {
                    var uniqueFileName = ReGetUniqueFileName(file.FileName);
                    var dir = Path.Combine(_env.ContentRootPath + "/" + "wwwroot", "Images");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    var filePath = Path.Combine(dir, uniqueFileName);
                    await file.CopyToAsync(new FileStream(filePath, FileMode.Create));

                    var pathtostore = "http://localhost:5119/images/" + uniqueFileName;
                    pathList.Add(pathtostore);
                }
                Random rnd = new Random();

                //add data to the object
                Property prop = new Property();
                Image image = new Image();


                prop.Pid = "PR" + GenerateUniqueNumber();
                prop.Price = model.Price;
                prop.Facilities = model.Facilities;
                prop.Location = model.location;
                prop.CountRoom = model.CountRoom;
                prop.Landownerid = model.landownerid;

                //Todo: save to database
                _ctx.Add(prop);
                var addedPro = _ctx.SaveChanges();

                pathList.ForEach(im => {
                    Image img = new Image();
                    img.Pid = prop.Pid;
                    img.ImageId = "Img" + GenerateUniqueNumber();
                    img.Url = im;
                    _ctx.Add(img);
                    var addimg = _ctx.SaveChanges();
                });


                return RedirectToAction("Index", "Landloard");
            }
            catch (Exception ex)
            {
                return View();

            }
        }

        public IActionResult ShowAllImages(string pid)
        {
            ReservationnContext _ctx = new ReservationnContext();
            var result = _ctx.Images.Where(e => e.Pid == pid).ToList();
            ViewBag.ImgList = result;
            return View();
        }

        private string ReGetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }

        private int GenerateUniqueNumber()
        {
            Random rd = new Random();
            return rd.Next(1000);
        }
    }
}



