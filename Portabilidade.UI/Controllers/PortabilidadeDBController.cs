using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Portabilidade.Domain.Entities;
using Portabilidade.Domain.Repositories;

namespace Portabilidade.UI.Controllers
{
    public class PortabilidadeDBController : Controller
    {
        private readonly ISqliteRepository _cliente;

        public PortabilidadeDBController(ISqliteRepository cliente)
        {
            _cliente = cliente;
        }

        //Incluir e Alterar
        [HttpPost("v2/portabilidade/cliente")]
        public async Task<IActionResult> Incluir([FromBody] dynamic json)
        {
            _cliente.CriarTabela();
            await _cliente.Incluir(json);
            return Ok();
        }

        //Listar 
        [HttpGet("v2/portabilidade/cliente")]
        public async Task<IActionResult> Listar()
        {
            string newPasta = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\Portabilidade.Infra\Data\Portabilidade.sqlite"));
            Console.WriteLine(newPasta);
            Console.WriteLine(System.IO.File.Exists(newPasta));
            _cliente.CriarTabela();
            return Ok();
        }

        //Trazer uma Especifica
        [HttpGet("v2/portabilidade/cliente/{id}")]
        public IActionResult ObterPorId(string id)
        {
            string output = _cliente.Obter(id);
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(output);
            return Ok(_cliente.Obter(id));
        }

        //Excluir
        [HttpDelete("v2/portabilidade/cliente/{id}")]
        public IActionResult Remover(string id)
        {
            return Ok((_cliente.Excluir(id) ? "Cliente Excluido Corretamente" : "Cliente Não Localizado"));
        }

    }
}