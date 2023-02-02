using ControleDeContatos.Filters;
using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ContatoController : Controller
    {

        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;

        public ContatoController(IContatoRepositorio contatoRepositorio, ISessao sessao)
        {
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;

        }
        public IActionResult Index()

        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos(usuarioLogado.id);
            return View(contatos);
        }

        public IActionResult Criar()
        {

            return View();
        }
        public IActionResult Json()
        {

            return View();
        }
        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorID(id);
            return View(contato);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorID(id);
            return View(contato);
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    contato.usuarioId = usuarioLogado.id;
                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato Cadastrado com sucesso!!!";
                    return RedirectToAction("Index");
                }

                return View(contato);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível cadastrar seu contato =( , tente novamente {erro.Message} ";
                return RedirectToAction("Index");

            }
        }
        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    contato.usuarioId = usuarioLogado.id;
                    _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato alterado com sucesso!!";
                    return RedirectToAction("Index");
                }

                return View(contato);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível Alterar seu contato =( , tente novamente {erro.Message} ";
                return RedirectToAction("Index");

            }
        }

        public IActionResult Remover(int id)
        {
            try
            {
                bool apagado = _contatoRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso!!!";
                }
                else
                {
                    TempData["MensagemErro"] = "Contato Não apagado ";
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                TempData["MensagemErro"] = "Contato Não apagado ";
                return RedirectToAction("Index");
            }

        }

    }
}
