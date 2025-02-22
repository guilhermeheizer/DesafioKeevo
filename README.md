
# Lançamento de Horas Trabalhas ou de Estudo

## Descrição 
O projeto permite realizar o lançamento de horas
trabalhdas ou de estudos com a finalidade de saber para cada usuário as horas lançadas para as tarefas num determinado período.

## Requisitos Funcionais
O projeto é constituido de 3 tabelas, duas para cadastrar os usuários e as tarefas e uma para a armazenar as horas trabalhdas ou de estudo dos usuários.

- Tabela de usuários: criar as funcionalidades de consulta por paginação, inclusão, alteração e exclusão.
- Tabela de tarefas: criar as funcionalidades de consulta por paginação, inclusão, alteração e exclusão.
- Tabela de lançamento de horas: criar as funcionalidades de consulta por paginação, inclusão, alteração e exclusão. Esta tabela (vide DER) se relaciona com as tabelas de usuários e tarefas.

![DER - Keevo Lançamento de Horas Trabalhadas](/Imagens/DER_Keevo.png)

## Requisitos Técnicos
- Backend: ASP .NET Core
- Frontend: Windows Form
- Banco de Dados: Postgres
- Persistência dos dados: EntityFramework Core
- Uso do Docker para o banco de dados

## Banco de dados no Docker
O banco de dados Postgre esta instalado no Docker. Carregar o Docker Desktop, depois no Windows PowerShell digitar o comando abaixo que cria o banco de dados num conteiner do Docker:

docker run --name horatrabalhada -p 5455:5432 -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=password -e POSTGRES_DB=horatrabalhadadocker -d postgres

## Como funciona do Lançamento de Horas
Preparei um script com insert das tabelas. Portanto abrir o Postgres no Docker o executar a carga das tabelas conforme "Insert_Tabelas_HoraTrabalhadaDocker.sql".

Na tabela Usuario tem o atributo "funcao" que pode ser  "Administrador", "Gerente", "Empregado" e "Operador".

Criei o usuario "gama" com a função "Administrador" e para os usuários com esta função a senha na tela de login não é checada. Os demais usuários com as outras funções, a senha é igual ao login. 

A primeira tela é a tela de login. São realizadas as consistências contra a tabela de Usuario e um token é gerado e checado toda vez que o Frontend realizar uma chamada de algum endpoint do projeto do Backend.

![Login](/Imagens/01_Login.png)

Após o login a tela principal é carregada apresentando 4 menu.
- Arquivo: Como opção sair do sistema
- Cadastro: Acessa o cadastro de tarefas
- Lançamento: Acessa o lançamento de horas nas tarefas
- Configurações: Acessa o cadastro de Usuários

Cada usuário tem uma função e esta função define o acesso aos menus. O usuário com a função "Administrador" ou "Gerente" tem acesso a todos os menus. O usuário com a função "Empregado" ou "Operador" não tem acesso ao menu "Configurações".

![Tela Pricipal](/Imagens/02_TelaPrincipal.png)

## Cadastro de Tarefas
O acesso ao cadastro de Tarefas é através  do menu "Cadastro".

As tarefas armazenadas na tabela Tarefa e ao carregar a tela são apresentados as 10 primeiras, onde pode-se consultar e navegar através dos botões "Primeira", "Anterior", "Próxima" e "Ultima".

A tarefa vai representar um serviço ou algum assunto para estudo. 

A manutenção é intuitiva, acessando através	dos botões "Novo", "Alterar" e "Excluir".

A tarefa possui o campo status:
- 1: Incluido
- 2: Executando
- 3: Finalizada
- 4: Cancelada

Somente pode lançar horas nas tarefas com status "Executando".

![Manutenção Tarefa](/Imagens/04_01_TarefaManutencao.png)
![Inclusão Tarefa](/Imagens/04_02_TarefaInclusao.png)
![Alteração Tarefa](/Imagens/04_03_TarefaAlteracao.png)
![Detalhes Tarefa](/Imagens/04_04_TarefaDetalhes.png)

## Cadastro de Usuários
O acesso ao cadastro de Usuários é através  do menu "Configurações" e somente o usuário com a função "Administrador" ou "Gerente" possuem acesso ao cadastro.

Os usuários são armazenadas na tabela Usuario e ao carregar a tela são apresentados as 10 primeiras, onde pode-se consultar e navegar através dos botões "Primeira", "Anterior", "Próxima" e "Ultima".

A manutenção é intuitiva, acessando através	dos botões "Novo", "Alterar", "Excluir" e "Nova Senha".

O botão "Nova Senha" reseta a senha, e fica igual ao login.

A opção de alterar a senha pelo próprio usuário não foi desenvolvida.

Ao cadstrar ou alterar um usuário, pode-se atribuir as funções: "Administrador", "Gerente", "Empregado" ou "Operador".

![Manutenção Usuário](/Imagens/03_01_UsuarioManutencao.png)
![Inclusão Usuário](/Imagens/03_02_UsuarioInclusao.png)
![Alteração Usuário](/Imagens/03_03_UsuarioAlteracao.png)
![Detalhes Usuário](/Imagens/03_04_UsuarioDetalhes.png)

## Lançamento de Horas
O acesso ao lançamento de horas é através  do menu "Lançamento". 

Os lançamentos de horas são armazenadas na tabela Lancto e ao carregar a tela são apresentados as 10 primeiras, onde pode-se consultar e navegar através dos botões "Primeira", "Anterior", "Próxima" e "Ultima".

Todos os usuários possuem permissão de lançar horas, mas um usuário não pode dar um lançamento de horas para outro usuário. 

Os usuários que possuem a função "Empregado" ou "Operador" somente visualizam seus lançamentos, já para as funções "Administrador" ou "Gerente" visualizam os lançamentos de todos, excluir ou finalizar uma tarefa.  

Na inclusão de um horário são realizadas as consistências:
- Tarefa não encontrada: informou-se um código de tarefa que não existe no cadastro de tarefa. Clicando no botão com "..." é carregada uma tela de pesquisa de tarefas.
- A data inicial não pode ser igual a data final: Informou-se as datas inicial e final igual dd/mm/yyyy hh:mm. Informe datas diferentes.
- A data inicial não pode ser maior a data final: Corriga a data inicial.
- O lançamento de horas tem que ser no mesmo dia: O usuário tem iniciar e finalizar uma tarefa no mesmo dia.
- Existe lançamento em aberto: Não pode incluir um lançamento de horas caso exista um em aberto, ou seja, o usuário iniciou uma tarefa mas não finalizou. Para saber qual lançamento esta em aberto, basta clicar no checkbox "Lançamento não finalizados" em azul e realizar a pesquisa.
-Existe lançamento com mesmo horário de início informado: Informe outro horário de início.
- Existe lançamento com sobreposição de horário: Se o usuário para um determinado dia tiver a sequencia 07:00 a 07:30, 07:30 a 08:00 e 10:00 a 11:30, não poderá informar o horário de 06:00 a 07:45 ou 10:30 a 10:45.

Na inclusão de uma tarefa clicando no checkbox "Inicializa com hora atual", então, a tarefa será incluída cem aberto.

Na opção de alteração, somente o código da tarefa pode ser trocado, o horário de inicio e fim não pode ser modificado, porque a consistência de sobreposição de horas é muito complexa e é mais simples excluir um horário para depois incluir um novo lançamento.

O botão "Fim Tarefa" irar finalizar a tarefa com hora atual. Esta opção agiliza o processo.

![Manutenção Lançamento](/Imagens/05_01_LanctoManutencao.png)
![Inclusão Lançamento](/Imagens/05_02_LanctoInclusao.png)
![Alteração Lançamento](/Imagens/05_03_LanctoAlteracao.png)
![Detalhes Lançamento](/Imagens/05_04_LanctoDetalhes.png)
![Consulta Tarefas](/Imagens/06_01_ConsultaTarefa.png)

# Como executar o projeto através do Visual Studio

## Visual Studio
- Abrir o Visual Studio, carregar o projeto backend - WFKeevo
  
  Obs: No projeto Keevo no CSharp configurar a string de conexão abaixo no arquivo "appsettings.Development.json":

String de conexão para acessar o banco do Docker:
       "ConnectionPostgres": "Server=localhost; Port=5455; User Id=admin; Password=password; Database=horatrabalhadadocker"

String de conexão para acessar o banco do Postgres: "ConnectionPostgres": "Server=localhost; Port=5432; User Id=postgres; Password=password; Database=horatrabalhada"

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

## Agradecimentos
Cooperaram com meu projeto e agradeço pela ajuda:

- Estevão Lemos Barbosa - https://www.linkedin.com/in/estevao-lemos-barbosa-0243a7?lipi=urn%3Ali%3Apage%3Ad_flagship3_profile_view_base_contact_details%3BHfelI78zTyiGkvlgTZZg8A%3D%3D - Agradeço imensamente por instalar e configurar o Docker Desktop com o banco de dados Postgres no meu notebook, o que foi crucial para o andamento do meu projeto.

- Marciel de Liz Santos - https://www.linkedin.com/in/marciel-de-liz-santos-29130991?lipi=urn%3Ali%3Apage%3Ad_flagship3_profile_view_base_contact_details%3BhrJXJqSfQRmDlFhRGR571Q%3D%3D - Sou extremamente grato pelo curso "Curso de Desenvolvimento de APIs com C#" que adquiri na Udemy, a partir do qual construí a base do meu projeto. Sua orientação foi essencial nesse processo.

- Lucas Gomes de Carvalho - https://www.linkedin.com/in/lucas-gomes-de-carvalho-24a75a15a?lipi=urn%3Ali%3Apage%3Ad_flagship3_profile_view_base_contact_details%3BYd0sV3Z2SISEtpwmw8qjPQ%3D%3D - Desenvolvedor em C# e colega de curso, agradeço por sua ajuda fundamental na correção de bugs do meu projeto, o que permitiu agregar qualidade ao resultado final.

- Pablo Dinella - https://www.linkedin.com/in/pablodinella?lipi=urn%3Ali%3Apage%3Ad_flagship3_profile_view_base_contact_details%3BxBGcXzveSIipcxVO9%2FDe9A%3D%3D: Gostaria de agradecer pelo vídeo em seu canal do YouTube, "Como encadear requests (com access_token) no Insomnia". Assisti a este vídeo, que foi fundamental para configurar correta e automaticamente o Insomnia, permitindo que o token do endpoint de login fosse corretamente utilizado nos demais endpoints do projeto. Confira o vídeo - https://youtu.be/KzZs1qB57wc?si=Co0ZaXScIOvEKCLe 

- Keevo Software - https://www.linkedin.com/company/keevosoftware/posts/?feedView=all A Keevo adota uma excelente prática ao solicitar que candidatos às vagas de desenvolvedor realizem um desafio técnico para avaliar suas habilidades na construção de aplicações. Esse desafio me inspirou a continuar evoluindo com meu projeto.






## Autor

- [@guilhermeheizer](https://www.github.com/guilhermeheizer)





