# ISPApplication

ISPApplication — это десктопное приложение для автоматизации работы автосервиса. Проект написан на C# с использованием WPF и Entity Framework для работы с базой данных.

### Функциональность

*   **Учёт клиентов:** Добавление, редактирование и удаление информации о клиентах.
*   **Учёт автомобилей:** Ведение базы автомобилей с привязкой к владельцам.
*   **Заказы и ремонты:** Создание заказов на ремонт, отслеживание статуса выполнения.
*   **Складской учёт:** Учёт запчастей и материалов на складе (если есть).
*   **Финансовый учёт:** Формирование счетов, учёт оплат (если есть).
*   **Отчёты:** Генерация отчётов по выполненным работам, клиентам, финансам.

### Стек технологий

*   **Язык:** C#
*   **Платформа:** .NET Framework / .NET Core 5
*   **Интерфейс:** WPF
*   **База данных:** Entity Framework,  MSSQL SQL Server
*   **Инструменты:** Git, Visual Studio

### Установка и запуск

## Требования

*   [.NET Framework / .NET SDK](https://dotnet.microsoft.com/download) (нужная версия)
*   [MSSQL SQL Server](https://www.microsoft.com/ru-ru/sql-server/sql-server-downloads) 
*   Visual Studio 2022 / 2019 (или любая другая IDE)

## Инструкция по запуску

1.  **Клонировать репозиторий**
    ```bash
    git clone https://github.com/IlyaRx/ISPApplication.git
    cd ISPApplication
    ```
2. **Открыть решение в Visual Studio**
    Найди файл ISPApplication.sln (или ISAuto.sln) в корне проекта и открой его.

3. **Восстановить пакеты NuGet**
    В Visual Studio: правой кнопкой по решению -> "Восстановить пакеты NuGet".

4. **Настроить базу данных**
   SQL Server: измени строку подключения в файле App.config или appsettings.json под свой сервер.

5. **Запустить приложение**
   Нажми F5 или кнопку "Start" в Visual Studio.


### Скриншоты
![Карзина](https://github.com/user-attachments/assets/9c9dcb80-834f-4c71-af7f-7b5b742c4270)
![Каталог 1](https://github.com/user-attachments/assets/6913322b-5c4c-4246-b272-e30981bbb2d1)
![Каталог 2](https://github.com/user-attachments/assets/dde759e9-9972-42c9-b1a3-7409f48b21c2)

### Контакты
Автор: Илья (IlyaRx)
GitHub: https://github.com/IlyaRx
Почта: ilyakiy41@gmail.com
