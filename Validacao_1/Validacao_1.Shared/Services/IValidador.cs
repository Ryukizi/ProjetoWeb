using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Validacao_1.Shared.Pages.Home;

namespace Validacao_1.Shared.Services
{
    public interface IValidador
    {
        public List<string> EntradaDeDados(Pessoa pessoa);

        public bool Idade(Pessoa pessoa);

        public string Nome(Pessoa pessoa);

        public string Email(Pessoa pessoa);

        public string Senha(Pessoa pessoa);
    }
}
