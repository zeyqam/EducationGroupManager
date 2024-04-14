using Domain.Models;
using Service.Helpers.Exceptions;
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
        private bool _ascendingOrder = true;
        public EducationController()
        {
            _educationService = new EducationService();

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
                ConsoleColor.Red.WriteConsole($" Name must be between {minLength} and {maxLength} characters.");
                goto Name;
            }

            ConsoleColor.Cyan.WriteConsole("Add education color:");
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
                string data = $"Id:{item.Id},Name: {item.Name}, Color: {item.Color}";
                Console.WriteLine(data);
            }

        }
        public async Task GetEducationByIdAsync()
        {
            int id;
            string idStr;

            do
            {
                ConsoleColor.Cyan.WriteConsole("Add Education id: ");
            Id: idStr = Console.ReadLine();

                if (!int.TryParse(idStr, out id))
                {
                    ConsoleColor.Red.WriteConsole("Id format is wrong, please try again");
                    continue;
                }

                if (id <= 0)
                {
                    ConsoleColor.Red.WriteConsole("Invalid ID. ID must be greater than zero.");
                    continue;
                }

                try
                {
                    var education = await _educationService.GetByIdAsync(id);

                    if (education != null)
                    {
                        ConsoleColor.Green.WriteConsole($"Education name: {education.Name}, Color: {education.Color}, Created Date: {education.CreatedDate}");
                        break;
                    }
                    else
                    {
                        ConsoleColor.Yellow.WriteConsole("Education not found.");
                    }

                }
                catch (AggregateException ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.InnerException.Message);
                    goto Id;
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                    goto Id;
                }

            } while (true);
        }

        public async Task DeleteEducationAsync()
        {
            int id;
            string idStr;

            do
            {
                ConsoleColor.Cyan.WriteConsole("Delete Education id: ");
                idStr = Console.ReadLine();

                if (!int.TryParse(idStr, out id))
                {
                    ConsoleColor.Red.WriteConsole("Id format is wrong, please try again");
                    continue;
                }

                try
                {
                    await _educationService.DeleteEducationAsync(id);
                    ConsoleColor.Green.WriteConsole($"Education with ID {id} and its associated groups deleted successfully!");
                    return;
                }
                catch (ArgumentException ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                }
                catch (NotFoundException ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                }

            } while (true);
        }


        public async Task UpdateEducationAsync()
        {
            Console.WriteLine("Enter the ID of the education to update:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid input. Please enter a valid education ID.");
                return;
            }

            Console.WriteLine("Enter the new name for the education:");
            string newName = Console.ReadLine();

            Console.WriteLine("Enter the new color for the education:");
            string newColor = Console.ReadLine();

            try
            {
                Education existingEducation = await _educationService.GetByIdAsync(id);
                if (existingEducation != null)
                {

                    if (string.IsNullOrWhiteSpace(newName) && string.IsNullOrWhiteSpace(newColor))
                    {
                        Console.WriteLine("No new data provided. Existing data remains unchanged.");
                    }
                    else
                    {

                        if (!string.IsNullOrWhiteSpace(newName))
                        {
                            existingEducation.Name = newName;
                        }


                        if (!string.IsNullOrWhiteSpace(newColor))
                        {
                            existingEducation.Color = newColor;
                        }


                        await _educationService.UpdateEducationAsync(existingEducation);
                        Console.WriteLine("Education updated successfully.");
                    }
                }
                else
                {
                    Console.WriteLine("Education not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating education: {ex.Message}");
            }
        }
        public async Task SearchEducationByNameAsync()
        {
            Console.WriteLine("Welcome to the Education Search");

            while (true)
            {
                Console.WriteLine("\nSearch education by name");
                Console.Write("Enter education name: ");
                string name = Console.ReadLine();

                var educations = await _educationService.SearchEducationAsync(name);

                if (educations != null && educations.Any())
                {
                    Console.WriteLine("\nMatching Educations:");
                    foreach (var education in educations)
                    {
                        Console.WriteLine($"ID: {education.Id}, Name: {education.Name}, Color: {education.Color}");
                    }
                }
                else
                {
                    Console.WriteLine("No educations found with the given name.");
                }

                Console.WriteLine("\nDo you want to search for another education? (yes/no)");
                string response = Console.ReadLine().ToLower();
                if (response != "yes")
                {
                    Console.WriteLine("Exiting the application. Goodbye!");
                    break;
                }
            }

        }

        public async Task GetAllWithGroupsAsync()
        {
            Console.WriteLine("Fetching all educations with groups...");

            var educations = await _educationService.GetAllWithGroupsAsync();

            if (educations != null && educations.Any())
            {
                foreach (var education in educations)
                {
                    Console.WriteLine($"Education ID: {education.Id}, Name: {education.Name}, Color: {education.Color}");
                    Console.WriteLine("Groups:");

                    foreach (var group in education.Groups)
                    {
                        Console.WriteLine($"  Group ID: {group.Id}, Name: {group.Name}, Capacity: {group.Capacity}");
                    }

                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No educations found.");
            }
        }
        public async Task SortByCreatedDateAsync()
        {
            string sortOrder = _ascendingOrder ? "ascending" : "descending";
            Console.WriteLine($"Sorting educations by created date in {sortOrder} order...");

            var educations = await _educationService.SortByCreatedDateAsync();

            if (educations != null && educations.Any())
            {
                foreach (var education in educations)
                {
                    Console.WriteLine($"ID: {education.Id}, Name: {education.Name}, Created Date: {education.CreatedDate}");
                }
            }
            else
            {
                Console.WriteLine("No educations found.");
            }
        }

    }
}
    

