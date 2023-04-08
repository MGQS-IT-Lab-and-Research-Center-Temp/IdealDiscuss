using IdealDiscuss.Models;
using IdealDiscuss.Models.Auth;
using IdealDiscuss.Models.User;

namespace IdealDiscuss.Service.Interface
{
    public interface IUserService
    {
        UserResponseModel GetUser(string userId);
        BaseResponseModel Register(SignUpViewModel request, string roleName = null);
        UserResponseModel Login(LoginViewModel request);
    }
}
