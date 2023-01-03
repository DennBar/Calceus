using System.ComponentModel.DataAnnotations;

namespace Calceus.Shared
{
    public class UserRegister
    {
        [Required(ErrorMessage = "El campo correo electrónico es obligatorio"),
            EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido")
        ]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo contraseña es obligatorio"),
            StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe contener al menos 6 caracteres")]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un rol")]
        public int RoleId { get; set; } = 0;
    }
}
