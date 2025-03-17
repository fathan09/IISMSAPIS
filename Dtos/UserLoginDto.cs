using System.ComponentModel.DataAnnotations;

namespace IISMSBackend.Dtos;

public record class UserLoginDto(
    [Required] string email, 
    [Required] string password
);
