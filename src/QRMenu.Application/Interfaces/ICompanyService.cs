// QRMenu.Application/Interfaces/ICompanyService.cs
using QRMenu.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QRMenu.Application.Interfaces
{
    /// <summary>
    /// Şirket işlemlerini yöneten servis arayüzü
    /// </summary>
    public interface ICompanyService
    {
        /// <summary>
        /// Tüm şirketleri getirir
        /// </summary>
        Task<IEnumerable<CompanyDTO>> GetAllAsync();

        /// <summary>
        /// Belirtilen ID'ye sahip şirketi getirir
        /// </summary>
        /// <param name="id">Şirket ID'si</param>
        Task<CompanyDTO> GetByIdAsync(int id);

        /// <summary>
        /// Belirtilen slug'a sahip şirketi getirir
        /// </summary>
        /// <param name="slug">Şirket slug'ı</param>
        Task<CompanyDTO> GetBySlugAsync(string slug);

        /// <summary>
        /// Yeni şirket oluşturur
        /// </summary>
        /// <param name="companyDto">Şirket bilgileri</param>
        Task<CompanyDTO> CreateCompanyAsync(CompanyDTO companyDto);

        /// <summary>
        /// Şirket bilgilerini günceller
        /// </summary>
        /// <param name="id">Şirket ID'si</param>
        /// <param name="companyDto">Güncellenecek şirket bilgileri</param>
        Task<CompanyDTO> UpdateAsync(int id, CompanyDTO companyDto);

        /// <summary>
        /// Şirketi siler (soft delete)
        /// </summary>
        /// <param name="id">Şirket ID'si</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Şirketin aktiflik durumunu günceller
        /// </summary>
        /// <param name="id">Şirket ID'si</param>
        /// <param name="isActive">Aktiflik durumu</param>
        Task UpdateStatusAsync(int id, bool isActive);

        /// <summary>
        /// Belirtilen slug'ın benzersiz olup olmadığını kontrol eder
        /// </summary>
        /// <param name="slug">Kontrol edilecek slug</param>
        /// <param name="excludeId">Hariç tutulacak şirket ID'si (güncelleme durumunda)</param>
        Task<bool> IsSlugUniqueAsync(string slug, int? excludeId = null);

        /// <summary>
        /// Süresi dolmuş servisleri işler
        /// </summary>
        Task ProcessExpiredServicesAsync();

        /// <summary>
        /// Şirket bağlantısını test eder
        /// </summary>
        /// <param name="id">Şirket ID'si</param>
        Task<bool> TestConnectionAsync(int id);

        /// <summary>
        /// Şirket verilerini senkronize eder
        /// </summary>
        /// <param name="companyId">Şirket ID'si</param>
        Task SyncCompanyDataAsync(int companyId);

        /// <summary>
        /// Şirketin sağlık durumunu günceller
        /// </summary>
        /// <param name="companyId">Şirket ID'si</param>
        /// <param name="isHealthy">Sağlık durumu</param>
        Task UpdateHealthStatusAsync(int companyId, bool isHealthy);

        /// <summary>
        /// Şirketin servis bitiş tarihini işler
        /// </summary>
        /// <param name="companyId">Şirket ID'si</param>
        Task ServiceEndDateAsync(int companyId);

        /// <summary>
        /// Belirtilen ID'ye sahip şubeyi getirir
        /// </summary>
        /// <param name="id">Şube ID'si</param>
        Task<BranchDTO> GetBranchByIdAsync(int id);
    }
}