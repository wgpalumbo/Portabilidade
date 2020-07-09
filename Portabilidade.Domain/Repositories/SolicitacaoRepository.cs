using System;
using System.Collections.Generic;
using System.Linq;
using Portabilidade.Domain.Entities;

namespace Portabilidade.Domain.Repositories
{
    public class SolicitacaoRepository : ISolicitacaoRepository
    {
        private readonly List<Solicitacao> _storage;

        public SolicitacaoRepository()
        {
            _storage = new List<Solicitacao>();
        }

        public void Incluir(Solicitacao solicitacao)
        {
          
            _storage.Add(solicitacao);
        }
 
        public bool Alterar(Guid id, Solicitacao solicitacao)
        {
            bool retorno = false;
            int index = _storage.FindIndex(x => x.CodigoInternoSolicitacao == id);
            Console.WriteLine(id);
            Console.WriteLine("Index " + index);
            if (index >= 0)
            {
                _storage[index] = solicitacao;
                retorno = true;
            }
            return retorno;
        }

        public IEnumerable<Solicitacao> ListarSolicitacao()
        {
            return _storage;
        }

        public Solicitacao ObterPorId(Guid id)
        {
            return _storage.FirstOrDefault(x => x.CodigoInternoSolicitacao == id);
        }

        public void Remover(Solicitacao solicitacao)
        {
            _storage.Remove(solicitacao);
        }
    }
}