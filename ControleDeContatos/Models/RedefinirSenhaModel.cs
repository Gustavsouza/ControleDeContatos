using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
	public class RedefinirSenhaModel
	{
		[Required(ErrorMessage = "Digite o nome de usuario")]
		public string Login { get; set; }
		[Required(ErrorMessage = "Digite o Email")]
		public string Email { get; set; }
	}
}
