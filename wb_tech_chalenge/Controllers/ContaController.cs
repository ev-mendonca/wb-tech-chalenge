using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Lib;
using Lib.Model;
using Lib.Model.Exceptions;
using Lib.Repository;
using Lib.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using wb_tech_chalenge.Models;

namespace wb_tech_chalenge.Controllers
{
    public class ContaController : Controller
    {
        private readonly ILogger<ContaController> _logger;
        private readonly IContaRepository _repository;
        private string _agencia, _numero;
        public ContaController(ILogger<ContaController> logger, IContaRepository repository)
        {
            _logger = logger;
            _repository = repository;
            
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            _agencia = HttpContext.Session.GetString("agencia");
            _numero = HttpContext.Session.GetString("numero");
        }

        public IActionResult IniciarConta()
        {
            if (SessaoAtiva())
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult IniciarConta(string agencia, string numero)
        {
            try
            {
                Conta conta = _repository.CarregarConta(agencia, numero).Result;
                if (conta == null)
                {
                    _repository.CriarConta(agencia, numero).Wait();
                }
                HttpContext.Session.SetString("agencia", agencia);
                HttpContext.Session.SetString("numero", numero);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewData["msgErro"] = "Ocorreu um erro interno e a conta não pode ser selecionada. Tente novamente, por favor";
            }

            return View();
        }

        public IActionResult Index()
        {
            if (!SessaoAtiva())
                return RedirectToAction("IniciarConta");

            ResponseModel response = ResponseModelFromTempData();
            response.Conta = CarregarConta();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> EfetuarSaque(string valor)
        {
            if (SessaoAtiva())
            {
                ResponseModel response;
                try
                {
                    decimal valorDecimal;
                    
                    if(decimal.TryParse(valor.Replace(".",","),System.Globalization.NumberStyles.Currency, CultureInfo.GetCultureInfo("pt-BR"), out valorDecimal))
                    {
                        await _repository.EfetuarSaque(_agencia, _numero, valorDecimal);
                        response = new ResponseModel
                        {
                            Sucesso = true,
                            Mensagem = $"Saque de R${valorDecimal.ToString("0.00")} realizado com sucesso!"
                        };
                    }
                    else
                    {
                        response = new ResponseModel
                        {
                            Sucesso = false,
                            Mensagem = $"Saque não realizado. Valor informado é inválido"
                        };
                    }
                    
                }
                catch (MovimentacaoInvalidaException ex)
                {
                    response = new ResponseModel
                    {
                        Sucesso = false,
                        Mensagem = $"Saque não realizado: {ex.Message}"
                    };
                }
                catch (Exception)
                {
                    response = new ResponseModel
                    {
                        Sucesso = false,
                        Mensagem = "Saque não realizado: Um erro inesperado aconteceu"
                    };
                }

                ResponseModelToTempData(response);
                return RedirectToAction("Index");
            }

            return RedirectToAction("IniciarConta");
        }

        [HttpPost]
        public async Task<IActionResult> EfetuarDeposito(string valor)
        {
            if (SessaoAtiva())
            {
                ResponseModel response;
                try
                {
                    decimal valorDecimal;
                    
                    if (decimal.TryParse(valor.Replace(".", ","), System.Globalization.NumberStyles.Currency, CultureInfo.GetCultureInfo("pt-BR"), out valorDecimal))
                    {
                        await _repository.EfetuarDeposito(_agencia, _numero, valorDecimal);
                        response = new ResponseModel
                        {
                            Sucesso = true,
                            Mensagem = $"Deposito de R${valorDecimal.ToString("0.00")} realizado com sucesso!"
                        };
                    }
                    else
                    {
                        response = new ResponseModel
                        {
                            Sucesso = false,
                            Mensagem = $"Saque não realizado. Valor informado é inválido"
                        };
                    }
                }
                catch (MovimentacaoInvalidaException ex)
                {
                    response = new ResponseModel
                    {
                        Sucesso = false,
                        Mensagem = $"Deposito não realizado: {ex.Message}"
                    };
                }
                catch (Exception)
                {
                    response = new ResponseModel
                    {
                        Sucesso = false,
                        Mensagem = "Deposito não realizado: Um erro inesperado aconteceu"
                    };
                }

                ResponseModelToTempData(response);
                return RedirectToAction("Index");
            }

            return RedirectToAction("IniciarConta");
        }

        [HttpPost]
        public async Task<IActionResult> EfetuarPagamento(string valor)
        {
            if (SessaoAtiva())
            {
                ResponseModel response;
                try
                {
                    decimal valorDecimal;

                    if (decimal.TryParse(valor.Replace(".", ","), System.Globalization.NumberStyles.Currency, CultureInfo.GetCultureInfo("pt-BR"), out valorDecimal))
                    {
                        await _repository.EfetuarPagamento(_agencia, _numero, valorDecimal);
                        response = new ResponseModel
                        {
                            Sucesso = true,
                            Mensagem = $"Pagamento de R${valorDecimal.ToString("0.00")} realizado com sucesso!"
                        };
                    }
                    else
                    {
                        response = new ResponseModel
                        {
                            Sucesso = false,
                            Mensagem = $"Saque não realizado. Valor informado é inválido"
                        };
                    }
                }
                catch (MovimentacaoInvalidaException ex)
                {
                    response = new ResponseModel
                    {
                        Sucesso = false,
                        Mensagem = $"Saque não realizado: {ex.Message}"
                    };
                }
                catch (Exception)
                {
                    response = new ResponseModel
                    {
                        Sucesso = false,
                        Mensagem = "Saque não realizado: Um erro inesperado aconteceu"
                    };
                }

                ResponseModelToTempData(response);
                return RedirectToAction("Index");
            }

            return RedirectToAction("IniciarConta");

        }

        public IActionResult AlterarConta()
        {
            HttpContext.Session.Remove("agencia");
            HttpContext.Session.Remove("numero");

            return RedirectToAction("IniciarConta");
        }

        private bool SessaoAtiva()
        {
            return !string.IsNullOrEmpty(_numero) && !string.IsNullOrEmpty(_agencia);
        }

        private Conta CarregarConta()
        {
            try
            {
                return _repository.CarregarConta(_agencia, _numero, true).Result;
            }
            catch (Exception)
            {
                return new Conta(_agencia, _numero);
            }
        }

        private void ResponseModelToTempData(ResponseModel model)
        {
            TempData["ResponseModel"] = JsonConvert.SerializeObject(model);
        }

        private ResponseModel ResponseModelFromTempData()
        {
            string responseModelJson = TempData["ResponseModel"] as string;

            if (!string.IsNullOrEmpty(responseModelJson))
            {
                ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(responseModelJson);
                if (model != null)
                    return model;
            }
            
            return new ResponseModel();
        }
    }
}
