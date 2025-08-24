using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartaoVacina.Domain.Entities
{
    public class Vacina : BaseEntity
    {
        public string Nome { get; private set; }

        public Vacina(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome inválido");
            Nome = nome.Trim();
        }
    }
}
