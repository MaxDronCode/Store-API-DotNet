namespace Store.Service.Models;

public class Sale
{
    public int Id { get; set; }
    public string ClientNif { get; set; }
    public IEnumerable<SaleDetail> Items { get; set; }
}