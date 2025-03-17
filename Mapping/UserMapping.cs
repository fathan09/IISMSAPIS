using IISMSBackend.Entities;
using IISMSBackend.Dtos;

namespace IISMSBackend.Mapping;

public static class UserMapping {
    public static User ToEntity(this UserRegistrationDto user) {
        return new User() {
            fullName = user.fullName,
            email = user.email,
            password = user.password,
            phoneNumber = user.phoneNumber,
            role = user.role
        };
    }

    public static User ToEntity(this EditProfileDto user, int id, string password, string role) {
        return new User() {
            userId = id,
            fullName = user.fullName,
            email = user.email,
            password = password,
            phoneNumber = user.phoneNumber,
            role = role
        };
    }

    public static UserDetailsDto ToUserDetailsDto(this User user) {
        return new(
            user.userId,
            user.fullName,
            user.email,
            user.password,
            user.phoneNumber,
            user.role
        );
    }

    
}