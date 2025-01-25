using System.ComponentModel.DataAnnotations;

namespace EasyMarketing.Models
{
    public class RegisterUserDto
    {
        [Key]
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public int Age { get; set; }
         public int Phone {  get; set; }
        public string? Address {  get; set; }
        public int PinCode {  get; set; }
    }
}
