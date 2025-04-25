using Microsoft.AspNetCore.Http;
using Resume.Domain.Dtos.User;

namespace Resume.Application.Services.Interface.User
{
    public interface IUserService : IAsyncDisposable
    {
        Task<bool> IsUserExistByPn(string phoneNumber);
        Task<List<FilterUserDto>> GetAllUsers(FilterUserDto filter);
        Task<CreateUserResult> CreateUser(RegisterUserDto user, IFormFile avatarImage);
        Task<UpdateUserDto> GetForEditUser(long id);
        Task<UpdateUserResult> EditUser(UpdateUserDto user, IFormFile avatarImage);
        Task<bool> BlockUser(long id);
        Task<bool> UnblockUser(long id);
        Task<UserDetailDto> GetUserDetail();
        Task<UserLoginResult> UserLogin(LoginDto login);
        Task<Domain.Entities.User.User> GetUserByPn(string phoneNumber);
        Task<Domain.Entities.User.User> GetUserByEa(string emailAddress);
        Task<UserDetailDto> GetUserById(long id);
        Task<string> GetStoredSalt(string phoneNumber);
        Task<string> GetStoredPassword(string phoneNumber);
    }
}
