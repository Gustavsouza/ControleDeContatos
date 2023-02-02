using ControleDeContatos.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class UsuarioSemSenhaModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Digite o nome do usuario")]
        public string nome { get; set; }
        [Required(ErrorMessage = "Digite o login do usuario")]
        public string login { get; set; }

        [Required(ErrorMessage = "*Digite o email do usuario")]
        [EmailAddress(ErrorMessage = "*Informe um email válido")]
        public string email { get; set; }
        [Required(ErrorMessage = "*Selecione o perfil do usuario")]
        public PerfilEnum? Perfil { get; set; }

    }
}
