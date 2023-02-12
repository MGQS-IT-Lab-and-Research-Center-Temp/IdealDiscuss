namespace IdealDiscuss.Dtos.UserDto
{
    public class ViewUserDeatail
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime LastModified { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
