using IISMSBackend.Entities;

namespace IISMSBackend.Dtos;

public record class OrderDetailsDto(
    int orderId,
    string customerName,
    string address,
    DateTime deliveryDate,
    string status,
    ICollection<OrderProduct>? OrderProducts
);
