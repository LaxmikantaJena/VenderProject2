using EasyMarketing.Models;

namespace EasyMarketing.Services
{
    public interface IUserService
    {
        Task<string> RegisterUserAsync(RegisterUserDto dto);
        Task<UserDto> GetUserByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<string> AddUserAsync(UserDto userDto);
        Task<string> RegisterUserAsync(Controllers.RegisterUserDto dto);
    }
}
