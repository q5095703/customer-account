using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AccountCustomer.Data;
using AccountCustomer.Models;


namespace AccountCustomer.Controllers
{
    public class HomeController : Controller
    {
        private readonly SchoolContext _context;

        public HomeController(SchoolContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            int ID = 1;
            var address = _context.Addresses.Include(P => P.Profile).Where(m => m.ProfileID == ID);
            return View(address.ToList());
          //  return View();
        }

        public JsonResult Addresses(int? add)
        {
            int ID = 2;
            var address = _context.Addresses.Include(P => P.Profile).Where(m => m.ProfileID == ID && (m.ID == add || add.Equals(null)) && m.isDeleted == false && m.Profile.isDeleted == false);
            return Json(address.ToList());
        }
        public JsonResult Profile()
        {
            int ID = 2;
            var address = _context.Profiles.Where(m => m.ID == ID && m.isDeleted == false);
            return Json(address.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Address address)
        {
            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!AddressExists(address.ID))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return Redirect("/Home/Index");

            }
            return Redirect("/Home/Index");

        }






        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
