// QRMenu.Core/Interfaces/INotificationService.cs
using System.Threading.Tasks;

namespace QRMenu.Core.Interfaces
{
    public interface INotificationService
    {
        Task SendLicenseExpirationNotificationAsync(int entityId);
        Task SendCompanyCreatedNotificationAsync(int companyId);
        Task SendCompanyDeletedNotificationAsync(int companyId);
        Task SendCompanyStatusChangedNotificationAsync(int companyId, bool isActive);
        Task SendServiceExpiredNotificationAsync(int companyId);
        Task SendHealthCheckFailedNotificationAsync(int companyId);
        Task SendServiceEndDateNotificationAsync(int companyId);
    }
}