using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Account
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Пустое поле!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is empty")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Неверный формат!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пустое поле!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Неверное значение!")]
        public string ConfirmPassword { get; set; }
    }
}
