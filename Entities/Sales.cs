using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IISMSBackend.Entities;

public class Sales {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int salesId {get; set;}
    public virtual ICollection<SalesProduct>? SalesProducts {get; set;}

    public required decimal totalCartPrice {get; set;}

    public required int totalCartQuantity {get; set;}

    public required DateTime salesTimestamp {get; set;}

}