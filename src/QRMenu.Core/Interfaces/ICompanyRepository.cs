// QRMenu.Core/Interfaces/ICompanyRepository.cs
using QRMenu.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QRMenu.Core.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company> GetByIdAsync(int id);
        Task<Company> GetBySlugAsync(string slug);
        Task<Company> AddAsync(Company company);
        Task<Company> UpdateAsync(Company company);
        Task DeleteAsync(int id);
        Task<bool> IsSlugUniqueAsync(string slug, int? excludeId = null);
        Task<IEnumerable<Company>> GetExpiredCompaniesAsync();
        Task<Company> GetByIdWithDetailsAsync(int id);
    }
}