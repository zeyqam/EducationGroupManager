﻿using Service.Helpers.Exceptions;
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
    }
}
