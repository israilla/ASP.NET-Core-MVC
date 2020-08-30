using System.Collections.Generic;
using System.Linq;

namespace DevIO.Business.Models.Validations.Documento
{
    public class CpfValidacao
    {
        public const int TamanhoCpf = 11;

        public static bool Validar(string cpf)
        {
            var cpfNumeros = Utils.ApenasNumeros(cpf);

            if (!TamanhoValido(cpfNumeros)) return false;
            return !TemDigitosRepetidos(cpfNumeros) && TemDigitosValidos(cpfNumeros);
        }

        private static bool TamanhoValido(string valor)
        {
            return valor.Length == TamanhoCpf;
        }

        private static bool TemDigitosRepetidos(string valor)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999",
            };
            return invalidNumbers.Contains(valor);
        }

        private static bool TemDigitosValidos(string valor)
        {
            var number = valor.Substring(0, TamanhoCpf - 2);
            var digitoVerificador = new DigitoVerificador(number)
                .ComMultiplicadoresDeAte(2, 11)
                .Substituindo("0", 10, 11);
            var firstDigit = digitoVerificador.CalculaDigito();
        }
    }
    public class CnpjValidacao
    {
        public const int TamanhoCnpj = 14;
        public static bool Validar(string cnpj)
        {
            var cnpjNumeros = Utils.ApenasNumeros(cnpj);

            if (!TemTamanhoValido(cnpjNumeros)) return false;
            return !TemDigitosRepetidos(cnpjNumeros) && TemDigitosValidos(cnpjNumeros);
        }
        private static bool TemTamanhoValido(string valor)
        {
            return valor.Length == TamanhoCnpj;
        }
        
        private static bool TemDigitosRepetidos(string valor)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999",
            };
            return invalidNumbers.Contains(valor);
        }
        private static bool TemDigitosValidos(string valor)
        {

        }
    }
    public class DigitoVerificador
    {
        private string _numero;
        private const int Modulo = 11;
        private readonly List<int> _multiplicadores = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly IDictionary<int, string> _subtituicoes = new Dictionary<int, string>();
        private bool _complemntarDoModulo = true;

        public DigitoVerificador ComMultiplicadoresDeAte(int primeiroMultiplicador, int ultimoMutiplicador)
        {
            _multiplicadores.Clear();
            for (var i = primeiroMultiplicador; i <= ultimoMutiplicador; i++)
                _multiplicadores.Add(i);
            return this;
        }

        public DigitoVerificador Subtistuindo(string subtituto, params int [] digitos)
        {
            foreach (var i in digitos)
            {
                _subtituicoes[i] = subtituto;
            }
            return this;
        }
        public void AddDigito(string digito)
        {
            _numero = string.Concat(_numero, digito);
        }
    }
    public class Utils
    {
        public static string ApenasNumeros(string valor)
        {
            var onlyNumber = "";
            foreach(var s in valor)
            {
                if (char.IsDigit(s))
                {
                    onlyNumber += s;
                }
            }
            return onlyNumber.Trim();
        }
    }
}
