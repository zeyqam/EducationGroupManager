using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IGroupRepository:IBaseRepository<Group>
    {
        Task<IEnumerable<Group>> FilterByEducationNameAsync(string educationName);
        Task<IEnumerable<Group>> GetAllWithEducationIdAsync(int educationId);
        Task<IEnumerable<Group>> SortWithCapacityAsync();
        Task<bool> EducationExistsAsync(int educationId);
    }
}
