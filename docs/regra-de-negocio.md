# Regras de Negócio

## Visão Geral

Este documento descreve as principais regras de negócio da aplicação de gerenciamento de rifas.

## RN001 - Geração de bilhetes por compra

O sistema deve gerar bilhetes no momento do registro da compra, de acordo com a quantidade comprada pelo participante.

A criação da rifa não deve exigir quantidade máxima de bilhetes nem gerar bilhetes antecipadamente.

Exemplo:

- Se uma compra possuir 3 bilhetes, o sistema deve gerar 3 novos bilhetes vinculados ao participante.

## RN002 - Rifa sem limite de bilhetes

A rifa não deve possuir limite máximo de bilhetes.

Enquanto a rifa estiver aberta, o sistema deve permitir novas compras.

## RN003 - Status do bilhete

Os bilhetes devem possuir um status de controle.

Status possíveis:

- pendente
- pago
- cancelado

## RN004 - Bilhetes pagos participam do sorteio

Somente bilhetes com status pago podem participar do sorteio.

## RN005 - Cancelamento de bilhete

Quando um bilhete for cancelado, ele deve ser preservado para fins de histórico e auditoria.

Bilhetes cancelados não devem participar do sorteio.

## RN006 - Participante pode possuir múltiplos bilhetes

Um participante pode possuir um ou vários bilhetes na mesma rifa.

## RN007 - Participante obrigatório

Um bilhete não pode existir vinculado a uma venda sem participante associado.

## RN008 - Usuário inativo

Usuários inativos não podem acessar o sistema.

## RN009 - Permissões administrativas

Somente administradores podem:

- cadastrar usuários
- editar usuários
- ativar/desativar usuários

## RN010 - Registro de responsável pela venda

O sistema deve armazenar qual usuário realizou o cadastro ou venda dos bilhetes.

## RN011 - Pesquisa de participantes

A pesquisa de participantes deve permitir busca por:

- nome
- telefone
- status do pagamento

## RN012 - Persistência de histórico básico

O sistema deve registrar informações básicas de auditoria.

Exemplo:

- usuário responsável
- data de criação
- data de alteração

## RN013 - Exclusão lógica de usuários

Usuários não devem ser removidos fisicamente do banco de dados.

O sistema deve utilizar controle de ativo/inativo.

## RN014 - Encerramento da rifa

Uma rifa encerrada não deve permitir:

- novas vendas
- alteração de bilhetes
- novos participantes
