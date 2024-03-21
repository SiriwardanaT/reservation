using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using test.Models;
using test.ViewModal;

namespace test.Controllers
{
    public class Location {
        public double lat { get; set; }
        public double lng { get; set; }

        public string Id { get; set; }

    }
    public class WardenController : Controller
    {
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
                    var PropertyListfromDb = _ctx.Properties.ToList();


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

        [HttpPost]

        public async Task<IActionResult>  AddStatus(string pid,int status,string reason)
        {
            var _ctx = new ReservationnContext();
             var recivedproperty = await _ctx.Properties.FindAsync(pid);
            if (recivedproperty != null)
            {
                recivedproperty.Status = status;
                recivedproperty.Reason = reason;
                _ctx.Update(recivedproperty);
                _ctx.SaveChanges();
                return RedirectToAction("Index", "Warden");
            }
            else
            {
                return RedirectToAction("Welcome", "Home");
            }
        }
    }
}
