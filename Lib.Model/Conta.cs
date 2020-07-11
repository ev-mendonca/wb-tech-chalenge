using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Lib.Model
{
    public class Conta
    {
        #region Construtores
        private Conta()
        {

        }

        public Conta(string agencia, string numero)
        {
            Agencia = agencia;
            Numero = numero;
            Movimentacoes = new List<Movimentacao>();

        }
        #endregion

        #region Metodos

        public void EfetuarSaque(decimal valor)
        {
            validarSaldoMinimo(valor);
            Movimentacoes.Add(new Movimentacao(valor, enumTipoMovimentacao.Saque));
            Saldo -= valor;
        }

        public void EfetuarDeposito(decimal valor)
        {
            validarValorMinimo(valor);
            Movimentacoes.Add(new Movimentacao(valor, enumTipoMovimentacao.Deposito));
            Saldo += valor;

        }

        public void EfetuarPagamento(decimal valor)
        {
            validarSaldoMinimo(valor);
            Movimentacoes.Add(new Movimentacao(valor, enumTipoMovimentacao.Pagamento));
            Saldo -= valor;
        }

        private void validarValorMinimo(decimal valor)
        {
            if (valor <= 0)
                throw new Exception("Somente são permitidos valores maiores que 0");
        }
        private void validarSaldoMinimo(decimal valor)
        {
            validarValorMinimo(valor);

            if (Saldo < valor)
                throw new Exception("Saldo insuficiente.");
        }
        #endregion


        [Required]
        public int Id { get; private set; }
        [Required, MaxLength(4)]
        public string Agencia { get; private set; }
        [Required, MaxLength(6)]
        public string Numero { get; private set; }
        [Required]
        public decimal Saldo{ get; private set; }

        public List<Movimentacao> Movimentacoes { get; private set; }
    }
}
