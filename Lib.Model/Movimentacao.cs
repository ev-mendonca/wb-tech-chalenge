using System;
using System.ComponentModel.DataAnnotations;

namespace Lib.Model
{
    public class Movimentacao
    {
        private Movimentacao() { }
        internal Movimentacao(decimal valor, enumTipoMovimentacao tipo)
        {
            Valor = valor;
            Tipo = tipo;
            Horario = DateTime.Now;
        }

        [Required]
        public int Id { get; private set; }
        [Required]
        public decimal Valor { get; private set; }
        [Required]
        public enumTipoMovimentacao Tipo { get; private set; }

        [Required]
        public DateTime Horario { get; private set; }
        
        [Required]
        public Conta Conta { get; private set; }
    }

    public enum enumTipoMovimentacao
    {
        Deposito = 1,
        Saque = 2,
        Pagamento = 3
    }
}
