namespace IdealDiscuss.Dtos.RoleDto
{
    public class CreateRole
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }
    }
}
