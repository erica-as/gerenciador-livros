# Gerenciador de Livros

API REST para gerenciamento de livros, desenvolvida com .NET.

## Convenção de Commits

Este projeto utiliza o [Conventional Commits](https://www.conventionalcommits.org/) para padronizar as mensagens de commit. O padrão é validado automaticamente pelo [commitlint](https://commitlint.js.org/) com o hook do [Husky](https://typicode.github.io/husky/).

### Formato

```
<tipo>(<escopo opcional>): <descrição>
```

**Exemplos:**
```
feat: adicionar endpoint de criação de livro
fix: corrigir validação do campo autor
docs: atualizar README com instruções de instalação
chore: atualizar dependências do projeto
```

### Tipos Aceitos

| Tipo       | Descrição                                              |
|------------|--------------------------------------------------------|
| `feat`     | Nova funcionalidade                                    |
| `fix`      | Correção de bug                                        |
| `hotfix`   | Correção urgente em produção                           |
| `chore`    | Tarefas de manutenção, configurações, dependências     |
| `docs`     | Alterações na documentação                             |
| `test`     | Adição ou correção de testes                           |
| `refactor` | Refatoração de código (sem nova funcionalidade ou fix) |
| `style`    | Formatação, ponto e vírgula, etc. (sem mudança lógica) |
| `ci`       | Alterações em configurações de CI/CD                   |
| `perf`     | Melhorias de performance                               |
| `revert`   | Reversão de um commit anterior                         |

### Configuração Local

Após clonar o repositório, instale as dependências para ativar a validação automática dos commits:

```bash
npm install
```

O hook `commit-msg` será configurado automaticamente pelo Husky e validará cada commit antes de ser registrado.

## Convenção de Branches

Este projeto adota um padrão para nomeação de branches, validado automaticamente pelo Husky (`pre-commit`) e pelo GitHub Actions.

### Formato

```
<tipo>/<descrição-em-kebab-case>
```

**Exemplos:**
```
feat/adicionar-endpoint-livro
fix/corrigir-validacao-autor
docs/atualizar-readme
hotfix/corrigir-bug-critico
```

### Tipos Aceitos

| Tipo        | Descrição                                              |
|-------------|--------------------------------------------------------|
| `feat`      | Nova funcionalidade                                    |
| `fix`       | Correção de bug                                        |
| `hotfix`    | Correção urgente em produção                           |
| `chore`     | Tarefas de manutenção, configurações, dependências     |
| `docs`      | Alterações na documentação                             |
| `test`      | Adição ou correção de testes                           |
| `refactor`  | Refatoração de código (sem nova funcionalidade ou fix) |
| `style`     | Formatação, ponto e vírgula, etc. (sem mudança lógica) |
| `ci`        | Alterações em configurações de CI/CD                   |
| `perf`      | Melhorias de performance                               |
| `revert`    | Reversão de um commit anterior                         |
| `release`   | Preparação de uma nova versão                          |

### Regras

- A descrição deve estar em **kebab-case** (letras minúsculas separadas por hífen)
- As branches `main` e `master` são protegidas e não passam por validação
- O hook `pre-commit` validará o nome da branch a cada commit local
