using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.UserDto;

namespace IdealDiscuss.Service.Interface
{
    public interface IUserService
    {
        ViewUserDto GetUser(int userId);
        BaseResponseModel AddUser(CreateUserDto request, string roleName = null);
        ViewUserDto Login(string username, string password);
    }
}
