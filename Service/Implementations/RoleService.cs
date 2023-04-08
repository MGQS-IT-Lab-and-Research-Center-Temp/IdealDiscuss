using IdealDiscuss.Entities;
using IdealDiscuss.Models;
using IdealDiscuss.Models.Role;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;

namespace IdealDiscuss.Service.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IUnitOfWork _unitOfWork;

        public RoleService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        { 
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public BaseResponseModel CreateRole(CreateRoleViewModel request)
        {
            var response = new BaseResponseModel();

            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;

            var roleExist = _unitOfWork.Roles.Exists(r => r.RoleName == request.RoleName);

            if (roleExist)
            {
                response.Message = $"Role with name {request.RoleName} already exist!";
                return response;
            }

            var role = new Role
            {
                RoleName = request.RoleName,
                Description = request.Description,
                CreatedBy = createdBy
            };

            try
            {
                _unitOfWork.Roles.Create(role);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Role created successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to create role: {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel DeleteRole(string roleId)
        {
            var response = new BaseResponseModel();
            var roleIdExist = _unitOfWork.Roles.Exists(c => c.Id == roleId);
            var role = _unitOfWork.Roles.Get(roleId);

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
                _unitOfWork.Roles.Update(role);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Role deleted successfully.";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Role delete failed: {ex.Message}";
                return response;
            }
        }

        public RolesResponseModel GetAllRole()
        {
            var response = new RolesResponseModel();

            try
            {
                var role = _unitOfWork.Roles.GetAll(r => r.IsDeleted == false);

                if (role.Count == 0)
                {
                    response.Message = "No records found!";
                    return response;
                }

                response.Data = role
                    .Select(r => new RoleViewModel
                    {
                        Id = r.Id,
                        RoleName = r.RoleName,
                        Description = r.Description
                    }).ToList();

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

            var roleExist = _unitOfWork.Roles.Exists(r => 
                                (r.Id == roleId) 
                                && (r.Id == roleId 
                                && r.IsDeleted == false));

            if (!roleExist)
            {
                response.Message = $"Role does not exist!";
                return response;
            }

            var role = _unitOfWork.Roles.Get(roleId);

            response.Data = new RoleViewModel
            {
                Id = roleId,
                RoleName = role.RoleName,
                Description = role.Description,
            };
            response.Message = "Success";
            response.Status = true;

            return response;
        }

        public BaseResponseModel UpdateRole(string id, UpdateRoleViewModel request)
        {
            var response = new BaseResponseModel();
            var modifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;

            var roleIdExist = _unitOfWork.Roles.Exists(c => c.Id == id);

            if (!roleIdExist)
            {
                response.Message = "Role does not exist.";
                return response;
            }

            var role = _unitOfWork.Roles.Get(id);

            role.Description = request.Description;
            role.ModifiedBy = modifiedBy;

            try
            {
                _unitOfWork.Roles.Update(role);
                _unitOfWork.SaveChanges();
                response.Message = "Role updated successfully.";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Could not update the role: {ex.Message}";
                return response;
            }
        }
    }
}
