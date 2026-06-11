using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Validacao_1.Shared.Services;
using static Validacao_1.Shared.Pages.Home;

namespace Validacao_1.Services
{
    public class Validador : IValidador
    {
        public List<string> EntradaDeDados(Pessoa pessoa)
        {
            var mensagem = new List<string>();

            if (Idade(pessoa))
            {
                mensagem.Add(Email(pessoa));
                mensagem.Add(Senha(pessoa));
                mensagem.Add(Nome(pessoa));
            }
            else
            {
                mensagem.Add("Pessoa é menor de idade, não é possível validar email e senha.");
            }
            return mensagem;
        }


        public bool Idade(Pessoa pessoa)
        {
            bool maiorIdade = false;
            if (pessoa.Idade >= 18)
            {
                maiorIdade = true;
            }
            return maiorIdade;
        }

        public string Nome(Pessoa pessoa)
        {
            bool ValidName = !string.IsNullOrWhiteSpace(pessoa.Nome) && pessoa.Nome.All(c => char.IsLetter(c) || c == ' ');

            bool temEspacoDuplicado = Regex.IsMatch(pessoa.Nome ?? string.Empty, @"\s{2,}");
            if (ValidName && !temEspacoDuplicado)
            {
                return "Nome valído";
            }
            else
            {
                return "Nome invalído";
            }
        }

        public string Email(Pessoa pessoa)
        {
            bool emailValido = Regex.IsMatch(pessoa.Email ?? string.Empty, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (emailValido)
            {
                return "Email válido";
            }
            return "Email inválido";
        }

        public string Senha(Pessoa pessoa)
        {
            bool senhaValida = Regex.IsMatch(pessoa.Senha ?? string.Empty, @"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$");

            if (senhaValida)
            {
                //return "Senha válida";
                return HashPassword(pessoa.Senha ?? string.Empty) + "senha valída";
            }
            return "senha inválida";
        }

        private static string HashPassword(string password)
        {
            const int iterations = 100_000;
            const int saltSize = 32;
            const int keySize = 32;

            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[saltSize];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var key = pbkdf2.GetBytes(keySize);

            string saltBase64 = Convert.ToBase64String(salt);
            string KeyBase64 = Convert.ToBase64String(key);

            return $"{iterations}.{saltBase64}.{KeyBase64}";
        }
    }
}
