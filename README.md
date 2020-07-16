# wb-tech-chalenge
Projeto contendo uma aplicação Web simulando algumas ações de uma conta de banco digital.

Para rodar a aplicação é necessário a versão 3.1 do .Net Core instalada. Caso não possua, pode encontrá-la em https://dotnet.microsoft.com/download/dotnet-core/3.1.

A aplicação utiliza o banco MySQL acessado no padrão Repository com o ORM EntityFrameworkCore. O projeto conta com Migrations, e na execução do projeto já são aplicadas todas as versões do migration, o que faz apenas ser necessário configurar a string de conexão correta para um banco mySQL, para que o projeto funcione corretamente.

O projeto foi idealizado seguindo o padrão DDD, caso existam sujestões de um melhor uso do DDD, serão bem vindas.

Há dísponível na aplicação um projeto de teste unitário, que serve para testar a classe de dominio.

O front-end do projeto foi feito usando um template gratuíto bottstrap, chamado AdminLTE. O mesmo pode ser encontrado no Github no link https://github.com/ColorlibHQ/AdminLTE. A versão usada foi a 3.0.5 do dia 19/05/2020
