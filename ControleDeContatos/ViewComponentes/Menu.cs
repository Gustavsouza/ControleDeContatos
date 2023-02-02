using ControleDeContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ControleDeContatos.ViewComponentes
{
	public class Menu : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{

			string sessaoDoMeuUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

			if (string.IsNullOrEmpty(sessaoDoMeuUsuario)) return null;

			UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoDoMeuUsuario);

			return View(usuario);
		}
	}
}
