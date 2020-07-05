using FluentValidation;

namespace Portabilidade.Domain.Entities
{
    public class AtivoValidator : AbstractValidator<Ativo>
    {

        public AtivoValidator()
        {
            RuleFor(x => x.Codigo).NotEmpty().WithMessage("Por Favor, Informe o Codigo do Ativo");

            RuleFor(x => x.Tipo).NotEmpty().WithMessage("Por Favor, Informe o Tipo do Ativo");

            RuleFor(x => x.Quantidade)
                .NotEmpty().WithMessage("Por Favor, Informe a Quantidade")
                .Must(x => (x >= 1)).WithMessage("Por Favor, Informe a Quantidade acima de 1");

        }

    }
}