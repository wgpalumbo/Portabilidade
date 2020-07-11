using System;
using System.Linq;
using FluentValidation;
using Portabilidade.Service.Util;

namespace Portabilidade.Domain.Entities
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {

        public ClienteValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Por Favor, Informe o Nome do Cliente")
                .Length(3, 150).WithMessage("O campo nome deve ter entre 3 e 150 caracteres");


            RuleFor(x => x.DocumentoCpf)
                .NotEmpty().WithMessage("Por Favor, Informe o Documento")
                .Must(BeAValidDocumento).WithMessage("Por Favor, Documento Corretamente !");

            RuleFor(x => x.Endereco).Length(20, 250).WithMessage("Por Favor, Endereço de 20 a 250 Caracteres");


        }

        private bool BeAValidDocumento(string cpdcnpj)
        {
            string documento = new String(cpdcnpj.Where(Char.IsDigit).ToArray());
            if (documento.Length == 11)
            {
                return (new CpfValidador(cpdcnpj)).EstaValido();
            }
            else
            {
                return (new CnpjValidador(cpdcnpj)).EstaValido();
            }
        }

    }

}