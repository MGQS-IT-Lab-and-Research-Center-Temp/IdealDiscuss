using System.ComponentModel.DataAnnotations;

namespace IdealDiscuss.Models.Role
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Role name is required!")]
        [MinLength(3, ErrorMessage = "The minimum length acceptable is 3")]
        [MaxLength(15)]
        public string RoleName { get; set; }

        [MinLength(20, ErrorMessage = "The minimum length acceptable is 20")]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
