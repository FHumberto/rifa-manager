# Regras de Negocio

## Visao Geral

Este documento descreve as principais regras de negocio da aplicacao de gerenciamento de rifas.

## RN001 - Geracao de bilhetes por compra

O sistema deve gerar bilhetes no momento do registro da compra, de acordo com a quantidade comprada pelo participante.

A criacao da rifa nao deve exigir quantidade maxima de bilhetes nem gerar bilhetes antecipadamente.

Exemplo:

- se uma compra possuir 3 bilhetes, o sistema deve gerar 3 novos bilhetes vinculados ao participante

## RN002 - Numeracao dos bilhetes

Os bilhetes devem ser numerados de forma incremental dentro da rifa.

Ao registrar uma compra, a numeracao deve continuar a partir do maior numero de bilhete ja existente naquela rifa.

## RN003 - Rifa sem limite de bilhetes

A rifa nao deve possuir limite maximo de bilhetes.

Enquanto a rifa estiver aberta, o sistema deve permitir novas compras.

## RN004 - Dados obrigatorios da rifa

Uma rifa deve possuir:

- nome
- descricao
- valor do bilhete maior que zero
- data de sorteio
- premio

A data do sorteio nao pode estar no passado.

## RN005 - Encerramento da rifa

Uma rifa encerrada nao deve permitir:

- novas compras
- cadastro de novos participantes vinculados a ela
- alteracao de bilhetes

Uma rifa ja encerrada nao pode ser encerrada novamente.

## RN006 - Status do bilhete

Os bilhetes devem possuir um status de controle.

Status possiveis:

- pendente
- pago
- cancelado

Ao criar um bilhete, seu status inicial deve ser pendente.

## RN007 - Alteracao de status do bilhete

A alteracao direta de status deve permitir marcar o bilhete como:

- pago
- cancelado

Nao deve ser permitido:

- marcar bilhete cancelado como pago
- marcar bilhete pago como cancelado
- marcar bilhete como pago e cancelado ao mesmo tempo
- alterar bilhete de rifa encerrada
- alterar bilhete que nao pertence a rifa informada

## RN008 - Cancelamento de bilhete

Quando um bilhete for cancelado, ele deve ser preservado para fins de historico e auditoria.

Bilhetes cancelados nao devem participar do sorteio.

## RN009 - Bilhetes pagos participam do sorteio

Somente bilhetes com status pago podem participar do sorteio.

Se a rifa nao possuir bilhetes pagos, o sorteio nao deve ser realizado.

## RN010 - Participante pode possuir multiplos bilhetes

Um participante pode possuir um ou varios bilhetes na mesma rifa.

## RN011 - Participante obrigatorio

Um bilhete nao pode existir vinculado a uma venda sem participante associado.

## RN012 - Edicao de participante

Participantes podem ser editados enquanto nao estiverem vinculados a bilhetes de rifas encerradas.

Participantes vinculados a uma rifa encerrada nao podem ser editados.

## RN013 - Pesquisa de participantes

A pesquisa de participantes deve permitir busca por:

- nome
- telefone
- numero do bilhete
- status do pagamento

## RN014 - Usuario inativo

Usuarios inativos nao podem acessar o sistema.

## RN015 - Permissoes administrativas

Somente administradores ativos podem:

- cadastrar usuarios
- consultar usuarios
- editar usuarios
- ativar/desativar usuarios

## RN016 - Exclusao logica de usuarios

Usuarios nao devem ser removidos fisicamente do banco de dados.

O sistema deve utilizar controle de ativo/inativo.

## RN017 - Validacao de usuario

Um usuario deve possuir:

- nome obrigatorio com no maximo 100 caracteres
- e-mail obrigatorio com no maximo 100 caracteres
- senha obrigatoria com no maximo 200 caracteres
- perfil valido

## RN018 - Registro de responsavel pela venda

O sistema deve armazenar qual usuario realizou a venda dos bilhetes.

## RN019 - Historico basico dos bilhetes

O sistema deve registrar:

- data de criacao do bilhete
- data de pagamento quando o bilhete for marcado como pago
- data de cancelamento quando o bilhete for cancelado
