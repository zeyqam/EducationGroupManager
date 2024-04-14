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
    public class EducationRepository : BaseRepository<Education>, IEducationRepository
    {
        private readonly AppDbContext _context;
        public EducationRepository()
        {
            _context = new AppDbContext();
        }
        public async Task<IEnumerable<Education>> GetAllWithGroupsAsync()
        {
            return await Task.FromResult(_context.Educations.Include(e => e.Groups));
        }

        public async Task<IEnumerable<Education>> SortByCreatedDateAsync()
        {
            
            
                return await _context.Educations.OrderBy(e => e.CreatedDate).ToListAsync();
           
        }
    }
}
