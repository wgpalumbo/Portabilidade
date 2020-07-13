using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portabilidade.Domain.Repositories
{
    public interface ISqliteRepository<T>
    {
        void CriarTabela();
        Task Incluir(dynamic json);        
        Task<bool> Excluir(string id);
        Task<T> Obter(string id);
         IEnumerable<T> Listar();
        
    }
}