using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portabilidade.Domain.Entities;
using Portabilidade.Domain.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Portabilidade.UI.Controllers
{
    public class PortabilidadeDBController : Controller
    {
        private readonly ISqliteRepository<Cliente> _cliente;

        public PortabilidadeDBController(ISqliteRepository<Cliente> cliente)
        {
            _cliente = cliente;
        }

        //Incluir e Alterar
        [HttpPost("v2/portabilidade/cliente")]
        [SwaggerOperation(
            Summary = "Incluir uma novo cliente",
            Tags = new[] { "Nova Cliente" }
            )]
        public async Task<IActionResult> Incluir([FromBody] dynamic json)
        {
            _cliente.CriarTabela();
            await _cliente.Incluir(json);
            return Ok();
        }

        //Listar 
        [HttpGet("v2/portabilidade/cliente")]
        [SwaggerOperation(
            Summary = "Listar clientes cadastrados",
            Tags = new[] { "Listar Clientes" }
            )]
        public async Task<IActionResult> Listar()
        {
            var Clientes = await _cliente.Listar();
            string result = JsonConvert.SerializeObject(Clientes, Formatting.Indented);
            return Ok(result);
        }

        //Trazer uma Especifica
        [HttpGet("v2/portabilidade/cliente/{id}")]
        [SwaggerOperation(
            Summary = "Obter um cliente especifico",
            Tags = new[] { "Obter Cliente" }
            )]
        public async Task<IActionResult> ObterPorId(string id)
        {
            string _id = System.Net.WebUtility.UrlDecode(id);
            //string output = _cliente.Obter(id);
            //Cliente cliente = JsonConvert.DeserializeObject<Cliente>(output);
            return Ok(await _cliente.Obter(_id));
        }

        //Excluir
        [HttpDelete("v2/portabilidade/cliente/{id}")]
        [SwaggerOperation(
            Summary = "Remover um cliente especifico",
            Tags = new[] { "Excluir Cliente" }
            )]
        public async Task<IActionResult> Remover(string id)
        {
            string _id = System.Net.WebUtility.UrlDecode(id);
            bool TrueOrFalse = await _cliente.Excluir(_id);
            return Ok(TrueOrFalse ? "Cliente Excluido Corretamente" : "Cliente Não Localizado");
        }

    }
}