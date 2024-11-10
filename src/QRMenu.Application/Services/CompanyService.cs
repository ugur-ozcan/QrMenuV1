// QRMenu.Application/Services/CompanyService.cs
using QRMenu.Application.DTOs;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Entities;
using QRMenu.Core.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QRMenu.Application.Services
{
    /// <summary>
    /// Şirket işlemlerini yöneten servis sınıfı
    /// </summary>
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        /// <summary>
        /// CompanyService constructor
        /// </summary>
        public CompanyService(
            ICompanyRepository companyRepository,
            IBranchRepository branchRepository,
            IMapper mapper,
            INotificationService notificationService)
        {
            _companyRepository = companyRepository;
            _branchRepository = branchRepository;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CompanyDTO>> GetAllAsync()
        {
            var companies = await _companyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CompanyDTO>>(companies);
        }

        /// <inheritdoc/>
        public async Task<CompanyDTO> GetByIdAsync(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            return _mapper.Map<CompanyDTO>(company);
        }

        /// <inheritdoc/>
        public async Task<CompanyDTO> GetBySlugAsync(string slug)
        {
            var company = await _companyRepository.GetBySlugAsync(slug);
            return _mapper.Map<CompanyDTO>(company);
        }

        /// <inheritdoc/>
        public async Task<CompanyDTO> CreateCompanyAsync(CompanyDTO companyDto)
        {
            // Slug benzersizliğini kontrol et
            if (!await IsSlugUniqueAsync(companyDto.Slug))
            {
                throw new ArgumentException("Bu slug zaten kullanımda. Lütfen başka bir slug seçin.");
            }

            var company = _mapper.Map<Company>(companyDto);
            
            // Oluşturma bilgilerini ayarla
            company.CreatedDate = DateTime.UtcNow;
            company.CreatedBy = "System"; // TODO: Gerçek kullanıcı bilgisi ile değiştirilmeli
            company.IsActive = true;

            var createdCompany = await _companyRepository.AddAsync(company);
            await _notificationService.SendCompanyCreatedNotificationAsync(createdCompany.Id);
            
            return _mapper.Map<CompanyDTO>(createdCompany);
        }

        /// <inheritdoc/>
        public async Task<CompanyDTO> UpdateAsync(int id, CompanyDTO companyDto)
        {
            var existingCompany = await _companyRepository.GetByIdAsync(id);
            if (existingCompany == null)
            {
                throw new ArgumentException($"ID: {id} olan şirket bulunamadı.");
            }

            // Slug değiştiyse ve yeni slug benzersiz değilse hata fırlat
            if (existingCompany.Slug != companyDto.Slug && !await IsSlugUniqueAsync(companyDto.Slug, id))
            {
                throw new ArgumentException("Bu slug zaten kullanımda. Lütfen başka bir slug seçin.");
            }

            _mapper.Map(companyDto, existingCompany);
            existingCompany.UpdatedDate = DateTime.UtcNow;
            existingCompany.UpdatedBy = "System"; // TODO: Gerçek kullanıcı bilgisi ile değiştirilmeli

            var updatedCompany = await _companyRepository.UpdateAsync(existingCompany);
            return _mapper.Map<CompanyDTO>(updatedCompany);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null)
            {
                throw new ArgumentException($"ID: {id} olan şirket bulunamadı.");
            }

            company.IsDeleted = true;
            company.DeletedDate = DateTime.UtcNow;
            company.DeletedBy = "System"; // TODO: Gerçek kullanıcı bilgisi ile değiştirilmeli

            await _companyRepository.UpdateAsync(company);
            await _notificationService.SendCompanyDeletedNotificationAsync(id);
        }

        /// <inheritdoc/>
        public async Task UpdateStatusAsync(int id, bool isActive)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null)
            {
                throw new ArgumentException($"ID: {id} olan şirket bulunamadı.");
            }

            company.IsActive = isActive;
            company.UpdatedDate = DateTime.UtcNow;
            company.UpdatedBy = "System"; // TODO: Gerçek kullanıcı bilgisi ile değiştirilmeli

            await _companyRepository.UpdateAsync(company);
            await _notificationService.SendCompanyStatusChangedNotificationAsync(id, isActive);
        }

        /// <inheritdoc/>
        public async Task<bool> IsSlugUniqueAsync(string slug, int? excludeId = null)
        {
            return await _companyRepository.IsSlugUniqueAsync(slug, excludeId);
        }

        /// <inheritdoc/>
        public async Task ProcessExpiredServicesAsync()
        {
            var expiredCompanies = await _companyRepository.GetExpiredCompaniesAsync();
            foreach (var company in expiredCompanies)
            {
                company.IsActive = false;
                company.UpdatedDate = DateTime.UtcNow;
                company.UpdatedBy = "System";
                
                await _companyRepository.UpdateAsync(company);
                await _notificationService.SendServiceExpiredNotificationAsync(company.Id);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> TestConnectionAsync(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null)
            {
                throw new ArgumentException($"ID: {id} olan şirket bulunamadı.");
            }

            // TODO: Bağlantı testi implementasyonu
            return true;
        }

        /// <inheritdoc/>
        public async Task SyncCompanyDataAsync(int companyId)
        {
            var company = await _companyRepository.GetByIdAsync(companyId);
            if (company == null)
            {
                throw new ArgumentException($"ID: {companyId} olan şirket bulunamadı.");
            }

            // TODO: Veri senkronizasyonu implementasyonu
            company.LastDataRefresh = DateTime.UtcNow;
            await _companyRepository.UpdateAsync(company);
        }

        /// <inheritdoc/>
        public async Task UpdateHealthStatusAsync(int companyId, bool isHealthy)
        {
            var company = await _companyRepository.GetByIdAsync(companyId);
            if (company == null)
            {
                throw new ArgumentException($"ID: {companyId} olan şirket bulunamadı.");
            }

            company.IsConnected = isHealthy;
            company.LastHealthCheck = DateTime.UtcNow;
            company.UpdatedDate = DateTime.UtcNow;
            company.UpdatedBy = "System";

            await _companyRepository.UpdateAsync(company);
            
            if (!isHealthy)
            {
                await _notificationService.SendHealthCheckFailedNotificationAsync(companyId);
            }
        }

        /// <inheritdoc/>
        public async Task ServiceEndDateAsync(int companyId)
        {
            var company = await _companyRepository.GetByIdAsync(companyId);
            if (company == null)
            {
                throw new ArgumentException($"ID: {companyId} olan şirket bulunamadı.");
            }

            if (company.LicenseEndDate <= DateTime.UtcNow)
            {
                company.IsActive = false;
                company.UpdatedDate = DateTime.UtcNow;
                company.UpdatedBy = "System";

                await _companyRepository.UpdateAsync(company);
                await _notificationService.SendServiceEndDateNotificationAsync(companyId);
            }
        }

        /// <inheritdoc/>
        public async Task<BranchDTO> GetBranchByIdAsync(int id)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            return _mapper.Map<BranchDTO>(branch);
        }
    }
}