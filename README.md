# Sistema de Login e Cadastro

Este projeto é uma aplicação de console em C# que implementa um sistema básico de login e cadastro utilizando SQL Server.

## Sumário

- [Introdução](#introdução)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Instalação](#instalação)
- [Uso](#uso)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Contribuição](#contribuição)
- [Licença](#licença)

## Introdução

O projeto implementa um sistema simples de login e cadastro que armazena emails e senhas em um banco de dados SQL Server. Ele permite criar novos usuários, validar emails e realizar login.

## Tecnologias Utilizadas

- C#
- .NET
- SQL Server
- Microsoft.Data.SqlClient

## Instalação

1. Clone este repositório:

    ```bash
    git clone https://github.com/seu-usuario/SistemaLoginCadastro.git
    ```

2. Abra o projeto no seu editor de código favorito.

3. Certifique-se de ter o .NET SDK instalado em sua máquina.

4. Configure a string de conexão no arquivo `Hello.cs` com os dados do seu servidor SQL Server:

    ```csharp
    private static readonly string connectionString = "Server=SeuServidor;Database=ConsoleAPP;Integrated Security=True;TrustServerCertificate=True;";
    ```

5. Crie o banco de dados e a tabela `Pessoas` no SQL Server:

    ```sql
    CREATE DATABASE ConsoleAPP;

    USE ConsoleAPP;

    CREATE TABLE Pessoas (
        ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
        Email NVARCHAR(50),
        Senha NVARCHAR(50)
    );
    ```

## Uso

Execute o projeto e siga as instruções no console para criar uma conta ou fazer login.

```bash
dotnet run
