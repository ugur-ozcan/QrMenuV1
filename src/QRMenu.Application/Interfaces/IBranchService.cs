// QRMenu.Application/Interfaces/IBranchService.cs
using QRMenu.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using QRMenu.Core.Extensions;
using QRMenu.Core.Interfaces;
using QRMenu.Core.Entities;

namespace QRMenu.Application.Interfaces
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchDTO>> GetAllAsync();
        Task<BranchDTO> GetByIdAsync(int id);
        Task<BranchDTO> GetBySlugAsync(string slug);
        Task<BranchDTO> CreateBranchAsync(BranchDTO branchDto);
        Task<BranchDTO> UpdateAsync(int id, BranchDTO branchDto);
        Task DeleteAsync(int id);
        Task UpdateStatusAsync(int id, bool isActive);
        Task<bool> IsSlugUniqueAsync(string slug, int? excludeId = null);
        Task ProcessExpiredServicesAsync();
        Task<bool> TestConnectionAsync(int id);
        Task SyncBranchDataAsync(int branchId);
        Task UpdateHealthStatusAsync(int branchId, bool isHealthy);
        Task LicenseEndDateAsync(int branchId);
    }
}