// QRMenu.Core/Interfaces/IBranchRepository.cs
using QRMenu.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QRMenu.Core.Interfaces
{
    public interface IBranchRepository
    {
        Task<IEnumerable<Branch>> ListAllAsync();
        Task<Branch> GetByIdAsync(int id);
        Task<Branch> GetBySlugAsync(string slug);
        Task<Branch> AddAsync(Branch branch);
        Task UpdateAsync(Branch branch);
        Task DeleteAsync(int id);
        Task UpdateStatusAsync(int id, bool isActive);
        Task<bool> IsSlugUniqueAsync(string slug, int? excludeId = null);
        Task<IEnumerable<Branch>> GetExpiredServicesAsync();
    }
}