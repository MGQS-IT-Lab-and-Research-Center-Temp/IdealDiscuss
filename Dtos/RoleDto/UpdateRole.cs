namespace IdealDiscuss.Dtos.RoleDto
{
    public class UpdateRole
    {
        public bool IsDeleted { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
    }
}
