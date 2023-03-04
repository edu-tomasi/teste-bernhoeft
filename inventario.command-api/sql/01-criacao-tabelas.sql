USE master

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'bernhoeft')
BEGIN
    CREATE DATABASE bernhoeft
END;

USE bernhoeft

IF OBJECT_ID('Categoria', 'U') IS NULL
BEGIN
    CREATE TABLE Categoria (
        Id UNIQUEIDENTIFIER NOT NULL, 
        Nome VARCHAR(200) NOT NULL, 
        Ativo BIT DEFAULT 1, 
        CONSTRAINT PK_Categoria PRIMARY KEY (Id)
    );
END;

IF OBJECT_ID('Produto', 'U') IS NULL
BEGIN
  CREATE TABLE Produto (
    Id UNIQUEIDENTIFIER NOT NULL, 
    Nome VARCHAR(200) NOT NULL, 
    Descricao VARCHAR(400) DEFAULT '', 
    Preco MONEY, 
    IdCategoria UNIQUEIDENTIFIER NOT NULL, 
    Ativo BIT DEFAULT 1, 
    CONSTRAINT PK_Produto PRIMARY KEY (Id),
    CONSTRAINT FK_CategoriaProduto FOREIGN KEY (IdCategoria) REFERENCES Categoria(Id)
    );
END;


-- INSERT INTO Categoria VALUES (NEWID(), 'Copos', 1)
-- INSERT INTO Produto VALUES (NEWID(), 'Copo Floripa Eco Festival 2022', 'Copo exclusivo para o evento Floripa Eco Festival, edição 2022', 9.99, (SELECT TOP 1 Id FROM Categoria WHERE Nome LIKE 'Copos'), 1)
-- INSERT INTO Produto VALUES (NEWID(), 'Copo Floripa Eco Festival 2021', 'Copo exclusivo para o evento Floripa Eco Festival, edição 2021', 9.99, (SELECT TOP 1 Id FROM Categoria WHERE Nome LIKE 'Copos'), 0)