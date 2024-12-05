using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Repository.Models;

[Table("sales_cab")]
public class SaleEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("client_nif")]
    [ForeignKey(nameof(ClientEntity.Nif))]
    public string ClientNif { get; set; }

    [Column("sell_date")]
    public DateTime SellDate { get; set; }
}