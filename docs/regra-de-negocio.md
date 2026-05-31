# Regras de Negócio

## Visão Geral

Este documento descreve as principais regras de negócio da aplicação de gerenciamento de rifas.

## RN001 - Bilhete único por rifa

Um bilhete não pode pertencer a duas pessoas dentro da mesma rifa.

Exemplo:

- O bilhete número 10 da Rifa A não pode ser atribuído simultaneamente para João e Maria.

## RN002 - Repetição de números entre rifas

O mesmo número de bilhete pode existir em rifas diferentes.

Exemplo:

- O bilhete número 10 pode existir na Rifa A e também na Rifa B.

## RN003 - Geração de bilhetes por compra

O sistema deve gerar bilhetes no momento do registro da compra, de acordo com a quantidade comprada pelo participante.

A criação da rifa não deve exigir quantidade máxima de números nem gerar bilhetes antecipadamente.

Exemplo:

- Se o último bilhete gerado na rifa for o número 10 e uma compra possuir 3 bilhetes, o sistema deve gerar os bilhetes 11, 12 e 13.

## RN004 - Rifa sem limite de bilhetes

A rifa não deve possuir limite máximo de bilhetes.

Enquanto a rifa estiver aberta, o sistema deve permitir novas compras e continuar a numeração sequencial dos bilhetes.

## RN005 - Status do bilhete

Os bilhetes devem possuir um status de controle.

Status possíveis:

- pendente
- pago
- cancelado

## RN006 - Bilhetes pagos participam do sorteio

Somente bilhetes com status pago podem participar do sorteio.

## RN007 - Cancelamento de bilhete

Quando um bilhete for cancelado, ele deve manter seu número para fins de histórico e auditoria.

Bilhetes cancelados não devem participar do sorteio e seus números não devem ser reutilizados em novas compras.

## RN008 - Participante pode possuir múltiplos bilhetes

Um participante pode possuir um ou vários bilhetes na mesma rifa.

## RN009 - Participante obrigatório

Um bilhete não pode existir vinculado a uma venda sem participante associado.

## RN010 - Usuário inativo

Usuários inativos não podem acessar o sistema.

## RN011 - Permissões administrativas

Somente administradores podem:

- cadastrar usuários
- editar usuários
- ativar/desativar usuários

## RN012 - Registro de responsável pela venda

O sistema deve armazenar qual usuário realizou o cadastro ou venda dos bilhetes.

## RN013 - Integridade da venda

O sistema não deve permitir duplicidade de números de bilhete dentro da mesma rifa.

## RN014 - Pesquisa de participantes

A pesquisa de participantes deve permitir busca por:

- nome
- telefone
- número do bilhete
- status do pagamento

## RN015 - Persistência de histórico básico

O sistema deve registrar informações básicas de auditoria.

Exemplo:

- usuário responsável
- data de criação
- data de alteração

## RN016 - Exclusão lógica de usuários

Usuários não devem ser removidos fisicamente do banco de dados.

O sistema deve utilizar controle de ativo/inativo.

## RN017 - Encerramento da rifa

Uma rifa encerrada não deve permitir:

- novas vendas
- alteração de bilhetes
- novos participantes

## RN018 - Numeração sequencial

Os bilhetes devem possuir numeração sequencial por rifa, iniciando em 1 e avançando conforme novas compras forem registradas.

Exemplo:

- primeira compra gera o bilhete 1
- compra seguinte de 2 bilhetes gera os bilhetes 2 e 3
