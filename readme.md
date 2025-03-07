# TODO API - Estudo

## Sobre

Projeto realizado para estudo de Asp.NET com intuito te aprender as principais extensões e suas aplicações. Não foi seguido nenhum tipo de Clean Architecture ou padrão durante o projeto.

## Ferramentas utilizadas

Todas as extensões foram utilizadas na versão 8.0.0 ou 8.0.2.

- .Net 8.
- Postman: Para testes manuais de API.
- AutoMapper: Para mapeamento entre objetos.
- FluentValidation: Para validaçÕes de entrada do usuário.
- AspNetCore JwtBearer Authentication: Para Autenticação via JWT Bearer.
- NewtonsoftJson: Para serialização de objetos para JSON.
- EF Core: Para realizar as operações no banco de dados utilizando o modelo ORM.
- Pomelo: Para a conexão ao banco de dados MySQL
- Swashbuckle: Para documentação da API através do Swagger.

## Projeto

### Sobre o projeto

Aplicação destinada a gerenciar uma lista de tarefas (TODO List), permitindo adicionar, remover, alterar e listar itens e listas, com suporte a autenticação de usuários e persistência de dados em um banco de dados relacional.

### Requisitos

#### Dos Itens

Cada item da lista de tarefas deve possuir os seguintes atributos:

- **Título**:
  - Obrigatório;
  - Máximo de 50 caracteres;
  - Não deve permitir valores nulos ou vazios.
- **Descrição**:
  - Opcional;
  - Máximo de 255 caracteres;
  - Não deve permitir caracteres especiais (ex.: <, >, &, *, etc.), apenas letras, números e pontuação básica (.,!?).
- **Status**:
  - Tipo booleano (true para "Concluído", false para "Não concluído");
  - Uma vez marcado como "Concluído", não pode ser revertido para "Não concluído".
- **Data de início**:
  - Tipo DateTime;
  - Registrada automaticamente no momento da criação do item;
  - Exibida no formato "DD/MM/YYYY".
- **Data de fim**:
  - Tipo DateTime;
  - Registrada automaticamente no momento em que o status é alterado para "Concluído";
  - Exibida no formato "DD/MM/YYYY";
  - Opcional (nula se o item não estiver concluído).

#### Das Listas

Cada lista de tarefas deve possuir os seguintes atributos:

- **Título**:
  - Obrigatório;
  - Máximo de 50 caracteres;
  - Não deve permitir valores nulos ou vazios.
- **ID do usuário**:
  - Obrigatório;
  - Deve ser atribuido automaticamente na criação.
- **Itens**:
  - Deve ser inicializado vazio;
  - Em caso de exclusão da lista, deve ser excluido por completo.


#### Do Usuário

Cada usuário deve possuir os seguintes atributos:

- **Login**
  - Deve ser único no sistema;
  - Comprimento entre 8 e 24 caracteres;
  - Pode conter letras (A-Z, a-z), números (0-9) e caracteres especiais permitidos (ex.: _, -, .).
- **Senha**
  - Comprimento mínimo de 8 caracteres e máximo de 16 caracteres;
  - Deve incluir:
    - Pelo menos uma letra maiúscula (A-Z);
    - Pelo menos uma letra minúscula (a-z);
    - Pelo menos um número (0-9);
    - Pelo menos um caractere especial (ex.: @, #, $, %, &, *, !);
  - Deve ser sensível a maiúsculas e minúsculas (case-sensitive).
- **Lista**:
    - Deve ser inicializado vazio;
    - Em caso de exclusão do usuário, deve ser excluido por completo.

#### Do Permissionamento

- Deve utilizar o formato **JWT** (JSON Web Token);
- O token deve expirar em 5 minutos após a emissão;
- Deve incluir informações de identificação do usuário (ID e Username), assim como sua data de criação e data de expiração.
