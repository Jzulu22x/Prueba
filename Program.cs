using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using nombre.Data;

Env.Load();
var host = Environment.GetEnvironmentVariable("DB_HOST");
var databaseName = Environment.GetEnvironmentVariable("DB_DATABASE");
var port = Environment.GetEnvironmentVariable("DB_PORT");
var username = Environment.GetEnvironmentVariable("DB_USERNAME");
var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
var connectionString = $"server={host};port={port};database={databaseName};uid={username};password={password}";

// Console.WriteLine(connectionString);


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.Parse("8.0.20-mysql")));


builder.Services.AddControllers();

var secretKey = "EstaEsUnaClaveSecretaParaFirmarElTokenJWT"; 

// Configurar la autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;  // Cambiar a true en producción para usar HTTPS
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,     // Se puede configurar para validar el emisor
        ValidateAudience = false,   // Se puede configurar para validar la audiencia
        ValidateLifetime = true,    // Verificar si el token ha expirado
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), // Usamos la clave secreta
        ClockSkew = TimeSpan.Zero   // El margen de error por el que se toleran diferencias de tiempo
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilitar autenticación y autorización
app.UseAuthentication();  // Esto habilita la autenticación de JWT
app.UseAuthorization();   // Esto habilita la autorización en los endpoints


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
