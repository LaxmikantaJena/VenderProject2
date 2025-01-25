using System.ComponentModel.DataAnnotations;

namespace EasyMarketing.Models
{
    public class UserDto
    {
        [Key]
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public int Phone {  get; set; }
        public string? Password {  get; set; }
        public int Age {  get; set; }
        public string? Address {  get; set; }
        public int PinCode {  get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
