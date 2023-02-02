using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        //Construtor 
        public ContatoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        /*Já que eu não consigo usar o bancoContext, pois ele está dentro de outro escopo*/

        private readonly BancoContext _bancoContext;

        public ContatoModel Adicionar(ContatoModel contato)
        {
            //Gravar no banco
            _bancoContext.Add(contato);
            _bancoContext.SaveChanges();
            return contato;
        }


        public ContatoModel ListarPorID(int id)
        {
            return _bancoContext.contatos.FirstOrDefault(x => x.id == id);
        }

        public List<ContatoModel> BuscarTodos(int id)
        {
            return _bancoContext.contatos.Where(x => x.usuarioId == id).ToList();
        }

        public ContatoModel Remover(ContatoModel contato)
        {
            _bancoContext.Remove(contato);
            _bancoContext.SaveChanges();
            return contato;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDb = ListarPorID(contato.id);

            if (contatoDb == null) throw new Exception("Houver um erro na atualização do contato");

            contatoDb.nome = contato.nome;
            contatoDb.email = contato.email;
            contatoDb.celular = contato.celular;

            _bancoContext.contatos.Update(contatoDb);
            _bancoContext.SaveChanges();
            return contatoDb;
        }

        public bool Apagar(int id)
        {
            ContatoModel contatoDb = ListarPorID(id);

            if (contatoDb == null) throw new Exception("Houver um erro na atualização do contato");

            contatoDb.id = id;
            _bancoContext.contatos.Remove(contatoDb);
            _bancoContext.SaveChanges();
            return true;

        }
    }
}
