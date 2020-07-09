using System.Threading.Tasks;

namespace Portabilidade.Domain.Repositories
{
    public interface ISqliteRepository
    {
        void CriarTabela();
        Task Incluir(dynamic json);        
        bool Excluir(string id);
        string Obter(string id);
        
    }
}