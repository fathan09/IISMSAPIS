using IISMSBackend.Entities;

namespace IISMSBackend.Dtos;

public record class OrderDetailsDto(
    int orderId,
    string customerName,
    DateTime deliveryDate,
    string status,
    ICollection<OrderProduct>? OrderProducts
);