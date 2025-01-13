
# Desafio Keevo 

## Descrição do Desafio
Crie uma aplicação web para gerenciar uma Lista de Tarefas. A aplicação deve consistir em um frontend interativo e responsivo.

## Requisitos Funcionais
- Cadastrar uma nova tarefa
- Editar uma tarefa da lista
- Remover uma tarefa da lista
- A tarefa deve possuir status que indiquem sua situação
- Visualizar a lista de tarefas
- Filtrar as tarefas por status

## Requisitos Técnicos
- Backend: .NET Core
- Frontend: Angular
- Banco de Dados: Postgres
- Persistência dos dados: utilize como ORM o - EntityFramework Core

## Critérios de Avaliação por parte da Keevo
- Atendimento aos requisitos técnicos e - funcionais
- Tratamento de erros
- Código limpo e organizado
- Modelagem de dados
- Usabilidade, design responsivo e - experiência do usuário interagindo com a interface
- Documentar o projeto no arquivo README.md fornecendo informações sobre o projeto e instruções para executá-lo
- Mensagens de commit bem descritas

## Pontos Extras
- Documentação da API (Swagger)
- Uso do Docker
- Funcionalidades extras

# Projeto WFKeevo

A API do projeto WFKeevo foi implementado em CSharp, ASP .NET e a metodologia utilizada para implementação foi na arquitetura (metodologia) REST.

O banco de dados Postgre esta instalado no Docker, o conteiner é horatrabalhada.

Carregar o Docker Desktop, depois no Windows PowerShell digitar o comando abaixo que cria o banco de dados num conteiner do Docker:

docker run --name horatrabalhada -p 5455:5432 -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=password -e POSTGRES_DB=horatrabalhadadocker -d postgres

No projeto Keevo no CSharp configurar a string de conexão abaixo no arquivo "appsettings.Development.json":

"ConnectionPostgres": "Server=localhost; Port=5455; User Id=admin; Password=password; Database=horatrabalhadadocker"


![DER - Keevo Lançamento de Horas Trabalhadas](/Imagens/DER_Keevo.png)

```
O projeto esta disponível no github: https://github.com/guilhermeheizer/DesafioKeevo

```


## Autor

- [@guilhermeheizer](https://www.github.com/guilhermeheizer)



