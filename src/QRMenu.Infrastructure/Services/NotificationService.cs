// QRMenu.Infrastructure/Services/NotificationService.cs
using Microsoft.Extensions.Logging;
using QRMenu.Core.Interfaces;
using System.Threading.Tasks;

namespace QRMenu.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;
        }

        public async Task SendLicenseExpirationNotificationAsync(int entityId)
        {
            _logger.LogInformation($"Lisans sona erme bildirimi gönderildi. Entity ID: {entityId}");
            await Task.CompletedTask;
        }

        public async Task SendCompanyCreatedNotificationAsync(int companyId)
        {
            _logger.LogInformation($"Şirket oluşturuldu bildirimi gönderildi. Company ID: {companyId}");
            await Task.CompletedTask;
        }

        public async Task SendCompanyDeletedNotificationAsync(int companyId)
        {
            _logger.LogInformation($"Şirket silindi bildirimi gönderildi. Company ID: {companyId}");
            await Task.CompletedTask;
        }

        public async Task SendCompanyStatusChangedNotificationAsync(int companyId, bool isActive)
        {
            _logger.LogInformation($"Şirket durumu değişti bildirimi gönderildi. Company ID: {companyId}, Is Active: {isActive}");
            await Task.CompletedTask;
        }

        public async Task SendServiceExpiredNotificationAsync(int companyId)
        {
            _logger.LogInformation($"Servis süresi doldu bildirimi gönderildi. Company ID: {companyId}");
            await Task.CompletedTask;
        }

        public async Task SendHealthCheckFailedNotificationAsync(int companyId)
        {
            _logger.LogInformation($"Sağlık kontrolü başarısız oldu bildirimi gönderildi. Company ID: {companyId}");
            await Task.CompletedTask;
        }

        public async Task SendServiceEndDateNotificationAsync(int companyId)
        {
            _logger.LogInformation($"Servis bitiş tarihi yaklaşıyor bildirimi gönderildi. Company ID: {companyId}");
            await Task.CompletedTask;
        }
    }
}