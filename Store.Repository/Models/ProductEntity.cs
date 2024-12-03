using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Repository.Models;

[Table("products")]
public class ProductEntity
{
    [Key]
    [Column("code")]
    [StringLength(10)]
    public string Code { get; set; }

    [Column("name")]
    [MaxLength(20)]
    public string Name { get; set; }
}