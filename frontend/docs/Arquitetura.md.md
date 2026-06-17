# Padrão do Frontend — RifaManager.Web

## 1. Contexto

Projeto: RifaManager.Web
Tipo: Blazor WebAssembly Standalone 
UI: MudBlazor  
Organização: tipo técnico no topo + domínio interno  
Cultura da aplicação: Português do Brasil (`pt-BR`)  
Validação de formulários: Model State / DataAnnotations

---

## 2. Estrutura de pastas sugerida

```text
RifaManager.Web/
├── wwwroot/
│   └── appsettings.json
│
├── Layout/
│   ├── MainLayout.razor
│   └── NavMenu.razor
│
├── Pages/
│   ├── Home.razor
│   ├── Login.razor
│   │
│   ├── Errors/
│   │   ├── NotFound.razor
│   │   ├── Unauthorized.razor
│   │   └── ServerError.razor
│   │
│   ├── Rifas/
│   │   ├── Listar.razor
│   │   ├── Cadastrar.razor
│   │   ├── Editar.razor
│   │   └── Detalhes.razor
│   │
│   ├── Participantes/
│   │   ├── Listar.razor
│   │   ├── Cadastrar.razor
│   │   └── Editar.razor
│   │
│   └── Bilhetes/
│       ├── Listar.razor
│       └── Pagamento.razor
│
├── Components/
│   ├── Common/
│   │   ├── Loading.razor
│   │   ├── ErrorAlert.razor
│   │   ├── EmptyState.razor
│   │   └── ConfirmDialog.razor
│   │
│   ├── Layout/
│   │   └── PageTitle.razor
│   │
│   ├── Rifas/
│   │   ├── RifaForm.razor
│   │   └── RifaCard.razor
│   │
│   ├── Participantes/
│   │   └── ParticipanteForm.razor
│   │
│   └── Bilhetes/
│       ├── BilheteGrid.razor
│       └── StatusPagamentoChip.razor
│
├── Models/
│   ├── Auth/
│   │   ├── LoginRequest.cs
│   │   └── LoginResponse.cs
│   │
│   ├── Rifas/
│   │   ├── RifaResponse.cs
│   │   ├── CriarRifaRequest.cs
│   │   └── AtualizarRifaRequest.cs
│   │
│   ├── Participantes/
│   │   ├── ParticipanteResponse.cs
│   │   ├── CriarParticipanteRequest.cs
│   │   └── AtualizarParticipanteRequest.cs
│   │
│   ├── Bilhetes/
│   │   ├── BilheteResponse.cs
│   │   ├── AtualizarPagamentoBilheteRequest.cs
│   │   └── StatusPagamento.cs
│   │
│   └── Common/
│       ├── ApiErrorResponse.cs
│       ├── ApiResult.cs
│       └── PagedResponse.cs
│
├── Services/
│   ├── Http/
│   │   ├── ApiClient.cs
│   │   ├── ApiClientOptions.cs
│   │   ├── ApiException.cs
│   │   └── HttpClientFactoryExtensions.cs
│   │
│   ├── Auth/
│   │   ├── IAuthService.cs
│   │   ├── AuthService.cs
│   │   ├── TokenStorageService.cs
│   │   └── AuthMessageHandler.cs
│   │
│   ├── Rifas/
│   │   ├── IRifaService.cs
│   │   └── RifaService.cs
│   │
│   ├── Participantes/
│   │   ├── IParticipanteService.cs
│   │   └── ParticipanteService.cs
│   │
│   └── Bilhetes/
│       ├── IBilheteService.cs
│       └── BilheteService.cs
│
├── State/
│   └── AuthState.cs
│
├── Constants/
│   ├── ApiRoutes.cs
│   ├── AppRoutes.cs
│   └── LocalStorageKeys.cs
│
├── Extensions/
│   ├── ServiceCollectionExtensions.cs
│   └── HttpResponseMessageExtensions.cs
│
├── App.razor
├── Program.cs
└── _Imports.razor
```

---

## 3. Critério de organização

A estrutura segue o padrão:

```text
Tipo técnico no topo + domínio interno
```

Exemplos:

```text
Pages/Rifas/Listar.razor
Components/Rifas/RifaForm.razor
Models/Rifas/RifaResponse.cs
Services/Rifas/RifaService.cs
```

Regras:

- `Pages`: páginas com rota.
- `Components`: componentes reutilizáveis sem rota.
- `Models`: DTOs, requests, responses e models de formulário.
- `Services`: chamadas HTTP e integração com a API.
- `State`: estado compartilhado simples.
- `Constants`: rotas, chaves e valores fixos.
- `Extensions`: métodos auxiliares e extensões.
- `Layout`: estrutura visual global.

---

## 4. Cultura da aplicação

A cultura padrão da aplicação será:

```text
pt-BR
```

Essa cultura deve ser usada para:

- datas;
- números;
- moedas;
- mensagens de validação;
- textos de interface;
- formatação visual.

A configuração deve ser feita no início da aplicação em `Program.cs`.

---

## 5. Estilo de validação

A validação dos formulários será feita com:

```text
Model State / DataAnnotations
```

Componentes utilizados:

- `EditForm`;
- `EditContext`, quando necessário;
- `DataAnnotationsValidator`;
- `ValidationMessage`;
- `ValidationSummary`, quando necessário;
- componentes do MudBlazor vinculados ao model.

Regras:

- models usados em formulário devem conter atributos de validação;
- mensagens de validação devem estar em português;
- validações simples ficam no frontend;
- validações críticas continuam obrigatórias no backend;
- erros retornados pela API devem ser exibidos de forma amigável;
- o frontend não substitui validação de domínio do backend.

---

## 6. Convenções

### Páginas

Usar nomes em português por ação:

```text
Listar.razor
Cadastrar.razor
Editar.razor
Detalhes.razor
Pagamento.razor
```

### Services

Usar interface e implementação:

```text
IRifaService.cs
RifaService.cs
```

### Models

Usar nomes próximos aos contratos da API:

```text
CriarRifaRequest.cs
AtualizarRifaRequest.cs
RifaResponse.cs
```

### Erros

Páginas de erro ficam em:

```text
Pages/Errors/
```

Erros de API ficam em:

```text
Models/Common/
Services/Http/
```
