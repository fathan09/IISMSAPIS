namespace IISMSBackend.Dtos;

public record class UpdateProductDto(
    byte[] productImage,
    string productName,
    string category,
    decimal size,
    string unit,
    decimal price,
    long quantity,
    DateTime expirationDate
);