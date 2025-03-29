using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IISMSBackend.Entities;

public record SalesProduct
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int salesProductId { get; set; }

    [Required]
    public int salesId { get; set; }

    [Required]
    public int productId { get; set; }

    [Required]
    public int salesQuantity { get; set; }

    [Required]
    public decimal unitPrice { get; set; }

    [Required]
    public decimal totalUnitPrice { get; set; }

    public virtual Sales Sale { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;


}