namespace IdealDiscuss.Entities.Dtos.User
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string HashSalt { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
