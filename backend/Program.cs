using backend.DB;
using backend.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // create connection string in appsettings.json

builder.Services.AddValidatorsFromAssemblyContaining<AssetValidator>(); // ask gemini what it does

// chhose identiy configuration method

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "api");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


/*
 * using backend.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. הגדרת ה-DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. הוספת Identity - הגדרת המשתמש והחיבור ל-DB
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// 3. יצירת ה-Endpoints של ה-Identity (Login, Register וכו') באופן אוטומטי
// זה יוסיף לך נתיבים כמו /register ו-/login
app.MapGroup("/api/auth").MapIdentityApi<IdentityUser>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "api"));
}

app.UseHttpsRedirection();

// 4. חשוב: וודא שהסדר נכון - קודם Authentication ואז Authorization
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
 */