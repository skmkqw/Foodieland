using System.Text;
using System.Text.Json.Serialization;
using foodieland.Data;
using foodieland.Models;
using foodieland.Repositories;
using foodieland.Repositories.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddIdentityCore<AppUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{ 
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{ 
    var secret = builder.Configuration["JwtConfig:Secret"]; 
    var issuer = builder.Configuration["JwtConfig:ValidIssuer"]; 
    var audience = builder.Configuration["JwtConfig:ValidAudiences"]; 
    if (secret is null || issuer is null || audience is null) 
    { 
        throw new ApplicationException("Jwt is not set in the configuration"); 
    } 
    options.SaveToken = true; 
    options.RequireHttpsMetadata = false; 
    options.TokenValidationParameters = new TokenValidationParameters() 
    { 
        ValidateIssuer = true, 
        ValidateAudience = true, 
        ValidAudience = audience, 
        ValidIssuer = issuer, 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)) 
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
