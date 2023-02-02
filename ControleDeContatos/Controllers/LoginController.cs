using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;

        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }
        public IActionResult Index()

        {

            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");
            return View();
        }


        public IActionResult RedefinirSenha()
        {
            return View();
        }


        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                Debug.WriteLine("Entrou no processo");
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);


                    if (usuario != null)
                    {
                        Console.WriteLine("Entrou no processo");
                        if (usuario.SenhaValidado(loginModel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = $"Não foi possível realizar o seu login =( , Sua senha está errada JKKKK ";
                    }
                    TempData["MensagemErro"] = $"Não foi possível realizar o seu login =( , tente novamente Caiu fora ";
                }
                return View("Index");
            }
            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Não foi possível realizar o seu login =( , tente novamente {erro.Message} ";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                Debug.WriteLine("Entrou no processo");
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmail(redefinirSenhaModel.Email, redefinirSenhaModel.Login);


                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        string mensagem = $"Sua nova senha é: {novaSenha}";
                        bool emailEnviado = _email.Enviar(usuario.email, "Sistema de contatos -  Nova Senha", mensagem);


                        try
                        {
                            if (emailEnviado)
                            {
                                _usuarioRepositorio.Atualizar(usuario);
                                TempData["MensagemSucesso"] = $"Enviamos para o seu e-mail cadastrado uma nova senha =) ";
                            }
                            else
                            {
                                TempData["MensagemErro"] = $"Não foi possível Enviar o Email =( Algo deu errado! ";
                            }
                        }
                        catch (Exception exe)
                        {

                            TempData["MensagemErro"] = $"Não foi possível Enviar o Email =( {exe.Message}";
                            return RedirectToAction("Index");
                        }



                        return RedirectToAction("Index", "Login");
                    }
                    TempData["MensagemErro"] = $"Não foi possível sua alteração de senha =( , tente novamente Caiu fora ";
                }
                return View("Index");
            }
            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Não foi possível realizar o sua alteração de senha =( , tente novamente {erro.Message} ";
                return RedirectToAction("Index");
            }
        }
    }

}

