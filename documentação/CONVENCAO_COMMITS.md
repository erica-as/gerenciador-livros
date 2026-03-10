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
