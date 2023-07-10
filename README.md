# GameLibrary
* Это тестовое задание
* Swagger находится по ссылке @/swagger/index.html 
## Стэк
* ASP.NET CORE 7.0
* C# 11.0
* Entity framework 8.0.0
* AutoMapper 12.0.1
* PostgreSQL
## Инструкция по запуску
1. Скачайте или клонируйте репозиторий
2. В файле appsettings.json, находящемся в GameLibrary.API замените строку подключения к бд(GameLibraryDb)
3. Миграции применятся автоматически при запуске
## Инструкция по замене Entity framework
1. Удалите класс DbMigrationExtension в проекте GameLibrary.API
2. Удалите класс AppDbContext в проекте GameLibrary.Infrastructure
3. В классе DbDependencyInjection удалите строчку для добавления AppDbContext, и в том же классе замените классы репозиториев на другие, не связанные с entity framework
4. Удалите миграции в проекте GameLibrary.Infrastructure
