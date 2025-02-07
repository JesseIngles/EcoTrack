using EcoTrack.DAL.CRepository;
using EcoTrack.DAL.Database.AppDbContexts;
using EcoTrack.DAL.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "CodePoint",
            ValidAudience = "CodePoint",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Tchilla"))
        };
    });

builder.Services.AddAuthorization(); // Adicionando autorização
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddTransient<IAuth, CAuthRepository>();
builder.Services.AddTransient<ISustainableAction, CSustainableActionRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Ativando autenticação
app.UseAuthorization();  // Ativando autorização

app.MapControllers();

app.Run();
