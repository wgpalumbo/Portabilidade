using System.ComponentModel;

namespace PortabilidadeContext.Portabilidade.Domain.Enums
{

    public enum EMotivoTransferencia
    {
        [Description("Doação")]
        doacao = 1,
        [Description("Venda Privada")]
        vendaPrivada = 2,
        [Description("Ordem Judicial")]
        ordermJudicial = 3,
        [Description("Herança")]
        heranca = 4,
        [Description("Conversão de ADR")]
        conversaoAdr = 5,
        [Description("Empréstimo Privado")]
        emprestimoPrivado = 6,
        [Description("Sucessão Societária")]
        sucessaoSocietaria = 7,
        [Description("Determinação Legal")]
        determinacaoLegal = 8,
        [Description("Int.Cotas ou Fundos de Investimentos")]
        integrCotasFundosInvestimentos = 9,
        [Description("Conversão de UNITS")]
        conversaoUnits = 10,
        [Description("Garantias de Ofertas")]
        garantiasOfertas = 11,
        [Description("Falhas de Alocação de Operação")]
        falhasAlocacaoOperacao = 12,
        [Description("Falhas na Liquidação")]
        falhasLiquidacao = 13,
        [Description("Mesma Titularidade")]
        mesmaTitulariedade = 14,
    };
}