# Поддержка и тестирование программных модулей

## Практическая работа №6 «Создание автоматизированных unit-тестов» (части 1–3)

**Цель работы:** провести тестирование разработанных программных модулей с использованием средств автоматизации (Visual Studio / `dotnet test`) методом **«белого ящика»**; закрепить работу с **Entity Framework 6** через сборки **`EntityFramework.dll`** и **`EntityFramework.SqlServer.dll`** (лежат в [`Bank/libs/`](Bank/libs/), подключены по `HintPath` в [`Bank/Bank/Bank.csproj`](Bank/Bank/Bank.csproj)).

**Исполнитель:** Богданова М.Н., группа ЗИСИП-423.

**Репозиторий:** [github.com/mildxw/pit](https://github.com/mildxw/pit) · ветка **`pr6`**.

---

### Структура решения

Папка [`Bank/`](Bank/):

| Проект | Назначение |
|--------|------------|
| **Bank** | Консольное приложение: `BankAccount` (`Debit`, `Credit`), XML-документация; **EF 6**: `BankDbContext`, `BankAccountRecord`, строка **`BankDb`** в [`App.config`](Bank/Bank/App.config) (LocalDB). |
| **BankTests** | Пять модульных тестов **MSTest** по методичке. |

Открытие в Visual Studio: **`Bank/Bank.slnx`**. Команды из каталога `Bank`:

```bash
dotnet build Bank/Bank.slnx
dotnet test Bank/Bank.slnx
```

### Часть 1 — класс `BankAccount` и unit-тесты

В коде добавлены XML-комментарии (`summary`, `param`, `exception`, `value` для свойств).

Тестовые методы:

- `Debit_WithValidAmount_UpdatesBalance`
- `Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange`
- `Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange`
- `Credit_WithValidAmount_UpdatesBalance`
- `Credit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange`

При первом прогоне типичная ошибка — в `Debit` вместо `-=` указано `+=`; после исправления и констант сообщений тесты проходят.

Консоль: **$11.99** → **+$5.77** → **−$11.22** → **$6.54**.

### Части 2–3 — EF 6 и SQL Server

- Сборки из **`Bank/libs/`** (см. [`libs/README.txt`](Bank/libs/README.txt)); при необходимости замени на файлы из методички.
- Провайдер **System.Data.SqlClient**, регистрация в `App.config` → `entityFramework` / `providers`.
- Для сохранения в БД нужен **SQL Server Express LocalDB** (или другой SQL Server — поправь строку подключения). Если БД недоступна, программа выведет сообщение об ошибке после строки с балансом.

### Скриншоты

| № | Описание |
|---|----------|
| 1 | Результат **`dotnet test`** — **5** пройденных тестов |
| 2 | Запуск **Bank** — баланс **$6.54** и строка про EF / ошибка LocalDB |

![Рис. 1 — dotnet test](screenshots/1.png)

![Рис. 2 — консоль Bank](screenshots/2.png)
---

Другие практики: **pr4** (WPF), **pr5** (системное тестирование CS2) — см. соответствующие ветки.
