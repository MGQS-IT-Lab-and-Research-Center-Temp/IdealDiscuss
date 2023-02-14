namespace IdealDiscuss.Dtos.RoleDto
{
    public class ViewRoleDetailDto
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }
    }
}
