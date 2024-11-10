// QRMenu.Application/Services/BranchService.cs
using AutoMapper;
using Microsoft.Extensions.Logging;
using QRMenu.Application.DTOs;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Entities;
using QRMenu.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QRMenu.Core.Extensions;
 

namespace QRMenu.Application.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BranchService> _logger;
        private readonly IEncryptionService _encryptionService;
        private readonly INotificationService _notificationService;

        public BranchService(
            IBranchRepository branchRepository,
            IMapper mapper,
            ILogger<BranchService> logger,
            IEncryptionService encryptionService,
            INotificationService notificationService)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
            _logger = logger;
            _encryptionService = encryptionService;
            _notificationService = notificationService;
        }

        public async Task<IEnumerable<BranchDTO>> GetAllAsync()
        {
            var branches = await _branchRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<BranchDTO>>(branches);
        }

        public async Task<BranchDTO> GetByIdAsync(int id)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            return _mapper.Map<BranchDTO>(branch);
        }

        public async Task<BranchDTO> GetBySlugAsync(string slug)
        {
            var branch = await _branchRepository.GetBySlugAsync(slug);
            return _mapper.Map<BranchDTO>(branch);
        }

        public async Task<BranchDTO> CreateBranchAsync(BranchDTO branchDto)
        {
            try
            {
                if (!await IsSlugUniqueAsync(branchDto.Slug))
                    throw new InvalidOperationException("Bu slug zaten kullanımda");

                var branch = _mapper.Map<Branch>(branchDto);

                branch.DatabasePassword = _encryptionService.Encrypt(branchDto.DatabasePassword);
                branch.LicenseEndDate = DateTime.UtcNow.AddYears(1);

                branch = await _branchRepository.AddAsync(branch);

                var resultDto = _mapper.Map<BranchDTO>(branch);
                resultDto.DatabasePassword = branchDto.DatabasePassword;

                return resultDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şube oluşturma hatası");
                throw;
            }
        }

        public async Task<BranchDTO> UpdateAsync(int id, BranchDTO branchDto)
        {
            try
            {
                if (!await IsSlugUniqueAsync(branchDto.Slug, id))
                    throw new InvalidOperationException("Bu slug zaten kullanımda");

                var branch = await _branchRepository.GetByIdAsync(id);
                if (branch == null)
                    throw new KeyNotFoundException($"ID'si {id} olan şube bulunamadı");

                if (!string.IsNullOrEmpty(branchDto.DatabasePassword))
                {
                    branch.DatabasePassword = _encryptionService.Encrypt(branchDto.DatabasePassword);
                }

                _mapper.Map(branchDto, branch);
                await _branchRepository.UpdateAsync(branch);

                var resultDto = _mapper.Map<BranchDTO>(branch);
                resultDto.DatabasePassword = branchDto.DatabasePassword;

                return resultDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şube güncelleme hatası: {Id}", id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _branchRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şube silme hatası: {Id}", id);
                throw;
            }
        }

        public async Task UpdateStatusAsync(int id, bool isActive)
        {
            try
            {
                await _branchRepository.UpdateStatusAsync(id, isActive);
                _logger.LogInformation("Şube durumu güncellendi. Id: {Id}, IsActive: {IsActive}", id, isActive);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şube durumu güncelleme hatası: {Id}", id);
                throw;
            }
        }

        public async Task<bool> IsSlugUniqueAsync(string slug, int? excludeId = null)
        {
            return await _branchRepository.IsSlugUniqueAsync(slug, excludeId);
        }

        public async Task ProcessExpiredServicesAsync()
        {
            try
            {
                var expiredBranches = await _branchRepository.GetExpiredServicesAsync();
                foreach (var branch in expiredBranches)
                {
                    await UpdateStatusAsync(branch.Id, false);
                    _logger.LogInformation("Şube hizmeti sona erdi ve devre dışı bırakıldı. Id: {Id}", branch.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Süresi dolmuş şube hizmetlerini işleme hatası");
                throw;
            }
        }

        public async Task<bool> TestConnectionAsync(int id)
        {
            try
            {
                var branch = await _branchRepository.GetByIdAsync(id);
                if (branch == null) return false;

                // Burada gerçek bağlantı testi yapılacak
                // Örnek olarak her zaman true dönüyoruz
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şube bağlantı testi hatası: {Id}", id);
                return false;
            }
        }

        public async Task SyncBranchDataAsync(int branchId)
        {
            try
            {
                var branch = await _branchRepository.GetByIdAsync(branchId);
                if (branch == null)
                    throw new KeyNotFoundException($"Şube bulunamadı: {branchId}");

                // Burada şube verilerini senkronize etme işlemleri yapılacak
                branch.LastDataRefresh = DateTime.UtcNow;
                await _branchRepository.UpdateAsync(branch);

                _logger.LogInformation($"Veri senkronizasyonu tamamlandı: Şube {branchId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Veri senkronizasyonu sırasında hata oluştu: Şube {branchId}");
                throw;
            }
        }

        public async Task UpdateHealthStatusAsync(int branchId, bool isHealthy)
        {
            try
            {
                var branch = await _branchRepository.GetByIdAsync(branchId);
                if (branch != null)
                {
                    branch.IsConnected = isHealthy;
                    branch.LastSyncTime = DateTime.UtcNow;
                    await _branchRepository.UpdateAsync(branch);
                    _logger.LogInformation($"Sağlık durumu güncellendi: Şube {branchId}, Durum: {isHealthy}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Sağlık durumu güncelleme hatası: Şube {branchId}");
                throw;
            }
        }

        // QRMenu.Application/Services/BranchService.cs
        public async Task LicenseEndDateAsync(int branchId)
        {
            try
            {
                var branch = await _branchRepository.GetByIdAsync(branchId);
                if (branch == null)
                    throw new KeyNotFoundException($"Şube bulunamadı: {branchId}");

                if (branch.IsLicenseExpired())
                {
                    await UpdateStatusAsync(branch.Id, false);
                    await _notificationService.SendLicenseExpirationNotificationAsync(branchId);
                    _logger.LogInformation($"Lisans süresi sona erdi: Şube {branchId}");
                }
                else if (branch.IsLicenseExpiringWithin(7))
                {
                    await _notificationService.SendLicenseExpirationNotificationAsync(branchId);
                    _logger.LogInformation($"Lisans süresi yakında sona erecek: Şube {branchId}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lisans sonu tarihi kontrolü sırasında hata oluştu: Şube {branchId}");
                throw;
            }
        }
    }
}