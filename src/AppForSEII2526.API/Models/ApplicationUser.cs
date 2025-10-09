using Microsoft.AspNetCore.Identity;

namespace AppForSEII2526.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser {
    [StringLength(50, ErrorMessage = "El nombre no puede ser mas largo de 50 caracteres", MinimumLength = 3)]
    public string NombreCliente { get; set; }

    [StringLength(50, ErrorMessage = "El apellido no pueder ser mas largo de 50 caracteres", MinimumLength = 3)]
    public string ApellidoCliente { get; set; }

    [StringLength(50, ErrorMessage = "El correo no es valido", MinimumLength = 3)]
    public string? CorreoElectronico { get; set; }

    [StringLength(9, ErrorMessage = "El teléfono debe ser de 9 dígitos")]
    public string? Telefono { get; set; }
}