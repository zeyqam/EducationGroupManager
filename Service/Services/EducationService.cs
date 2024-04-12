using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
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
        public EducationService()
        {
            _educationRepo = new EducationRepository();
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
    }
}
