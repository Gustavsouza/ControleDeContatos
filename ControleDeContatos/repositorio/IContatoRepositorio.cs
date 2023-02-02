using ControleDeContatos.Models;

namespace ControleDeContatos.repositorio
{
    public interface IContatoRepositorio
    {
        ContatoModel ListarPorID(int id);
        List<ContatoModel> BuscarTodos(int id);
        ContatoModel Adicionar(ContatoModel contato);
        ContatoModel Atualizar(ContatoModel contato);
        bool Apagar(int id);

    }
}
