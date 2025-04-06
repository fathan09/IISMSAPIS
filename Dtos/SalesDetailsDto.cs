using IISMSBackend.Entities;

namespace IISMSBackend.Dtos;

public record class SalesDetailsDto(
    int salesId,
    decimal totalCartPrice,
    int totalCartQuantity,
    DateTime salesTimestamp,
    ICollection<SalesProduct>? SalesProducts
);