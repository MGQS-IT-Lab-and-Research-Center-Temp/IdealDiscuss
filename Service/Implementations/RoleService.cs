using IdealDiscuss.Controllers;
using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.RoleDto;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace IdealDiscuss.Service.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<RoleService> _logger;

        public RoleService(
            IRoleRepository roleRepository, 
            IHttpContextAccessor httpContextAccessor,
            ILogger<RoleService> logger)
        {
            _roleRepository = roleRepository;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public BaseResponseModel CreateRole(CreateRoleDto createRoleDto)
        {
            var response = new BaseResponseModel();

            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;

            var roleExist = _roleRepository.Exists(r => r.RoleName == createRoleDto.RoleName);

            if (roleExist)
            {
                response.Message = $"Role with name {createRoleDto.RoleName} already exist!";
                return response;
            }

            if (string.IsNullOrWhiteSpace(createRoleDto.RoleName))
            {
                response.Message = "Role name is required!";
                return response;
            }

            var role = new Role()
            {
                RoleName = createRoleDto.RoleName,
                Description = createRoleDto.Description,
                CreatedBy = createdBy,
                DateCreated = DateTime.Now
            };

            try
            {
                _roleRepository.Create(role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "-" + ex.StackTrace);
                response.Message = $"Failed to create role:";
                return response;
            }

            response.Status = true;
            response.Message = "Role created successfully.";
            _logger.LogInformation(response.Message);
            return response;
        }

        public BaseResponseModel DeleteRole(int roleId)
        {
            var response = new RoleResponseModel();
            var roleIdExist = _roleRepository.Exists(c => c.Id == roleId);

            if (!roleIdExist)
            {
                response.Message = "Role does not exist.";
                return response;
            }

            var role = _roleRepository.Get(roleId);
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

            response.Status = true;
            response.Message = "Role deleted successfully.";
            return response;
        }

        public RolesResponseModel GetAllRole()
        {
            var response = new RolesResponseModel();

            var role = _roleRepository.GetAll();

            if (role.Count == 0)
            {
                response.Message = "No records found!";
                return response;
            }

            response.Roles = role.Select(r => new ViewRoleDto
            {
                Id = r.Id,
                RoleName = r.RoleName,
                Description = r.Description
            }).ToList();

            response.Status = true;
            response.Message = "Success";

            return response;
        }

        public RoleResponseModel GetRole(int roleId)
        {
            var response = new RoleResponseModel();

            var roleExist = _roleRepository.Exists(c => c.Id == roleId);

            if (!roleExist)
            {
                response.Message = $"Role with id {roleId} does not exist.";
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

        public BaseResponseModel UpdateRole(int id, UpdateRoleDto updateRoleDto)
        {
            var response = new BaseResponseModel();

            var roleIdExist = _roleRepository.Exists(c => c.Id == id);

            if (!roleIdExist)
            {
                response.Message = "Role does not exist.";
                return response;
            }

            var role = _roleRepository.Get(id);

            role.RoleName = updateRoleDto.RoleName;
            role.Description = updateRoleDto.Description;

            try
            {
                _roleRepository.Update(role);
            }
            catch (Exception ex)
            {
                response.Message = $"Could not update the role: {ex.Message}";
                return response;
            }
            response.Message = "Role updated successfully.";
            return response;
        }
    }
}
