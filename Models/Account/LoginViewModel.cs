using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Пустое поле!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пустое поле!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
