
CREATE DATABASE AdminLTE
GO
USE AdminLTE
GO

--TABLAS--
CREATE TABLE Apoyos
(
IdApoyo INT IDENTITY PRIMARY KEY,
Nombre NVARCHAR(50) NOT NULL,
Descripcion NVARCHAR(100) NOT NULL,
Cantidad INT NOT NULL,
CHECK (Cantidad>=0)
);
GO

CREATE TABLE Estados
(
IdEstado INT IDENTITY PRIMARY KEY,
Estado NVARCHAR(50) NOT NULL
);

CREATE TABLE Habitantes
(
IdHabitante INT IDENTITY PRIMARY KEY,
Nombre NVARCHAR(50) NOT NULL,
ApePat NVARCHAR(50) NOT NULL,
ApeMat NVARCHAR(50) NOT NULL,
Sexo NVARCHAR(7) NOT NULL,
Direccion NVARCHAR(100) NOT NULL,
FechaNac DATETIME NOT NULL,
Telefono VARCHAR(14) NOT NULL,
);
GO

SET DATEFORMAT dmy
GO

CREATE TABLE Usuarios
(
IdUsuario INT IDENTITY PRIMARY KEY,
Nombre NVARCHAR(50) NOT NULL,
ApePat NVARCHAR(50) NOT NULL,
ApeMat NVARCHAR(50) NOT NULL,
Email NVARCHAR(256) NOT NULL,
EmailNormalizado NVARCHAR(256) NOT NULL,
PasswordHash NVARCHAR(MAX) NOT NULL
)
GO

CREATE TABLE Beneficios
(
IdBeneficio INT IDENTITY PRIMARY KEY ,
IdHabitante INT,
IdApoyo INT,
Cantidad INT NOT NULL,
IdEstado INT,
Fecha DATETIME,

CONSTRAINT FK_Beneficio_Habitante FOREIGN KEY (IdHabitante) REFERENCES Habitantes (IdHabitante),
CONSTRAINT FK_Beneficio_Apoyo FOREIGN KEY (IdApoyo) REFERENCES Apoyos (IdApoyo),
CONSTRAINT FK_Beneficio_Estado FOREIGN KEY (IdEstado) REFERENCES Estados (IdEstado)
);


DROP TABLE Beneficios
GO
DROP TABLE Apoyos 
GO
DROP TABLE Estados
GO
DROP TABLE Habitantes
GO
DROP TABLE Usuarios
GO




