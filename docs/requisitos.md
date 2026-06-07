# Requisitos

## Visao Geral

O sistema permite que usuarios autenticados gerenciem rifas, participantes, bilhetes, pagamentos e sorteios.

Administradores tambem podem gerenciar usuarios do sistema.

# Requisitos Funcionais

## RF001 - Autenticacao

O sistema deve permitir que usuarios realizem login utilizando e-mail cadastrado e senha.

Usuarios inativos nao devem conseguir acessar o sistema.

## RF002 - Cadastro de usuarios

O administrador deve conseguir:

- cadastrar usuarios
- consultar usuario por id
- editar usuarios
- ativar/desativar usuarios

## RF003 - Cadastro, consulta e edicao de rifas

O sistema deve permitir:

- cadastrar rifas
- listar rifas
- consultar rifa por id
- editar rifas
- encerrar rifas

Uma rifa deve conter:

- nome
- descricao
- valor do bilhete
- data do sorteio
- premio
- status de encerramento

## RF004 - Geracao de bilhetes

O sistema deve gerar bilhetes conforme as compras forem registradas, sem exigir uma quantidade maxima na criacao da rifa.

Exemplo:

- uma compra de 3 bilhetes gera 3 novos bilhetes vinculados ao participante

## RF005 - Cadastro, consulta e edicao de participantes

O sistema deve permitir:

- cadastrar participantes
- consultar participante por id
- listar participantes por rifa
- pesquisar participantes
- editar participantes

Um participante deve conter:

- nome
- telefone
- observacao

## RF006 - Registro de compra de bilhetes

O sistema deve permitir registrar a compra de um ou mais bilhetes por um participante.

Ao registrar a compra, o sistema deve:

- criar os bilhetes da rifa
- vincular os bilhetes ao participante
- vincular os bilhetes ao usuario responsavel pela venda
- gerar a numeracao a partir do maior numero ja existente na rifa

Exemplo:

- participante Joao
- quantidade comprada: 3 bilhetes
- bilhetes gerados: 3

## RF007 - Controle de pagamento

O sistema deve permitir alterar o status de pagamento dos bilhetes.

Status possiveis:

- pendente
- pago
- cancelado

A alteracao direta de status deve aceitar somente:

- pago
- cancelado

## RF008 - Cancelamento de bilhetes

O sistema deve permitir cancelar um bilhete.

Bilhetes cancelados devem ser preservados para historico e nao devem participar do sorteio.

## RF009 - Pesquisa de participantes

O sistema deve permitir pesquisar participantes por:

- nome
- telefone
- numero do bilhete
- status do pagamento

## RF010 - Consulta de bilhetes

O sistema deve permitir consultar:

- bilhete por id
- bilhetes de uma rifa
- bilhetes por status
- bilhetes por status filtrando opcionalmente por rifa

## RF011 - Sorteio

O sistema deve permitir realizar sorteios utilizando apenas bilhetes pagos.

O retorno do sorteio deve informar os dados da rifa, do bilhete sorteado e do participante vencedor.

# Requisitos Nao Funcionais

## RNF001 - Seguranca

As rotas privadas devem exigir autenticacao.

Rotas administrativas devem exigir perfil de administrador.

## RNF002 - Autorizacao

Apenas administradores ativos podem gerenciar usuarios.

## RNF003 - Persistencia

Os dados devem ser armazenados em banco relacional.

## RNF004 - Auditoria basica

O sistema deve registrar informacoes basicas de auditoria quando aplicavel.

Exemplo:

- usuario responsavel pela venda de bilhetes
- data de criacao do bilhete
- data de pagamento do bilhete
- data de cancelamento do bilhete

## RNF005 - Resiliencia

As operacoes assincronas da API devem propagar `CancellationToken` para permitir cancelamento de requisicoes.

Cancelamentos de requisicao nao devem ser tratados como erro interno do servidor.

## RNF006 - Limitacao de requisicoes

A API deve aplicar rate limit global particionado por usuario autenticado ou, para acessos anonimos, por IP.

Quando o limite for excedido, a API deve retornar HTTP 429.

# Regras de Negocio

## RN001 - Sorteio apenas com bilhetes pagos

Somente bilhetes com pagamento confirmado podem participar do sorteio.

## RN002 - Usuario inativo

Usuarios inativos nao podem acessar o sistema.

## RN003 - Permissao administrativa

Apenas administradores ativos podem gerenciar usuarios.

## RN004 - Cancelamento de bilhete

Bilhetes cancelados nao devem participar do sorteio.

## RN005 - Criacao aberta de bilhetes

A rifa nao deve possuir limite maximo de bilhetes.

Enquanto a rifa estiver aberta, novas compras devem gerar novos bilhetes.

## RN006 - Rifa encerrada

Uma rifa encerrada nao deve permitir:

- novas compras
- cadastro de novos participantes vinculados a ela
- alteracao de bilhetes
