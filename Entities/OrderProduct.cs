using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IISMSBackend.Entities;

public record OrderProduct
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int orderProductId { get; set; }

    [Required]
    public int orderId { get; set; }

    [Required]
    public int productId { get; set; }

    [Required]
    public int  orderQuantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

}