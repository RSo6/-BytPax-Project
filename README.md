1. ✨**_Bytpax_**✨
2. Purposes of the program: 😎
   - Monitoring the analytic about athlets depend on type of sport;
   - Historical episodes about this sport evolution;
   - Whole history of records about choosen sport;
   - Monitoring sport matches and competitions;
   - Insight news about sport;
   - Provide user interface, with commonly operating;
     **_But the main point - is make daily sport events monitoring easily for people_** 😇
3. Users of application:👨🏻‍💼
   - Administrator(admin) - the main tasks and abilities are update the analytics, news and historicals data about sport(CRUD);
   - User - ability to consume pleasant view(using grafical user interface) of information about sport: analytics, episodes and competitions;
4. Main logic of application(MVC): 🎯
   - Database;
   - Business logic;
   - View(ilustration) logic;
5. Application Opportunities: ✅
   - Consuming and enter out any information(especially about sport);
   - Make analytics conclusion about any events in sport;
   - Make it comfortable to read it information from the app;
   - Also the others not too major functionality...
6. User web interface techonology: 📱
   - ASP.NET

7. Team Membership: 🦔 🦔 🦔
    - Oleksandra Butko 👩🏻‍✈️
    - Ilya  Bondarenko 👨‍✈️
    - Roman Shnep 👨‍✈️
    
Thank you for attention... ☺️


#  ASP.NET Core + PostgreSQL Проєкт

Цей проєкт реалізує веб-додаток на основі ASP.NET Core з використанням Entity Framework Core та бази даних PostgreSQL.

---

##  Вимоги до запуску

###  Необхідне ПЗ:

- [.NET SDK](https://dotnet.microsoft.com/en-us/download) ≥ **7.0**
- [PostgreSQL](https://www.postgresql.org/download/) ≥ **13**
- [Entity Framework Core CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) ≥ **7.0**
- [Visual Studio](https://visualstudio.microsoft.com/) або [Visual Studio Code](https://code.visualstudio.com/)
- `dotnet-ef` інструмент

---

##  Перевірка середовища
### Можна здійснити в терміналі за такими командами:

```bash
# Перевірка версії .NET SDK
dotnet --version

# Повна інформація про середовище
dotnet --info

# Перевірка Entity Framework CLI
dotnet ef --version

# Перевірка версії PostgreSQL
psql --version


Запуск проекту:
1.  Клонування репозиторію:
git clone https://github.com/your-username/BytPax-Project.git
cd BytPax-Project

2. Налаштування підключення до БД у файлі appsettings.json:
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=mydb;Username=postgres;Password=yourpassword"
}

3. Застосування міграцій (EF Core) за допомогою вбудованого термінала:
# Якщо міграції вже є:
dotnet ef database update
# Якщо міграції немає:
dotnet ef migrations add InitialCreate
dotnet ef database update

4.Запуск додатку:
dotnet run або якщо використовується Visual Studio то Ctrl + F5

5. Підключення PostgreSQL
Для роботи з PostgreSQL потрібен NuGet пакет:
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

6. Структура проекту:
|--BytPax
    ├── Properties/
    ├── wwwroot/
    ├── Areas/
        ├── Admin/
            ├── Controllers/
            ├── Models/
            ├── Views/
    ├── Controllers/ 
    ├── Data/
        ├── Database/         #data storage using data base & EF
        ├── JsonStorage.cs/   #alternative data base storage  
    ├── Instructions/
        ├── IDataStorage.cs/  #data storage interface
    ├── Migrations/
    ├── Models/
    ├── Repositories/
        ├── Repository.cs/
    ├── Services/
    ├── Views/
    ├── appsettings.json
    ├── Program.cs
    ├── Program.cs
├── BytPax.Tests/
    ├── Services/
