## Funcionalidade 30%

Avalie se o projeto atende a todos os requisitos funcionais definidos.
* Posso ativar ou desativar vendedores pela lista, mas não o mesmo com categorias ou "meus produtos" (vendedor logado).
* Demais funcionalidades atendidas.

## Qualidade do Código 20%

Considere clareza, organização e uso de padrões de codificação.

### Core
* O projeto `Core` poderia ser separado entre projeto persistência e projeto de regra de negócio.

### Core.Data
* Repositórios estão fazendo _dispose_ do contexto. O contexto é gerenciado por _Dependency Injection_ e não deve ser descartado manualmente.
* A abstração `Repository<T>` possui métodos públicos que permitem acesso direto ao contexto, evitando as classes especializadas.
* Os repositórios especializados extendem de `Repository<T>` mas não fazem uso dos métodos base.

### Core.Business 
* Em `BaseEntity` e `PessoaBase`, use métodos abstratos ao invés de virtuais para `IsValid()`.
```csharp
// Evite
public virtual bool IsValid() => throw new NotImplementedException();
// Adote
public abstract bool IsValid();
```
* Em `AuthService`, possui dependência em `ClienteService` e `VendedorService`. é ruim ter dependência cruzada entre serviços do mesmo nível (`Business`). Considere acessar diretamente os repositórios.
* Em `CategoriaService`, o método `Delete` não verifica se a categoria existe antes de tentar remover. Isso quebra sua idempotência.
* Em `ClientService` faça uso de sobrecarga de métodos.
```csharp
// Ao invés de 
Task<Cliente> BuscarPorId(Guid id);
Task<Cliente> BuscarPorEmail(string email);

// Use
Task<Cliente> Buscar(Guid id);
Task<Cliente> Buscar(string email);
```
* Em `FavoritoService.Existe()` traz dados do banco desnecessariamente. Um recurso específic do repositório aqui é necessário.

### API
* Em `CategoriasController`, o método `Salvar()` retorna "Categoria não encontrada" se `DBConcurrencyException` for lançada. Isso não é verdade, a exceção indica que houve conflito de concorrência na atualização.
* Em `ProdutosController`, o método `GetById()` possui regras de negócio que deveriam estar no serviço.
* Evitar uso de "magic string" para as _Roles_. Considere usar constantes ou enumerações.

### MVC
* Em `CategoriasController`, o método `DeleteConfirmed()` traz dados do banco desnecessariamente. 
* Não é claro a necessidade do serviço `CategoriaHelperService`.
* Em `ProdutosController`, existe dependencia em `CategoriaService` mas não é usado.

### Geral
* Remover códigos não utilizados.
* Remover `usings` não utilizados.
* Remover campos não utilizados.
* Evitar comentários desnecessários.
* Evitar uso de `Region`.
* Escolher idioma único (PT-BR ou EN-US) para nomes de classes, métodos e variáveis.
* Use o `Dispose()` para liberar recursos não gerenciados, não para limpar referências ou manipular o _Garbage Collector_.

## Eficiência e Desempenho 20%

Avalie o desempenho e a eficiência das soluções implementadas.



## Inovação e Diferenciais 10%

Considere a criatividade e inovação na solução proposta.
* Bom ver endidades comportamentais.

## Documentação e Organização 10%

Verifique a qualidade e completude da documentação, incluindo README.md.
* Em "Configuração do Banco de Dados", menciona `appsettings.json` mas não diz de qual projeto.


## Resolução de Feedbacks 10%

Avalie a resolução dos problemas apontados na primeira avaliação de frontend

## Notas

| Critério                     | Peso | Nota | Nota Ponderada |
|------------------------------|------|-----:|---------------:|
| Funcionalidade               | 30%  |    9 |            2.7 |
| Qualidade do Código          | 20%  |    8 |            1.6 |
| Eficiência e Desempenho      | 20%  |    9 |            1.8 |
| Inovação e Diferenciais      | 10%  |    9 |            0.9 |
| Documentação e Organização   | 10%  |    9 |            0.9 |
| Resolução de Feedbacks       | 10%  |   10 |            1.0 |
| **Total**                    |      |      |        **8.9** |
