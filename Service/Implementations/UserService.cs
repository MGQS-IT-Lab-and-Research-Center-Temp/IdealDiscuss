using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.UserDto;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace IdealDiscuss.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public BaseResponseModel AddUser(CreateUserDto request, string roleName)
        {
            try
            {
                byte[] salt = new byte[128 / 8];

                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                string saltString = Convert.ToBase64String(salt);

                string hashedPassword = HashPassword(request.Password, saltString);

                var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
                var createdDate = DateTime.Now;

                var userExist = _userRepository.Exists(x => x.UserName == request.UserName || x.Email == request.Email);

                if (userExist)
                {
                    return new BaseResponseModel
                    {
                        Message = $"User with {request.UserName} or {request.Email} already exist",
                        Status = false
                    };
                }

                roleName ??= "AppUser";

                var role = _roleRepository.Get(x => x.RoleName == roleName);

                var user = new User
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    HashSalt = saltString,
                    PasswordHash = hashedPassword,
                    RoleId = role.Id,
                    CreatedBy = createdBy,
                    DateCreated = createdDate,
                };

                _userRepository.Create(user);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel
                {
                    Message = $"Unable to create user: {ex.Message}"
                };
            }

            return new BaseResponseModel
            {
                Message = $"User with {request.UserName} added succesfully",
                Status = true
            };
        }

        public ViewUserDto GetUser(string userId)
        {
            var user = _userRepository.GetUser(x => x.Id == userId);

            if (user is null)
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
            var response = new ViewUserDto();

            try
            {
                var user = _userRepository.GetUser(x => (x.UserName.ToLower() == username.ToLower() || x.Email.ToLower() == username.ToLower()));

                if (user is null)
                {
                    return new ViewUserDto
                    {
                        Message = $"Account does not exist!",
                        Status = false
                    };
                }

                string hashedPassword = HashPassword(password, user.HashSalt);

                if (!user.PasswordHash.Equals(hashedPassword))
                {
                    return new ViewUserDto
                    {
                        Message = $"Incorrect username or password!",
                        Status = false
                    };
                }

                response.Id = user.Id;
                response.UserName = user.UserName;
                response.Email = user.Email;
                response.RoleId = user.RoleId;
                response.RoleName = user.Role.RoleName;
                response.Message = $"User successfully retrieved";
                response.Status = true;

            }
            catch (Exception ex)
            {
                response.Message = $"An error occured: {ex.Message}";
                return response;
            }

            return response;
        }

        private string HashPassword(string password, string salt)
        {
            byte[] saltByte = Convert.FromBase64String(salt);

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltByte,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
