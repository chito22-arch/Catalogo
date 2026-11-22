-- ============================================================
-- Práctica 3 - Catálogo de videojuegos: acceso a BBDD MySQL
-- Crea la base de datos, la tabla Usuarios y los usuarios de prueba
-- Ejecutar como root:  mysql -u root -p < database.sql
-- ============================================================

DROP DATABASE IF EXISTS catalogo_db;
CREATE DATABASE catalogo_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE catalogo_db;

CREATE TABLE Usuarios (
    id              INT AUTO_INCREMENT PRIMARY KEY,
    nombre_usuario  VARCHAR(50)  NOT NULL UNIQUE,
    password        VARCHAR(100) NOT NULL
);

-- Usuarios de prueba
INSERT INTO Usuarios (nombre_usuario, password) VALUES
    ('admin',    '1234'),
    ('jugador1', 'pass2024'),
    ('profesor', 'dam123');

-- Usuario de MySQL propio de la aplicación (mínimos privilegios: solo SELECT e INSERT)
CREATE USER IF NOT EXISTS 'catalogo_app'@'localhost' IDENTIFIED BY 'Catalogo2026!';
GRANT SELECT, INSERT ON catalogo_db.Usuarios TO 'catalogo_app'@'localhost';
FLUSH PRIVILEGES;

SELECT * FROM Usuarios;
