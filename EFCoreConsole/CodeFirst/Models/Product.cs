using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models;
public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null;

    [Column(TypeName ="decimal(6,3)")]
    public decimal Price { get; set; }
}
