# Docker — Como executar o projeto GerenciadorLivros

Este documento descreve os passos para configurar, executar e depurar a API `GerenciadorLivros` usando Docker e Docker Compose.

Pré-requisitos
- Docker e Docker Compose instalados
- .NET SDK (quando for executar `dotnet ef` localmente)

Arquivos importantes
- `.env.example` — template público (commitar)
- `.env` — arquivo local com credenciais (NÃO commitar)
- `docker-compose.yml` — definição dos serviços (na raiz)

1) Preparar variáveis de ambiente
- Copie o template: `cp .env.example .env` e preencha os valores (DB_PASSWORD, etc.).
- Variáveis esperadas: `DB_SERVER`, `DB_NAME`, `DB_USER`, `DB_PASSWORD`, `ASPNETCORE_ENVIRONMENT`.

2) Subir os containers
- Na raiz do repositório:
  - `docker-compose up -d` — sobe em background
  - `docker-compose up --build -d` — força rebuild e sobe

3) Ver logs e estado
- `docker ps -a` — listar containers
- `docker-compose logs -f` — logs consolidados
- `docker logs -f api-livros` / `docker logs -f sql-server-livros` — logs por container

4) Migrations / banco de dados
- O `Program.cs` já tenta executar `db.Database.Migrate()` na inicialização da API.
- Criar migration localmente:
  - `dotnet ef migrations add NomeDaMigration --project src/GerenciadorLivros.Infrastructure --startup-project src/GerenciadorLivros.API`
  - `dotnet ef database update --project src/GerenciadorLivros.Infrastructure --startup-project src/GerenciadorLivros.API`
- Executar migration dentro do container (alternativa): `docker exec -it api-livros dotnet ef database update`.

5) Parar e limpar
- `docker-compose down` — para e remove containers
- `docker-compose down -v` — para, remove containers e volumes (apaga dados do banco)

6) Desenvolvimento sem Docker
- `dotnet run --project src/GerenciadorLivros.API` — usa LocalDB por padrão quando `DB_USER`/`DB_PASSWORD` não estão definidos

7) Problemas comuns
- "Login failed for user 'sa'": senha incorreta ou banco não criado no volume atual. Solução: ajustar `.env`, `docker-compose down -v` e `docker-compose up --build`.
- "PendingModelChangesWarning": modelos divergentes das migrations — crie uma migration antes de rodar `database update`.
- `String or binary data would be truncated`: coluna com tamanho menor que o texto — ajustar migration/model e aplicar migration.

8) Boas práticas
- Nunca comite `.env` com senhas reais; suba somente `.env.example`.
- Versione as migrations (pasta `src/GerenciadorLivros.Infrastructure/Migrations`).

9) Comandos rápidos
```
docker-compose up -d
docker-compose up --build -d
docker-compose logs -f
docker-compose down -v
```
