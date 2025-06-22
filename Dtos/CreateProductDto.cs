using System.ComponentModel.DataAnnotations;

namespace IISMSBackend.Dtos;

public record class CreateProductDto(
    byte[] productImage,
    [Required] string productName,
    [Required] string category,
    [Required] decimal size,
    [Required] string unit,
    [Required] decimal price,
    [Required] long quantity,
    DateTime? manufactureDate,
    DateTime? expirationDate,
    string? productDescription
);
