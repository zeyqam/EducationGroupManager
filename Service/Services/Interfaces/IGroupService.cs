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
        Task<Group> GetByIdAsync(int? id);
        Task DeleteGroupAsync(int? id);
        Task<IEnumerable<Group>> GetGroupsByEducationIdAsync(int? educationId);
        Task UpdateGroupAsync(Group updateGroup);
        Task<IEnumerable<Group>> SearchGroupAsync(string name);
        Task<IEnumerable<Group>> FilterByEducationNameAsync(string educationName);
        Task<IEnumerable<Group>> SortByCapacityAsync();
    }
}
