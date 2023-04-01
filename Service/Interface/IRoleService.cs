using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.RoleDto;
using IdealDiscuss.Models.Role;

namespace IdealDiscuss.Service.Interface
{
    public interface IRoleService
    {
        BaseResponseModel CreateRole(CreateRoleViewModel request);
        BaseResponseModel DeleteRole(string roleId);
        BaseResponseModel UpdateRole(string roleId, UpdateRoleViewModel request);
        RoleResponseModel GetRole(string roleId);
        RolesResponseModel GetAllRole();
    }
}
