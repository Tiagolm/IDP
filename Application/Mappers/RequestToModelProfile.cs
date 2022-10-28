using Application.ViewModels.Contact;
using Application.ViewModels.User;
using AutoMapper;
using Domain.Models.Auth;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels.PhoneContact;
using Application.Extensions;

namespace Application.Mappers
{
    public class RequestToModelProfile : Profile
    {
        public RequestToModelProfile()
        {
            CreateMap<ContactRequest, Contact>()
                .MergeList(x => x.Phones, vm => vm.Phones);

            CreateMap<UserRequest, User>();

            CreateMap<PhoneContactRequest, PhoneContact>()
                .ForMember(x => x.Ddd, m => m.MapFrom(vm => vm.FormattedPhone.SplitFormattedPhone().Item1))
                .ForMember(x => x.Phone, m => m.MapFrom(vm => vm.FormattedPhone.SplitFormattedPhone().Item2));
        }
    }
}
