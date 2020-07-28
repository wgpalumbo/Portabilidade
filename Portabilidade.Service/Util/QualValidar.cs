using System;
using System.Linq;

namespace Portabilidade.Service.Util
{
    public class QualValidar : IQualValidar
    {        

        public bool IsValid(string cpfcnpj)
        {
            try
            {
                string documento = new String(cpfcnpj.Where(Char.IsDigit).ToArray());

                if (documento.Length == 11)
                {
                    var validarDoc = (IValidarStrategy)Activator.CreateInstance(typeof(ValidarCpf));
                    return validarDoc.IsValid(documento);
                }
                else if (documento.Length == 14)
                {
                    var validarDoc = (IValidarStrategy)Activator.CreateInstance(typeof(ValidarCnpj));
                    return validarDoc.IsValid(documento);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error no JSON Input {e}");
            }

            return false;


        }
    }
}