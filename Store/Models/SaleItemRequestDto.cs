using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Store.Api.Models;

public class SaleItemRequestDto
{
    [Required]
    public string ProductName { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive integer.")]
    [JsonConverter(typeof(IntConverter))]
    public int Quantity { get; set; }
}