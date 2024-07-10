# MyPizza

![MyPizza logo](https://t4.ftcdn.net/jpg/02/11/55/17/360_F_211551718_Ol7eOQYNDK5S8pbEHMkagk9kbdYTJ2iX.jpg)

## ✨ Використані технології ✨

### Back-End
- **ASP.NET Core**
  
  📚 Залежності
  - *AutoMapper*
  - *Bogus*
  - *FluentValidation*
  - *Microsoft.AspNetCore.Authentication.JwtBearer*
  - *Microsoft.AspNetCore.Identity.EntityFrameworkCore*
  - *Microsoft.EntityFrameworkCore.Tools*
  - *Npgsql.EntityFrameworkCore.PostgreSQL*
  - *SixLabors.ImageSharp*

### Front-End
- **React**
  
  📚 Залежності
  - *dnd-kit*  
  - *hookform*
  - *reduxjs/toolkit*
  - *tailwind*
  - *react-helmet* 
  - *react-hook-form* 
  - *zod*

### База даних
- **PostgreSQL**

### Інші технології
- **Docker**
- **Nginx**
- **Git**
  
## 🔧 Налаштування проекту

### Створення файлу `.env` у папці `client`

Щоб налаштувати змінні оточення для клієнтської частини проекту, вам необхідно створити файл `.env` у папці `client` та додати наступний рядок:

```env
VITE_API_URL=http://localhost:5174
```

### Запуск Docker Compose
У кореневій директорії проекту запустіть команди:

```
docker-compose build
```
та
```
docker-compose up -d
```
Відкрийте браузер і перейдіть за адресою:
```
http://localhost:5173
```

## 📸 Скріншоти
![Screens](/screenshots/scr1.png)
![Screens](/screenshots/scr2.png)
![Screens](/screenshots/scr3.png)
![Screens](/screenshots/scr4.png)
![Screens](/screenshots/scr5.png)
![Screens](/screenshots/scr6.png) 
