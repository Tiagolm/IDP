using Application.ViewModels.Contact;
using Application.ViewModels.PhoneContact;
using Bogus;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Fakers.Requests.ContactRequests
{
    public static class ContactRegisterRequestFaker
    {
        public static ContactRequest CustomerRegisterRequestGenerate()
        {
            var customer = new Faker<ContactRequest>("pt_BR")
                    .RuleFor(p => p.Name, p => p.Person.FullName)
                    .RuleFor(p => p.Phones, p => PhonesGenerate())
                    .Generate();

            return customer;
        }

        public static IEnumerable<PhoneContactRequest> PhonesGenerate()
        {
            var phones = new Faker<PhoneContactRequest>("pt_BR")
                   .CustomInstantiator(f => Activator.CreateInstance(typeof(PhoneContactRequest), nonPublic: true) as PhoneContactRequest)
                   .RuleFor(p => p.FormattedPhone, p => p.Phone.PhoneNumberFormat())
                   .RuleFor(p => p.Description, p => p.Lorem.Text())
                   .RuleFor(p => p.PhoneContactTypeId, p => 3)
                   .GenerateBetween(1, 5);

            return phones;
        }
    }
}
