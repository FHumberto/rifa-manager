# Cronograma do Front-end Angular — Rifa Manager

Este documento define um cronograma de alto nível para implementar o front-end Angular do projeto **Rifa Manager**, consumindo a API já existente.

O objetivo principal é aprender Angular na prática, sem tentar fazer tudo de uma vez. Por isso, o front-end será implementado em etapas pequenas, priorizando primeiro funcionamento e entendimento. **Não serão implementados testes automatizados neste front-end neste momento**, pois o foco atual é aprendizado, entrega funcional e domínio básico da stack.

---

## Stack definida

- **Framework:** Angular
- **Estilo do projeto:** Standalone Components
- **Comunicação com API:** HttpClient
- **Autenticação:** JWT Bearer Token
- **Formulários:** Reactive Forms
- **UI:** HTML simples no começo; PrimeNG apenas depois do fluxo principal funcionar
- **Testes:** não serão usados neste momento

---

## Estrutura inicial sugerida

```txt
src/app/
  core/
    auth/
    guards/
    interceptors/
    services/
  shared/
    models/
    components/
  features/
    auth/
    rifas/
    participantes/
    bilhetes/
    usuarios/
```

### Responsabilidade das pastas

- `core/`: código global da aplicação, como autenticação, interceptors, guards e serviços compartilhados.
- `shared/`: componentes, modelos e utilitários reutilizáveis.
- `features/`: funcionalidades principais do sistema, separadas por domínio.

---

# Fase 0 — Setup do projeto

## Objetivo

Criar o projeto Angular e deixar a base pronta para consumir a API.

## Implementar

1. Criar o projeto Angular.
2. Configurar rotas.
3. Configurar `HttpClient`.
4. Criar arquivo de environment com a URL da API.
5. Criar a estrutura de pastas inicial.

Exemplo de environment:

```ts
export const environment = {
  apiUrl: 'https://localhost:7224/api/v1'
};
```

## Onde posso travar

- Versão do Angular.
- Configuração de rotas standalone.
- Configuração do `HttpClient`.
- CORS no backend.
- Certificado HTTPS local.

## Links de apoio

- Angular Components: https://angular.dev/guide/components
- Angular Routing: https://angular.dev/guide/routing
- Angular HttpClient: https://angular.dev/guide/http

---

# Fase 1 — Teste de comunicação com a API

## Objetivo

Antes de implementar login, confirmar que o front consegue chamar o backend.

## Implementar

Criar uma tela simples para chamar a rota de checagem da API:

```http
GET https://localhost:7224/
```

## Resultado esperado

A tela deve exibir uma mensagem simples confirmando que a API respondeu.

## Onde posso travar

- API desligada.
- URL incorreta.
- Erro de CORS.
- Erro de certificado HTTPS.
- Requisição bloqueada pelo navegador.

## Observação

Se travar aqui, não avance para login ainda. Primeiro resolva a comunicação básica entre Angular e backend.

---

# Fase 2 — Login

## Objetivo

Implementar o primeiro fluxo real da aplicação: enviar email/senha e receber o token JWT.

## Endpoint

```http
POST /api/v1/Auth/login
```

Body esperado:

```json
{
  "email": "admin@email.com",
  "senha": "123456"
}
```

Resposta esperada:

```json
{
  "accessToken": "token-jwt"
}
```

## Implementar

```txt
features/auth/
  login.component.ts
  login.component.html

core/auth/
  auth.service.ts
  token.service.ts
```

## Responsabilidades

### AuthService

Responsável por chamar a API de login.

### TokenService

Responsável por:

- salvar o token;
- ler o token;
- remover o token;
- verificar se existe token.

## Resultado esperado

Após login com sucesso, redirecionar para:

```txt
/rifas
```

## Onde posso travar

- Criar formulário.
- Usar `ReactiveFormsModule`.
- Entender `Observable`.
- Usar `subscribe`.
- Salvar token no `localStorage`.
- Tratar erro `401 Unauthorized`.

## Links de apoio

- Angular Reactive Forms: https://angular.dev/guide/forms/reactive-forms
- Angular HttpClient: https://angular.dev/guide/http

---

# Fase 3 — Interceptor JWT e Guard

## Objetivo

Fazer o Angular enviar automaticamente o token nas requisições protegidas e bloquear rotas privadas quando o usuário não estiver logado.

## Implementar

```txt
core/interceptors/auth.interceptor.ts
core/guards/auth.guard.ts
```

## Interceptor

Adicionar o header nas requisições:

```http
Authorization: Bearer {token}
```

## Guard

Bloquear acesso às rotas protegidas se não houver token.

Rotas públicas:

```txt
/login
```

Rotas protegidas:

```txt
/rifas
/rifas/:id
/participantes
/bilhetes
/usuarios
```

## Onde posso travar

- Configurar interceptor em aplicação standalone.
- Token salvo, mas não enviado.
- Backend retornando `401`.
- Redirecionamento para login.
- Loop de navegação.

## Links de apoio

- Angular Interceptors: https://angular.dev/guide/http/interceptors
- Angular Route Guards: https://angular.dev/guide/routing/route-guards

---

# Fase 4 — Rifas: listar e cadastrar

## Objetivo

Implementar o primeiro CRUD parcial da aplicação.

## Endpoints

```http
GET /api/v1/Rifas
POST /api/v1/Rifas
```

## Implementar

```txt
features/rifas/
  models/rifa.model.ts
  services/rifa.service.ts
  pages/rifas-list.page.ts
  pages/rifa-form.page.ts
```

## Campos da rifa

```txt
nome
descricao
valorBilhete
dataSorteio
premio
```

## Resultado esperado

O usuário deve conseguir:

1. Abrir `/rifas`.
2. Ver a lista de rifas.
3. Clicar em “Nova rifa”.
4. Preencher o formulário.
5. Salvar.
6. Voltar para a lista.

## Onde posso travar

- Criar service.
- Criar interface TypeScript.
- Trabalhar com data.
- Trabalhar com valor decimal.
- Tratar resposta `201 Created`.
- Atualizar lista após cadastro.

## Observação

Nesta fase, ainda não usar PrimeNG. Usar HTML simples.

---

# Fase 5 — Rifas: detalhe, edição, encerrar e sortear

## Objetivo

Completar o fluxo principal de rifa.

## Endpoints

```http
GET /api/v1/Rifas/{id}
PUT /api/v1/Rifas/{id}
PATCH /api/v1/Rifas/{id}/encerrar
POST /api/v1/Rifas/{id}/sortear
```

## Implementar

```txt
/rifas/:id
/rifas/:id/editar
```

## Tela de detalhe da rifa

Exibir:

- nome;
- descrição;
- valor do bilhete;
- data do sorteio;
- prêmio;
- status encerrada/não encerrada;
- botão editar;
- botão encerrar;
- botão sortear.

## Onde posso travar

- Pegar `id` pela rota.
- Reaproveitar formulário de cadastro para edição.
- Lidar com resposta `204 No Content`.
- Exibir resultado do sorteio.
- Bloquear ações quando a rifa estiver encerrada.

## Links de apoio

- Angular Routing com parâmetros: https://angular.dev/guide/routing/read-route-state

---

# Fase 6 — Participantes por rifa

## Objetivo

Cadastrar e listar participantes vinculados a uma rifa.

## Endpoints

```http
GET /api/v1/Participantes/rifa/{rifaId}
POST /api/v1/Participantes
GET /api/v1/Participantes/{id}
PUT /api/v1/Participantes/{id}
```

## Implementar

```txt
/rifas/:rifaId/participantes
/rifas/:rifaId/participantes/novo
/participantes/:id
/participantes/:id/editar
```

## Campos do participante

```txt
rifaId
nome
telefone
observacao
```

## Onde posso travar

- Passar `rifaId` corretamente.
- Criar formulário com campo opcional.
- Navegar entre rifa e participantes.
- Atualizar lista após cadastro.

## Observação

Não colocar máscara de telefone no início. Primeiro fazer funcionar.

---

# Fase 7 — Registrar compra de bilhetes

## Objetivo

Permitir que um participante compre uma quantidade de bilhetes.

## Endpoint

```http
POST /api/v1/Bilhetes/compras
```

Body esperado:

```json
{
  "rifaId": "uuid-da-rifa",
  "participanteId": "uuid-do-participante",
  "quantidade": 5
}
```

## Implementar

Tela ou seção para registrar compra de bilhetes.

Campos:

```txt
participanteId
quantidade
```

O `rifaId` deve vir da tela atual.

## Resultado esperado

Após registrar a compra, exibir os bilhetes gerados.

## Onde posso travar

- Saber de onde vem o `rifaId`.
- Saber de onde vem o `participanteId`.
- Atualizar a tela após registrar compra.
- Exibir os números dos bilhetes.
- Lidar com erro de validação.

---

# Fase 8 — Listar bilhetes e alterar status

## Objetivo

Controlar pagamento e cancelamento dos bilhetes.

## Endpoints

```http
GET /api/v1/Bilhetes/rifa/{rifaId}
GET /api/v1/Bilhetes/status/{status}?rifaId={rifaId}
PATCH /api/v1/Bilhetes/{id}/status
PATCH /api/v1/Bilhetes/{id}/cancelar
GET /api/v1/Bilhetes/{id}
```

## Implementar

Na tela de detalhe da rifa, criar uma seção/listagem de bilhetes.

Colunas sugeridas:

```txt
Número
Status
Participante
Criado em
Pago em
Cancelado em
Ações
```

Ações sugeridas:

```txt
Marcar como pago
Marcar como pendente
Cancelar
Ver detalhes
```

## Onde posso travar

- Descobrir valores do enum `StatusPagamento`.
- Atualizar item após `PATCH`.
- Lidar com resposta `204 No Content`.
- Filtrar por status.
- Formatar datas.

## Observação

Provavelmente será necessário conferir no backend quais valores numéricos representam cada status.

---

# Fase 9 — Pesquisa de participantes

## Objetivo

Criar uma tela para encontrar participantes por nome, telefone, número do bilhete ou status de pagamento.

## Endpoint

```http
GET /api/v1/Participantes/pesquisar
```

Query params:

```txt
Nome
Telefone
NumeroBilhete
StatusPagamento
```

## Implementar

```txt
/participantes/pesquisar
```

Campos de filtro:

```txt
Nome
Telefone
Número do bilhete
Status
```

## Onde posso travar

- Montar query string.
- Enviar somente filtros preenchidos.
- Converter número.
- Trabalhar com enum de status.
- Limpar filtros.

---

# Fase 10 — Usuários

## Objetivo

Implementar a parte administrativa de usuários somente depois do fluxo principal da rifa estar funcionando.

## Endpoints

```http
POST /api/v1/Usuario
GET /api/v1/Usuario/{id}
PUT /api/v1/Usuario/{id}
PATCH /api/v1/Usuario/{id}/ativar
PATCH /api/v1/Usuario/{id}/desativar
```

## Implementar

Somente o necessário para o sistema funcionar.

## Onde posso travar

- Perfil de usuário.
- Autorização por papel.
- Ausência de rota para listar usuários.
- Gestão administrativa sem endpoint de listagem.

## Observação

Pelo Swagger atual, existe rota para obter usuário por id, mas não existe rota de listagem de usuários. Então não planejar uma tela completa de listagem antes de confirmar se o backend terá esse endpoint.

---

# Fase 11 — PrimeNG e melhoria visual

## Objetivo

Depois do fluxo principal funcionar, melhorar a interface.

## Quando instalar PrimeNG

Somente depois de funcionar:

```txt
Login
Token
Listar rifas
Cadastrar rifa
Detalhar rifa
```

## Melhorar com PrimeNG

- Botões.
- Inputs.
- Tabelas.
- Cards.
- Dialogs.
- Toasts.
- Dropdowns.

## Onde posso travar

- Compatibilidade de versão do Angular com PrimeNG.
- Instalação de tema.
- Imports de componentes.
- Estilização global.

## Links de apoio

- PrimeNG Installation: https://primeng.org/installation
- PrimeNG Components: https://primeng.org/components

---

# Ordem final de implementação

```txt
1. Setup Angular
2. Health check da API
3. Login
4. TokenService
5. Auth interceptor
6. Auth guard
7. Listar rifas
8. Cadastrar rifa
9. Detalhar rifa
10. Editar rifa
11. Encerrar rifa
12. Cadastrar participante
13. Listar participantes por rifa
14. Registrar compra de bilhetes
15. Listar bilhetes por rifa
16. Alterar status do bilhete
17. Cancelar bilhete
18. Pesquisar participantes
19. Sortear rifa
20. Usuários
21. PrimeNG e melhoria visual
```

---

# Como usar IA/Codex sem perder o controle

Evitar prompts grandes como:

```txt
Faça todo o CRUD de rifas em Angular.
```

Preferir prompts pequenos e objetivos:

```txt
Estou usando Angular standalone.
Tenho o endpoint GET /api/v1/Rifas.
Crie somente o service Angular com HttpClient.
Explique cada linha.
Não crie componente ainda.
```

Outro exemplo:

```txt
Tenho esse endpoint POST /api/v1/Rifas.
Me ajude a criar um Reactive Form simples.
Não use PrimeNG ainda.
Explique o fluxo antes de mostrar o código.
```

Outro exemplo:

```txt
Tenho esse erro no console do Angular.
Explique a causa provável e a menor correção possível.
Não reescreva o projeto inteiro.
```

Regra prática:

> Se eu não consigo explicar o código, o código ainda não é meu.

---

# Primeiro marco real do projeto

Antes de pensar em layout bonito, o primeiro marco deve ser:

```txt
Login funcionando
Token salvo
Token enviado nas requisições
Tela /rifas listando dados reais da API
Cadastro de rifa funcionando
```

Quando esse marco estiver concluído, o núcleo mais importante do Angular para vagas já terá sido praticado:

- componentes;
- rotas;
- services;
- HttpClient;
- Reactive Forms;
- JWT;
- interceptor;
- guard;
- consumo de API REST.

---

# Decisão importante

Este front-end será construído primeiro para aprendizado e entrega funcional. Por isso, neste momento:

- não usar testes automatizados;
- não começar com layout complexo;
- não começar com PrimeNG;
- não tentar criar arquitetura perfeita;
- não pedir para a IA gerar módulos grandes demais;
- implementar uma funcionalidade pequena por vez.

O objetivo é terminar um sistema simples, funcional e compreendido.
