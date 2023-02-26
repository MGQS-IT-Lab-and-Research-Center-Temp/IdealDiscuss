using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.RoleDto;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;

namespace IdealDiscuss.Service.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public BaseResponseModel CreateRole(CreateRoleDto createRoleDto)
        {
            var roleResponse = new RoleResponseModel();
            var role = new Role()
            {
                RoleName = createRoleDto.RoleName,
                Description = createRoleDto.Description,
                CreatedBy = "Admin",
                DateCreated = DateTime.Now
            };
            try
            {
                _roleRepository.Create(role);
                _roleRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                roleResponse.Message = $"Failed to create role.{ex.Message}";
                return roleResponse;
            } 
            roleResponse.Status = true;
            roleResponse.Message = "Role created successfully.";
            return roleResponse;
        }

        public BaseResponseModel DeleteRole(int roleId)
        {
            var response = new RoleResponseModel();

            if (!_roleRepository.Exists(c => c.Id == roleId))
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

            var roles = _roleRepository.GetAll();

            response.Role = roles.Select(role => new ViewRoleDto
            {
                Id = role.Id,
                Description = role.Description,
                RoleName = role.RoleName
            }).ToList();

            response.Status = true;
            response.Message = "Success";

            return response;
        }

        public RoleResponseModel GetRole(int roleId)
        {
            var response = new RoleResponseModel();

            if (!_roleRepository.Exists(c => c.Id == roleId))
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
                RoleName = roles.RoleName,
            };

            return response;
        }

        public BaseResponseModel UpdateRole(int roleId, UpdateRoleDto updateRoleDto)
        {
            var response = new RoleResponseModel();

            if (!_roleRepository.Exists(c => c.Id == roleId))
            {
                response.Message = "Role does not exist.";
                return response;
            }

            var role = _roleRepository.Get(roleId);

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
