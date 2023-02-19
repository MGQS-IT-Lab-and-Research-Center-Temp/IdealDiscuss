using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.UserDto;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;

namespace IdealDiscuss.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public BaseResponseModel AddUser(CreateUserDto request, string roleName)
        {
            //Check if the User exist
            var userExist = _userRepository.Exists(x => x.UserName == request.UserName || x.Email ==  request.Email);
            if (userExist)
            {
                return new BaseResponseModel
                {
                    Message = $"User with {request.UserName} already exist",
                    Status = false
                };
            }
            
            roleName ??= "AppUser";

            var role = _roleRepository.Get(x => x.RoleName == roleName);
            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
                RoleId = role.Id
            };

            _userRepository.Create(user);
            return new BaseResponseModel
            {
                Message = $"User with {request.UserName} added succesfully",
                Status = true
            };
        }

        public ViewUserDto GetUser(int userId)
        {
            var user = _userRepository.GetUser(x => x.Id == userId);
            if(user is null)
            {
                return new ViewUserDto
                {
                    Message = $"User with {userId} does not exist",
                    Status = false
                };
            }

            return new ViewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                RoleId = user.RoleId,
                RoleName = user.Role.RoleName,
                Message = $"User successfully retrieved",
                Status = true
            };
        }

        public ViewUserDto Login(string username, string password)
        {
            var user = _userRepository.GetUser(x => (x.UserName.ToLower() == username.ToLower() || x.Email.ToLower() == username.ToLower()) && x.Password == password);
            if(user is null)
            {
                return new ViewUserDto
                {
                    Message = $"Invalid username or password",
                    Status = false
                };
            }
            return new ViewUserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                RoleId = user.RoleId,
                RoleName = user.Role.RoleName,
                Message = $"User successfully retrieved",
                Status = true
            };

        }
    }
}
