#### Projeto de estudo com DynamoDB, Webapi e Angular 9.


Arquitetura do projeto
- Api criada com Webapi(3.1.2).
- Front criado com Angular(9.1.6).
- Testes criado com xUnit.
- Testes para UI criado com Selenium WebDriver.

#### Instalação

Se faz  necessário o [.NET CORE](https://dotnet.microsoft.com/download) para execução do projeto.

```sh
$ git  clone https://github.com/httpmurilo/dynamodb-webapi-angular.git
$ dotnet restore
$ cd Source.Api 
$ dotnet run
```

Para execução do front-end

```sh
$ cd Source.Web && npm install && ng serve -o
```

#### Testes

A camada de testes está separada nas pastas Source.Tests e Source.Ui.Testes para executar basta digitar o comando : 
```sh
$ dotnet test
```

#### AWS

Necessário informar a AcessKeyId e Secret Key na classe DynamoDbInitialiazer.
