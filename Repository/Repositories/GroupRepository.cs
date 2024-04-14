using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        private readonly AppDbContext _context;
        public GroupRepository()
        {
            _context = new AppDbContext();
        }
        public async Task<bool> EducationExistsAsync(int educationId)
        {
            return await _context.Educations.AnyAsync(e => e.Id == educationId);

        }

        public async Task<IEnumerable<Group>> FilterByEducationNameAsync(string educationName)
        {
            if (string.IsNullOrWhiteSpace(educationName))
            {
                throw new ArgumentException("Education name cannot be null or empty.");
            }

            educationName = educationName.Trim().ToLower();

            return await _context.Groups
                .Include(g => g.Education)
                .Where(g => g.Education.Name.ToLower().Contains(educationName))
                .ToListAsync();
        }

        public async Task<IEnumerable<Group>> GetAllWithEducationIdAsync(int educationId)
        {
            return await _context.Groups
            .Where(g => g.EducationId == educationId)
            .ToListAsync();
        }

        public async Task<IEnumerable<Group>> SortWithCapacityAsync()
        {
            return await _context.Groups.OrderBy(e => e.Capacity).ToListAsync();
        }
    }
}
