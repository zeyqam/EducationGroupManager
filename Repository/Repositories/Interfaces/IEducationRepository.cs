using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IEducationRepository:IBaseRepository<Education>
    {
        Task<IEnumerable<Education>> GetAllWithGroupsAsync();
        Task<IEnumerable<Education>> SortByCreatedDateAsync();
    }
}
