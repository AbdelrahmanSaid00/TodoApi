﻿using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs
{
    public class LoginDto
    {

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
