using System.ComponentModel.DataAnnotations;

namespace MiniPloomes.Data.Dto
{
    public class CreateClienteDto
    {
        [Required]
        public string Nome { get; set; }
    }
}
