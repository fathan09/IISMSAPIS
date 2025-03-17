namespace IISMSBackend.Dtos;

public record class ProductDetailsDto(
    int productId,
    byte[]? productImage,
    string productName,
    byte[] productBarcode,
    string category,
    decimal size,
    string unit,
    decimal price,
    long quantity,
    DateTime? expirationDate,
    DateTime firstCreationTimestamp
);