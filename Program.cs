using EcoTrack.DAL.CRepository;
using EcoTrack.DAL.Database.AppDbContexts;
using EcoTrack.DAL.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.UTF8.GetBytes("Tchilla".PadRight(32, '0'));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Apenas para testes locais, remova em produção
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "CodePoint",
        ValidAudience = "CodePoint",
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 } // 🔥 Adicionado para evitar conflitos
    };
});

builder.Services.AddAuthorization(); // Adicionando autorização
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddTransient<IAuth, CAuthRepository>();
builder.Services.AddTransient<ISustainableAction, CSustainableActionRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EcoTrack API", Version = "v1" });

    // Configuração do JWT no Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Digite 'Bearer {seu_token}' para autenticar."
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
            new List<string>()
        }
    });
});


var app = builder.Build();

// Configure o pipeline HTTP

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication(); // Ativando autenticação
app.UseAuthorization();  // Ativando autorização

app.MapControllers();

app.Run();
