using EasyMarketing.Models;
using EasyMarketing.Repositories;


namespace EasyMarketing.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> RegisterUserAsync(RegisterUserDto dto)
        {
            // Check if user already exists
            if (await _repository.GetUserByEmailAsync(dto.Email) != null)
                return "Email is already in use.";

            // Hash the password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash);
            // Create and save user
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = hashedPassword,
                Phone=dto.Phone,
                Address = dto.Address,
                Age = dto.Age,
                PinCode = dto.PinCode,
                
            };
            await _repository.AddUserAsync(user);

            return "Registration successful.";
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            // Map your entity to a DTO
            var userDto = new UserDto
            {
                
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                Age = user.Age,
                PinCode = user.PinCode,
                // Add other properties as needed
            };

            return userDto;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _repository.GetAllUsersAsync();
            // Map the entity list to a DTO list
            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                userDtos.Add(new UserDto
                {
                   
                    FullName = user.FullName,
                    Email = user.Email,
                    Phone = user.Phone,
                    Address = user.Address,
                    Age = user.Age,
                    PinCode = user.PinCode,
                    
                    // Add other properties as needed
                });
            }

            return userDtos;
        }
        public async Task<string> AddUserAsync(UserDto userDto)
        {
            var user = new User
            {
                FullName = userDto.FullName,
                Email = userDto.Email,
                Phone = userDto.Phone,
                Address = userDto.Address,
                Age= userDto.Age,
                PinCode = userDto.PinCode,
                // Add other properties as needed
            };
            await _repository.AddUserAsync(user);
            return "User added successfully.";
        }

        public Task<string> RegisterUserAsync(Controllers.RegisterUserDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
