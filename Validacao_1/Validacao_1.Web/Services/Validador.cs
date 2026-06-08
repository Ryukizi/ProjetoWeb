using Microsoft.AspNetCore.Mvc.RazorPages;
using Validacao_1.Shared.Services;

namespace Validacao_1.Web.Services
{
    public class Validador : IValidador
    {
        public string Idade(int input)
        {
            if (input < 18)
            {

                return "você é menor de idade";
            }
            else
            {
                return "Você é maior de idade";
            }
        }

    }
}
