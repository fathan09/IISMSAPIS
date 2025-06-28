using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IISMSBackend.Entities;

public class Order {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int orderId {get; set;}
    public required string customerName { get; set; }
    public  byte[]? customerSignature {get; set;}
    public required string address { get; set; }
    public required string status { get; set; }
    public required DateTime deliveryDate { get; set; }
    public virtual ICollection<OrderProduct>? OrderProducts { get; set; }


}
