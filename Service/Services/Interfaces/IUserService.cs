using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(string fullName, string username, string email, string password);
        Task<bool> LoginAsync(string username, string password);
        Task GetMenusAsync();
    }
}
