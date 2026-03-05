CREATE DATABASE ApiColombiaDB
GO

USE ApiColombiaDB;
GO

-- ENTIDAD REGION 
CREATE TABLE Region (
    Id INT IDENTITY(1,1) PRIMARY KEY, 
    ExternalId INT NULL,              
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(500)
);
GO

-- ENTIDAD USER
CREATE TABLE [User] (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(500) NOT NULL,
);
GO

-- Usuario 
INSERT INTO [User] (Username, PasswordHash)
VALUES ('admin', '123');