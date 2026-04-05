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

### Validação no GitHub Actions

O projeto possui dois workflows de validação:

1. **Branch Naming** (`branch-naming.yml`): Valida o nome das branches criadas/mergeadas
   - Aplica-se a PRs para `develop`, `main`, `master` e `release/*`
   - Padrão esperado: `<tipo>/<descrição-em-kebab-case>`

2. **Commitlint** (`commitlint.yml`): Valida as mensagens de commit
   - Aplica-se a PRs e pushes para `master` e `main`
   - **Merge commits são automaticamente excluídos** da validação
   - Commits feitos durante o desenvolvimento na branch devem seguir a convenção

### Merge Commits

**Nota importante:** Merge commits (criados automaticamente ao fazer merge de PRs) são **excluídos da validação** de commitlint. Isso permite que você:
- Valide commits normalmente durante o desenvolvimento
- Crie PRs sem se preocupar com a validação de merge commits
- Mergear PRs sem erros de validação

Os únicos commits que precisam estar em conformidade são aqueles feitos durante o desenvolvimento na sua branch de feature/fix.
