# Animes Land API
Bem-vindo à documentação da Anime API, uma aplicação web desenvolvida para gerenciar informações sobre animes. Este projeto foi criado para demonstrar habilidades técnicas em desenvolvimento de software, utilizando as melhores práticas e tecnologias modernas. Abaixo, você encontrará todas as informações necessárias para entender, executar e contribuir para este projeto.

## Índice
- [Visão Geral](#-visão-geral)
- [Funcionalidades](#-funcionalidades)
- [Tecnologias Utilizadas](#️-tecnologias-utilizadas)
- [Arquitetura](#️-arquitetura)
- [Estrutura de Diretórios](#-estrutura-de-diretórios)
- [Como Executar o Projeto](#-configuração-e-execução)
    - [Pré-requisitos](#-pré-requisitos)
    - [Configuração e Execução](#-configuração-e-execução)
- [Testes](#-testes)
- [Versionamento de Endpoints](#-versionamento-de-endpoints)
- [Rotas da API](#-rotas-da-api)
- [Testando a API com Swagger](#-testando-a-api-com-swagger)
    - [Acessando o Swagger UI](#1-acessando-o-swagger-ui)
    - [Endpoints disponíveis](#2-endpoints-disponíveis)
    - [Exemplos de Respostas](#3-exemplos-de-respostas)
- [Logging](#-logging)
- [Deploy com Docker](#-deploy-com-docker)
- [Branches e Repositório](#-branches-e-repositório)
- [Contribuição](#-contribuição)

## 📌 Visão Geral
A Anime API é uma aplicação web que permite gerenciar informações sobre animes. Ela oferece operações básicas de CRUD (Create, Read, Update, Delete) para animes, além de funcionalidades de busca por ID, nome ou diretor. A aplicação foi desenvolvida seguindo os princípios da Clean Architecture e utiliza as melhores práticas de desenvolvimento de software.

## 🎯 Funcionalidades
- 🔍 **Obter todos os animes**
- 🔍 **Buscar animes por ID, Nome ou Diretor** (suportando combinações desses campos)
- ➕ **Cadastrar um novo anime**
- ✏️ **Atualizar dados de um anime**
- ❌ **Excluir um anime**

## 🏗️ Tecnologias Utilizadas
- .NET 8.0.403
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server 
- MediatR
- xUnit (testes unitários)
- Docker
- Logging de exceções

## 🏛️ Arquitetura
O projeto segue o padrão **Clean Architecture**, separando responsabilidades em diferentes camadas:
- **Domain** → Contém as entidades e regras de negócio
- **Application** → Contém os casos de uso e interações com os repositórios
- **Infrastructure** → Implementação de banco de dados e serviços
- **Presentation (API)** → Exposição dos endpoints REST

## 📄 Estrutura de Diretórios
```
AnimeAPI/
│-- src/
│   ├── Api/     # Controllers e configuração de endpoints
│   ├── Application/      # Casos de uso e regras de aplicação
│   ├── Domain/           # Entidades e interfaces de domínio
│   ├── Infrastructure/   # Implementação do banco de dados
│-- tests/
│   ├── Application.Tests/   # Testes unitários
│-- Dockerfile
│-- docker-compose.yaml
│-- README.md
```

## 🚀 Como Executar o Projeto
### 🔧 Pré-requisitos
- [.NET SDK](https://dotnet.microsoft.com/)
- [SQL Server](https://www.microsoft.com/sql-server)
- [Docker](https://www.docker.com/)

### 📦 Configuração e Execução
1. Clone o repositório:
   ```sh
   git clone https://github.com/seu-usuario/nome-do-repositorio.git
   cd nome-do-repositorio
   ```
2. Configure a string de conexão no `appsettings.json`.
3. Suba os containers Docker para o banco de dados e a API:
   - Iniciar os containers Docker com o Docker Compose:
   ```shell
   docker-compose up -d
   ```
   - Verifique se os containers estão em execução:
   ```shell
   docker ps
   ```

4. Execute os seguintes comandos:
   ```shell
   dotnet restore
   dotnet build
   dotnet run --project src/AnimesLand.Api
   ```
   Isso iniciará a API, que agora estará conectada ao banco de dados no Docker.

## 🧪 Testes
Para rodar os testes unitários, utilize o comando:
```sh
dotnet test
```

## 🔄 Versionamento de Endpoints
Os endpoints seguem um padrão de versionamento, exemplo:
```
GET /api/v1/animes
GET /api/v1/animes/{id}
```

## 📝 Rotas da API
| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/v1/animes` | Obtém todos os animes |
| GET | `/api/v1/animes/{id}` | Obtém um anime por ID |
| GET | `/api/v1/animes?nome=Naruto&diretor=Masashi` | Busca por nome e diretor |
| POST | `/api/v1/animes` | Cadastra um novo anime |
| PUT | `/api/v1/animes/{id}` | Atualiza os dados de um anime |
| DELETE | `/api/v1/animes/{id}` | Exclui um anime |

## 🚀 Testando a API com Swagger

O Swagger é uma ferramenta que permite visualizar e testar os endpoints da API de forma interativa. Para testar a API, siga os passos abaixo:

### 1. Acessando o Swagger UI
- Execute a aplicação em ambiente de desenvolvimento.

- Acesse a interface do Swagger no navegador através da URL:
    ```sh
    http://localhost:5000/swagger
    ```
    (Substitua 5000 pela porta configurada no seu projeto, se necessário.)

- No Swagger UI, você verá uma lista de endpoints disponíveis, organizados por versão (v1, v2, etc.).

### 2. Endpoints disponíveis
#### Listar todos os animes (GET /api/v1/animes)

- Descrição: Retorna uma lista de todos os animes cadastrados.

- Como testar:

    1. No Swagger UI, expanda o endpoint GET /api/v1/animes.
    
    2. Clique em Try it out.

    3. Clique em Execute para enviar a requisição.

    4. Veja a resposta no campo Responses.

#### Buscar anime por ID (GET /api/v1/animes/{id})

- Descrição: Retorna os detalhes de um anime específico com base no ID.

- Como testar:

    1. No Swagger UI, expanda o endpoint GET /api/v1/animes/{id}.

    2. Clique em Try it out.

    3. Insira um ID válido no campo id.

    4. Clique em Execute para enviar a requisição.

Veja a resposta no campo Responses.

#### Pesquisar animes (GET /api/v1/animes/search)
- Descrição: Retorna uma lista de animes com base nos critérios de pesquisa (nome ou diretor).

- Como testar:

    1. No Swagger UI, expanda o endpoint GET /api/v1/animes/search.

    2. Clique em Try it out.

    3. Preencha os parâmetros nome e/ou diretor (pelo menos um é obrigatório).

    4. Clique em Execute para enviar a requisição.

    5. Veja a resposta no campo Responses.

#### Criar um anime (POST /api/v1/animes)
- Descrição: Cria um novo anime com base nos dados fornecidos.

- Como testar:

1. No Swagger UI, expanda o endpoint POST /api/v1/animes.

2. Clique em Try it out.

3. Insira um ID válido no campo id.

4. No campo body, insira um JSON com os dados do anime. Exemplo:
```json
{
  "id": 1,
  "nome": "Naruto Shippuden",
  "diretor": "Hayato Date",
  "resumo": "A continuação da história de Naruto."
}
```
5. Clique em Execute para enviar a requisição.

6. Veja a resposta no campo Responses.

#### Excluir um anime (DELETE /api/v1/animes/{id})
- Descrição: Exclui um anime com base no ID.

- Como testar:

1. No Swagger UI, expanda o endpoint DELETE /api/v1/animes/{id}.

2. Clique em Try it out.

3. Insira um ID válido no campo id.

4. Clique em Execute para enviar a requisição.

5. Veja a resposta no campo Responses.

### 3. Exemplos de Respostas
Resposta de sucesso (200 OK):
```json
[
  {
    "id": 1,
    "nome": "Naruto",
    "diretor": "Hayato Date",
    "resumo": "Um ninja que busca se tornar Hokage."
  }
]
```
Resposta de erro (404 Not Found):
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Not Found",
  "status": 404,
  "detail": "Nenhum anime encontrado."
}
```
Resposta de erro (400 Bad Request):
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "Bad Request",
  "status": 400,
  "detail": "Pelo menos um critério de pesquisa (Nome ou Diretor) deve ser fornecido."
}
```

## 📊 Logging
Caso ocorra uma exceção, a API gera logs automaticamente, podendo ser configurados para armazenar em arquivo, banco de dados ou outros targets.

## 📌 Branches e Repositório
- **`develop`** → Desenvolvimento ativo
- **`main`** → Versão principal do código
- Todos os commits na branch principal devem ser feitos via **pull request**.

## 📢 Contribuição
Sugestões e melhorias são bem-vindas! Para contribuir:
1. Crie um fork do repositório
2. Crie uma branch com sua feature (`git checkout -b minha-feature`)
3. Faça o commit das mudanças (`git commit -m 'Adiciona minha feature'`)
4. Envie um push para a branch (`git push origin minha-feature`)
5. Abra um Pull Request

---
🚀 **Anime API** - Desenvolvido como parte de um teste técnico para avaliação de habilidades em .NET e arquitetura de software.
