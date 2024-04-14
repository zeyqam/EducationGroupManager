using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Helpers.Constants;
using Service.Helpers.Exceptions;
using Service.Helpers.Extensions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepo;
        
        public GroupService()
        {
            _groupRepo=new GroupRepository();
            
        }

        public async Task<bool> CheckIfEducationExistsAsync(int educationId)
        {
            return await _groupRepo.EducationExistsAsync(educationId);

        }

        public async Task CreateGroupAsync(string name, int capacity, int educationId)
        {
            bool educationExists = await CheckIfEducationExistsAsync(educationId);
            if (!educationExists)
            {
                ConsoleColor.Red.WriteConsole("Xəta: Daxil edilmiş tehsil İD-si movcud deyil.");
                return;
            }

            Group newGroup = new Group
            {
                Name = name,
                Capacity = capacity,
                EducationId = educationId,
                CreatedDate = DateTime.Now 
            };

            await _groupRepo.AddAsync(newGroup);

        }

        public async Task DeleteGroupAsync(int? id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID. ID must be greater than zero.");
            }

            var group = await _groupRepo.GetByIdAsync(id);
            if (group == null)
            {
                throw new NotFoundException(ResponseMessages.DataNotFound);
            }

            await _groupRepo.DeleteAsync(group);
        }

        public async Task<IEnumerable<Group>> FilterByEducationNameAsync(string educationName)
        {
            return await _groupRepo.FilterByEducationNameAsync(educationName);
        }

        public  async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _groupRepo.GetAllAsync();
        }

        public async Task<Group> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Group group = await _groupRepo.GetByIdAsync((int)id);
            if (group == null)
            {
                throw new NotFoundException(ResponseMessages.DataNotFound);
            }
            return group;
        }

        public async Task<IEnumerable<Group>> GetGroupsByEducationIdAsync(int? educationId)
        {
            if (educationId == null || educationId <= 0)
            {
                throw new ArgumentException("Invalid education ID. ID must be greater than zero.");
            }

            return await _groupRepo.GetAllWithEducationIdAsync((int)educationId);
        }

        public async Task<IEnumerable<Group>> SearchGroupAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ConsoleColor.Red.WriteConsole("Name cannot be null or empty.");
            }

            name = name.Trim().ToLower();

            Expression<Func<Group, bool>> predicate = group => group.Name.ToLower().Contains(name);
            return await _groupRepo.SearchAsync(predicate);
        }

        public  async Task<IEnumerable<Group>> SortByCapacityAsync()
        {
            return await _groupRepo.SortWithCapacityAsync();
        }

        public async Task UpdateGroupAsync(Group updateGroup)
        {
            if (updateGroup == null)
                throw new NotFoundException(ResponseMessages.DataNotFound);

            Group existingGroup = await _groupRepo.GetByIdAsync(updateGroup.Id);
            if (existingGroup != null)
            {
                existingGroup.Name = updateGroup.Name;
                existingGroup.Capacity = updateGroup.Capacity;

            }
            else
            {
                throw new InvalidOperationException("Education not found");
            }

            if (existingGroup == null)
                throw new NotFoundException(ResponseMessages.DataNotFound);

            await _groupRepo.UpdateAsync(existingGroup);
        }
    }
}
