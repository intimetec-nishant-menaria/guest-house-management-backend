using guest_house_management_backend.Data;
using guest_house_management_backend.Extensions;
using guest_house_management_backend.Repositories.RoleRepo;
using guest_house_management_backend.Repositories.UserRepo;
using guest_house_management_backend.Repositories.UserTokenRepo;
using guest_house_management_backend.Services.Auth;
using guest_house_management_backend.Services.Email;
using guest_house_management_backend.Services.UserManagement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins(builder.Configuration["Frontend:URL"]!)
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});
builder.Services.AddJwtService(builder.Configuration);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleRepository , RoleRepository>();
builder.Services.AddScoped<IUserManagementService , UserManagementService>();
builder.Services.AddScoped<IEmailSender , EmailSender>();
builder.Services.AddScoped<IUserTokenRepository , UserTokenRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
