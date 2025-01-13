Banco de dados
    Está configurado corretamente a utilização do banco de dados online.
    Utilizando SQL Server Azure online.


API Rest
    Utilizado net 9.0 para api rest. Adicionado alem de metodos basicos(REST), alguns exemplos de metodos mais complexos com DTO para comunicação(O ideal é criar uma padronização com isso).
    Utilizando JWT para autenticação do usuario e controle de permissão para acessar demais requisições.

Front
    Utilizado Angular 13


Para executar basta somente rodar a api ja que o banco esta online(Azure) e rodar o front angular e seguir os passos. 
*Caso necessário conectar na base com a connection string para verificação dos dados.

Criado controle de usuarios para Supervisor e Executor.
Ex: login: supervisor   
    senha: 123456

    login: executor1
    senha: 123456

    login: executor2
    senha: 123456

Criação dos itens do checklists via menu, dinâmicos.
Criação de Checklists para gerenciamento conforme perfil.(é possivel selecionar quais itens iram ser necessários para o checklist).

Regra de Negocio: 
    RN1 - Garantir que a execução do checklist seja feita em partes, porém após iniciada por um executor, barrar a execução de um novo executor. 
        Rn1 é possivel que o executor inicie o processo de checklist assumindo o checklist especifico. Apos isso, é possivel realizar os itens 1 a 1. 
        Quando finalizar todos o sistema ira mudar o status para 'finalizado executor' dessa forma o supervisor(tem acesso a todos os status do processo) terá acesso para reprovar ou aprovar a execução.
    RN2 - Para efetivar o checklist é necessário que o supervisor aprove ou reprove o checklist que foi executado.
        Reprovando retorno ao executor original aprovando encerra o processo.
