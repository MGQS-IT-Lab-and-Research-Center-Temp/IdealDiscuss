using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.RoleDto;

namespace IdealDiscuss.Service.Interface
{
    public interface IRoleService
    {
        BaseResponseModel CreateRole(CreateRoleDto request);
        BaseResponseModel DeleteRole(string roleId);
        BaseResponseModel UpdateRole(string roleId, UpdateRoleDto request);
        RoleResponseModel GetRole(string roleId);
        RolesResponseModel GetAllRole();
    }
}
