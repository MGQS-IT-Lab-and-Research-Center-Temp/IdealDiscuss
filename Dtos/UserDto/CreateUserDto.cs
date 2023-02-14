namespace IdealDiscuss.Dtos.UserDto
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }
    }
}
