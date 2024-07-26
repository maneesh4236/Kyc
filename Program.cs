using KYC_apllication_2.Data;
using KYC_apllication_2.Repositories;
using KYC_apllication_2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<KYCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CodePulseConnectionstring")));

// Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IKycDetailsService, KycDetailsService>();
// Ensure repositories are also registered
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IKycDetailsRepository, KycDetailsRepository>();

// Configure JWT authentication
var key = Encoding.UTF8.GetBytes("fe836114546aabf37b31a22aa2f3cab7705ead160926c0fa73c6310e2a12d110");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Uncomment if HTTPS redirection is needed
// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
