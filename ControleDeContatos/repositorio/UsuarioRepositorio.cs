using ControleDeContatos.Data;
using ControleDeContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        //Construtor 
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        /*Já que eu não consigo usar o bancoContext, pois ele está dentro de outro escopo*/

        private readonly BancoContext _bancoContext;

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            //Gravar no banco
            usuario.DataDeCadastro = DateTime.Now;
            _bancoContext.Add(usuario);
            usuario.SetSenhaHash();
            _bancoContext.SaveChanges();
            return usuario;
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.login.ToUpper() == login.ToUpper());
        }


        public UsuarioModel BuscarPorEmail(string email, string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.email.ToUpper() == email.ToUpper() && x.login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel ListarPorID(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.id == id);
        }


        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios.Include(x => x.Contatos).ToList();
        }

        public UsuarioModel Remover(UsuarioModel usuario)
        {
            _bancoContext.Remove(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDb = ListarPorID(usuario.id);

            if (usuarioDb == null) throw new Exception("Houver um erro na atualização do Usuario");

            usuarioDb.nome = usuario.nome;
            usuarioDb.login = usuario.login;
            usuarioDb.email = usuario.email;
            usuarioDb.Perfil = usuario.Perfil;
            //usuarioDb.Senha = usuario.Senha;
            //usuarioDb.DataDeCadastro = usuario.DataDeCadastro;
            usuarioDb.DataDeAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDb);
            _bancoContext.SaveChanges();
            return usuarioDb;
        }


        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDB = ListarPorID(alterarSenhaModel.Id);
            if (usuarioDB == null) throw new Exception("Houver um erro na atualização da senha, usuario não encontrado");

            if (!usuarioDB.SenhaValidado(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual não confere");

            if (usuarioDB.SenhaValidado(alterarSenhaModel.NovaSenha)) throw new Exception("Nova senha já utilizada");

            usuarioDB.SetNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioDB.DataDeAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();

            return usuarioDB;
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDb = ListarPorID(id);

            if (usuarioDb == null) throw new Exception("Houver um erro na exclusão do contato");

            usuarioDb.id = id;
            _bancoContext.Usuarios.Remove(usuarioDb);
            _bancoContext.SaveChanges();
            return true;

        }


    }
}
