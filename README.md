# **XpertStore - Aplicação de Gestão de Mini Loja Virtual com Angular, MVC e API RESTful**

## **1. Apresentação**
---
Bem-vindo ao repositório do projeto **DevXpert.Store**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao **módulo 2 - Desenvolvimento Full-Stack Avançado com ASP.NET Core**.
O objetivo principal desenvolver uma aplicação de Gestão de Mini Loja Virtual que permite aos usuários com perfil vendedor criar, editar, visualizar e excluir produtos, tanto através de uma interface web utilizando MVC quanto através de uma API RESTful. Permite aos clientes se cadastrarem e favoritarem produtos, através da interface angular. Permite aos Admins criar, editar, visualizar e excluir categorias e também inativar um vendedor/cliente e/ou um produto.


### :notebook: **Autores**
---
:white_check_mark: Chayene Freitas - @chaya3090
:white_check_mark: Cristian Kruger Silva - @mr.krug3r
:white_check_mark: Edson Junio Araújo dos Santos - @edsonsantos3976
:white_check_mark: Gilberto Moshim Yabiku Junior - @junmoriyama3d
:white_check_mark: Tiago Henrique de Castilhos - @zsfnightmare
:white_check_mark: Victor Higaki - @victorhigaki

## :notebook: **2. Proposta do Projeto**
---
O projeto consiste em:

- **Aplicação MVC:** Interface web para interação com a Gestão de Mini Loja Virtual (back office). Acesso exclusivo do(s) Admins e Vendedores.
- **API RESTful:** Exposição dos recursos da Gestão de Mini Loja Virtual para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **Angular:** Exposição da vitrine de Mini Loja Virtual para acesso dos clientes.
- **Autenticação e Autorização:** Implementação de controle de acesso, diferenciando administradores e usuários comuns.
- **Acesso a Dados:** Implementação de acesso ao banco de dados através de ORM.

## :notebook: **3. Tecnologias Utilizadas**
---
- **Linguagem de Programação:** C#
- **Frameworks:**
  - ASP.NET Core MVC
  - ASP.NET Core Web API
  - Angular 
  - Entity Framework Core
- **Banco de Dados:** SQL Server / SQLite
- **Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- **Front-end:**
  - Razor Pages/Views
  - HTML/CSS para estilização básica
  - Angular
- **Documentação da API:** Swagger

## :notebook: **4. Estrutura do Projeto**
---
A estrutura do projeto é organizada da seguinte forma:

```
|-- docs
|   |-- postman                  → Coleção postman com requisições para API
|-- sql
|   |-- script.sql   → Script idempotente do banco de dados (exclusivo SQL Server)
|-- src
|   |-- Backend
|   |   |-- DevXpert.Store.Api       → API RESTful.
|   |   |-- DevXpert.Store.MVC       → Projeto MVC - back office.
|   |   |-- DevXpert.Store.Core
|   |       |-- Application          → Configuração de ViewModels consumidas pela API e MVC, Configurações communs aos projetos MVC e API e mapeamento entre ViewModels e Models.
|   |       |-- Business             → Mapeamento das entidades, Aplicação de validações e regras de negócio seguindo as boas práticas do SOLID.
|   |       |-- Data                 → Mapeamento de Modelos de Dados, Configuração do EF Core e Seed do banco de dados (/Seed/SeedDatabase.cs).
|   |-- Frontend
|           |-- Angular              → Projeto Angular - Vitrine para acesso dos clientes.
|-- .gitignore                       → Confguração de quais arquivos o Git não deve versionar.
|-- FEEDBACK.md                      → Arquivo para Consolidação dos Feedbacks
|-- DevXpert.Store.sln               → solution do projeto
|-- README.md                        → Arquivo de Documentação/Wiki do Projeto
```
## :notebook: **5. Funcionalidades Implementadas**
---

- **CRUD para Categorias:** Permite ao Admin criar, editar, visualizar e excluir categorias.
- **CRUD para Produtos:** Permite ao Vendedor criar, editar, visualizar e excluir Produtos. Permite ao Admin inativar um produto de um vendedor.
- **CRUD para Vendedores e Clientes:** Permite ao Vendedor e Cliente se cadastrar. Permite ao Admin inativar demais usuários.
- **Autenticação e Autorização:** Diferenciação entre Vendedores, clientes e administradores.
- **API RESTful:** Exposição de endpoints para operações CRUD via API.
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.

## :gear: **6. Como Executar o Projeto**
---
### **Pré-requisitos**

- .NET SDK 9.0 ou superior
- SQL Server / SQLite
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

### **Passos para Execução**
---
### 1. **Clone o Repositório:**
   - `git clone https://github.com/TiagoCastilhos/mba-devio-projeto2`
   - `cd XpertStore`

### 2. Configuração do Banco de Dados:
  
No arquivo `appsettings.json`, configure a string de conexão do SQL Server (caso deseje executar em modo não "development"). Para execução em modo "Development" (debug), basta executar o projeto (irá subir uma instancia do `SQLite`).

Execute o projeto para que a configuração do Seed crie o banco e popule com os dados básicos.

**:warning: As Migrations são aplicadas de forma automática através do método de extensão `MigrateDatabase() => src/Backend/DevXpert.Store.Core/Application/Configurations/DatabaseConfig.cs`;**<br>
**:warning: Uma carga inicial é feita na base de dados através do método `OnModelCreating() => src/Backend/DevXpert.Store.Core/Data/Context/AppDbContext.cs`, com base no método `Seed(modelBuilder) => src/Backend/DevXpert.Store.Core/Data/Seed/SeedDatabase.cs`;**<br>
**:warning: Credenciais default do banco: usuário &rarr; `teste@teste.com` | senha &rarr; `@Aa12345`**<br>


### 3. **Executar a Aplicação MVC:**
   - `cd src/Backend/DevXpert.Store.Mvc/`
   - `dotnet run --environment=Development`
   - Acesse a aplicação em: http://localhost:7019

### 4. **Executar a API:**
   - `cd src/Backend/DevXpert.Store.Mvc/`
   - `dotnet run --environment=Development`
   - Acesse a documentação da API em: http://localhost:7094/swagger

## :gear: **7. Instruções de Configuração**

**JWT para API:** As chaves de configuração do JWT estão no appsettings.json.

**Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core (Não é necessário aplicar o comando update-database devido a configuração do projeto)

## :gear: **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em através do link [https://localhost:7094/swagger](https://localhost:7094/swagger)

**:warning: Obs.: Em ambientes não `development`, é necessário informar usuario e senha para expor a página do swagger, devido à implementação do securityMiddleware. Por default, essas credenciais são `admin` e `123` e podem ser alteradas através do nó `AppCredentials` no `appsettings.[ambiente].json`**

## :white_check_mark: **9. Avaliação**
---
- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.

## :white_check_mark: **10. To Do List**
---

:white_check_mark: implementar entidade cliente (relacionamento com produtos/favoritos);
:white_check_mark: implementar identity na camada MVC;
:white_check_mark: implementar AuthController, VendedorController, ClienteController e ProdutoController;
:white_check_mark: implementar Migrations e rever seed;
:white_check_mark: implementar as Roles ou Claims;
