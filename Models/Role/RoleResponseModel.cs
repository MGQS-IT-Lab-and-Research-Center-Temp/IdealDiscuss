namespace IdealDiscuss.Models.Role
{
    public class RoleResponseModel : BaseResponseModel
    {
        public RoleViewModel Role { get; set; }
    }

    public class RolesResponseModel : BaseResponseModel
    {
        public List<RoleViewModel> Roles { get; set; }
    }
}
