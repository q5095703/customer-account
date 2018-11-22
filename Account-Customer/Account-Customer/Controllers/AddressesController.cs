using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AccountCustomer.Data;
using AccountCustomer.Models;

namespace AccountCustomer.Controllers
{
    public class AddressesController : Controller
    {
        private readonly SchoolContext _context;

        public AddressesController(SchoolContext context)
        {
            _context = context;
        }



        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create( Address address)
        {
            if (ModelState.IsValid)
            {
                var item = new Address();
                item.isDeleted = false;
                item.Name = address.Name;
                item.AddressLine1 = address.AddressLine1;
                item.AddressLine2 = address.AddressLine2;
                item.Country = address.Country;
                item.Town = address.Town;
                item.County = address.County;
                item.Postcode = address.Postcode;
                item.ProfileID = address.ProfileID;

                _context.Add(item);
                await _context.SaveChangesAsync();
                return Redirect("/Home/Index");

            }
            return Redirect("/Home/Index");

        }



        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    if (!AddressExists(address.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("/Home/Index");

            }
            return Redirect("/Home/Index");

        }


        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var address = await _context.Addresses.SingleOrDefaultAsync(m => m.ID == id);
            address.isDeleted = true;
            _context.Update(address);
            // _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return Redirect("/Home/Index");
        }

        private bool AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.ID == id);
        }


    }
}
