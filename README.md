
# 🩺 Cartão de Vacinação

O **Cartão de Vacinação** é uma API desenvolvida em **.NET 8** para gerenciar informações de pessoas, vacinas e doses aplicadas.
O objetivo é fornecer um backend limpo, escalável e organizado, utilizando boas práticas de **Clean Architecture** e **CQRS**.

---

## 📐 Arquitetura

O projeto segue o padrão **Clean Architecture**, dividido em camadas:

- **Domain** → Contém as entidades, regras de negócio e validações centrais.
- **Application** → Camada de orquestração da lógica, contendo casos de uso (CQRS com MediatR), validações (FluentValidation) e interfaces de repositórios.
- **Infrastructure** → Implementação de persistência de dados (EF Core com SQL Server), repositórios e Unit of Work.
- **API** → Camada de apresentação (ASP.NET Core minimal API / Controllers), exposição de endpoints, autenticação, Swagger e injeção de dependências.

📌 Padrões aplicados:

- **CQRS (Command and Query Responsibility Segregation)** com **MediatR**
- **FluentValidation** para validação de entradas
- **Repository + Unit of Work** para abstração da persistência
- **JWT** para autenticação e autorização
- **Swagger/OpenAPI** para documentação da API

---

## � Executando com Docker

### Pré-requisitos

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

### Inicialização Rápida

1. **Clone o repositório:**
   ```bash
   git clone <repo-url>
   cd CartaoVacina
   ```

2. **Configure as variáveis de ambiente:**
   ```bash
   # Copie o arquivo .env.example para .env (se necessário)
   # O arquivo .env já está configurado com valores padrão
   ```

3. **Inicie com PowerShell:**
   ```powershell
   .\start-docker.ps1
   ```

   **Ou manualmente:**
   ```bash
   docker-compose up --build -d
   ```

### Serviços Disponíveis

- **API:** http://localhost:5000 (Swagger na raiz)
- **SQL Server:** localhost:1433
  - Usuário: `sa`
  - Senha: `YourStrong@Passw0rd` (configurada no .env)

### Comandos Úteis

```bash
# Ver status dos containers
docker-compose ps

# Ver logs em tempo real
docker-compose logs -f

# Ver logs de um serviço específico
docker-compose logs -f api
docker-compose logs -f sqlserver

# Parar os containers
docker-compose down

# Parar e remover volumes (limpa o banco)
docker-compose down -v

# Reconstruir apenas a API
docker-compose build api
docker-compose up -d api
```

---

## �📚 Rotas Planejadas

### 👤 Pessoas

- `POST /api/pessoas` → cadastrar uma pessoa
- `GET /api/pessoas` → listar todas as pessoas
- `GET /api/pessoas/{id}` → obter pessoa por ID
- `PUT /api/pessoas/{id}` → atualizar dados da pessoa
- `DELETE /api/pessoas/{id}` → remover pessoa

### 💉 Vacinas

- `POST /api/vacinas` → cadastrar vacina
- `GET /api/vacinas` → listar todas as vacinas
- `GET /api/vacinas/{id}` → obter vacina por ID
- `PUT /api/vacinas/{id}` → atualizar vacina
- `DELETE /api/vacinas/{id}` → remover vacina

### 📋 Cartão de Vacinação

- `POST /api/cartoes` → criar cartão de vacinação para uma pessoa
- `GET /api/cartoes/{pessoaId}` → listar vacinas aplicadas de uma pessoa
- `POST /api/cartoes/{pessoaId}/aplicar` → registrar dose aplicada
- `DELETE /api/cartoes/{pessoaId}/remover/{doseId}` → remover registro de dose

---

## 🚀 Tecnologias

- [.NET 8](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://learn.microsoft.com/ef/core/) + [SQL Server](https://www.microsoft.com/sql-server)
- [MediatR](https://github.com/jbogard/MediatR)
- [FluentValidation](https://fluentvalidation.net/)
- [Swagger / Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- [Docker](https://www.docker.com/) + [Docker Compose](https://docs.docker.com/compose/)

---

## 📌 Próximos Passos

- Implementar autenticação JWT
- Configurar migrations automáticas do EF Core
- Adicionar testes unitários e de integração
- Criar CI/CD simples (GitHub Actions)

---
