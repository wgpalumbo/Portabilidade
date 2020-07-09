using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portabilidade.Domain.Repositories
{
    public interface ISqliteRepository<T>
    {
        void CriarTabela();
        Task Incluir(dynamic json);        
        bool Excluir(string id);
        string Obter(string id);
         IEnumerable<T> Listar();
        
    }
}