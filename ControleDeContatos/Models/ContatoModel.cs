using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class ContatoModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "*Digite o nome do contato")]
        public string? nome { get; set; }
        [Required(ErrorMessage = "*Digite o email do contato")]
        [EmailAddress(ErrorMessage = "*Informe um email válido")]
        public string? email { get; set; }
        [Required(ErrorMessage = "*Digite o celular do contato")]
        [Phone(ErrorMessage = "*Digite um número de celular váilido")]
        public string? celular { get; set; }

        public int? usuarioId { get; set; }

        public UsuarioModel? Usuario { get; set; }
    }
}
