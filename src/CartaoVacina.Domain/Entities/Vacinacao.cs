using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartaoVacina.Domain.Entities
{
    internal class Vacinacao : BaseEntity
    {
        public Guid PessoaId { get; private set; }
        public Guid VacinaId { get; private set; }


        public int Dose { get; private set; } // 1, 2, 3...
        public DateTime DataAplicacao { get; private set; }


        // Navegação (opcional)
        public Pessoa? Pessoa { get; private set; }
        public Vacina? Vacina { get; private set; }


        public Vacinacao(Guid pessoaId, Guid vacinaId, int dose, DateTime dataAplicacao)
        {
            if (dose <= 0) throw new ArgumentException("Dose deve ser > 0");
            PessoaId = pessoaId;
            VacinaId = vacinaId;
            Dose = dose;
            DataAplicacao = dataAplicacao.Date;
        }
    }
}
