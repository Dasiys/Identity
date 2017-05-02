using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class UserViewModel
    {
        [Required]
        public string Name { set; get; }

        [Required]
        public string Email { set; get; }

        [Required]
        public string Password { set; get; }
    }

    public class LoginModel
    {
        [Required]
        public string Name { set; get; }

        [Required]
        public string Password { set; get; }
    }
}
