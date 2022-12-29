using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calceus.Shared
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Ingrese un correo electrónico")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingrese una contraseña")]
        public string Password { get; set; } = string.Empty;
    }
}
