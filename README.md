# Lista de Tarefas .Net e Sql Server
Lista de tarefas simples feita em C# e banco SQL Server

###
Requisitos:
- SQL Server instalado
- Visual Studio (de preferência v.2019) instalado

Passos de instalação:

1 - Clone o repositório deste projeto para sua máquina

2 - Siga os passos das imagens para importar no SQL Server o arquivo TarefasDB.bak, 
que está na pasta Documentos do repositório

<img src="/Documentos/Restaurar 1.png" alt=""/>

<img src="/Documentos/Restaurar 2.png" alt=""/>

3 - Abra o arquivo Tarefas.sln

4 - Localize o arquivo "appsettings.json" dentro da pasta Tarefas. Edite-o colocando o nome do servidor SQL Server da sua máquina local, no trecho da chave "DefaultConnection" na parte que está escrito "SeuServidorSQLServerLocal" e salve o arquivo (Ctrl + S).

<img src="/Documentos/Editar Conexao.png" alt=""/>

5 - Selecione a depuração "Tarefas" e clique em iniciar ou pressione F5

<img src="/Documentos/Executar 1.png" alt=""/>
