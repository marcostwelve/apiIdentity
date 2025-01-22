# API Identity


API de contatos, é um projeto com finalidade de aprender a utilização dos recursos do Asp.Net Core Identity.

![image](https://github.com/user-attachments/assets/09d6c25a-8edb-4337-aadf-5c631d0115a1)



## 🔥 Introdução

API foi criada com os métodos Http, com todos os endpoints do Http: Get, Post, Put, Delete.
Para realizar todas as operações, será necessário registrar e autenticar o um novo usuário.

### ⚙️ Pré-requisitos
* .Net Core versão 6.0 [.Net Core 6.0 Download](https://dotnet.microsoft.com/pt-br/download/dotnet/6.0)
* Entity Framework Core versão 6.0 [Documentação](https://learn.microsoft.com/pt-br/ef/)
* Visual studio 2022, ou IDE que tenha suporte ao .Net 6.0 [Visual Studio 2022 Download](https://visualstudio.microsoft.com/pt-br/downloads/)
* Sql Server versão 2022 [Sql Server Download](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
* Sql Server Management Studio (SSMS) [SSMS Download](https://learn.microsoft.com/pt-br/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
* Swagger [Documentação](https://swagger.io/)


### 🔨 Guia de instalação

Para utilizar este projeto, necessário instalar o Entity Framework, e configurar o banco de dados no arquivo appsettings.Development.json, e instalar as migrations para conexão com o banco de dados

Etapas para instalar:

```bash
dotnet tool install --global dotnet-ef
```
Passo 2:
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```
Passo 3:
```bash
Install-Package Microsoft.EntityFrameworkCore.Design
```
Passo 4:
```bash
dotnet-ef migrations add (Nome da migration do projeto)
```

Passo 5:
```bash
dotnet-ef database update
```


## 🛠️ Executando os testes (caso tenha testes)

Para executar o projeto, para testes. Digite o seguinte comando no terminal do Visual Studio

```bash
dotnet watch run
```
## Autenticação ✒️
Para realizar a autenticação, será necessário acessar o endpoint "api/user/registrar", para registrar um novo usuário
![image](https://github.com/user-attachments/assets/67b5ab2e-01dc-4555-9b09-26893e3d3451)



### Login 💻
Para realizar o login para obter o token de acesso, o endpoint "api/user/login", inserindo o dados registrados na api
![image](https://github.com/user-attachments/assets/b4ae4c1b-f2a0-4ada-a1cd-2ed2b5867d11)

