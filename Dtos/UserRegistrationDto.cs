using System.ComponentModel.DataAnnotations;

namespace IISMSBackend.Dtos;

public record class UserRegistrationDto(
    [Required] [StringLength(50)] string fullName, 
    [Required] string email, 
    [Required] [MinLength(8)] [MaxLength(16)] string password, 
    [Required] [MinLength(10)] [MaxLength(15)] string phoneNumber, 
    [Required] string role
);