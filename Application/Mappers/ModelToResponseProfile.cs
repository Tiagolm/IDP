using Application.ViewModels.Contact;
using Application.ViewModels.User;
using Application.ViewModels.UserRole;
using Domain.Core;
using Domain.Models.Auth;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Application.ViewModels.PhoneContact;
using Application.ViewModels.PhoneContactType;

namespace Application.Mappers
{
    public class ModelToResponseProfile : Profile
    {
        public ModelToResponseProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<UserRole, UserRoleResponse>();

            CreateMap<Contact, ContactResponse>();
            CreateMap<Contact, AdminContactResponse>();
            CreateMap<PhoneContact, PhoneContactResponse>();
            CreateMap<PhoneContactType, PhoneContactTypeResponse>();

            CreateMap(typeof(PaginationResult<>), typeof(PaginationResult<>));
        }
    }
}
