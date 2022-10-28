using Application.SearchParams;
using Application.ViewModels.User;
using Application.ViewModels.UserRole;

namespace Application.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserRoleResponse> UserRoles();
        Task<IEnumerable<UserResponse>> Search(UserQueryParam parametroBusca);
        Task<UserResponse> GetById(int id);
        Task<UserResponse> Add(UserRequest model);
        Task<UserResponse> Update(int id, UserRequest model);
        Task Delete(int id);
    }
}