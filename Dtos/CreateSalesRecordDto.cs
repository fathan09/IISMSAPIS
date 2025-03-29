namespace IISMSBackend.Dtos;

public record CreateSalesRecordDto(
    string[] productName,
    decimal[] unitPrice,
    decimal [] totalUnitPrice,
    int [] quantity,
    int totalCartQuantity,
    decimal totalCartPrice
);