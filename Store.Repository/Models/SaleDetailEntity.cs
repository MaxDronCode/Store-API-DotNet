using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Repository.Models;

public class SaleDetailEntity
{
    [Key]
    [Column("sale_id")]
    [ForeignKey(nameof(SaleEntity.Id))]
    public int SaleId { get; set; }

    [Key]
    [Column("product_code")]
    [ForeignKey(nameof(ProductEntity.Code))]
    public string ProductCode { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }
}