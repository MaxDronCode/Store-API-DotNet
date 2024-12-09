using System.ComponentModel.DataAnnotations;

namespace Store.Api.Models;

public class SaleRequestDto
{
    [Required]
    public string ClientNif { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "At least one sale item is required.")]
    public IEnumerable<SaleItemRequestDto> Items { get; set; }
}