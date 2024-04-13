using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IEducationService
    {
        Task<Education> CreateEducationAsync(string name,string color);
        Task<IEnumerable<Education>> GetAllAsync();
        Task<Education>GetByIdAsync(int? id);
        Task DeleteEducationAsync(int? id);
        Task UpdateEducationAsync(Education updateEducation);
        Task<IEnumerable<Education>> SearchEducationAsync(string name);
    }
}
