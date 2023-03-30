
namespace IdealDiscuss.Dtos.UserDto
{
    public class ViewUserDto : BaseResponseModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
