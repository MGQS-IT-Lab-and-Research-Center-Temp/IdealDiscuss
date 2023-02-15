namespace IdealDiscuss.Dtos.RoleDto
{
    public class UpdateRoleDto
    {
        public bool IsDeleted { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
    }
}
