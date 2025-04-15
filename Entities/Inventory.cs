using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IISMSBackend.Entities;

public class Inventory {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int inventoryId {get; set;}

    public required int productId { get; set; }

    public required string operation { get; set; }

    public required int quantity {get; set;}

    public required DateTime inventoryTimestamp {get; set;}

}