﻿using IdealDiscuss.Entities;
using IdealDiscuss.Helper;
using IdealDiscuss.Models;
using IdealDiscuss.Models.Auth;
using IdealDiscuss.Models.User;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;

namespace IdealDiscuss.Service.Implementations;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
    {
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponseModel> Register(SignUpViewModel request, string roleName)
    {
        var response = new BaseResponseModel();
        string saltString = HashingHelper.GenerateSalt();
        string hashedPassword = HashingHelper.HashPassword(request.Password, saltString);          
        var userExist = await _unitOfWork.Users.ExistsAsync(x => x.UserName == request.UserName || x.Email == request.Email);

        if (userExist)
        {
            response.Message = $"User with {request.UserName} or {request.Email} already exist";
            return response;
        }

        roleName ??= "AppUser";

        var role = await _unitOfWork.Roles.GetAsync(x => x.RoleName == roleName);

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
            RoleId = role.Id              
        };

        try
        {
            await _unitOfWork.Users.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            response.Message = $"You have succesfully signed up on IdealDiscuss";
            response.Status = true;

            return response;
        }
        catch (Exception ex)
        {
            return new BaseResponseModel
            {
                Message = $"Unable to signup, an error occurred {ex.Message}"
            };
        }
    }

    public async Task<UserResponseModel> GetUser(string userId)
    {
        var response = new UserResponseModel();
        var user = await _unitOfWork.Users.GetUser(x => x.Id == userId);

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

    public async Task<UserResponseModel> Login(LoginViewModel model)
    {
        var response = new UserResponseModel();

        try
        {
            var user = await _unitOfWork.Users.GetUser(x =>
                            (x.UserName.ToLower() == model.UserName.ToLower()
                            || x.Email.ToLower() == model.UserName.ToLower()));

            if (user is null)
            {
                response.Message = $"Account does not exist!";
                return response;
            }

            string hashedPassword = HashingHelper.HashPassword(model.Password, user.HashSalt);

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
            response.Message = $"Welcome {user.UserName}";
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
