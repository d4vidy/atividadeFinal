# APS2 - Clean Architecture / DDD

Aplicação web desenvolvida com **C# / ASP.NET Core MVC**, **Entity Framework Core** e **PostgreSQL**, aplicando os princípios de **Clean Architecture** e **Domain-Driven Design (DDD)**.

## Tema
Sistema de **Gerenciamento de Categorias e Produtos** (relação 1:N).

## Pré-requisitos
- **.NET 9 SDK** instalado
- **PostgreSQL 12+** instalado e em execução

## Setup Rápido

### 1. Clonar e restaurar dependências
```powershell
cd c:\Users\david\atividadeFinal
dotnet restore
```

### 2. Configurar PostgreSQL e Application
Certifique-se de que o PostgreSQL está em execução na máquina local (porta 5432, usuário `postgres`).

A connection string padrão está configurada em `Presentation/appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=aps2db;Username=postgres;Password=davidy;Pooling=true;Trust Server Certificate=true"
}
```

Se seus dados de conexão ao PostgreSQL forem diferentes, atualize a connection string acima.

### 3. Aplicar Migrations
Execute as migrations para criar o banco e as tabelas:
```powershell
dotnet ef database update -p Infrastructure -s Presentation
```

### 4. Executar a Aplicação
```powershell
dotnet run --project Presentation
```

A aplicação estará disponível em: `https://localhost:5001` ou `http://localhost:5000`

## Estrutura do Projeto

```
APS2/
├── Domain/                          # Camada de Domínio
│   └── Entities/
│       ├── Categoria.cs            # Entidade Categoria (Validações + Invariantes)
│       └── Produto.cs              # Entidade Produto (FK -> Categoria)
│
├── Application/                     # Camada de Aplicação
│   ├── Interfaces/
│   │   ├── ICategoriaRepository.cs
│   │   └── IProdutoRepository.cs
│   ├── Services/
│   │   ├── CategoriaService.cs
│   │   └── ProdutoService.cs
│   ├── ViewModels/
│   │   ├── CategoriaViewModel.cs
│   │   └── ProdutoViewModel.cs
│   ├── Validations/                # Custom Validation Attributes
│   │   ├── NomeUnicoAttribute.cs  # Custom validation #1
│   │   └── PrecoMaiorQueAttribute.cs # Custom validation #2
│   └── Mappings/
│       └── MappingConfig.cs        # Mapster configuration
│
├── Infrastructure/                  # Camada de Infraestrutura
│   ├── Data/
│   │   ├── AppDbContext.cs         # EF Core DbContext
│   │   └── Migrations/             # Database migrations
│   ├── Repositories/
│   │   ├── CategoriaRepository.cs
│   │   └── ProdutoRepository.cs
│   └── Seed/
│       └── DataSeed.cs             # Dados iniciais
│
└── Presentation/                    # Camada de Apresentação
    ├── Controllers/
    │   ├── HomeController.cs
    │   ├── CategoriasController.cs  # CRUD Categorias
    │   └── ProdutosController.cs    # CRUD Produtos
    ├── Views/
    │   ├── Categorias/              # Index, Create, Edit
    │   ├── Produtos/                # Index, Create, Edit
    │   └── Home/                    # Index
    ├── wwwroot/
    │   └── css/
    │       └── site.css
    ├── Program.cs                   # Configuração de DI e startup
    └── appsettings.json             # Configurações
```

## Recursos Implementados

### ✅ Obrigatórios
1. **Estrutura em 4 camadas** — Domain, Application, Infrastructure, Presentation
2. **Relacionamento 1:N** — Categoria (1) → Produto (muitos)
3. **Mapeamento com Mapster** — Automático entre entidades e ViewModels
4. **Entity Framework Core** — ORM com PostgreSQL, migrations automáticas
5. **CRUD Completo** — Criar, listar, editar, deletar para Categoria e Produto
6. **Validações** — Data Annotations + 2 Custom Validation Attributes
7. **Busca Dinâmica com AJAX** — Sem recarregar página
8. **Injeção de Dependências (DI/IoC)** — Registradas no `Program.cs`
9. **Boas Práticas** — Código limpo, organizado, sem duplicação

### Validações Customizadas
1. **`NomeUnicoAttribute`** — Valida que nomes não contenham números consecutivos iguais
2. **`PrecoMaiorQueAttribute`** — Valida que preço está dentro de range aceitável

### Dados Iniciais (Seed)
O seed automático cria 5 categorias e 11 produtos de exemplo ao executar pela primeira vez.

## Comandos Úteis

### Criar migration novo (após alterações no modelo)
```powershell
dotnet ef migrations add NomeDaMigration -p Infrastructure -s Presentation -o Migrations
```

### Remover última migration
```powershell
dotnet ef migrations remove -p Infrastructure -s Presentation
```

### Atualizar banco para latest migration
```powershell
dotnet ef database update -p Infrastructure -s Presentation
```

### Compilar projeto
```powershell
dotnet build
```

### Rodar testes (se existirem)
```powershell
dotnet test
```

## Observações

- A senha do PostgreSQL (`davidy`) está em `appsettings.json`. Em produção, use **secrets manager** ou variáveis de ambiente.
- As views usam **Bootstrap 5** para estilo responsivo.
- O seed de dados é executado automaticamente na primeira execução da aplicação.
- Validações e erros são exibidos com alertas amigáveis (Bootstrap alerts).

## Entidades Principais

### Categoria
- **Id** (PK)
- **Nome** (string, 3-150 caracteres, obrigatório)
- **Produtos** (collection 1:N)

### Produto
- **Id** (PK)
- **Nome** (string, 3-200 caracteres, obrigatório)
- **Preco** (decimal, > 0, obrigatório)
- **CategoriaId** (FK → Categoria, obrigatório)
- **Categoria** (navigation property)

## Contribuidor
Desenvolvido como trabalho final da disciplina de Clean Architecture & DDD.

