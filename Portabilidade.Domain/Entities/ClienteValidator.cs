using System;
using FluentValidation;
using Portabilidade.Service.Util;

namespace Portabilidade.Domain.Entities
{

    public class ClienteValidator : AbstractValidator<Cliente>
    {

        public ClienteValidator()
        {
            var validarDocumento = (IQualValidar)Activator.CreateInstance(typeof(QualValidar));

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Por Favor, Informe o Nome do Cliente")
                .Length(3, 150).WithMessage("O campo nome deve ter entre 3 e 150 caracteres");


            RuleFor(x => x.DocumentoCpf)
                .NotEmpty().WithMessage("Por Favor, Informe o Documento")
                .Must(validarDocumento.IsValid).WithMessage("Por Favor, Documento Corretamente !");

            RuleFor(x => x.Endereco).Length(20, 250).WithMessage("Por Favor, Endereço de 20 a 250 Caracteres");

        }

    }

}