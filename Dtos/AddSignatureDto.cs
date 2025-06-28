namespace IISMSBackend.Dtos;

public record class AddSignatureDto(
    string status, 
    byte[]? customerSignature
);