namespace Store.Api.Models;

public class SaleResponseDto
{
    public int Id { get; set; }
    public string ClientNif { get; set; }
    public List<SaleItemResponseDto> Items { get; set; }
}