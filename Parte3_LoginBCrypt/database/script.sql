-- Ejecutar en MySQL Workbench
CREATE DATABASE IF NOT EXISTS login_db;
USE login_db;

-- Tabla de usuarios (guarda el HASH, nunca la contraseña en texto plano)
CREATE TABLE IF NOT EXISTS usuarios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre_usuario VARCHAR(50) NOT NULL UNIQUE,
    nombre_completo VARCHAR(100) NOT NULL,
    password_hash VARCHAR(100) NOT NULL,
    fecha_creacion DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Tabla de intentos (exitosos y fallidos) para análisis posterior
CREATE TABLE IF NOT EXISTS intentos_login (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario VARCHAR(50) NOT NULL,
    exitoso TINYINT(1) NOT NULL,
    fecha DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Consultas útiles para el análisis
-- SELECT * FROM usuarios;
-- SELECT * FROM intentos_login ORDER BY fecha DESC;
-- SELECT usuario, SUM(exitoso = 0) AS fallidos, SUM(exitoso = 1) AS exitosos
--   FROM intentos_login GROUP BY usuario;
