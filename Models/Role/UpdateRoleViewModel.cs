using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IdealDiscuss.Models.Role
{
    public class UpdateRoleViewModel
    {
        public string Id { get; set; }

        [ReadOnly(true)]
        public string RoleName { get; set; }

        [MinLength(20, ErrorMessage = "The minimum length acceptable is 20")]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
