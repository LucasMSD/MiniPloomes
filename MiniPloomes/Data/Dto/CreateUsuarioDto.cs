using System.ComponentModel.DataAnnotations;

namespace MiniPloomes.Data.Dto
{
    public class CreateUsuarioDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
