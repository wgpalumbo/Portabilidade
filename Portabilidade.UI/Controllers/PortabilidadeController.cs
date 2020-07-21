using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Portabilidade.Domain.Entities;
using Portabilidade.Domain.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace Portabilidade.UI.Controllers
{
    public class PortabilidadeController : Controller
    {
        private readonly ISqliteRepository<Solicitacao> _repositorio;

        public PortabilidadeController(ISqliteRepository<Solicitacao> repositorio)
        {
            _repositorio = repositorio;
            _repositorio.CriarTabela();
        }

        //Incluir
        [HttpPost("v1/portabilidade")]
        [SwaggerOperation(
            Summary = "Incluir uma nova solicitação",            
            Tags = new[] { "Nova Solicitação" }
            )]
        public async Task<IActionResult> Incluir([FromBody] dynamic json)
        {
            await _repositorio.Incluir(json);
            return Ok();
        }

        //Listar 
        [HttpGet("v1/portabilidade")]
        [SwaggerOperation(
            Summary = "Listar solicitações cadastradas",            
            Tags = new[] { "Listar Solicitações" }
            )]
        //[Produces("application/json")]
        public async Task<IActionResult> Listar()
        {
            var Todas = await _repositorio.Listar();
            string result = JsonConvert.SerializeObject(Todas, Formatting.Indented);
            return Ok(result);
        }

        //Trazer uma Especifica
        [HttpGet("v1/portabilidade/{id}")]
        [SwaggerOperation(
            Summary = "Obter uma solicitação especifica",            
            Tags = new[] { "Obter Solicitação" }
            )]
        public async Task<IActionResult> ObterPorId(string id)
        {
            string _id = System.Net.WebUtility.UrlDecode(id);
            //string output = _cliente.Obter(id);
            //Cliente cliente = JsonConvert.DeserializeObject<Cliente>(output);
            return Ok(await _repositorio.Obter(_id));
        }

        //Excluir
        [HttpDelete("v1/portabilidade/{id}")]
        [SwaggerOperation(
            Summary = "Remover uma solicitação especifica",            
            Tags = new[] { "Excluir Solicitação" }
            )]
        public async Task<IActionResult> Remover(string id)
        {
            string _id = System.Net.WebUtility.UrlDecode(id);
            bool TrueOrFalse = await _repositorio.Excluir(_id);
            return Ok(TrueOrFalse ? "Solicitacao Excluida Corretamente" : "Solicitacao Não Localizada");
        }

    }
}