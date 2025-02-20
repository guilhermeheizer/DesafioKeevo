
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

A API do projeto backend WFKeevo foi implementado em CSharp, ASP .NET e a metodologia utilizada para implementação foi na arquitetura (metodologia) REST.
O projeto frontend FrontKeevo foi desenvolvido em CSharp Aplicativo do Windows Form.

## Banco de dados no Docker
O banco de dados Postgre esta instalado no Docker. Carregar o Docker Desktop, depois no Windows PowerShell digitar o comando abaixo que cria o banco de dados num conteiner do Docker:

docker run --name horatrabalhada -p 5455:5432 -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=password -e POSTGRES_DB=horatrabalhadadocker -d postgres

![DER - Keevo Lançamento de Horas Trabalhadas](/Imagens/DER_Keevo.png)

## Visual Studio
- Abrir o Visual Studio, carregar o projeto backend - WFKeevo
  
  Obs: No projeto Keevo no CSharp configurar a string de conexão abaixo no arquivo "appsettings.Development.json":

       "ConnectionPostgres": "Server=localhost; Port=5455; User Id=admin; Password=password; Database=horatrabalhadadocker"

- Recompilar o projeto WDKeevo
- Gerar o banco a partir da Migration
  - No menu "Ferramentas --> Console do Gerenciador de Pacotes" para carregar o console.
    Digitar o comando: update-database -verbose

    Este vai criar o banco de dados.
- No DBeaver clicar para atualizar para mostrar os bancos de dados.
  Abrir o arquivo insert_banco.sql e executar os insert das tabelas tarefa e usuario.

- Abrir o Insomnia e importar o arquivo Insomnia_2025-01-15.json para executar para testar dos endpoints.

- Executar o projeto WFKeevo debugando debugando.
  - Executar o enpoint Usuario/Login na pasta Usuario e informar:
    {
	    "login": "gama",
	    "password": "gama"
    }
    O token será gerado, pois será utilizado em todos endpoints.

  - Executar o endpoint AlteraSenhaGeral na pasta Usuario, para criptografar a senha do usuário gerando a nova senha a partir do login.
  - Todos os endpoints para as tabelas tarefa, usuario e lancto foram desenvolvidos. Vide o Swagger para conferir os detalhes.

-  Abrir outro Visual Studio, carregar o projeto frontend - FrontKeevo.
   - Recompilar a solução e depois executar debugando.
   - A primeira tela é a de login, informe em login: anamaria e senha: anamaria
   - Depois carregar a tela pricipal
   - Liberado as telas de manutenção de tarefa e usuário.





```
O projeto esta disponível no github: https://github.com/guilhermeheizer/DesafioKeevo

```


## Autor

- [@guilhermeheizer](https://www.github.com/guilhermeheizer)



