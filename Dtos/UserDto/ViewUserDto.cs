
namespace IdealDiscuss.Dtos.UserDto
{
    public class ViewUserDto : BaseResponseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
