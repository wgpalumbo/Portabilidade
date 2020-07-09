using System;
using System.Collections.Generic;
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
        private readonly ISqliteRepository<Cliente> _cliente;

        public PortabilidadeDBController(ISqliteRepository<Cliente> cliente)
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
        public IActionResult Listar()
        {
            IEnumerable<Cliente> Clientes = _cliente.Listar();            
            var result = JsonConvert.SerializeObject(Clientes, Formatting.Indented);            
            return Ok(result);
        }

        //Trazer uma Especifica
        [HttpGet("v2/portabilidade/cliente/{id}")]
        public IActionResult ObterPorId(string id)
        {
            //string output = _cliente.Obter(id);
            //Cliente cliente = JsonConvert.DeserializeObject<Cliente>(output);
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