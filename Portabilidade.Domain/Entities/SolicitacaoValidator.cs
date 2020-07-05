using System;
using FluentValidation;
using PortabilidadeContext.Portabilidade.Domain.Enums;

namespace Portabilidade.Domain.Entities
{
    public class SolicitacaoValidator : AbstractValidator<Solicitacao>
    {

        public SolicitacaoValidator()
        {
            RuleFor(x => x.Cliente).SetValidator(new ClienteValidator());

            //se quiser usar uma validação do tipo abaixo
            //RuleFor(x => x.DataTransferencia).Must(BeAValidDate).WithMessage("Por Favor, Informe a Data de Transferencia");
            RuleFor(x => x.DataTransferencia).NotEmpty().WithMessage("Por Favor, Informe a Data de Transferencia");

            RuleFor(x => x.AgenteCedente.Instituicao).NotEmpty().WithMessage("Por Favor, Informe a Instituição Cedente");
            RuleFor(x => x.AgenteCedente.CodigoInvestidor).NotEmpty().WithMessage("Por Favor, Informe o Codigo de Investidor no Agente Cedente");

            RuleFor(x => x.AgenteCessionario.Instituicao).NotEmpty().WithMessage("Por Favor, Informe a Instituição Cessionario");
            RuleFor(x => x.AgenteCessionario.CodigoInvestidor).NotEmpty().WithMessage("Por Favor, Informe  Codigo de Investidor no Agente Cessionario");

            var _enumMotivoCount = EMotivoTransferencia.GetNames(typeof(EMotivoTransferencia)).Length;
            RuleFor(x => x.Motivo).Must(x => (x >= 1 && x <= _enumMotivoCount)).WithMessage("Por Favor, Informe o Motivo Entre 1 e " + _enumMotivoCount.ToString());

            RuleFor(x => x.Ativos.Count).Must(x => (x >= 1)).WithMessage("Por Favor, Informe os Ativos");

            RuleForEach(x => x.Ativos).SetValidator(new AtivoValidator());

        }


        // private bool BeAValidDate(DateTime date)
        // {
        //     return !date.Equals(default(DateTime));
        // }
        // private bool BeAValidDateString(string valuedate)
        // {
        //     DateTime date;
        //     return DateTime.TryParse(valuedate, out date);
        // }
    }
}