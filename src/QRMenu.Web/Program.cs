// QRMenu.Web/Program.cs
using QRMenu.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using QRMenu.Application.Interfaces;
using QRMenu.Application.Services;
using QRMenu.Core.Interfaces;
using QRMenu.Infrastructure.Data;
using QRMenu.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DbContext'i kaydet
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper'ý ekle
builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(CompanyService).Assembly);

// Repository'leri kaydet
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();

// Lisans kontrol servisini ekle
builder.Services.AddHostedService<LicenseExpirationService>();

// Infrastructure Services
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Application Services
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IBranchService, BranchService>();

// Swagger/OpenAPI yapýlandýrmasý
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "QRMenu API", Version = "v1" });

    // XML yorum dosyasýný okuyarak API dokümantasyonunu zenginleþtirme
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QRMenu API v1"));
    app.UseExceptionHandler("/Home/Error");
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// API Controllers için endpoint'leri ekle
app.MapControllers();

// MVC Controllers için default route'u ekle
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();