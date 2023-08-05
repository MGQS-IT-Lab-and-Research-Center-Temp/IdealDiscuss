﻿using System.ComponentModel.DataAnnotations;

namespace IdealDiscuss.DTOs.Flag;

public class FlagUpdateDto
{
    public string Id { get; set; }

    [Required(ErrorMessage = "Enter flag Name!")]
    [MinLength(3, ErrorMessage = "The minimum length is 3")]
    [MaxLength(150, ErrorMessage = "The Maximum length is 150")]
    public string FlagName { get; set; }

    public string Description { get; set; }
}
