using AdFit.API;
using AdFit.Core.Mapping;
using AdFit.Core.Repositories;
using AdFit.Core.Service;
using AdFit.Data;
using AdFit.Data.Repositories;
using AdFit.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// רישום השירותים (Services)
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdvertisementService, AdvertisementService>();
builder.Services.AddScoped<INewspaperService, NewspaperService>();
builder.Services.AddScoped<IPageService, PageService>();

// רישום הריפוזיטוריז (Repositories)
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
builder.Services.AddScoped<INewspaperRepository, NewspaperRepository>();
builder.Services.AddScoped<IPageRepository, PageRepository>();

builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(@"Server=TAMAR;Database=AdFitDB;TrustServerCertificate=True;Trusted_Connection=True"));
builder.Services.AddAutoMapper(typeof(MappingProfile), typeof(PostModelsMappingProfile));


//builder.Services.AddAuthorization(options =>
//{
//    options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme; 
//    options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme; 
//}).Add
var app = builder.Build();

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
