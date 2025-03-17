using IISMSBackend.Endpoints;
using IISMSBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var key = "b!$p@JvXy$GzW8qLzC!kM4dV3mFzN0vY";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "fathan",
            ValidAudience = "precisioncultivations",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddDbContext<IISMSContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase"))
);

var app = builder.Build();

app.UseCors("AllowAll");


app.UseAuthorization();
app.UseAuthentication(); 

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<IISMSContext>();
    await dbContext.Database.MigrateAsync();
}

app.MapUserEndpoint();
app.MapProductEndpoint();
app.MapControllers();

app.Run();
