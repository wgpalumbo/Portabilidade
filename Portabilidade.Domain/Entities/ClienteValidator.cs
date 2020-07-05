using System;
using FluentValidation;

namespace Portabilidade.Domain.Entities
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {

        public ClienteValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Por Favor, Informe o Nome do Cliente")
                .Length(3, 150).WithMessage("O campo nome deve ter entre 3 e 150 caracteres");


            RuleFor(x => x.DocumentoCpf).NotEmpty().WithMessage("Por Favor, Informe o C.P.F.");

            RuleFor(x => x.Endereco).Length(20, 250).WithMessage("Por Favor, Endereço de 20 a 250 Caracteres");


        }

    }

}