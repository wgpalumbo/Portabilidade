using System;
using System.Collections.Generic;
using System.Linq;

namespace Portabilidade.Service.Util
{
    public class ValidarCnpj : IValidarStrategy
    {

        public bool IsValid(string cnpj)
        {
            bool retorno = false;
            try
            {

                int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                cnpj = new String(cnpj.Where(Char.IsDigit).ToArray());

                if (cnpj.Length != 14)
                    return false;

                if (CnpjComDigitosRepetidos(cnpj))
                    return false;

                string tempCnpj = cnpj.Substring(0, 12);
                int soma = 0;

                for (int i = 0; i < 12; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

                int resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                string digito = resto.ToString();
                tempCnpj = tempCnpj + digito;
                soma = 0;
                for (int i = 0; i < 13; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = digito + resto.ToString();

                retorno = (cnpj.EndsWith(digito));
            }
            catch(Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }

            return retorno;

        }

        private bool CnpjComDigitosRepetidos(string numero)
        {
            bool retorno = false;
            try
            {
                var cnpjInvalidos = new List<string>
                {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
                };

                retorno = (cnpjInvalidos.Contains(numero));
            }
           catch(Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
            return retorno;
        }

    }
}