using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
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
    public class GroupController
    {
        private readonly IGroupService _groupService;
        private bool _ascendingOrder=true;

        public GroupController()
        {
            _groupService = new GroupService();

        }

        public async Task GroupCreateAsync()
        {
            string name;
            bool isValidName = false;
            do
            {
                ConsoleColor.Cyan.WriteConsole("Qrup adını daxil edin:");
                name = Console.ReadLine();
                isValidName = IsNameValid(name);
            } while (!isValidName);

            int capacity;
            bool isValidCapacity = false;
            do
            {
                ConsoleColor.Cyan.WriteConsole("Qrupun kapasitesini daxil edin:");
                string capacityInput = Console.ReadLine();
                isValidCapacity = int.TryParse(capacityInput, out capacity) && capacity > 0;
                if (!isValidCapacity)
                {
                    ConsoleColor.Red.WriteConsole("Xahiş edirik, düzgün bir kapasite daxil edin.");
                }
            } while (!isValidCapacity);

            int educationId;
            bool educationExists = false;
            do
            {
                ConsoleColor.Cyan.WriteConsole("Təhsil İD-sini daxil edin:");
                string educationIdInput = Console.ReadLine();
                if (int.TryParse(educationIdInput, out educationId))
                {
                    educationExists = await _groupService.CheckIfEducationExistsAsync(educationId);
                    if (!educationExists)
                    {
                        ConsoleColor.Red.WriteConsole("Xəta: Daxil edilmiş təhsil İD-si mövcud deyil. Əvvəlcə təhsil yaradın.");
                    }
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Düzgün Təhsil İD formatı daxil edilməyib. Xahiş edirik yenidən daxil edin:");
                }
            } while (!educationExists);


            await _groupService.CreateGroupAsync(name, capacity, educationId);
            ConsoleColor.Green.WriteConsole("Qrup uğurla yaradıldı.");
        }

        private bool IsNameValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ConsoleColor.Red.WriteConsole("Qrup adı boş ola bilməz.");
                return false;
            }

            return true;
        }


        public async Task GetAllGroupsAsync()
        {
            var datas = await _groupService.GetAllAsync();
            foreach (var item in datas)
            {
                string data = $"Id:{item.Id},Name: {item.Name}, Capacity: {item.Capacity}, Education id : {item.EducationId}";
                Console.WriteLine(data);
            }
        }

        public async Task GetGroupByIdAsync()
        {
            int id;
            string idStr;

            do
            {
                ConsoleColor.Cyan.WriteConsole("Add Group id: ");
                idStr = Console.ReadLine();

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
                    var group = await _groupService.GetByIdAsync(id);

                    if (group != null)
                    {

                        ConsoleColor.Green.WriteConsole($"Id:{group.Id},Group name: {group.Name}, Capacity: {group.Capacity}, Education Id: {group.EducationId}");
                        return;
                    }
                    else
                    {
                        ConsoleColor.Red.WriteConsole("Group not found.");
                    }

                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                }

            } while (true);
        }


        public async Task GetGroupsByEducationIdAsync()
        {
            int id;
            string idStr;

            do
            {
                ConsoleColor.Cyan.WriteConsole("Enter Education ID: ");
                idStr = Console.ReadLine();

                if (!int.TryParse(idStr, out id))
                {
                    ConsoleColor.Red.WriteConsole("ID format is wrong, please try again");
                    continue;
                }

                try
                {
                    var groups = await _groupService.GetGroupsByEducationIdAsync(id);
                    if (groups.Any())
                    {
                        ConsoleColor.Green.WriteConsole($"Groups for Education ID {id}:");
                        foreach (var group in groups)
                        {
                            ConsoleColor.Yellow.WriteConsole($"Group ID: {group.Id}, Name: {group.Name}, Capacity: {group.Capacity}, Created Date: {group.CreatedDate}");
                        }
                    }
                    else
                    {
                        ConsoleColor.Yellow.WriteConsole($"No groups found for Education ID {id}.");
                    }
                    return;
                }
                catch (ArgumentException ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                }
            } while (true);
        }

        public async Task DeleteGroupsAsync()
        {
            int id;
            string idStr;

            do
            {
                ConsoleColor.Cyan.WriteConsole("Delete Group id: ");
                idStr = Console.ReadLine();

                if (!int.TryParse(idStr, out id))
                {
                    ConsoleColor.Red.WriteConsole("Id format is wrong, please try again");
                    continue;
                }

                try
                {
                    await _groupService.DeleteGroupAsync(id);
                    ConsoleColor.Green.WriteConsole($"Group with ID {id} deleted successfully!");
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


        public async Task UpdateGroupAsync()
        {
            Console.WriteLine("Enter the ID of the group to update:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid input. Please enter a valid group ID:");
            }

            Console.WriteLine("Enter the new name for the group:");
            string newName = Console.ReadLine();

            Console.WriteLine("Enter the new capacity for the group:");
            int newCapacity;
            while (!int.TryParse(Console.ReadLine(), out newCapacity))
            {
                Console.WriteLine("Invalid input. Please enter a valid number for capacity:");
            }

            try
            {
                Group existingGroup = await _groupService.GetByIdAsync(id);
                if (existingGroup != null)
                {
                    existingGroup.Name = newName;
                    existingGroup.Capacity = newCapacity;
                    if (string.IsNullOrWhiteSpace(newName) && newCapacity <= 0)
                    {
                        Console.WriteLine("No new data provided. Existing data remains unchanged.");
                        return;
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(newName))
                        {
                            existingGroup.Name = newName;
                        }

                        if (newCapacity > 0)
                        {
                            existingGroup.Capacity = newCapacity;
                        }

                        await _groupService.UpdateGroupAsync(existingGroup);
                        Console.WriteLine("Group updated successfully.");
                    }

                }
                else
                {
                    Console.WriteLine("Group not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating group: {ex.Message}");
            }
        }
        public async Task SearchGroupByNameAsync()
        {
            Console.WriteLine("Welcome to the Group Search");

            while (true)
            {
                Console.WriteLine("\nSearch groups by name");
                Console.Write("Enter group name: ");
                string name = Console.ReadLine();

                var groups = await _groupService.SearchGroupAsync(name);

                if (groups != null && groups.Any())
                {
                    Console.WriteLine("\nMatching Groups:");
                    foreach (var group in groups)
                    {
                        Console.WriteLine($"ID: {group.Id}, Name: {group.Name}, Capacity:{group.Capacity}");
                    }
                }
                else
                {
                    Console.WriteLine("No groups found with the given name.");
                }

                Console.WriteLine("\nDo you want to search for another group? (yes/no)");
                string response = Console.ReadLine().ToLower();
                if (response != "yes")
                {
                    Console.WriteLine("Exiting the application. Goodbye!");
                    break;
                }
            }

        }
        public async Task FilterByEducationNameAsync()
        {
            Console.WriteLine("Enter education name:");
            string educationName = Console.ReadLine();

            try
            {
                var groups = await _groupService.FilterByEducationNameAsync(educationName);
                if (groups.Any())
                {
                    Console.WriteLine("\nMatching Groups:");
                    foreach (var group in groups)
                    {
                        Console.WriteLine($"ID: {group.Id}, Name: {group.Name}, Capacity: {group.Capacity}");
                    }
                }
                else
                {
                    Console.WriteLine("No groups found with the given education name.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public async Task SortByCapacityAsync()
        {
            string sortOrder = _ascendingOrder ? "ascending" : "descending";
            Console.WriteLine($"Sorting educations by created date in {sortOrder} order...");

            var groups = await _groupService.SortByCapacityAsync();

            if (groups != null && groups.Any())
            {
                foreach (var group in groups)
                {
                    Console.WriteLine($"ID: {group.Id}, Name: {group.Name}, Capacity: {group.Capacity}");
                }
            }
            else
            {
                Console.WriteLine("No educations found.");
            }
        }



    }

}
