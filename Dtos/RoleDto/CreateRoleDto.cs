namespace IdealDiscuss.Dtos.RoleDto
{
    public class CreateRoleDto
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
      
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }
    }
}
