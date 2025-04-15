using System.ComponentModel.DataAnnotations;

namespace IISMSBackend.Dtos;

public record class CreateInventoryDto(
    [Required] int productId,
    [Required] string operation,
    [Required] int quantity
);
