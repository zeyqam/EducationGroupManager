using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationGroupManager.Controllers
{
    public class UserController
    {
        private readonly IUserService _userService;
        public UserController()
        {
            _userService = new UserService();
        }

        public async Task RegisterUserAsync()
        {
            Console.WriteLine("User Registration:");
            Console.Write("Enter your full name: ");
            string fullName = Console.ReadLine();
            Console.Write("Enter your desired username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your email address: ");
            string email = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            await _userService.RegisterAsync(fullName, username, email, password);
        }

        public async Task LoginUserAsync()
        {
            bool loginSuccessful = false;
            do
            {
                Console.WriteLine("\nUser Login:");
                Console.Write("Enter your username: ");
                string username = Console.ReadLine();
                Console.Write("Enter your password: ");
                string password = Console.ReadLine();

                 loginSuccessful = await _userService.LoginAsync(username, password);
                if (!loginSuccessful)
                {
                    
                
                    Console.WriteLine("Login failed. Would you like to register instead?(yes/no)");
                    string response = Console.ReadLine();
                    if (response.ToLower() == "yes")
                    {
                        await RegisterUserAsync();
                        await LoginUserAsync();
                    }
                }
            } while (!loginSuccessful);
            await _userService.GetMenusAsync();
        }
        public async Task GetMenusAsync()
        {
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
