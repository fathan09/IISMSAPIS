using System.Drawing;

namespace IISMSBackend.Dtos;

public record class CreateOrderDto(
    string customerName,
    string address,
    DateTime deliveryDate,
    string status, 
    string[] productName,
    int [] quantity
);