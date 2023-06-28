using IdealDiscuss.Models;
using IdealDiscuss.Models.Role;

namespace IdealDiscuss.Service.Interface;

public interface IRoleService
{
    Task<BaseResponseModel> CreateRole(CreateRoleViewModel request);
    Task<BaseResponseModel> DeleteRole(string roleId);
    Task<BaseResponseModel> UpdateRole(string roleId, UpdateRoleViewModel request);
    Task<RoleResponseModel> GetRole(string roleId);
    Task<RolesResponseModel> GetAllRole();
}
