using LibraryWebApp.API.Mappings;
using LibraryWebApp.Domain.Interfaces;
using LibraryWebApp.Persistence;
using LibraryWebApp.API.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using LibraryWebApp.Application.Interfaces.Authors;
using LibraryWebApp.Application.UseCases.Authors;
using LibraryWebApp.Application.Interfaces.Books;
using LibraryWebApp.Application.UseCases.Books;
using LibraryWebApp.Application.Interfaces.Images;
using LibraryWebApp.Application.UseCases.Images;
using LibraryWebApp.Application.Interfaces.SignIn;
using LibraryWebApp.Application.UseCases.SignIn;
using LibraryWebApp.Application.Interfaces.SignUp;
using LibraryWebApp.Application.UseCases.SignUp;
using LibraryWebApp.Application.Interfaces.Tokens;
using LibraryWebApp.Application.UseCases.Tokens;
using LibraryWebApp.Application.Interfaces.Users;
using LibraryWebApp.Application.UseCases.Users;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
#region DataAccess
builder.Services.AddDbContext<LibraryWebAppDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryWebAppDbContext"));
    });
#endregion
builder.Services.AddAutoMapper(typeof(MappingProfile));

#region JWT
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
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["tasty-cookies"];

            return Task.CompletedTask;
        }
    };
});
#endregion

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

#region Interfaces
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IGetAllAuthorsUseCase, GetAllAuthorsUseCase>();
builder.Services.AddScoped<IGetAuthorByIdUseCase, GetAuthorByIdUseCase>();
builder.Services.AddScoped<IAddAuthorUseCase, AddAuthorUseCase>();
builder.Services.AddScoped<IUpdateAuthorUseCase, UpdateAuthorUseCase>();
builder.Services.AddScoped<IDeleteAuthorUseCase, DeleteAuthorUseCase>();

builder.Services.AddScoped<IAddBookUseCase, AddBookUseCase>();
builder.Services.AddScoped<IDeleteBookUseCase, DeleteBookUseCase>();
builder.Services.AddScoped<IGetAllBooksUseCase, GetAllBooksUseCase>();
builder.Services.AddScoped<IGetBookByIdUseCase, GetBookByIdUseCase>();
builder.Services.AddScoped<IGetBooksByUserUseCase, GetBooksByUserUseCase>();
builder.Services.AddScoped<IUpdateBookUseCase, UpdateBookUseCase>();
builder.Services.AddScoped<IIssueBookUseCase, IssueBookUseCase>();
builder.Services.AddScoped<IReturnBookUseCase, ReturnBookUseCase>();

builder.Services.AddScoped<ICreateImageUseCase, CreateImageUseCase>();
builder.Services.AddScoped<IGetImageUseCase, GetImageUseCase>();

builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();
builder.Services.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();

builder.Services.AddScoped<IRegisterUseCase, RegisterUseCase>();

builder.Services.AddScoped<IGenerateAccessTokenUseCase, GenerateAccessTokenUseCase>();
builder.Services.AddScoped<IGenerateRefreshTokenUseCase, GenerateRefreshTokenUseCase>();

builder.Services.AddScoped<IGetUserByUsernameUseCase, GetUserByUsernameUseCase>();
#endregion

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowCredentials();
    });
});

builder.Services.AddMemoryCache();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Add JWT Authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter into field the word 'Bearer' followed by a space and the JWT value",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
});

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors();
app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.Initialize(services);
}

app.Run();
