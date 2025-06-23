using System.ComponentModel.DataAnnotations;

namespace IISMSBackend.Dtos;

public record ChangePasswordDto(
    [Required] string email, 
    [Required] string newPassword
);