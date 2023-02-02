using ControleDeContatos.Data;
using ControleDeContatos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoApiController : ControllerBase

    {
        private readonly BancoContext _bancoContext; /* a variavel do tipo bancoContext que realiza as inserções dentro do banco, que está sendo atribuiada no construtor*/
        public ContatoApiController(BancoContext bancoContext)
        {
            _bancoContext = bancoContext; /*Aqui estou atribuindo o contexto("conexão") para uma variavel*/
        }



        [Route("api/salvar")] /*link para acessar minha API*/
        [HttpPost] /*Aqui é um método de solicitação HTTP usado para enviar dados para um servidor web.*/

        /*Sempre usar Async em API, para não travar o processo (não tem fila de espera para o banco) */
        public async Task<ContatoModel> Salvar(ContatoModel contato)
        {
            await _bancoContext.AddAsync(contato);
            _bancoContext.SaveChanges();
            return contato;
        }
        [Route("api/TodosContatos")] /*link para acessar minha API*/
        [AllowAnonymous]
        [HttpGet]
        public async Task<List<ContatoModel>> ListarTodos()
        {
            List<ContatoModel> contato = await _bancoContext.contatos.ToListAsync();

            /*Dessa forma é mais rápido, passando chapado*/
            //return await _bancoContext.contatos.ToListAsync();

            return contato;
        }
    }
}
