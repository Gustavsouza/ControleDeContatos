using ControleDeContatos.Models;

namespace ControleDeContatos.repositorio
{
    public interface IUsuarioRepositorio
    {

        UsuarioModel BuscarPorLogin(string login);
        UsuarioModel BuscarPorEmail(String email, string login);
        UsuarioModel ListarPorID(int id);
        List<UsuarioModel> BuscarTodos();
        UsuarioModel Adicionar(UsuarioModel usuario);
        UsuarioModel Atualizar(UsuarioModel usuario);
        UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel);
        bool Apagar(int id);

    }
}
