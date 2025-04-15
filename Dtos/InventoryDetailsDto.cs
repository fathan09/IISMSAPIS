namespace IISMSBackend.Dtos;

public record class InventoryDetailsDto(
    int inventoryId,
    int productId, 
    string operation,
    int quantity,
    DateTime inventoryTimestamp
);