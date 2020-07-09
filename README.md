# Portabilidade
Exemplo de Uso de Tecnologias .Net

Este teste prático usou a conceito de DDD.

A implementação ficou apenas nos Entities (para efeito de leitura simples)<br/>
mas poderia ter criado os ValueObjects

Praticou-se o uso da Linguagem Ubiqua e Clean Code.

Respeitou as regras do SOLID<br/>(menos no Controller, Pois queria deixar claro a leitura da String JSON antes de encapsular ela em uma classe especifica)

Uso do FluentValidation como Pattern de Validação e Notificação

Não usei DataAnnotation pois as regras de Validação se focavam no Fluent.

Os TDD foram feitos tambem respeitando as validações do Fluent.

Uso do RepositoryPattern para "simular" uma persistencia.

Não usei o CQRS, pois não foi defina as regras de acesso a(aos) BD(s).

O BD foi um simulado de um List<T>, em memoria.

Instalado o SWAGGER para testes.

#### Novas Implementações

Instalado Dapper, e SQLite (ver packages.txt)

Coloque a Interface Generica ISqliteRepository (Tipo Factory) na pasta Domain

na pasta INFRA

Criei a partir da ISqliteRepository a classe real SqliteClienteRepository para tratar a tabela cliente

Coloquei o BD Sqlite na pasta DATA, (eu optei por um BD isolado que não alteraria nada na maquina cliente)

(para acompanhar pode-se entrar pelo Shell na pasta DATA e chamar sqlite3 Portabilidade.sqlite)
(pode usar   select * from cliente;  para acompanhar)

Modifiquei todos os TargetFramework para netcoreapp3.1


#### Para testar entrar no Contexto UI , e executar o Run

Laboratório:
Simulou-se um simples CRUD para inserir dados do Formulario abaixo:

![Formulario](https://github.com/wgpalumbo/Portabilidade/blob/master/solicitacao.png)

