﻿using System.ComponentModel.DataAnnotations;

namespace IdealDiscuss.DTOs.Flag;

public class FlagCreateDto
{
    [Required(ErrorMessage = "Enter flag Name!")]
    [MinLength(3, ErrorMessage = "The minimum lenghh is 3")]
    [MaxLength(150, ErrorMessage = "The Maximum length is 150")]
    public string FlagName { get; set; }

    [MinLength(10, ErrorMessage = "The minimum lenghh is 3")]
    public string Description { get; set; }
}
