using Lib.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repository.Interfaces
{
    public interface IContaRepository
    {
        Task CriarConta(string agencia, string numero);
        Task<Conta> CarregarConta(string agencia, string numero);
        Task EfetuarSaque(string agencia, string numero, decimal valor);
        Task EfetuarDeposito(string agencia, string numero, decimal valor);
        Task EfetuarPagamento(string agencia, string numero, decimal valor);

    }
}
