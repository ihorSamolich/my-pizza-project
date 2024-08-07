using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebPizza.Infrastructure.Data;
using WebPizza.Core.Entities.Identity;
using WebPizza.Mapper;
using WebPizza.Core.Interfaces;
using WebPizza.Application.Services;
using WebPizza.Core.Interfaces.ControllerInterfaces;
using WebPizza.Application.Services.ControllerServices;
using WebPizza.Core.DTO.Category;
using WebPizza.Core.DTO.Pizza;
using WebPizza.Application.Services.PaginationServices;
using Microsoft.OpenApi.Models;
using WebPizza.Core.DTO.Order;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PizzaDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Npgsql")));

builder.Services
    .AddIdentity<UserEntity, RoleEntity>(options =>
    {
        options.Stores.MaxLengthForKeys = 128;

        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<PizzaDbContext>()
    .AddDefaultTokenProviders();

var singinKey = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(
        builder.Configuration["Authentication:Jwt:SecretKey"]
            ?? throw new NullReferenceException("Authentication:Jwt:SecretKey")
    )
);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = singinKey,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
    });


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Description = "Jwt Auth header using the Bearer scheme",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer"
        }
    );
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
   });
});

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddAutoMapper(typeof(AppMapProfile));
builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddTransient<IIdentityService, IdentityService>();
builder.Services.AddScoped<IScopedIdentityService, ScopedIdentityService>();
builder.Services.AddTransient<IImageValidator, ImageValidator>();
builder.Services.AddTransient<IExistingEntityCheckerService, ExistingEntityCheckerService>();

builder.Services.AddTransient<IAccountsControllerService, AccountsControllerService>();

builder.Services.AddTransient<ICategoryControllerService, CategoryControllerService>();
builder.Services.AddTransient<IPaginationService<CategoryVm, CategoryFilterVm>, CategoryPaginationService>();

builder.Services.AddTransient<IIngredientControllerService, IngredientControllerService>();

builder.Services.AddTransient<IOrderControllerService, OrderControllerService>();
builder.Services.AddTransient<IPaginationService<OrderVm, OrderFilterVm>, OrderPaginationService>();


builder.Services.AddTransient<IPizzaControllerService, PizzaControllerService>();
builder.Services.AddTransient<IPaginationService<PizzaVm, PizzaFilterVm>, PizzaPaginationService>();

var app = builder.Build();

string imagesDirPath = Path.Combine(Directory.GetCurrentDirectory(), builder.Configuration["ImagesDir"]);

if (!Directory.Exists(imagesDirPath))
{
    Directory.CreateDirectory(imagesDirPath);
}


// ��� ����������� ����
app.UseCors(
    configuration => configuration
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesDirPath),
    RequestPath = "/images"
});

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseAuthorization();

app.MapControllers();

app.SeedData();

app.Run();
