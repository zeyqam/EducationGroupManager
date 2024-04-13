using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Helpers.Constants;
using Service.Helpers.Exceptions;
using Service.Helpers.Extensions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

       
    }
}
