using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartaoVacina.Domain.Entities
{
    public class Pessoa : BaseEntity
    {
        public string Nome { get; private set; }
        public string Documento { get; private set; } // CPF/Identificação única
        public int Idade { get; private set; }
        public SexoPessoa Sexo { get; private set; }

        public enum SexoPessoa
        {
            Masculino,
            Feminino,
            Outro
        }


        private readonly List<Vacinacao> _vacinacoes = new();
        public IReadOnlyCollection<Vacinacao> Vacinacoes => _vacinacoes.AsReadOnly();


        public Pessoa(string nome, string documento)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome inválido");
            if (string.IsNullOrWhiteSpace(documento)) throw new ArgumentException("Documento inválido");
            Nome = nome.Trim();
            Documento = documento.Trim();
        }


        public void RegistrarVacinacao(Vacinacao vacinacao)
        {
            _vacinacoes.Add(vacinacao);
        }
    }
}
