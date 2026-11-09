# Parcial #3 – Herramientas de la Programación Aplicada III (.NET)

- **Universidad Tecnológica de Panamá** – Facultad de Ingeniería en Sistemas
- **Campus:** Víctor Levi Sasso
- **Grupo:** 1IL133 · II Semestre 2026
- **Instructor:** Ing. Irina Fong
- **Estudiante:** Chelsy Rios

---

## Estructura del repositorio

| Carpeta | Contenido |
|---|---|
| `Parte1_Figuras` | Clase abstracta `Figura`, clases derivadas, arreglo `Figura[]` y polimorfismo |
| `Parte2_Json` | Serialización de un objeto a JSON con Newtonsoft.Json |
| `Parte3_LoginBCrypt` | Login con Windows Forms, BCrypt.Net-Next y MySQL |
| `Parte3_LoginBCrypt/database` | `script.sql` (estructura) y `backup_login_db.sql` (backup con datos) |
| `capturas` | Capturas de pantalla de la ejecución |

---

## Parte 1: Clase abstracta y polimorfismo (15 pts)

Se diseñó una jerarquía de clases en C#:

- **Clase abstracta `Figura`** con los métodos abstractos `CalcularArea()` y `CalcularPerimetro()`.
- **Clases derivadas** `Rectangulo`, `Cuadrado` y `Triangulo`, que desarrollan ambos métodos con `override`.
- **Arreglo unidimensional `Figura[]`** que almacena instancias de las tres figuras.
- **Recorrido polimórfico** con un bucle `for` que muestra el nombre de la clase, el área y el perímetro de cada figura.

**Ejecución:**

```bash
cd Parte1_Figuras
dotnet run
```

**Captura de la salida:**

<img width="1460" height="701" alt="Captura de pantalla 2026-11-09 103048" src="https://github.com/user-attachments/assets/8fb7d91d-fc6a-4401-b17e-a5c1d48cd230" />

---

## Parte 2: JSON con Newtonsoft.Json (15 pts)

Se instaló el paquete con `dotnet add package Newtonsoft.Json` y se serializó un objeto `persona` (Nombre, Edad y Carrera) con `JsonConvert.SerializeObject`.

**Ejecución:
```bash
cd Parte2_Json
dotnet run
```

**Captura de la salida:**

<img width="1470" height="660" alt="Captura de pantalla 2026-11-09 103418" src="https://github.com/user-attachments/assets/3263c656-6500-423e-897c-5116fc32c408" />

---

## Parte 3: Login con BCrypt (20 pts)

Aplicación de **Windows Forms** en C# que usa la biblioteca **BCrypt.Net-Next** para generar y verificar el hash de las contraseñas, con una base de datos **MySQL**.

### Base de datos (`login_db`)

| Tabla | Descripción |
|---|---|
| `usuarios` | Guarda el nombre de usuario, el nombre completo y el **hash** BCrypt (nunca la contraseña en texto plano) |
| `intentos_login` | Guarda cada intento de acceso: usuario, si fue exitoso o fallido y la fecha, para un análisis posterior |

El script de creación está en `Parte3_LoginBCrypt/database/script.sql` y el backup con datos en `Parte3_LoginBCrypt/database/backup_login_db.sql`.

**Captura de las tablas creadas en MySQL Workbench:**

<img width="1207" height="1077" alt="Captura de pantalla 2026-11-09 104841" src="https://github.com/user-attachments/assets/d54da17c-3f4e-418b-b2fe-ba55fa661276" />

### Funcionamiento

1. **Registro de usuario:** la contraseña se convierte en hash con `BCrypt.HashPassword()` y el hash se guarda en la tabla `usuarios`. (5 pts)
2. **Inicio de sesión:** se obtiene el hash del usuario y se compara con `BCrypt.Verify()`. Cada intento, exitoso o fallido, se guarda en `intentos_login`. (5 pts)
3. **Acceso exitoso:** se muestra una pantalla de bienvenida con el nombre del usuario. (5 pts)

**Registro de un usuario:**

<img width="353" height="392" alt="Captura de pantalla 2026-11-09 110206" src="https://github.com/user-attachments/assets/646ee6b4-7545-466e-ac8c-431ed82626ba" />

**Hash BCrypt generado y guardado en la base de datos:**

<img width="676" height="477" alt="Captura de pantalla 2026-11-09 110215" src="https://github.com/user-attachments/assets/54008268-aeb7-4d1d-b96b-8d2b9c213a6c" />

**Intento fallido (contraseña incorrecta):**

<img width="347" height="297" alt="Captura de pantalla 2026-11-09 110309" src="https://github.com/user-attachments/assets/e4e70cbe-a9ec-41b3-acea-68fa9c0fe106" />

**Acceso exitoso con el nombre del usuario:**

<img width="515" height="382" alt="Captura de pantalla 2026-11-09 110254" src="https://github.com/user-attachments/assets/e7bf5618-c77a-4e1b-9c67-5bb2573f15c5" />

**Tabla `intentos_login` en MySQL Workbench (intentos exitosos y fallidos):**

<img width="408" height="212" alt="Captura de pantalla 2026-11-09 110343" src="https://github.com/user-attachments/assets/7b404409-2a96-40e4-9fff-1d8489836bc0" />

### Cómo ejecutar la Parte 3

1. Abrir MySQL Workbench y ejecutar `database/script.sql` (o restaurar `database/backup_login_db.sql`).
2. En `Db.cs`, cambiar `TU_PASSWORD` por la contraseña de MySQL.
3. Ejecutar el proyecto:

```bash
cd Parte3_LoginBCrypt
dotnet run
```

También se puede abrir `Parte3_LoginBCrypt.csproj` en Visual Studio y presionar **F5**. Requiere Windows y el SDK de .NET.

---

## Tecnologías utilizadas

- C# / .NET
- Windows Forms
- MySQL y MySQL Workbench
- BCrypt.Net-Next
- MySqlConnector
- Newtonsoft.Json
