using ControleDeContatos.Filters;
using ControleDeContatos.Models;
using ControleDeContatos.repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{

    [PaginaParaUsuarioLogado]
    [PaginaRestritaSomenteAdmin]
    public class UsuarioController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        //private readonly IEmail _email;
        private readonly IContatoRepositorio _contatoRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IContatoRepositorio contatoRepositorio /*IEmail email*/)
        {
            _usuarioRepositorio = usuarioRepositorio;
            //_email = email;
            _contatoRepositorio = contatoRepositorio;
        }

        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }
        public IActionResult Criar()
        {

            return View();
        }
        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorID(id);
            return View(usuario);
        }


        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorID(id);
            return View(usuario);
        }

        public IActionResult ListarContatosPorUsuarioId(int id)
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos(id);
            return PartialView("_ContatosUsuario", contatos);
        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Contato Cadastrado com sucesso!!";
                    //bool emailEnviado = _email.Enviar(usuario.email, "Sistema de contatos -  Novo Usuario", $"Bem vindo ao sistema de cadastro de contatos! suas credenciais de acesso são:Email: {usuario.email}, Login: {usuario.login},Perfil:{usuario.Perfil} ");
                    return RedirectToAction("Index");
                }
                Console.WriteLine("Passou fora dnv");

                return View(usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível cadastrar seu usuário =( , tente novamente {erro.Message} ";
                return RedirectToAction("Index");

            }
        }

        [HttpPost]
        public IActionResult Alterar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {
            try
            {

                UsuarioModel usuario = null;

                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        id = usuarioSemSenhaModel.id,
                        nome = usuarioSemSenhaModel.nome,
                        login = usuarioSemSenhaModel.login,
                        email = usuarioSemSenhaModel.email,
                        Perfil = usuarioSemSenhaModel.Perfil

                    };
                    usuario = _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuario alterado com sucesso!!!";
                    return RedirectToAction("Index");
                }

                return View(usuario);
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
                bool apagado = _usuarioRepositorio.Apagar(id);
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
