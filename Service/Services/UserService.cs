using Repository.Repositories.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Services.Interfaces;
using Domain.Models;

namespace Service.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService()
        {
            _userRepo = new UserRepository();
        }

        

        public async Task<bool> LoginAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Username and password are required.");
                return false;
            }
            var user = await _userRepo.GetUserByUsernameAsync(username);
            if (user != null && user.Password == password)
            {
                Console.WriteLine("Login successful.");
                return true;
            }
            else
            {
                Console.WriteLine("Login failed.");
                return false;
            }
        }

        public async Task RegisterAsync(string fullName, string username, string email, string password)
        {
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("All fields are required.");
                return;
            }
            var existingUser = await _userRepo.GetUserByUsernameAsync(username);
            if (existingUser != null)
            {
                Console.WriteLine("Username already exists. Please choose another one.");
                return;
            }

            var newUser = new User
            {
                FullName = fullName,
                Username = username,
                Email = email,
                Password = password,
                CreatedDate = DateTime.Now
            };
            await _userRepo.AddUserAsync(newUser);
            Console.WriteLine("Registration successful.");
        }
        public async Task GetMenusAsync()
        {
             Console.WriteLine("Menus are available");


            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                     Menu Operations                     ║");
            Console.WriteLine("╠══════╦══════════════════════════════════════════════════╣");
            Console.WriteLine("║ Code ║                 Operation                        ║");
            Console.WriteLine("╠══════╬══════════════════════════════════════════════════╣");
            Console.WriteLine("║   1  ║ Create Education                                 ║");
            Console.WriteLine("║   2  ║ Create Group                                     ║");
            Console.WriteLine("║   3  ║ GetAllEducation                                  ║");
            Console.WriteLine("║   4  ║ GetAllGroups                                     ║");
            Console.WriteLine("║   5  ║ GetEducationById                                 ║");
            Console.WriteLine("║   6  ║ GetGroupsBiyId                                   ║");
            Console.WriteLine("║   7  ║ GetAllGroupsWithEducationId                      ║");
            Console.WriteLine("║   8  ║ DeleteEducation                                  ║");
            Console.WriteLine("║   9  ║ DeleteGroup                                      ║");
            Console.WriteLine("║  10  ║ UpdateEducation                                  ║");
            Console.WriteLine("║  11  ║ UpdateGroup                                      ║");
            Console.WriteLine("║  12  ║ SearchEducation                                  ║");
            Console.WriteLine("║  13  ║ SearchGroups                                     ║");
            Console.WriteLine("║  14  ║ GetAllWithGroups                                 ║");
            Console.WriteLine("║  15  ║ SortWithCreatedDateEducation                     ║");
            Console.WriteLine("║  16  ║ FilterByEducationName                            ║");
            Console.WriteLine("║  17  ║ SortWithCapacity                                 ║");
            Console.WriteLine("╚══════╩══════════════════════");

        }
    }
}
