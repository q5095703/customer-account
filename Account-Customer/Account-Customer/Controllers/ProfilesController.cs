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
    public class ProfilesController : Controller
    {
        private readonly SchoolContext _context;

        public ProfilesController(SchoolContext context)
        {
            _context = context;
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit( Profile profile)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.ID))
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

        private bool ProfileExists(int id)
        {
            return _context.Profiles.Any(e => e.ID == id);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profile = await _context.Profiles.SingleOrDefaultAsync(m => m.ID == id);

            profile.isDeleted =true;
            _context.Update(profile);
            //  _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
            return Redirect("/Home/Index");

        }


    }
}
