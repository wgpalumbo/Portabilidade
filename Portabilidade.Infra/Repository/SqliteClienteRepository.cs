using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json.Linq;
using Portabilidade.Domain.Entities;
using Portabilidade.Domain.Repositories;
using Portabilidade.Service.Util;

namespace Portabilidade.Infra.Repository
{
    public sealed class SqliteClienteRepository : SqliteBaseRepository, ISqliteRepository<Cliente>
    {

        public void CriarTabela()
        {
            if (File.Exists(DbFile))
            {
                using (IDbConnection cnn = SimpleDbConnection())
                {
                    cnn.Open();
                    cnn.Execute(
                        @"create table if not exists cliente
                      (
                        Nome           varchar(100) not null,
                        DocumentoCPF   varchar(20) PRIMARY KEY,
                        Endereco       varchar(100) not null                         
                      )");
                    cnn.Close();
                }

            }

        }
        public async ValueTask Incluir(dynamic json)
        {
            try
            {
                string jsonString = Convert.ToString(json);
                Console.WriteLine(jsonString);
                dynamic data = JObject.Parse(jsonString);
                //------------
                //Verificando Campo Cliente                
                Cliente cliente = new Cliente(Convert.ToString(data.cliente.nome), Convert.ToString(data.cliente.documentoCpf), Convert.ToString(data.cliente.endereco));
                var validatorCliente = new ClienteValidator();
                var validResCliente = validatorCliente.Validate(cliente);
                //cd . Console.WriteLine("CPF Cliente OK SQLite? => " + (new CpfValidador(cliente.DocumentoCpf)).EstaValido());
                Console.WriteLine("Cliente OK SQLite? => " + validResCliente.IsValid);
                Console.WriteLine(cliente.Nome);
                if (validResCliente.IsValid)
                {
                    var parameters = new { clienteNome = cliente.Nome, clienteDocumentoCPF = cliente.DocumentoCpf, clienteEndereco = cliente.Endereco };
                    string query = "INSERT INTO cliente ( Nome, DocumentoCPF, Endereco ) VALUES ( @clienteNome, @clienteDocumentoCPF, @clienteEndereco ) ON CONFLICT(DocumentoCPF) DO UPDATE SET Nome=excluded.Nome,Endereco=excluded.Endereco";
                    Console.WriteLine(@query, parameters);
                    using (var cnn = SimpleDbConnection())
                    {
                        await cnn.OpenAsync();
                        await cnn.ExecuteAsync(query, parameters);
                        await cnn.CloseAsync();
                        Console.WriteLine("Cliente Incluido SQLite Corretamente!");
                    }
                }
                else
                {
                    Console.WriteLine("Cliente Com Dados Incorretos!");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error no JSON Input {e}");
            }

        }

        async ValueTask<Cliente> ISqliteRepository<Cliente>.Obter(string id)
        {
            Console.WriteLine("ID = " + id);

            //Cliente retorno = null;

            using (var cnn = SimpleDbConnection())
            {
                await cnn.OpenAsync();
                var retorno = await cnn.QueryAsync<Cliente>(
                    @"SELECT Nome, DocumentoCPF, Endereco
                    FROM cliente 
                    WHERE DocumentoCPF = @id", new { id });
                await cnn.CloseAsync();
                return retorno.FirstOrDefault();
            }

            //return retorno;
        }

        async ValueTask<bool> ISqliteRepository<Cliente>.Excluir(string id)
        {
            Console.WriteLine("ID = " + id);
            bool retorno = false;
            try
            {
                using (var cnn = SimpleDbConnection())
                {
                    await cnn.OpenAsync();
                    var affectedrows = await cnn.ExecuteAsync("DELETE FROM cliente WHERE DocumentoCPF = @Id", new { Id = id });
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

        async ValueTask<IEnumerable<Cliente>> ISqliteRepository<Cliente>.Listar()
        {
            string query = "SELECT * FROM cliente ORDER BY Nome";
            using (var cnn = SimpleDbConnection())
            {
                await cnn.OpenAsync();
                var Clientes = await cnn.QueryAsync<Cliente>(query);
                await cnn.CloseAsync();

                return Clientes;
            }
        }


    }
}