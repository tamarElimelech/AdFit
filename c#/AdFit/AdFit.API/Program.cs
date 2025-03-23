using AdFit.API;
using AdFit.Core.Mapping;
using AdFit.Core.Repositories;
using AdFit.Core.Service;
using AdFit.Data;
using AdFit.Data.Repositories;
using AdFit.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Name = "Authorization",
    Description = "Bearer Authentication with JWT Token",
    Type = SecuritySchemeType.Http
});

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});



builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdvertisementService, AdvertisementService>();
builder.Services.AddScoped<IAdminAdvertisementService, AdminAdvertisementService>();
builder.Services.AddScoped<INewspaperService, NewspaperService>();
builder.Services.AddScoped<IPageService, PageService>();
builder.Services.AddScoped<IArrangeService, ArrangeService>();
builder.Services.AddScoped<IPriceService, PriceService>();



builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
builder.Services.AddScoped<IAdminAdvertisementRepository, AdminAdvertisementRepository>();
builder.Services.AddScoped<INewspaperRepository, NewspaperRepository>();
builder.Services.AddScoped<IPageRepository, PageRepository>();
builder.Services.AddScoped<IPriceRepository, PriceRepository>();


builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(@"Server=TAMAR;Database=AdFitDB;TrustServerCertificate=True;Trusted_Connection=True"));
builder.Services.AddAutoMapper(typeof(MappingProfile), typeof(PostModelsMappingProfile));

//CROS
var policy = "policy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policy, policy =>
    {
        policy.WithOrigins("http://localhost:4200") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

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
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

builder.WebHost.UseUrls("https://*:7194");


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy);

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
