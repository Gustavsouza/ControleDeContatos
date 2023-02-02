using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
	public class AlterarSenhaController : Controller
	{
		private readonly IUsuarioRepositorio _usuarioRepositorio;
		private readonly ISessao _sessao;
		//private readonly IEmail _email;
		/*PORQUE NÃO PODE TER MULTIPLICOS CONSTRUTURORES ?*/
		public AlterarSenhaController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao/*, IEmail email*/)
		{
			_usuarioRepositorio = usuarioRepositorio;
			_sessao = sessao;
			//_email = email;
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]

		public IActionResult Alterar(AlterarSenhaModel alterarSenhaModel)
		{
			try
			{
				UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
				alterarSenhaModel.Id = usuarioLogado.id;
				if (ModelState.IsValid)
				{
					_usuarioRepositorio.AlterarSenha(alterarSenhaModel);
					TempData["MensagemSucesso"] = "Senha Alterada com sucesso!";
					//bool emailEnviado = _email.Enviar(usuarioLogado.email, "Sistema de contatos - Senha", $"Sua senha foi alterada com sucesso {usuarioLogado.nome}");
				}
				return View("Index", alterarSenhaModel);

			}
			catch (Exception erro)
			{
				TempData["MensagemErro"] = $"Não foi possível Alterar sua senha =( , tente novamente {erro.Message} ";
				return View("Index", alterarSenhaModel);
			}
		}
	}
}
