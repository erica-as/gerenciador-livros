# Docker - Como executar o projeto GerenciadorLivros

Este documento descreve os passos para configurar, executar e depurar a API `GerenciadorLivros` usando Docker e Docker Compose v2.

Pre-requisitos
- Docker Engine instalado e daemon em execucao
- Plugin Docker Compose v2 (`docker compose`)
- .NET SDK (quando for executar `dotnet ef` localmente)

Arquivos importantes
- `.env.example` - template publico (commitar)
- `.env` - arquivo local com credenciais (NAO commitar)
- `docker-compose.yml` - definicao dos servicos (na raiz)

1) Preparar variaveis de ambiente
- Copie o template: `cp .env.example .env` e preencha os valores (`DB_PASSWORD`, etc.).
- Variaveis esperadas: `DB_SERVER`, `DB_NAME`, `DB_USER`, `DB_PASSWORD`, `ASPNETCORE_ENVIRONMENT`.

2) Subir os containers
- Na raiz do repositorio:
  - `docker compose up -d` - sobe em background
  - `docker compose up --build -d` - forca rebuild e sobe

3) Ver logs e estado
- `docker ps -a` - listar containers
- `docker compose logs -f` - logs consolidados
- `docker logs -f api-livros` / `docker logs -f sql-server-livros` - logs por container

4) Migrations / banco de dados
- O `Program.cs` ja tenta executar `db.Database.Migrate()` na inicializacao da API.
- Criar migration localmente:
  - `dotnet ef migrations add NomeDaMigration --project src/GerenciadorLivros.Infrastructure --startup-project src/GerenciadorLivros.API`
  - `dotnet ef database update --project src/GerenciadorLivros.Infrastructure --startup-project src/GerenciadorLivros.API`
- Executar migration dentro do container (alternativa): `docker exec -it api-livros dotnet ef database update`.

5) Parar e limpar
- `docker compose down` - para e remove containers
- `docker compose down -v` - para, remove containers e volumes (apaga dados do banco)

6) Desenvolvimento sem Docker
- `dotnet run --project src/GerenciadorLivros.API` - usa LocalDB por padrao quando `DB_USER`/`DB_PASSWORD` nao estao definidos

7) Problemas comuns
- `Cannot connect to the Docker daemon at .../.docker/desktop/docker.sock` (Ubuntu):
  1. Limpe variavel de ambiente e volte para o contexto padrao:
     - `unset DOCKER_HOST`
     - `docker context use default`
  2. Inicie o daemon Linux:
     - `sudo systemctl enable --now docker`
     - `sudo systemctl status docker --no-pager`
  3. Se der erro de permissao sem `sudo`, adicione o usuario ao grupo docker:
     - `sudo usermod -aG docker $USER`
     - `newgrp docker`
- `Login failed for user 'sa'`: senha incorreta ou banco nao criado no volume atual. Solucao: ajustar `.env`, `docker compose down -v` e `docker compose up --build`.
- `PendingModelChangesWarning`: modelos divergentes das migrations - crie uma migration antes de rodar `database update`.
- `String or binary data would be truncated`: coluna com tamanho menor que o texto - ajustar migration/model e aplicar migration.

8) Boas praticas
- Nunca comite `.env` com senhas reais; suba somente `.env.example`.
- Versione as migrations (pasta `src/GerenciadorLivros.Infrastructure/Migrations`).
- No `docker-compose.yml`, remova a chave `version` para evitar warning no Compose v2.

9) Comandos rapidos
```bash
docker compose up -d
docker compose up --build -d
docker compose logs -f
docker compose down -v
```
