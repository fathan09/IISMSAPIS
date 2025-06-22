using Microsoft.EntityFrameworkCore;
using IISMSBackend.Data;
using IISMSBackend.Mapping;
using IISMSBackend.Dtos;
using IISMSBackend.Entities;
using IISMSBackend.Support;


namespace IISMSBackend.Endpoints;


public static class UserEndpoint { 

    const string GetUserEndpointName = "GetUser";

    public static RouteGroupBuilder MapUserEndpoint(this WebApplication app) {
        var group = app.MapGroup("user").WithParameterValidation();

        group.MapGet("/all",  async(IISMSContext dbContext) => 
            await dbContext.Users
                .Select(user => user.ToUserDetailsDto())
                .AsNoTracking()
                .ToListAsync()
        ).RequireAuthorization();

        group.MapGet("/{id}", async(int id, IISMSContext dbContext) => {
            User? user = await dbContext.Users.FindAsync(id);
            return user is null ? Results.NotFound() : Results.Ok(user.ToUserDetailsDto());
        }).WithName(GetUserEndpointName).RequireAuthorization();

        group.MapPost("/login", async (UserLoginDto loginUser, IISMSContext dbContext) => {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.email == loginUser.email);
            
            if (user is null || !BCrypt.Net.BCrypt.Verify(loginUser.password, user.password)) {
                return Results.Unauthorized();
            }

        
            string token = JwtHelper.GenerateJwtToken(user);
            return Results.Ok(new { token, user});
        });

        

       group.MapPost("/registration", async (UserRegistrationDto newUser, IISMSContext dbContext) => {

            if (string.IsNullOrWhiteSpace(newUser.fullName) || 
                string.IsNullOrWhiteSpace(newUser.email) || 
                string.IsNullOrWhiteSpace(newUser.password)) 
            {
                return Results.BadRequest("Full name, email, and password are required.");
            }

            User user = newUser.ToEntity();
            user.password = BCrypt.Net.BCrypt.HashPassword(newUser.password);

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(GetUserEndpointName, new { id = user.userId }, user.ToUserDetailsDto());

        }).WithParameterValidation();


        group.MapPut("/editprofile/{id}", async (int id, EditProfileDto editedUser, IISMSContext dbContext) => {
            var existingUser = await dbContext.Users.FindAsync(id);

            if (existingUser is null) {
                return Results.NotFound();
            }

            dbContext.Entry(existingUser).CurrentValues.SetValues(editedUser.ToEntity(id, existingUser.password, existingUser.role));

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        }).RequireAuthorization();


        return group;
    }
}
