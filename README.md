# C# Console Projects

Пак консольних апок на **C#**, які демонструють основні принципи ООП (абстракція, інкапсуляція, композиція).

![.NET](https://img.shields.io/badge/-10.0-512BD4?style=flat&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-13-512BD4?style=flat&logo=csharp&logoColor=white)
![Visual Studio](https://img.shields.io/badge/Visual_Studio-5C2D91?style=flat&logo=visualstudio&logoColor=white)

---

## Зміст

- [Проєкти](#проєкти)
- [Структура репозиторію](#структура-репозиторію)
- [Вимоги](#вимоги)
- [Запуск](#запуск)

---

## Проєкти

| Проєкт | Опис | Ключові теми |
|--------|------|--------------|
| [Task1_StoreSimulation](Task1_StoreSimulation/README.md) | Симуляція магазину: випадкові покупці купують випадкові товари, програма рахує прибуток і статистику | Класи, `struct`, LINQ, `Random`, агрегація даних |
| [Task2_SocialNetworkSimulation](Task2_SocialNetworkSimulation/README.md) | Імітація соціальної мережі Parodygram: користувачі, пости, лайки та коментарі, інтерактивне консольне меню | Класи, `struct`, `List<T>`, меню на `switch`, валідація введення |

### Task1_StoreSimulation

Програма генерує задану кількість покупців із випадковими картками та балансами. Вони по черзі купують випадкові товари з асортименту, доки вистачає грошей і товарів. Наприкінці виводяться прибуток магазину, найактивніші покупці та залишки на складі.

### Task2_SocialNetworkSimulation

Користувач обирає один із готових профілів і може переглядати останні пости інших користувачів, ставити лайки, писати коментарі, а також створювати й видаляти власні пости. Дані зберігаються в пам'яті на час роботи програми.

Детальний опис, схеми класів і приклади виводу є в README кожного проєкту.

---

## Структура репозиторію

Рішення `Practise4` складається з двох проєктів:

```text
Practise4/
├── Task1_StoreSimulation/
│   ├── Program.cs
│   ├── README.md
│   └── Task1_StoreSimulation.csproj
├── Task2_SocialNetworkSimulation/
│   ├── Program.cs
│   ├── README.md
│   └── Task2_SocialNetworkSimulation.csproj
├── .gitignore
├── Practise4.slnx
└── README.md
```

---

## Вимоги

- [.NET SDK](https://dotnet.microsoft.com/download) версії 6.0 або новішої
- Термінал із підтримкою UTF-8 для коректного відображення кирилиці (наприклад, Windows Terminal або консоль Visual Studio)

---

## Запуск

```bash
# 1. Клонувати репозиторій
git clone https://github.com/Darkc0de-nvm/practise4_oop.git

# 2. Перейти до потрібного проєкту та запустити його
cd Task1_StoreSimulation
dotnet run
```

Для запуску іншого проєкту поверніться в корінь репозиторію та перейдіть у відповідну папку:

```bash
cd ../Task2_SocialNetworkSimulation
dotnet run
```

Також рішення можна відкрити у **Visual Studio** або **VS Code**, обрати потрібний проєкт стартовим і запустити.