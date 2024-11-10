// QRMenu.Infrastructure/Repositories/CompanyRepository.cs
using Microsoft.EntityFrameworkCore;
using QRMenu.Core.Entities;
using QRMenu.Core.Interfaces;
using QRMenu.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRMenu.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public async Task<Company> GetBySlugAsync(string slug)
        {
            return await _context.Companies.FirstOrDefaultAsync(c => c.Slug == slug);
        }

        public async Task<Company> AddAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<Company> UpdateAsync(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task DeleteAsync(int id)
        {
            var company = await GetByIdAsync(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsSlugUniqueAsync(string slug, int? excludeId = null)
        {
            return !await _context.Companies
                .AnyAsync(c => c.Slug == slug && (!excludeId.HasValue || c.Id != excludeId.Value));
        }

        public async Task<IEnumerable<Company>> GetExpiredCompaniesAsync()
        {
            return await _context.Companies
                .Where(c => c.LicenseEndDate < DateTime.UtcNow && c.IsActive)
                .ToListAsync();
        }

        public async Task<Company> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Companies
                .Include(c => c.Branches)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}