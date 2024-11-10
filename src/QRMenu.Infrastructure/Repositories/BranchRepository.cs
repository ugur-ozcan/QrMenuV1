// QRMenu.Infrastructure/Repositories/BranchRepository.cs
using Microsoft.EntityFrameworkCore;
using QRMenu.Core.Entities;
using QRMenu.Core.Interfaces;
using QRMenu.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QRMenu.Core.Extensions;
 

namespace QRMenu.Infrastructure.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ApplicationDbContext _context;

        public BranchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Branch>> ListAllAsync()
        {
            return await _context.Branches.ToListAsync();
        }

        public async Task<Branch> GetByIdAsync(int id)
        {
            return await _context.Branches.FindAsync(id);
        }

        public async Task<Branch> GetBySlugAsync(string slug)
        {
            return await _context.Branches.FirstOrDefaultAsync(b => b.Slug == slug);
        }

        public async Task<Branch> AddAsync(Branch branch)
        {
            await _context.Branches.AddAsync(branch);
            await _context.SaveChangesAsync();
            return branch;
        }

        public async Task UpdateAsync(Branch branch)
        {
            _context.Entry(branch).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch != null)
            {
                _context.Branches.Remove(branch);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateStatusAsync(int id, bool isActive)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch != null)
            {
                branch.IsActive = isActive;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsSlugUniqueAsync(string slug, int? excludeId = null)
        {
            return !await _context.Branches.AnyAsync(b => b.Slug == slug && (!excludeId.HasValue || b.Id != excludeId.Value));
        }

        public async Task<IEnumerable<Branch>> GetExpiredServicesAsync()
        {
            return await _context.Branches
                .Where(b => b.LicenseEndDate <= DateTime.UtcNow && b.IsActive)
                .ToListAsync();
        }
    }
}