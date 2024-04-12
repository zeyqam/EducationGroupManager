using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Helpers.Extensions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
