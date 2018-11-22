using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountCustomer.Models;

namespace AccountCustomer.Data
{
    public class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Addresses.Any())
            {
                return;   // DB has been seeded
            }

            var Addresses = new List<Address>()
                {
                   new Address { Name = "Mrs K Bond",
                       Postcode ="Ts11 8JY",
                   Country="UK",
                   AddressLine1="1 hunstaton grove",
                   AddressLine2=" redcar",
                   Town="middlesbrough",
                   County ="Cleveland",
                   ProfileID = 1,
                   isDeleted = false} };
            foreach (Address s in Addresses)
            {
                context.Addresses.Add(s);
            }
            context.SaveChanges();

            var profiles = new Profile[]
            {
             new Profile {Name ="kirsty bond",
                        Email ="kirstybond96@gmail.com",
                        PhoneNumber = Int32.Parse("0976642710721"),
                        Password ="1s2w2", isDeleted = false }
            };
            foreach (Profile c in profiles)
            {
                context.Profiles.Add(c);
            }
            context.SaveChanges();

        }
    }
}
