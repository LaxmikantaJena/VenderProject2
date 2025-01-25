using EasyMarketing.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyMarketing.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
       
    }
}
