using System.ComponentModel.DataAnnotations;

namespace Store.Api.Models;

public class SaleItemDto
{
    [Required]
    public string ProductName { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive integer.")]
    public int Quantity { get; set; }
}