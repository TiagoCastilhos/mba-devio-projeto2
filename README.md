# **DevXpert.Store - Aplicação de Gestão de Mini Loja Virtual com Angular, MVC e API RESTful**

## :trophy: **1. Apresentação**

Bem-vindo ao repositório do projeto **DevXpert.Store**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET referente ao **módulo 2 - Desenvolvimento Full-Stack Avançado com ASP.NET Core**.
O objetivo principal desenvolver uma aplicação de Gestão de Mini Loja Virtual que permite aos usuários com perfil vendedor se cadastrar e criar, editar, visualizar e excluir produtos, através de uma interface web utilizando MVC. Permite usuários com perfil cliente se cadastrar, consultar e favoritar produtos, através da interface angular e API RESTful. Permite ao usuário com perfil Admin criar, editar, visualizar e excluir categorias e também (in)ativar vendedor(es) e/ou produto(s).


### :notebook: **Autores**
---

- :white_check_mark: Chayene Freitas - @chaya3090
- :white_check_mark: Cristian Kruger Silva - @mr.krug3r
- :white_check_mark: Edson Junio Araújo dos Santos - @edsonsantos3976
- :white_check_mark: Gilberto Moshim Yabiku Junior - @junmoriyama3d
- :white_check_mark: Tiago Henrique de Castilhos - @zsfnightmare
- :white_check_mark: Victor Higaki - @victorhigaki

## :gear: **2. Proposta do Projeto**

O projeto consiste em:

- **Aplicação MVC:** Interface web para interação com a Gestão de Mini Loja Virtual (back office). Acesso exclusivo do(s) Admins e Vendedores.
- **API RESTful:** Exposição dos recursos da Gestão de Mini Loja Virtual para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **Angular:** Exposição da vitrine de Mini Loja Virtual para acesso dos clientes.
- **Autenticação e Autorização:** Implementação de controle de acesso, diferenciando administradores, vendedores e clientes (JWT para API RESTful e Cookie para o MVC).
- **Acesso a Dados:** Implementação de acesso ao banco de dados através de ORM Entity Framework Core.

## :gear: **3. Tecnologias Utilizadas**

- **Linguagem de Programação:** C# 13
- **Frameworks:**
  - ASP.NET Core MVC
  - ASP.NET Core Web API
  - Angular 19 
  - Entity Framework Core
- **Banco de Dados:** SQL Server / SQLite
- **Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
  - Cookie para autenticação no MVC
- **Front-end:**
  - Razor Pages/Views
  - HTML/CSS/bootstrap para estilização básica
  - Angular 19
- **Documentação da API:** Swagger

## :gear: **4. Estrutura do Projeto**

A estrutura do projeto é organizada da seguinte forma:

```
|-- docs
|   |-- postman                      → Coleção postman com requisições para API
|-- sql
|   |-- script.sql                   → Script idempotente do banco de dados (exclusivo SQL Server)
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
## :gear: **5. Funcionalidades Implementadas**


- API e Angular:
  - **Autenticação via ASP.NET Core Identity.**
  - **CRUD para Categorias:** Permite ao Admin autenticado criar, editar, visualizar e excluir categorias.
  - **CRUD para Produtos:** Permite ao Vendedor autenticado criar, editar, visualizar e excluir Produtos. Permite ao Admin autenticado (in)ativar um produto de um vendedor.
  - **CRUD para favoritos:** Permite ao cliente autenticado adicionar ou remover um produto à sua lista de favoritos.
  - **API RESTful:** Exposição de endpoints para operações CRUD via API.
  - **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.

- MVC:  
  - **Autenticação via ASP.NET Core Identity** para usuários com perfil Admin e Vendedor.
  - **CRUD para Categorias:** Permite ao Admin autenticado criar, editar, visualizar e excluir categorias.
  - **CRUD para Produtos:** Permite ao Vendedor autenticado criar, editar, visualizar e excluir Produtos. Permite ao Admin autenticado (in)ativar um produto de um vendedor.
  - **CRUD para Vendedores:** Permite ao Admin (in)ativar vendedores.
  

## :gear: **6. Como Executar o Projeto**

### **Pré-requisitos**

- .NET SDK 9.0 ou superior
- SQL Server / SQLite
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git
- Node 22.0.0
- Angular CLI 19

### **Passos para Execução**
---
### 1. **Clone o Repositório:**
   - `git clone https://github.com/TiagoCastilhos/mba-devio-projeto2`
   - `cd mba-devio-projeto2`

### 2. Configuração do Banco de Dados:
  
No arquivo `appsettings.json`, configure a string de conexão do SQL Server (caso deseje executar em modo não "development"). Para execução em modo "Development" (debug), basta executar o projeto (irá subir uma instancia do `SQLite`).

Execute o projeto para que a configuração do Seed crie o banco e popule com os dados básicos.

**:warning: As Migrations são aplicadas de forma automática através do método de extensão `MigrateDatabase() => src/Backend/DevXpert.Store.Core/Application/Configurations/DatabaseConfig.cs`;**<br>
**:warning: Uma carga inicial é feita na base de dados através do método `OnModelCreating() => src/Backend/DevXpert.Store.Core/Data/Context/AppDbContext.cs`, com base no método `Seed(modelBuilder) => src/Backend/DevXpert.Store.Core/Data/Seed/SeedDatabase.cs`;**<br>
**:warning: Credenciais default do banco:**
  - Usuário com perfil Admin &rarr; `admin@teste.com` | senha &rarr; `@Aa12345`<br>
  - Usuário com perfil Vendedor &rarr; `vendedor@teste.com` | senha &rarr; `@Aa12345`<br>
  - Usuário com perfil Cliente &rarr; `cliente@teste.com` | senha &rarr; `@Aa12345`<br>

### 3. **Executar o BackOffice (MVC):**
   - a partir da pasta clonada do projeto, abra o prompt de comando e digite:
   - `cd src/Backend/DevXpert.Store.Mvc/`
   - `dotnet run --environment=Development`
   - Abra o browser e acesse a aplicação em: https://localhost:7019

### 4. **Executar a Loja virtual API:**
   - a partir da pasta clonada do projeto, abra o prompt de comando e digite:
   - `cd src/Backend/DevXpert.Store.API/`
   - `dotnet run --environment=Development`
   - Abra o browser e acesse a documentação da API em: https://localhost:7094/swagger

### 5. **Executar a Loja virtual Angular:**
   - a partir da pasta clonada do projeto, abra o prompt de comando e digite:
   - `cd src/Frontend/angular`
   - `npm i`
   - `ng serve --configuration=development`
   - Abra o browser e acesse a aplicação em: http://localhost:4200/
   - **:warning: Obs.: Para execução da aplicação angular, a API deve estar em execução!**

## :gear: **7. Instruções de Configuração**

**JWT para API:** As chaves de configuração do JWT estão no arquivo `appsettings.{environment}.json`.

**Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core (Não é necessário aplicar o comando update-database devido a configuração do projeto)

## :gear: **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em através do link [https://localhost:7094/swagger](https://localhost:7094/swagger)

**:warning: Obs.: Em ambientes não `development`, é necessário informar usuario e senha para expor a página do swagger, devido à implementação do securityMiddleware. Por default, essas credenciais são `admin` e `123` e podem ser alteradas através do nó `AppCredentials` no `appsettings.[ambiente].json`**

## :white_check_mark: **9. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.

## :white_check_mark: **10. To Do List**

MVC:

- :white_check_mark: Padronização dos icones (font awesome) nos botões (adicionar, visualizar, editar, excluir e pesquisar/filtrar);
- :white_check_mark: Na tela de listagem de Categorias, implementar filtro de pesquisa pelos campos (string busca, bool? ativo);
- :white_check_mark: Na tela de listagem de vendedores, alterar o botão "atualizar" para "Ativar" ou "inativar" conforme status do vendedor; Este botão segue o padrão do excluir, porém, somente atualiza o status do vendedor - chamada do método vendedorService.AlternarStatus(vendedorId) - sugestão de levar para uma tela de confirmação de (in)ativação ao invés de simplesmente chamar a ação de (in)ativar;
- :white_check_mark: Ainda na tela de vendedor, implementar filtro de pesquisa pelos campos (string busca, bool? ativo);
- :white_check_mark: Ainda na tela de vendedor, incluir a quantidade de produtos do vendedor na listagem;
- :white_check_mark: Ainda na tela de vendedor, Alterar o checkbox para label (Ativo/Inativo);
- :white_check_mark: Na tela de produtos do vendedor, adicionar o e-mail do vendedor concatenado ao título da página; 
- :white_check_mark: Na tela de produtos do vendedor, renderizar a imagem do produto;
- :white_check_mark: O botão Ativar/Inativar produto, deve seguir a mesma lógica de Ativar/Inativar Vendedor, chamando o serviço "AlternarStatus" passando o produtoId;
- [ ] : impedir o login de um vendedor inativado. Ao inativar um vendedor, o mesmo ainda consegue logar;
- [ ] No cadastro de produtos (na visão do vendedor): ajustar o campo Preço para comportar o idioma pt-br.
- :white_check_mark: Na tela de cadastro e atualização de produto, renderizar a imagem ao selecionar o arquivo;
- [ ] na tela de listagem de produtos, renderizar a imagem;
- :white_check_mark: na tela de listagem de produtos, implementar filtro de pesquisa pelos campos (string busca, bool? ativo)

Core/API:
- :white_check_mark: Padronizar o wwwroot do MVC e Angular para ler a imagem de um unico lugar 

Angular
- :white_check_mark: Tela do produto - Arrumar o botão para favoritar;
- :white_check_mark: Tela do produto - produtos do mesmo vendedor não mostra os favoritados;
- :white_check_mark:Tela do produto - Colocar email do vendedor na tela do produto (Vendido por: abc@abc.com);
- :white_check_mark: Carrossel no produtos do mesmo vendedor: mostrar apenas 3, se o usuário queiser ver outros ele navega pelo carrossel;
