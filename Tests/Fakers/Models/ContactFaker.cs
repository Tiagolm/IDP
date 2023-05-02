using Bogus;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Fakers.Models
{
    public static class ContactFaker
    {
        public static Contact ContactGenerate()
        {
            var contact = new Faker<Contact>("pt_BR")
                    .CustomInstantiator(f => Activator.CreateInstance(typeof(Contact), nonPublic: true) as Contact)
                    .RuleFor(p => p.Name, p => p.Person.FullName)
                    .RuleFor(p => p.UserId, p => 2) // User Tiago
                    //.RuleFor(p => p.User.Username, p => p.Person.UserName)
                    //.RuleFor(p => p.User.Password, p => p.Database.Random.AlphaNumeric(13))
                    //.RuleFor(p => p.User.UserRoleId, 2)
                    .Generate();

            return contact;
        }

        public static IEnumerable<PhoneContact> PhonesGenerate() 
        {
            var phones = new Faker<PhoneContact>("pt_BR")
                   .CustomInstantiator(f => Activator.CreateInstance(typeof(PhoneContact), nonPublic: true) as PhoneContact)
                   .RuleFor(p => p.Phone, p => p.Phone.PhoneNumber())
                   .RuleFor(p => p.FormattedPhone, p => p.Phone.PhoneNumberFormat())
                   .RuleFor(p => p.Description, p => p.Lorem.Text())
                   .RuleFor(p => p.Ddd, p => p.Random.Int(11, 99))
                   .RuleFor(p => p.Description, p => p.Lorem.Text())
                   .GenerateBetween(1, 5);

            return phones;
        }
    }
}
