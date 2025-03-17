using System.ComponentModel.DataAnnotations;

namespace IISMSBackend.Dtos;

public record class EditProfileDto(
    [StringLength(50)] string fullName, 
    string email, 
    [MinLength(10)] [MaxLength(15)] string phoneNumber
);