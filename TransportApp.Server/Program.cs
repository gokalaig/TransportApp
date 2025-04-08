using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TransportApp.Server;
using TransportApp.Server.Data;
using TransportApp.Server.JWT_Helper;
using TransportApp.Server.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// 🔹 Register IConfiguration (Already Correct)
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<IJwtService, JwtService>();
// ✅ Register JwtHelper WITHOUT circular dependency
builder.Services.AddScoped<IJwtHelper, JwtHelper>();
builder.Services.AddScoped<ITokenValidator, TokenValidator>();
// ✅ Register TruckRepository AFTER JwtHelper
builder.Services.AddScoped<ITruckRepository, TruckRepository>();
builder.Services.AddScoped<ICommissionBillRepository, CommissionBillRepository>();
//builder.Services.AddScoped<ICommissionBillService, CommissionBillService>();


// Add services to the container
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




// Configure JWT Authentication
builder.Services.AddAuthentication(options => { options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
                                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; }) .AddJwtBearer(
                                    options => { var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]); 
                                    options.TokenValidationParameters = new TokenValidationParameters 
                                    { ValidateIssuer = true, ValidateAudience = true, ValidateLifetime = true, ValidateIssuerSigningKey = true, 
                                      ValidIssuer = builder.Configuration["JwtSettings:Issuer"], 
                                      ValidAudience = builder.Configuration["JwtSettings:Audience"], 
                                      IssuerSigningKey = new SymmetricSecurityKey(key) };});
builder.Services.AddAuthorization();

// Add JWT Authentication

// ✅ Add Swagger and Configure JWT Authorization
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Transport API", Version = "v1" });

    // 🔹 Add JWT Authorization to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter the Bearer Token: **Bearer YOUR_JWT_TOKEN_HERE**",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
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
            new string[] {}
        }
    });
});


var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
