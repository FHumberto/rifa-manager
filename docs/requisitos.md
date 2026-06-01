# Requisitos

## Visão Geral

O sistema permitirá que usuários autenticados cadastrem rifas, participantes, bilhetes e controlem pagamentos.

# Requisitos Funcionais

## RF001 - Autenticação

O sistema deve permitir que usuários realizem login utilizando e-mail cadastrado e senha.

## RF002 - Cadastro de usuários

O administrador deve conseguir:

- cadastrar usuários
- editar usuários
- ativar/desativar usuários

## RF003 - Cadastro e edição de rifas

O sistema deve permitir cadastrar e editar uma rifa contendo:

- nome
- descrição
- valor do bilhete
- data do sorteio
- prêmio

## RF004 - Geração de bilhetes

O sistema deve gerar bilhetes conforme as compras forem registradas, sem exigir uma quantidade máxima na criação da rifa.

Exemplo:

- uma compra de 3 bilhetes gera 3 novos bilhetes vinculados ao participante

## RF005 - Cadastro de participantes

O sistema deve permitir cadastrar participantes contendo:

- nome
- telefone
- observação

## RF006 - Registro de compra de bilhetes

O sistema deve permitir registrar a compra de um ou mais bilhetes por um participante.

Ao registrar a compra, o sistema deve criar os bilhetes da rifa e vinculá-los ao participante.

Exemplo:

- participante João
- quantidade comprada: 3 bilhetes
- bilhetes gerados: 3

## RF007 - Controle de pagamento

O sistema deve permitir informar o status do pagamento dos bilhetes.

Status possíveis:

- pendente
- pago
- cancelado

## RF008 - Pesquisa de participantes

O sistema deve permitir pesquisar participantes por:

- nome
- telefone
- número do bilhete
- status do pagamento

## RF009 - Consulta de bilhetes

O sistema deve permitir consultar:

- bilhetes gerados
- bilhetes pagos
- bilhetes pendentes
- bilhetes cancelados

## RF010 - Sorteio

O sistema deve permitir realizar sorteios utilizando apenas bilhetes pagos.

# Requisitos Não Funcionais

## RNF001 - Segurança

As rotas privadas devem exigir autenticação.

## RNF002 - Responsividade

O sistema deve funcionar em dispositivos móveis e desktop.

## RNF003 - Performance

As consultas principais devem possuir tempo de resposta inferior a 2 segundos.

## RNF004 - Persistência

Os dados devem ser armazenados em banco relacional.

## RNF005 - Auditoria básica

O sistema deve registrar qual usuário realizou operações importantes.

Exemplo:

- cadastro de participante
- alteração de pagamento
- criação de rifa

# Regras de Negócio

## RN001 - Sorteio apenas com bilhetes pagos

Somente bilhetes com pagamento confirmado podem participar do sorteio.

## RN002 - Usuário inativo

Usuários inativos não podem acessar o sistema.

## RN003 - Permissão administrativa

Apenas administradores podem gerenciar usuários.

## RN004 - Cancelamento de bilhete

Bilhetes cancelados não devem participar do sorteio.

## RN005 - Criação aberta de bilhetes

A rifa não deve possuir limite máximo de bilhetes.

Enquanto a rifa estiver aberta, novas compras devem gerar novos bilhetes.
