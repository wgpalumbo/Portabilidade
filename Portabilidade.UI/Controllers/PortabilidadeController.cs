using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Portabilidade.Domain.Entities;
using Portabilidade.Domain.Repositories;

namespace Portabilidade.UI.Controllers
{
    public class PortabilidadeController : Controller
    {
        private readonly ISolicitacaoRepository _repositorio;

        public PortabilidadeController(ISolicitacaoRepository repositorio)
        {
            _repositorio = repositorio;
        }


        //Incluir
        [HttpPost("v1/portabilidade")]
        public IActionResult Incluir([FromBody] dynamic json)
        {
            try
            {
                string jsonString = Convert.ToString(json);
                dynamic data = JObject.Parse(jsonString);
                //---------
                //Guid codigoInternoSolicitacao = data.codigoInternoSolicitacao;
                DateTime dataTransferencia = data.dataTransferencia;
                Agente agenteCedente = new Agente(Convert.ToString(data.agenteCedente.instituicao), Convert.ToString(data.agenteCedente.codigoInvestidor));
                Agente agenteCessionario = new Agente(Convert.ToString(data.agenteCessionario.instituicao), Convert.ToString(data.agenteCessionario.codigoInvestidor));
                //------------
                //Verificando Campo Cliente
                Cliente cliente = new Cliente(Convert.ToString(data.cliente.nome), Convert.ToString(data.cliente.documentoCpf), Convert.ToString(data.cliente.endereco));
                var validatorCliente = new ClienteValidator();
                var validResCliente = validatorCliente.Validate(cliente);
                Console.WriteLine("Cliente OK? => " + validResCliente.IsValid);
                //------------ 
                int motivo = data.motivo;
                //---------
                var solicitacao = new Solicitacao(Guid.NewGuid(),
                                              dataTransferencia,
                                              agenteCedente,
                                              agenteCessionario,
                                              cliente,
                                              motivo);
                //---------
                foreach (var item in data.ativos)
                {
                    Ativo ativo = new Ativo(Convert.ToString(item.codigo), Convert.ToString(item.tipo), Convert.ToInt16(item.quantidade));
                    var validatorAtivo = new AtivoValidator();
                    var validResAtivo = validatorAtivo.Validate(ativo);
                    Console.WriteLine(item.codigo);
                    Console.WriteLine("Ativo " + item.codigo + " OK? => " + validResAtivo.IsValid);
                    if (validResAtivo.IsValid)
                    {
                        solicitacao.AdicionarAtivo(ativo);
                    }
                }
                //---------
                //Testando solicitacao
                var validatorSolicitacao = new SolicitacaoValidator();
                var validResSolicitacao = validatorSolicitacao.Validate(solicitacao);
                Console.WriteLine("Solicitacao OK? => " + validResSolicitacao.IsValid);
                if (validResSolicitacao.IsValid)
                {
                    _repositorio.Incluir(solicitacao);
                }
                Console.WriteLine("Solicitacao Incluida Corretamente!");

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error no JSON Input {e}");
            }

            return Ok();
        }

        //Alterar         
        [HttpPut("v1/portabilidade/{id}")]
        public IActionResult Alterar(Guid id, [FromBody] dynamic json)
        {
            try
            {
                string jsonString = Convert.ToString(json);
                dynamic data = JObject.Parse(jsonString);
                //---------
                Guid codigoInternoSolicitacao = data.codigoInternoSolicitacao;
                DateTime dataTransferencia = data.dataTransferencia;
                Agente agenteCedente = new Agente(Convert.ToString(data.agenteCedente.instituicao), Convert.ToString(data.agenteCedente.codigoInvestidor));
                Agente agenteCessionario = new Agente(Convert.ToString(data.agenteCessionario.instituicao), Convert.ToString(data.agenteCessionario.codigoInvestidor));
                //------------
                //Verificando Campo Cliente
                Cliente cliente = new Cliente(Convert.ToString(data.cliente.nome), Convert.ToString(data.cliente.documentoCpf), Convert.ToString(data.cliente.endereco));
                var validatorCliente = new ClienteValidator();
                var validResCliente = validatorCliente.Validate(cliente);
                Console.WriteLine("Cliente OK Alterar? => " + validResCliente.IsValid);
                //------------ 
                int motivo = data.motivo;
                //---------
                var solicitacao = new Solicitacao(id,
                                              dataTransferencia,
                                              agenteCedente,
                                              agenteCessionario,
                                              cliente,
                                              motivo);
                //---------
                foreach (var item in data.ativos)
                {
                    Ativo ativo = new Ativo(Convert.ToString(item.codigo), Convert.ToString(item.tipo), Convert.ToInt16(item.quantidade));
                    var validatorAtivo = new AtivoValidator();
                    var validResAtivo = validatorAtivo.Validate(ativo);
                    Console.WriteLine(item.codigo);
                    Console.WriteLine("Ativo " + item.codigo + " OK? => " + validResAtivo.IsValid);
                    if (validResAtivo.IsValid)
                    {
                        solicitacao.AdicionarAtivo(ativo);
                    }
                }
                //---------
                //Testando solicitacao
                var validatorSolicitacao = new SolicitacaoValidator();
                var validResSolicitacao = validatorSolicitacao.Validate(solicitacao);
                Console.WriteLine("Solicitacao OK Alterar? => " + validResSolicitacao.IsValid);
                if (validResSolicitacao.IsValid)
                {
                    if (_repositorio.Alterar(id, solicitacao))
                    {
                        Console.WriteLine("Solicitacao Alterada Corretamente!");
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error no JSON Input {e}");
            }

            return Ok();
        }

        //Listar 
        [HttpGet("v1/portabilidade")]
        public IActionResult Listar()
        {
            return Ok(_repositorio.ListarSolicitacao());
        }

        //Trazer uma Especifica
        [HttpGet("v1/portabilidade/{id}")]
        public IActionResult ObterPorId(Guid id)
        {
            var solicitacaoPedida = _repositorio.ObterPorId(id);
            if (solicitacaoPedida == null)
            {
                return NotFound();
            }

            return Ok(solicitacaoPedida);
        }

        //Excluir
        [HttpDelete("v1/portabilidade/{id}")]
        public IActionResult Remover(Guid id)
        {
            var solicitacaoPedida = _repositorio.ObterPorId(id);
            if (solicitacaoPedida == null)
            {
                return NotFound();
            }
            _repositorio.Remover(solicitacaoPedida);

            return Ok("Solicitacao Excluida !");
        }


    }
}