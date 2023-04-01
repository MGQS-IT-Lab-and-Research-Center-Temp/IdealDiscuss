using System.ComponentModel.DataAnnotations;

namespace IdealDiscuss.Models.Flag;

public class UpdateFlagViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Enter flag Name!!!")]
    [MinLength(3, ErrorMessage = "The minimum lenghth is 3.")]
    [MaxLength(150, ErrorMessage = "The Maximum lenghth is 150.")]
    public string FlagName { get; set; }
    public string Description { get; set; }
}
