using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IGroupService
    {
        Task CreateGroupAsync(string name, int capacity, int educationId);
        Task<bool> CheckIfEducationExistsAsync(int educationId);
        Task<IEnumerable<Group>> GetAllAsync();
    }
}
