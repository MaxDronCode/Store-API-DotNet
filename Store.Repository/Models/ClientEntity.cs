using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Repository.Models;

[Table("clients")]
public class ClientEntity
{
    [Key]
    [Column("nif")]
    [StringLength(9)]
    public string Nif { get; set; }

    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; }

    [Column("address")]
    [MaxLength(100)]
    public string Address { get; set; }

    public virtual ICollection<SaleEntity> Sales { get; set; }

    public ClientEntity()
    {
        Sales = new List<SaleEntity>();
    }
}