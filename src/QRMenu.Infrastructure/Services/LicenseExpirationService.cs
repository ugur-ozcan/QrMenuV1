// QRMenu.Infrastructure/Services/LicenseExpirationService.cs
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using QRMenu.Core.Entities;
using QRMenu.Core.Interfaces;
using QRMenu.Infrastructure.Data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QRMenu.Infrastructure.Services
{
    public class LicenseExpirationService : ILicenseExpirationService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<LicenseExpirationService> _logger;
        private Timer _timer;

        public LicenseExpirationService(
            IServiceProvider services,
            ILogger<LicenseExpirationService> logger)
        {
            _services = services;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("License Expiration Check Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromHours(24)); // Her 24 saatte bir çalışır

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            using (var scope = _services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                try
                {
                    // Süresi biten firmaları bul
                    var expiredCompanies = await dbContext.Companies
                        .Where(c => c.LicenseEndDate < DateTime.UtcNow && c.IsActive)
                        .ToListAsync();

                    // Süresi biten şubeleri bul
                    var expiredBranches = await dbContext.Branches
                        .Where(b => b.LicenseEndDate < DateTime.UtcNow && b.IsActive)
                        .ToListAsync();

                    // Firmaları pasife çek
                    foreach (var company in expiredCompanies)
                    {
                        company.IsActive = false;
                        _logger.LogInformation($"Firma lisansı sona erdi ve pasife çekildi: {company.Name} (ID: {company.Id})");
                    }

                    // Şubeleri pasife çek
                    foreach (var branch in expiredBranches)
                    {
                        branch.IsActive = false;
                        _logger.LogInformation($"Şube lisansı sona erdi ve pasife çekildi: {branch.Name} (ID: {branch.Id})");
                    }

                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Lisans kontrol işlemi sırasında bir hata oluştu");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("License Expiration Check Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);
            _timer?.Dispose();

            return Task.CompletedTask;
        }
    }
}