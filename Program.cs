using System.Text;
using AppServiciosIdentidad.Gateways.Interfaces;
using AppServiciosIdentidad.Gateways.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TeamHubSessionsServices.DTOs;
using TeamHubSessionsServices.Entities;
using TeamHubSessionsServices.Gateways.Interfaces;
using TeamHubSessionsServices.Gateways.Providers;
using TeamHubSessionsServices.UseCases.Interfaces;
using TeamHubSessionsServices.UseCases.Provider;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidateUser, ValidateUser>();
builder.Services.AddScoped<ISessionManager, SessionManager>();

builder.Services.AddScoped<ITokenGenerator, TokenGeneratorJWT>();
builder.Services.AddScoped<IServicesUsers, ServicesUsersMySQL>();
builder.Services.AddScoped<IServicesSessions, ServiceSessionsMySQL>();

builder.Services.AddAuthentication(options => {    
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;    
}).AddJwtBearer(options => {
    var config = builder.Configuration;
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    var key = config["JWTSettings:Key"];
    options.TokenValidationParameters = new TokenValidationParameters() {
        ValidIssuer = config["JWTSettings:Issuer"],
        ValidAudience = config["JWTSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),        
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});


builder.Services.AddDbContext<Context>(options => {
    var connectionString = builder.Configuration
                           .GetConnectionString("MySQLCursos");
    options.UseMySQL(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapPost("/TeamHub/Sessions/validateUser", (IValidateUser validateUser, 
                                ISessionManager sessionManager,
                                HttpContext request, 
                                IConfiguration config,
                                SessionLoginRequest sessionLoginRequest) =>
{            
        var student = validateUser.Validate(sessionLoginRequest);
        var response = new UserValidationResponse() { };
        if (student == null)
            response.IsValid = false;
        else {            
            var ip = request.Request.HttpContext.Connection.RemoteIpAddress;
            studentsession session = sessionManager.CreateSesion(student, ip.ToString(), Int32.Parse(config["JWTSettings:Duration"]));
            response.Token = session.Token;
            response.IsValid = true;
            response.User = new User() { 
                Email = student.Email, 
                FullName = $"{student.LastName} {student.SurName} {student.Name}"
            };
        }
        return response;    
})
.WithName("IniciarSesion")
.WithOpenApi();

app.Run();

