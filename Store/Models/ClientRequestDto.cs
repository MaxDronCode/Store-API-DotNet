using System.ComponentModel.DataAnnotations;

namespace Store.API.Models;

public class ClientRequestDto
{
    [Required(ErrorMessage = "NIF is required")]
    [StringLength(9, MinimumLength = 9, ErrorMessage = "NIF must have 9 characters")]
    [RegularExpression(@"^[0-9]{8}[A-Z]{1}$", ErrorMessage = "NIF must have 8 digits and 1 uppercase letter")]
    public string Nif { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name must have a maximum of 100 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Address is required")]
    [StringLength(100, ErrorMessage = "Address must have a maximum of 100 characters")]
    public string Address { get; set; }
}