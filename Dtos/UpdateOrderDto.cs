namespace IISMSBackend.Dtos;

public record class UpdateOrderDto(
    string customerName,
    string address,
    DateTime deliveryDate,
    string status, 
    string[] productName,
    int [] quantity
);