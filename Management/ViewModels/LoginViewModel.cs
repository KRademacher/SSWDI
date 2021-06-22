using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Management.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string EmailAddress { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }

        [Display(Name = "Remember credentials?")]
        public bool RememberCredentials { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}