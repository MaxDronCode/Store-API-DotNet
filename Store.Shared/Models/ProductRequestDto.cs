using System.ComponentModel.DataAnnotations;

namespace Store.Api.Models;

public class ProductRequestDto
{
    [Required]
    [MaxLength(20)]
    public string Name { get; set; }
}