using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portabilidade.Domain.Repositories
{
    public interface ISqliteRepository<T>
    {
        void CriarTabela();
        ValueTask Incluir(dynamic json);
        ValueTask<bool> Excluir(string id);
        ValueTask<T> Obter(string id);
        ValueTask<IEnumerable<T>> Listar();

    }
}