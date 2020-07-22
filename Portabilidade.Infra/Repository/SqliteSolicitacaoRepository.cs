using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Portabilidade.Domain.Entities;
using Portabilidade.Domain.Repositories;

namespace Portabilidade.Infra.Repository
{
    public sealed class SqliteSolicitacaoRepository : SqliteBaseRepository, ISqliteRepository<Solicitacao>
    {
        public void CriarTabela()
        {
            if (File.Exists(DbFile))
            {
                using (IDbConnection cnn = SimpleDbConnection())
                {
                    cnn.Open();
                    cnn.Execute(
                        @"create table if not exists solicitacao
                      (                        
                        Id             varchar(40) PRIMARY KEY,
                        Solicitacao    json not null                         
                      )");
                    cnn.Close();
                }

            }
        }

        async ValueTask<bool> ISqliteRepository<Solicitacao>.Excluir(string id)
        {
            Console.WriteLine("ID = " + id);
            bool retorno = false;
            try
            {
                using (var cnn = SimpleDbConnection())
                {
                    await cnn.OpenAsync();
                    var affectedrows = await cnn.ExecuteAsync("DELETE FROM Solicitacao WHERE Id = @Id", new { Id = id });
                    await cnn.CloseAsync();
                    retorno = (affectedrows > 0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error no JSON Excluir {e}");
            }
            return retorno;
        }

        public async ValueTask Incluir(dynamic json)
        {
            try
            {
                string jsonString = Convert.ToString(json);
                dynamic data = JObject.Parse(jsonString);
                //------------
                Console.WriteLine(data.agenteCedente.instituicao);
                Console.WriteLine(data.agenteCedente.codigoInvestidor);
                Console.WriteLine(data.agenteCessionario.instituicao);
                Console.WriteLine(data.agenteCessionario.codigoInvestidor);
                //Verificando Campo Cliente                
                Cliente cliente = new Cliente(Convert.ToString(data.cliente.nome), Convert.ToString(data.cliente.documentoCpf), Convert.ToString(data.cliente.endereco));
                Agente agenteCedente = new Agente(Convert.ToString(data.agenteCedente.instituicao), Convert.ToString(data.agenteCedente.codigoInvestidor));
                Agente agenteCessionario = new Agente(Convert.ToString(data.agenteCessionario.instituicao), Convert.ToString(data.agenteCessionario.codigoInvestidor));
                //------------
                //Verificando Campo Solicitacao
                Guid _guid = Guid.NewGuid();
                var solicitacao = new Solicitacao(_guid,
                                                              Convert.ToDateTime("01/07/2020"),
                                                              agenteCedente,
                                                              agenteCessionario,
                                                              cliente,
                                                              10);
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
                var validator = new SolicitacaoValidator();
                var validRes = validator.Validate(solicitacao);
                if (validRes.IsValid)
                {

                    string jsonSolicitacao = JsonConvert.SerializeObject(solicitacao);
                    Console.WriteLine(jsonSolicitacao);
                    var parameters = new { solicitacaoId = Convert.ToString(_guid), solicitacaoJson = Convert.ToString(jsonSolicitacao) };
                    string query = "INSERT INTO solicitacao ( Id, Solicitacao ) VALUES ( @solicitacaoId, @solicitacaoJson ) ON CONFLICT(Id) DO UPDATE SET Solicitacao=excluded.Solicitacao";
                    Console.WriteLine(@query, parameters);
                    using (var cnn = SimpleDbConnection())
                    {
                        await cnn.OpenAsync();
                        await cnn.ExecuteAsync(query, parameters);
                        await cnn.CloseAsync();
                        Console.WriteLine("Solicitacao Incluida SQLite Corretamente!");
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error no JSON Input {e}");
            }
        }

        async ValueTask<IEnumerable<Solicitacao>> ISqliteRepository<Solicitacao>.Listar()
        {
            string query = "SELECT Solicitacao FROM Solicitacao ORDER BY Id";
            using (var cnn = SimpleDbConnection())
            {
                await cnn.OpenAsync();
                var ResultadoString = await cnn.QueryAsync<string>(query);
                await cnn.CloseAsync();
                //-----------
                var Resultado = new List<Solicitacao>();
                foreach (string line in ResultadoString)
                {
                    Console.WriteLine(line);
                    Solicitacao solicitacao = JsonConvert.DeserializeObject<Solicitacao>(line);

                    var obj = JObject.Parse(line);
                    foreach (var child in obj["Ativos"].Children())
                    {
                        var codigo = child["Codigo"].ToString();
                        var tipo = child["Tipo"].ToString();
                        var quantidade = Convert.ToDecimal(child["Quantidade"].ToString());
                        Ativo ativo = new Ativo(codigo, tipo, quantidade);
                        solicitacao.AdicionarAtivo(ativo);
                    }

                    Resultado.Add(solicitacao);
                }
                //-----------                

                return Resultado;
            }
        }

        async ValueTask<Solicitacao> ISqliteRepository<Solicitacao>.Obter(string id)
        {
            Console.WriteLine("ID = " + id);

            Solicitacao solicitacao = null;

            try
            {
                using (var cnn = SimpleDbConnection())
                {
                    await cnn.OpenAsync();
                    var retorno = await cnn.QuerySingleOrDefaultAsync<string>(
                        @"SELECT Solicitacao 
                    FROM Solicitacao 
                    WHERE Id = @id", new { id });
                    await cnn.CloseAsync();

                    solicitacao = JsonConvert.DeserializeObject<Solicitacao>(retorno);

                    var obj = JObject.Parse(retorno);
                    foreach (var child in obj["Ativos"].Children())
                    {
                        var codigo = child["Codigo"].ToString();
                        var tipo = child["Tipo"].ToString();
                        var quantidade = Convert.ToDecimal(child["Quantidade"].ToString());
                        Ativo ativo = new Ativo(codigo, tipo, quantidade);
                        solicitacao.AdicionarAtivo(ativo);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" {0} exceção capturada. ", e);
            }

            return solicitacao;
        }

    }
}