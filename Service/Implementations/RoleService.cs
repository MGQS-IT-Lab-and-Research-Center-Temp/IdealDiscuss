using IdealDiscuss.Controllers;
using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.RoleDto;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Implementations;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace IdealDiscuss.Service.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(
            IRoleRepository roleRepository,
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public BaseResponseModel CreateRole(CreateRoleDto request)
        {
            var response = new BaseResponseModel();

            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;

            var roleExist = _roleRepository.Exists(r => r.RoleName == request.RoleName);

            if (roleExist)
            {
                response.Message = $"Role with name {request.RoleName} already exist!";
                return response;
            }

            if (string.IsNullOrWhiteSpace(request.RoleName))
            {
                response.Message = "Role name is required!";
                return response;
            }

            var role = new Role()
            {
                RoleName = request.RoleName,
                Description = request.Description,
                CreatedBy = createdBy,
                DateCreated = DateTime.Now,

            };

            try
            {
                _roleRepository.Create(role);
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to create role: {ex.Message}";
                return response;
            }

            _unitOfWork.SaveChanges();
            response.Status = true;
            response.Message = "Role created successfully.";
            return response;
        }

        public BaseResponseModel DeleteRole(string roleId)
        {
            var response = new RoleResponseModel();
            var roleIdExist = _roleRepository.Exists(c => c.Id == roleId);
            var role = _roleRepository.Get(roleId);

            if (!roleIdExist)
            {
                response.Message = "Role does not exist!";
                return response;
            }

            if (role.RoleName == "Admin" || role.RoleName == "AppUser")
            {
                response.Message = "Role cannot be deleted!";
                return response;
            }

            role.IsDeleted = true;

            try
            {
                _roleRepository.Update(role);
            }
            catch (Exception ex)
            {
                response.Message = $"Role delete failed: {ex.Message}";
                return response;
            }

            _unitOfWork.SaveChanges();
            response.Status = true;
            response.Message = "Role deleted successfully.";
            return response;
        }

        public RolesResponseModel GetAllRole()
        {
            var response = new RolesResponseModel();

            try
            {
                var role = _roleRepository.GetAll(r => r.IsDeleted == false);

                if (role.Count == 0)
                {
                    response.Message = "No records found!";
                    return response;
                }

                response.Roles = role
                    .Select(r => new ViewRoleDto
                    {
                        Id = r.Id,
                        RoleName = r.RoleName,
                        Description = r.Description
                    })
                .ToList();

                response.Status = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured: {ex.Message}";
                return response;
            }
            return response;
        }

        public RoleResponseModel GetRole(string roleId)
        {
            var response = new RoleResponseModel();

            var roleExist = _roleRepository.Exists(r => 
                                (r.Id == roleId) 
                                && (r.Id == roleId 
                                && r.IsDeleted == false));

            if (!roleExist)
            {
                response.Message = $"Role does not exist!";
                return response;
            }

            var roles = _roleRepository.Get(roleId);

            response.Message = "Success";
            response.Status = true;

            response.Role = new ViewRoleDto
            {
                Id = roleId,
                Description = roles.Description,
                RoleName = roles.RoleName
            };

            return response;
        }

        public BaseResponseModel UpdateRole(string id, UpdateRoleDto updateRoleDto)
        {
            var response = new BaseResponseModel();
            var modifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;

            var roleIdExist = _roleRepository.Exists(c => c.Id == id);

            if (!roleIdExist)
            {
                response.Message = "Role does not exist.";
                return response;
            }

            var role = _roleRepository.Get(id);

            role.RoleName = updateRoleDto.RoleName;
            role.Description = updateRoleDto.Description;
            role.ModifiedBy = modifiedBy;
            role.LastModified = DateTime.Now;

            try
            {
                _roleRepository.Update(role);
            }
            catch (Exception ex)
            {
                response.Message = $"Could not update the role: {ex.Message}";
                return response;
            }

            _unitOfWork.SaveChanges();
            response.Message = "Role updated successfully.";
            return response;
        }
    }
}
