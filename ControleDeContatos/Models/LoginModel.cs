using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
	public class LoginModel
	{
		[Required(ErrorMessage = "Digite o nome de usuario")]
		public string Login { get; set; }
		[Required(ErrorMessage = "Digite a senha do usuario")]
		public string Senha { get; set; }
	}
}
