using System.Security.Claims;
using System.Text;
using ElsherbinyTask05.API.Data;
using ElsherbinyTask05.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();


builder.Services.AddDbContext<AppDbContext>(options =>

    options.UseNpgsql(builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection"),o=>o.UseNodaTime())
    

);

builder.Services.AddAuthorization(options =>
{
    // options.AddPolicy("isAdmin", policy =>
    // {
    //     policy.RequireClaim(ClaimTypes.Role, "Admin");
    // });



    // options.AddPolicy("isEditor", policy =>
    // {
    //     policy.RequireRole("Editor");
    // });






    options.FallbackPolicy = new AuthorizationPolicyBuilder()
                                                         .RequireAuthenticatedUser()
                                                         .Build();
});


builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration.GetValue<string>("JWT:Issuer"),

        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetValue<string>("JWT:Audience"),


        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWT:SecretKey")!))
    };
});


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.UseHttpsRedirection();

app.Run();
