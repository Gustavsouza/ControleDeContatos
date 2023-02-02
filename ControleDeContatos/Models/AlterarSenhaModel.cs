using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class AlterarSenhaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite a senha Atual")]

        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Digite a  nova senha ")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Digite a confirmação da nova senha")]
        [Compare("NovaSenha", ErrorMessage = "As senhas não são iguais")]
        public string ConfirmarNovaSenha { get; set; }
    }
}
