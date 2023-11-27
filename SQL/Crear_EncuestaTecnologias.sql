CREATE DATABASE EncuestaTecnologias;

USE EncuestaTecnologias;

CREATE TABLE Paises (
    PaisID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL
);

CREATE TABLE LenguajesProgramacion (
    LenguajeID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL
);

CREATE TABLE RolesTI (
    RolID INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion NVARCHAR(100) NOT NULL
);

CREATE TABLE Encuestas (
    EncuestaID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(100) NOT NULL,
    PaisID INT,
    RolID INT,
    LenguajePrimarioID INT,
    LenguajeSecundarioID INT,
    FOREIGN KEY (PaisID) REFERENCES Paises(PaisID),
    FOREIGN KEY (RolID) REFERENCES RolesTI(RolID),
    FOREIGN KEY (LenguajePrimarioID) REFERENCES LenguajesProgramacion(LenguajeID),
    FOREIGN KEY (LenguajeSecundarioID) REFERENCES LenguajesProgramacion(LenguajeID)
);

CREATE TABLE Reportes (
    LenguajeID INT PRIMARY KEY,
    NumeroDePosicion INT,
    ClasificacionPorcentual float,
    DiferenciaPorcentual float,
    FOREIGN KEY (LenguajeID) REFERENCES LenguajesProgramacion(LenguajeID)
);
