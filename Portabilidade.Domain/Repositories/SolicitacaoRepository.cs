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

        public void Alterar(Solicitacao solicitacao)
        {
            var index = _storage.FindIndex(0, 1, x => x.CodigoInternoSolicitacao == solicitacao.CodigoInternoSolicitacao);
            _storage[index] = solicitacao;
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