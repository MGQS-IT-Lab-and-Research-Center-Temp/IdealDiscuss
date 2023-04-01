using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.UserDto;
using IdealDiscuss.Entities;
using IdealDiscuss.Helper;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;

namespace IdealDiscuss.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public BaseResponseModel AddUser(CreateUserDto request, string roleName)
        {
            try
            {
                string saltString = HashingHelper.GenerateSalt();
                string hashedPassword = HashingHelper.HashPassword(request.Password, saltString);
                var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
                var createdDate = DateTime.Now;

                var userExist = _unitOfWork.Users.Exists(x => x.UserName == request.UserName || x.Email == request.Email);

                if (userExist)
                {
                    return new BaseResponseModel
                    {
                        Message = $"User with {request.UserName} or {request.Email} already exist",
                        Status = false
                    };
                }

                roleName ??= "AppUser";

                var role = _unitOfWork.Roles.Get(x => x.RoleName == roleName);

                if (role is null)
                {
                    return new BaseResponseModel
                    {
                        Message = $"Role does not exist",
                        Status = false
                    };
                }

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

                _unitOfWork.Users.Create(user);
                _unitOfWork.SaveChanges();
                return new BaseResponseModel
                {
                    Message = $"User with {request.UserName} added succesfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel
                {
                    Message = $"Unable to create user: {ex.Message}"
                };
            }
        }

        public ViewUserDto GetUser(string userId)
        {
            var user = _unitOfWork.Users.GetUser(x => x.Id == userId);

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
                var user = _unitOfWork.Users.GetUser(x => 
                                (x.UserName.ToLower() == username.ToLower() 
                                || x.Email.ToLower() == username.ToLower()));

                if (user is null)
                {
                    return new ViewUserDto
                    {
                        Message = $"Account does not exist!",
                        Status = false
                    };
                }

                string hashedPassword = HashingHelper.HashPassword(password, user.HashSalt);

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

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured: {ex.Message}";
                return response;
            }
        }
    }
}
