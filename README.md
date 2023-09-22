# IWantApp

## Finalidade

O programa é um aplicativo web ASP.NET Core destinado a fornecer uma API RESTful para gerenciamento de usuários, produtos, clientes e pedidos. Ele implementa autenticação via JWT, autorização com políticas personalizadas, e logging com Serilog.

## Funcionalidades Principais

1. **Gerenciamento de Usuários**: Criação e autenticação de usuários com JWT.
2. **Gerenciamento de Produtos**: CRUD para produtos.
3. **Gerenciamento de Clientes**: CRUD para clientes.
4. **Gerenciamento de Pedidos**: Criação e listagem de pedidos.
5. **Autorização**: Políticas de autorização baseadas em claims.
6. **Logging**: Registra logs no console e no SQL Server.
7. **Documentação da API**: Swagger para documentação da API.

## Tecnologias Utilizadas

- ASP.NET Core
- Entity Framework
- JWT para autenticação
- Serilog para logging
- SQL Server para armazenamento de dados
- Swagger para documentação da API

## Como Rodar

1. Configure a string de conexão para o SQL Server em `appsettings.json`.
2. Execute o comando `dotnet run` para iniciar o aplicativo.

## Endpoints

- `/token`: Autenticação de usuários.
- `/products`: CRUD de produtos.
- `/clients`: CRUD de clientes.
- `/orders`: Criação e listagem de pedidos.
  
## Tratamento de Erros

O aplicativo possui um manipulador de erros global que captura exceções e retorna respostas apropriadas.
