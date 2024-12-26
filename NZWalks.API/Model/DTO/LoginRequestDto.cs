using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Model.DTO
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
