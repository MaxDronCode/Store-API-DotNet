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

    [Required]
    [Column("client_nif")]
    public string ClientNif { get; set; }

    [ForeignKey(nameof(ClientNif))]
    public virtual ClientEntity Client { get; set; }

    [Required]
    [Column("sell_date")]
    public DateTime SellDate { get; set; }

    public virtual ICollection<SaleDetailEntity> SaleDetails { get; set; }

    public SaleEntity()
    {
        SaleDetails = new List<SaleDetailEntity>();
        SellDate = DateTime.Now;
    }
}