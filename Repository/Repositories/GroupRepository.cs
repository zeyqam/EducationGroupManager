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

        public Task<IEnumerable<Group>> FilterByEducationNameAsync(string educationName)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Group>> GetAllWithEducationIdAsync(int educationId)
        {
            return await _context.Groups
            .Where(g => g.EducationId == educationId)
            .ToListAsync();
        }

        public Task<IEnumerable<Group>> SortWithCapacityAsync()
        {
            throw new NotImplementedException();
        }
    }
}
