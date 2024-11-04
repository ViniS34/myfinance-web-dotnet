
using Microsoft.AspNetCore.Mvc;
using myfinance_web_dotnet.Models;
using myfinance_web_dotnet_domain.Entities;
using myfinance_web_dotnet_service.Interfaces;

namespace myfinance_web_dotnet.Controllers
{
    [Route("[controller]")]
    public class TransacaoController : Controller
    {
        private readonly ILogger<TransacaoController> _logger;
        private readonly ITransacaoService _transacaoService;

        public TransacaoController(ILogger<TransacaoController> logger, ITransacaoService transacaoService)
        {
            _logger = logger;
            _transacaoService = transacaoService;
        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            var listaTransacoes = _transacaoService.ListarRegistros();
            List<TransacaoModel> listaTransacaoModel = new List<TransacaoModel>();

            foreach (var item in listaTransacoes)
            {
                var transacaoModel = new TransacaoModel() {
                    Id = item.Id,
                    Historico = item.Historico,
                    Data = item.Data,
                    Valor = item.Valor,
                    Tipo = item.PlanoConta.Tipo,
                    Id_Plano_Conta = item.Id_Plano_Conta
                };

                listaTransacaoModel.Add(transacaoModel);
            }

            ViewBag.ListaTransacao = listaTransacaoModel;
            
            return View();
        }

        [HttpGet]
        [Route("Cadastrar")]
        [Route("Cadastrar/{Id}")]
        public IActionResult Cadastrar(int? id)
        {
            if (id != null)
            {
                var transacao = _transacaoService.RetornarRegistro((int)id);
                var transacaoModel = new TransacaoModel() {
                    Id = transacao.Id,
                    Historico = transacao.Historico,
                    Data = transacao.Data,
                    Valor = transacao.Valor,
                    Id_Plano_Conta = transacao.Id_Plano_Conta
                };

                return View(transacaoModel);
            }
            return View();
        }

        [HttpPost]
        [Route("Cadastrar")]
        [Route("Cadastrar/{Id}")]
        public IActionResult Cadastrar(TransacaoModel model)
        {
            var transacao = new Transacao()
            {
                Id = model.Id,
                Historico = model.Historico,
                Data = model.Data,
                Valor = model.Valor,
                Id_Plano_Conta = model.Id_Plano_Conta
            };
            
            _transacaoService.Cadastrar(transacao);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Excluir/{Id}")]
        public IActionResult Excluir(int? Id)    
        {
            _transacaoService.Excluir((int)Id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}