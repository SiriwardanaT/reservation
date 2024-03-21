using Azure.Core;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Security.Cryptography;
using test.Models;
using System.Net.Mail;
using System.Net;

namespace test.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Reserve(ReservationRequest reqesut) 
        {
            try
            {
                ReservationnContext _ctx = new ReservationnContext();
                var value = HttpContext.Session.GetString("userId");
                reqesut.Rid = Guid.NewGuid().ToString().Substring(1,4);
                reqesut.Status = 500;
                reqesut.Uid = value;
                _ctx.Add(reqesut);
                _ctx.SaveChanges();

                


                return RedirectToAction("ShowMyReservation", "Student");

            }
            catch (Exception ex)
            {
                return View();
            }   
        }

        public  IActionResult ShowMyReservation()
        {
            try
            {
                var value = HttpContext.Session.GetString("userId");
                if (value != null)
                {
                    ReservationnContext _ctx = new ReservationnContext();
                    var myeservation = _ctx.ReservationRequests.Where(e => e.Uid == value).ToList();
                    ViewBag.myeservation = myeservation;
                    return View();
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

        public IActionResult ShowMyReservationByPid(string pids)
        {
            try
            {
                var value = HttpContext.Session.GetString("userId");
                if (value != null)
                {
                    ReservationnContext _ctx = new ReservationnContext();
                    var myeservation = _ctx.ReservationRequests.Where(e => e.Pid == pids).ToList();
                    ViewBag.myeservation = myeservation;
                    return View();
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
        public async Task<IActionResult> Approve(string Rid,string expids)
        {
            try
            {
                var _ctx = new ReservationnContext();
                var requestEx = await _ctx.ReservationRequests.FindAsync(Rid);
                if (requestEx != null)
                {
                    requestEx.Status = 1;
                    _ctx.Update(requestEx);
                    _ctx.SaveChanges();
                    return RedirectToAction("ShowMyReservationByPid", "Student", new { pids = expids });
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Reject(string Rid, string expids)
        {
            try
            {
                var _ctx = new ReservationnContext();
                var requestEx = await _ctx.ReservationRequests.FindAsync(Rid);
                if (requestEx != null)
                {
                    requestEx.Status = 0;
                    _ctx.Update(requestEx);
                    _ctx.SaveChanges();
                    return RedirectToAction("ShowMyReservationByPid", "Student", new { pids = expids });
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                return View();
            }
        }


    }
}
