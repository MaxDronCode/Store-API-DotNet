using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Repository;

[Table("clients")]
internal class ClientEntity
{
    [Key]
    [Column("nif")]
    public string Nif { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("address")]
    public string Address { get; set; }
}