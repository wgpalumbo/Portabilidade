using System;
using System.Collections.Generic;
using Portabilidade.Domain.Entities;

namespace Portabilidade.Domain.Repositories
{
    public interface ISolicitacaoRepository
    {
        void Incluir(Solicitacao solicitacao);
        IEnumerable<Solicitacao> ListarSolicitacao();
        Solicitacao ObterPorId(Guid id);
        void Alterar(Solicitacao solicitacao);
        void Remover(Solicitacao solicitacao);

    }
}