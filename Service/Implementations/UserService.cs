using IdealDiscuss.Entities;
using IdealDiscuss.Helper;
using IdealDiscuss.Models;
using IdealDiscuss.Models.Auth;
using IdealDiscuss.Models.User;
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

        public BaseResponseModel AddUser(SignUpViewModel request, string roleName)
        {
            var response = new BaseResponseModel();
            string saltString = HashingHelper.GenerateSalt();
            string hashedPassword = HashingHelper.HashPassword(request.Password, saltString);
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;           
            var userExist = _unitOfWork.Users.Exists(x => x.UserName == request.UserName || x.Email == request.Email);

            if (userExist)
            {
                response.Message = $"User with {request.UserName} or {request.Email} already exist";
                return response;
            }

            roleName ??= "AppUser";

            var role = _unitOfWork.Roles.Get(x => x.RoleName == roleName);

            if (role is null)
            {
                response.Message = $"Role does not exist";
                return response;
            }

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                HashSalt = saltString,
                PasswordHash = hashedPassword,
                RoleId = role.Id,
                CreatedBy = createdBy,                
            };

            try
            {
                _unitOfWork.Users.Create(user);
                _unitOfWork.SaveChanges();
                response.Message = $"User with {request.UserName} added succesfully";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponseModel
                {
                    Message = $"Unable to create user: {ex.Message}"
                };
            }
        }

        public UserResponseModel GetUser(string userId)
        {
            var response = new UserResponseModel();
            var user = _unitOfWork.Users.GetUser(x => x.Id == userId);

            if (user is null)
            {
                response.Message = $"User with {userId} does not exist";
                return response;
            }

            response.Data = new UserViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                RoleId = user.RoleId,
                RoleName = user.Role.RoleName,
            };
            response.Message = $"User successfully retrieved";
            response.Status = true;

            return response;
        }

        public UserResponseModel Login(string username, string password)
        {
            var response = new UserResponseModel();

            try
            {
                var user = _unitOfWork.Users.GetUser(x =>
                                (x.UserName.ToLower() == username.ToLower()
                                || x.Email.ToLower() == username.ToLower()));

                if (user is null)
                {
                    response.Message = $"Account does not exist!";
                    return response;
                }

                string hashedPassword = HashingHelper.HashPassword(password, user.HashSalt);

                if (!user.PasswordHash.Equals(hashedPassword))
                {
                    response.Message = $"Incorrect username or password!";
                    return response;
                }

                response.Data = new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    RoleId = user.RoleId,
                    RoleName = user.Role.RoleName,
                };
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
