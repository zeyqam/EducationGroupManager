using Service.Helpers.Extensions;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationGroupManager.Controllers
{
    public class EducationController
    {
        private readonly IEducationService _educationService;
        public EducationController()
        {
            _educationService=new EducationService();
            
        }

        public async Task CreateEducationAsync()
        {
            ConsoleColor.Cyan.WriteConsole("Add education name:");
            Name: string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto Name;
            }
            int minLength = 3;
            int maxLength = 50;

            
            if (name.Length < minLength || name.Length > maxLength)
            {
                Console.WriteLine($" Name must be between {minLength} and {maxLength} characters.");
                goto Name;
            }

            Console.WriteLine("Add education color:");
            Color: string color = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(color))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto Color;
            }

            await _educationService.CreateEducationAsync(name, color);
            ConsoleColor.Green.WriteConsole("Data successfully added");
        }

        public async Task GetAllEducationAsync()
        {
            var datas = await _educationService.GetAllAsync();
            foreach (var item in datas)
            {
                string data = $"Name: {item.Name}, Color: {item.Color}";
                Console.WriteLine(data);
            }

        }
    }
}
