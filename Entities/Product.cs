using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IISMSBackend.Entities;

public class Product {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int productId {get; set;}
    public  byte[]? productImage {get; set;}
    public required string productName {get; set;}
    public required byte[] productBarcode {get; set;}
    public required string category {get; set;}
    public required decimal size {get; set;}
    public required string unit {get; set;}
    public required decimal price {get; set;}
    public required long quantity {get; set;}
    public DateTime? expirationDate {get; set;}
    public required DateTime firstCreationTimestamp {get; set;}
}