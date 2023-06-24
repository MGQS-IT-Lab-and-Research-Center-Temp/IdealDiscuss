using IdealDiscuss.Models;
using IdealDiscuss.Models.Auth;
using IdealDiscuss.Models.User;

namespace IdealDiscuss.Service.Interface;

public interface IUserService
{
    Task<UserResponseModel> GetUser(string userId);
    Task<BaseResponseModel> Register(SignUpViewModel request, string roleName = null);
    Task<UserResponseModel> Login(LoginViewModel request);
}
