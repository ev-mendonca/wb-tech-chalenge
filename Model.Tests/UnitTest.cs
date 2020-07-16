using Lib.Model;
using Lib.Model.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Model.Tests
{
    [TestClass]
    public class UnitTest
    {
        private Conta conta = new Conta("1","1");


        #region Deposito
        
        [TestMethod]
        public void EfetuarDepositoValorNegativoThrowException()
        {
            try
            {
                conta.EfetuarDeposito(-6);
                Assert.Fail($"Uma excessão {nameof(MovimentacaoInvalidaException)} deveria ter sido disparada");
            }
            catch (MovimentacaoInvalidaException ex)
            {
                Assert.AreEqual("Somente são permitidos valores maiores que 0", ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Uma excessão não esperada foi disparada: {ex.Message}");
            }
        }

        [TestMethod]
        public void EfetuarDeposito20Reais()
        {
            conta.EfetuarDeposito(20);
            Assert.AreEqual(20, conta.Saldo);
        }

        [TestMethod]
        public void EfetuarDepositoValorZeradoThrowException()
        {
            try
            {
                conta.EfetuarDeposito(0);
                Assert.Fail($"Uma excessão {nameof(MovimentacaoInvalidaException)} deveria ter sido disparada");
            }
            catch (MovimentacaoInvalidaException ex)
            {
                Assert.AreEqual("Somente são permitidos valores maiores que 0", ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Uma excessão não esperada foi disparada: {ex.Message}");
            }
        }

        #endregion


        #region Saque
        
        [TestMethod]
        public void EfetuarSaqueValorNegativoThrowException()
        {
            try
            {
                conta.EfetuarSaque(-20);
                Assert.Fail($"Uma excessão {nameof(MovimentacaoInvalidaException)} deveria ter sido disparada");
            }
            catch (MovimentacaoInvalidaException ex)
            {
                Assert.AreEqual("Somente são permitidos valores maiores que 0", ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Uma excessão não esperada foi disparada: {ex.Message}");
            }
        }

        [TestMethod]
        public void EfetuarSaqueValorZeradoThrowException()
        {
            try
            {
                conta.EfetuarSaque(0);
                Assert.Fail($"Uma excessão {nameof(MovimentacaoInvalidaException)} deveria ter sido disparada");
            }
            catch (MovimentacaoInvalidaException ex)
            {
                Assert.AreEqual("Somente são permitidos valores maiores que 0", ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Uma excessão não esperada foi disparada: {ex.Message}");
            }
        }

        [TestMethod]
        public void EfetuarSaque40ComSaldo30ThrowException()
        {
            try
            {
                conta.EfetuarDeposito(30);
                conta.EfetuarSaque(40);
                Assert.Fail($"Uma excessão {nameof(MovimentacaoInvalidaException)} deveria ter sido disparada");
            }
            catch (MovimentacaoInvalidaException ex)
            {
                Assert.AreEqual("Saldo insuficiente.", ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Uma excessão não esperada foi disparada: {ex.Message}");
            }
        }

        [TestMethod]
        public void EfetuarSaque40ComSaldo50()
        {
            conta.EfetuarDeposito(50);
            conta.EfetuarSaque(40);
            Assert.AreEqual(10, conta.Saldo);
        }

        [TestMethod]
        public void EfetuarSaque40ComSaldo40()
        {
            conta.EfetuarDeposito(40);
            conta.EfetuarSaque(40);
            Assert.AreEqual(0, conta.Saldo);
        }


        #endregion

        #region Pagamento

        [TestMethod]
        public void EfetuarPagamentoValorNegativoThrowException()
        {
            try
            {
                conta.EfetuarPagamento(-35);
                Assert.Fail($"Uma excessão {nameof(MovimentacaoInvalidaException)} deveria ter sido disparada");
            }
            catch (MovimentacaoInvalidaException ex)
            {
                Assert.AreEqual("Somente são permitidos valores maiores que 0", ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Uma excessão não esperada foi disparada: {ex.Message}");
            }
        }

        [TestMethod]
        public void EfetuarPagamentoValorZeradoThrowException()
        {
            try
            {
                conta.EfetuarSaque(0);
                Assert.Fail($"Uma excessão {nameof(MovimentacaoInvalidaException)} deveria ter sido disparada");
            }
            catch (MovimentacaoInvalidaException ex)
            {
                Assert.AreEqual("Somente são permitidos valores maiores que 0", ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Uma excessão não esperada foi disparada: {ex.Message}");
            }
        }

        [TestMethod]
        public void EfetuarPagamento100ComSaldo99ThrowException()
        {
            try
            {
                conta.EfetuarDeposito(99);
                conta.EfetuarSaque(100);
                Assert.Fail($"Uma excessão {nameof(MovimentacaoInvalidaException)} deveria ter sido disparada");
            }
            catch (MovimentacaoInvalidaException ex)
            {
                Assert.AreEqual("Saldo insuficiente.", ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Uma excessão não esperada foi disparada: {ex.Message}");
            }
        }

        [TestMethod]
        public void EfetuarPagamento100ComSaldo150()
        {
            conta.EfetuarDeposito(150);
            conta.EfetuarSaque(100);
            Assert.AreEqual(50, conta.Saldo);
        }

        [TestMethod]
        public void EfetuarPagamento100ComSaldo100()
        {
            conta.EfetuarDeposito(100);
            conta.EfetuarSaque(100);
            Assert.AreEqual(0, conta.Saldo);
        }


        #endregion
    }
}
