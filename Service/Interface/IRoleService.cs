using IdealDiscuss.Dtos.CommentReport;
using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.RoleDto;

namespace IdealDiscuss.Service.Interface
{
    public interface IRoleService
    {
        BaseResponseModel CreateRole(CreateRoleDto createRoleDto);
        BaseResponseModel DeleteRole(int roleId);
        BaseResponseModel UpdateRole(int roleId, UpdateRoleDto updateRoleDto);
        RoleResponseModel GetRole(int roleId);
        RolesResponseModel GetAllRole();
    }
}
