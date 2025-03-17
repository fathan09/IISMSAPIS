namespace IISMSBackend.Dtos;

public record class UserDetailsDto(
    int userId, 
    string fullName, 
    string email, 
    string password, 
    string phoneNumber, 
    string role
);