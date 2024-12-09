using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Repository.Models;

[Table("sales_det")]
public class SaleDetailEntity
{
    [Column("sale_id")]
    public int SaleId { get; set; }

    [Column("product_code")]
    public string ProductCode { get; set; }

    [Required]
    [Column("quantity")]
    public int Quantity { get; set; }

    public virtual SaleEntity Sale { get; set; }

    public virtual ProductEntity Product { get; set; }
}