using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Helpers.Constants;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _educationRepo;
        private readonly IGroupRepository _groupRepo;
        public EducationService()
        {
            _educationRepo = new EducationRepository();
            _groupRepo = new GroupRepository();
        }

        public async Task<Education> CreateEducationAsync(string name, string color)
        {
            
            var newEducation = new Education
            {
                Name = name,
                Color =  color,
                CreatedDate = DateTime.Now 
            };
             await _educationRepo.AddAsync(newEducation);
            return newEducation;
        }

        public async Task DeleteEducationAsync(int? id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID. ID must be greater than zero.");
            }

            var education = await _educationRepo.GetByIdAsync(id);
            if (education == null)
            {
                throw new NotFoundException(ResponseMessages.DataNotFound);
            }

            
            var groups = await _groupRepo.GetAllWithEducationIdAsync((int)id);
            foreach (var group in groups)
            {
                await _groupRepo.DeleteAsync(group);
            }

            
            await _educationRepo.DeleteAsync(education);
        }

        public async  Task<IEnumerable<Education>> GetAllAsync()
        {
           return await _educationRepo.GetAllAsync();
        }

        public async Task<Education> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Education education = await _educationRepo.GetByIdAsync((int)id);
            if (education == null)
            {
                throw new NotFoundException(ResponseMessages.DataNotFound);
            }
            return education;
        }
    }
    
}
