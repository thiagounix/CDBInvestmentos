# CDB Investimentos Solution

## Descrição

Este repositório contém uma solução para cálculo de investimentos em CDB, desenvolvida com uma arquitetura baseada em **CQRS** (Command Query Responsibility Segregation) e com métodos assíncronos (**async/await**) para otimização e organização do código. O projeto não possui integração com banco de dados nem persistência de dados, sendo utilizado exclusivamente para fins de cálculo e exibição no frontend. 

A solução está dividida em dois projetos principais:
- **API .NET (CDBInvestimentosAPI)** - Backend responsável por expor endpoints para cálculos de investimentos em CDB.
- **Frontend Angular (CdbInvestimentosFrontend)** - Interface do usuário desenvolvida com Angular, para interação e visualização dos resultados dos cálculos.

---

## Estrutura de Pastas
/SolutionRoot
├─ CdbInvestimentos.API
├── Controllers
│   └── CdbController.cs
├── Middlewares
├── appsettings.json
└── Program.cs
├─ CdbInvestimentos.Aplicacao
├── Commands
│   └── CalcularCdbCommand.cs
├── Handlers
│   └── CalcularCdbCommandHandler.cs
├── Interfaces
│   └── IServicoCalculadoraCdb.cs
├── Responses
│   └── CdbCalculoResultado.cs
└── Services
    └── Calculators
        ├── CdbCalculator.cs
        └── ServicoCalculadoraCdb.cs
├─ CdbInvestimentos.Dominio
└── Entities
    └── CdbInvestimento.cs
├─ CdbInvestimentos.Infraestrutura
└── Dependencies
├─ CdbInvestimentos.Testes
└── CalculadoraCdb
    └── ServicoCalculadoraCdbTests.cs
├─ cdb-investimentos-frontend
└── src
    └── app
        ├── components
        │   └── cdb-calculo-form
        │       ├── cdb-calculo-form.component.html
        │       ├── cdb-calculo-form.component.scss
        │       ├── cdb-calculo-form.component.spec.ts
        │       └── cdb-calculo-form.component.ts
        ├── services
        │   ├── cdb-calculo.service.spec.ts
        │   └── cdb-calculo.service.ts
        ├── app.component.html
        ├── app.component.scss
        ├── app.component.spec.ts
        ├── app.component.ts
        ├── app.config.ts
        ├── app.routes.ts
        ├── main.ts
        └── styles.scss

# CDB Investimentos

Este projeto foi desenvolvido para calcular investimentos em CDB (Certificado de Depósito Bancário), utilizando uma arquitetura moderna e organizada que facilita a manutenção e a evolução do sistema.

## Tecnologias e Ferramentas Utilizadas

- **Backend (.NET 8.0)**
  - Desenvolvido com **C#** e **ASP.NET Core** para API.
  - Utilização do padrão **CQRS** (Command Query Responsibility Segregation) para separar comandos e consultas.
  - Métodos assíncronos (async) em toda a lógica de aplicação, garantindo melhor performance e escalabilidade, apesar de não haver acesso a uma base de dados externa.
  - Arquitetura baseada em **DDD (Domain-Driven Design)** e **Clean Code**, promovendo código legível, testável e fácil de manter.

- **Frontend (Angular 18)**
  - Desenvolvido com **Angular CLI** em TypeScript, com **componentes Standalone** para modularidade.
  - Integração com a API do backend para cálculo do investimento.
  - Estilo e organização com **SCSS**.
  - Testes unitários usando **Karma e Jasmine** para garantir o funcionamento e a consistência dos componentes.

## Estrutura do Projeto

### Backend

O projeto de backend é construído com uma API .NET, seguindo princípios de **CQRS** e **DDD**. A separação entre comandos e consultas permite que as operações de cálculo sejam executadas de forma clara e independente, mesmo sem acesso a um banco de dados externo.

- **CQRS**: Implementado para uma divisão de responsabilidades clara, onde operações de leitura e escrita são manipuladas separadamente.
- **Métodos Assíncronos (async)**: Toda a aplicação faz uso de métodos async para simular operações que poderiam envolver processos de longa duração, como o acesso a bancos de dados ou outras APIs.
- **DDD e Clean Code**: Estrutura modular e aderente a princípios de design como SOLID, facilitando a escalabilidade e organização do código.

### Frontend

O projeto de frontend, desenvolvido com **Angular Standalone Components**, consome a API do backend e fornece uma interface amigável para que o usuário faça simulações de investimentos.

- **Formulário**: Permite a entrada de valores e prazos para o cálculo do CDB.
- **Validações**: Apresenta validações de campos e mensagens de erro do backend de forma amigável.
- **UI Responsiva**: Utiliza SCSS e um layout flexível para uma experiência de usuário consistente em diferentes dispositivos.

## Como Executar os Projetos

### Pré-requisitos

Certifique-se de ter instalado:

- **Node.js** e **npm** - Necessários para o frontend Angular.
- **.NET SDK** versão 6.0 ou superior - Necessário para a API em .NET.

### Instruções

1. **Clone o repositório**
   ```bash
   git clone https://github.com/seu-usuario/CDBInvestimentos.git
   cd CDBInvestimentos

Execute o Backend (.NET)

### Navegue até a pasta CDBInvestimentosAPI:

cd CDBInvestimentosAPI
Restaure as dependências e inicie o servidor:

dotnet restore
dotnet run

** API estará disponível ** em http://localhost:5000. Verifique a documentação da API em http://localhost:5000/swagger.
Execute o Frontend (Angular)

Em uma nova janela de terminal, navegue até CdbInvestimentosFrontend:

cd ../CdbInvestimentosFrontend
Instale as dependências:

npm install
Inicie o servidor Angular:

ng serve
Acesse o frontend em http://localhost:4200 no navegador.

## Observações Finais

Este projeto foi desenvolvido com o objetivo de demonstrar conhecimento em arquitetura de software.
Embora o sistema não tenha acesso a bancos de dados nem faça integração com APIs externas, foi implementado o padrão **CQRS (Command Query Responsibility Segregation)** e métodos **assíncronos (async)** em todo o backend.
Essa escolha foi feita para evidenciar boas práticas e a aplicação de princípios de **arquitetura limpa (Clean Code)** e **DDD (Domain-Driven Design)**.

A utilização de **CQRS** permite uma clara separação entre comandos (operações de gravação) e consultas (operações de leitura), promovendo uma organização que facilita a escalabilidade e a manutenção do sistema.
Embora não fosse necessário neste contexto, onde as operações são executadas localmente e não envolvem persistência de dados, essa abordagem foi incluída para mostrar como a arquitetura pode ser projetada para sistemas mais complexos, nos quais o desacoplamento das operações de leitura e escrita seria essencial.

Da mesma forma, os métodos **async** foram empregados para simular cenários onde as operações poderiam envolver processos assíncronos e de longa duração, como consultas a bancos de dados ou chamadas a serviços externos.
Isso evidencia o entendimento de como otimizar o desempenho do sistema para futuras expansões, caso a aplicação venha a ter interações com serviços ou banco de dados.
Essa estrutura também é um indicativo de como construir soluções com foco em performance e escalabilidade, mesmo quando essas características não são essenciais no cenário atual.

## Ausência de implementação da camada de Infraestrutura ##
Neste projeto, devido à sua natureza e aos requisitos específicos, não foi implementado uma estrutura de acesso a dados completa. Como não há necessidade de persistir ou recuperar dados de uma base, não foram utilizados Entity Framework, Dapper ou qualquer outro ORM. Isso significa que não há mapeamento das entidades para tabelas em um banco de dados, nem a criação de um contexto (DbContext) com DbSets.

Além disso, não foi necessário aplicar padrões como Unit of Work, que geralmente gerencia transações de banco de dados, nem o uso do AutoMapper para mapear entidades e DTOs (Data Transfer Objects). Em um cenário onde a persistência de dados fosse exigida, essas tecnologias e padrões seriam essenciais para garantir organização, escalabilidade e eficiência no acesso e manipulação dos dados.
