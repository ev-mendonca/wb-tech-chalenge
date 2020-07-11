using Lib.Model;
using Lib.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Lib.Repository
{
    public class ContaRepository : IContaRepository
    {
        private readonly Context _context;
        private readonly DbSet<Conta> _dbSet;

        public ContaRepository(Context context)
        {
            _context = context;
            _dbSet = _context.Set<Conta>();
        }

        public async Task<Conta> CarregarConta(string agencia, string numero)
        {
            return await _dbSet.Where(c => c.Agencia == agencia && c.Numero == numero).SingleOrDefaultAsync();
        }

        public async Task CriarConta(string agencia, string numero)
        {
            _dbSet.Add(new Conta(agencia, numero));
            await _context.SaveChangesAsync();

        }

        public async Task EfetuarDeposito(string agencia, string numero, decimal valor)
        {
            Conta conta = await CarregarConta(agencia, numero);
            conta.EfetuarDeposito(valor);
            await _context.SaveChangesAsync();
        }

        public async Task EfetuarPagamento(string agencia, string numero, decimal valor)
        {
            Conta conta = await CarregarConta(agencia, numero);
            conta.EfetuarPagamento(valor);
            await _context.SaveChangesAsync();
        }

        public async Task EfetuarSaque(string agencia, string numero, decimal valor)
        {
            Conta conta = await CarregarConta(agencia, numero);
            conta.EfetuarSaque(valor);
            await _context.SaveChangesAsync();

        }
    }
}
