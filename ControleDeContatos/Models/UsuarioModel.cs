using ControleDeContatos.Enums;
using ControleDeContatos.Helper;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class UsuarioModel
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
        public string Senha { get; set; }

        public DateTime DataDeCadastro { get; set; }

        public DateTime? DataDeAtualizacao { get; set; }

        public virtual List<ContatoModel>? Contatos { get; set; }


        public bool SenhaValidado(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }

        public void SetNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash();
        }
        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = novaSenha.GerarHash();
            return novaSenha;
        }

    }
}
