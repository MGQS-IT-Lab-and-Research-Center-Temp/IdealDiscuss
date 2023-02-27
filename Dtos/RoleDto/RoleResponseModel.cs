namespace IdealDiscuss.Dtos.RoleDto
{
    public class RoleResponseModel : BaseResponseModel
    {
        public ViewRoleDto Role { get; set; }
    }

    public class RolesResponseModel : BaseResponseModel
    {
        public List<ViewRoleDto> Roles { get; set; }
    }
}
