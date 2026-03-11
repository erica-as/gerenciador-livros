# Configuração de Variáveis de Ambiente

## Para Desenvolvimento Local (Visual Studio)

### Opção 1: Variáveis de Ambiente do Windows

1. **Defina as variáveis no PowerShell (temporário):**
   ```powershell
   $env:DB_SERVER="(localdb)\MSSQLLocalDB"
   $env:DB_NAME="LivroDb"
   # Não precisa de DB_USER e DB_PASSWORD para LocalDB
   ```

2. **Execute a aplicação:**
   ```powershell
   dotnet run --project src/GerenciadorLivros.API
   ```

### Opção 2: Deixar como está (LocalDB automático)
Se você **NÃO** definir as variáveis `DB_USER` e `DB_PASSWORD`, a aplicação usará automaticamente o **LocalDB** do Windows.

---

## Para Docker

1. **Edite o arquivo `.env`** (já criado) com suas credenciais:
   ```env
   DB_SERVER=sql-server
   DB_NAME=LivrosDb
   DB_USER=sa
   DB_PASSWORD=SuaSenhaSegura@2024
   ```

2. **Suba os containers:**
   ```bash
   docker-compose -f src/GerenciadorLivros.API/docker-compose.yml up -d
   ```

3. **Execute as migrations:**
   ```bash
   docker exec -it api-livros dotnet ef database update
   ```

---

## Para Produção (Azure/AWS/etc)

Configure as variáveis de ambiente no painel da plataforma:

```
DB_SERVER=seu-servidor.database.windows.net
DB_NAME=LivrosDb
DB_USER=seu-usuario
DB_PASSWORD=SuaSenhaSegura@2024
ASPNETCORE_ENVIRONMENT=Production
```

## Testando a Connection String

Execute para ver qual string está sendo usada:
```bash
dotnet run --project src/GerenciadorLivros.API
```

O console mostrará se conectou no **LocalDB** ou no **SQL Server Docker**.
