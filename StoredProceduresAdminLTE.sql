
--PROCEDIMIENTOS ALMACENADOS APOYOS--

--==============================================================================================--
CREATE PROCEDURE Apoyos_ObtenerId (@IdApoyo INT)
AS
BEGIN
SELECT * FROM Apoyos WHERE IdApoyo = @IdApoyo
END
--==============================================================================================--
CREATE PROCEDURE Apoyos_Listar
AS
BEGIN
SELECT * FROM Apoyos
END
--==============================================================================================--
CREATE PROCEDURE Apoyos_Crear (@Nombre NVARCHAR(50), @Descripcion NVARCHAR(100), @Cantidad INT)
AS
BEGIN
INSERT INTO Apoyos (Nombre, Descripcion, Cantidad) VALUES (@Nombre, @Descripcion, @Cantidad)
SELECT SCOPE_IDENTITY()
END
--==============================================================================================--
CREATE PROCEDURE Apoyos_Actualizar (@IdApoyo INT, @Nombre NVARCHAR(50), @Descripcion NVARCHAR(100), @Cantidad INT)
AS
BEGIN
UPDATE Apoyos SET Nombre = @Nombre, Descripcion = @Descripcion, Cantidad = @Cantidad WHERE IdApoyo = @IdApoyo
END
--==============================================================================================--
CREATE PROCEDURE Apoyos_Eliminar (@IdApoyo INT)
AS
BEGIN
DELETE FROM Apoyos WHERE IdApoyo = @IdApoyo
END
--==============================================================================================--

--PROCEDIMIENTOS ALMACENADOS Habitantes--

--==============================================================================================--
CREATE PROCEDURE Habitantes_ObtenerId (@IdHabitante INT)
AS
BEGIN
SELECT * FROM Habitantes WHERE IdHabitante = @IdHabitante
END
--==============================================================================================--
CREATE PROCEDURE Habitantes_Listar
AS
BEGIN
SELECT * FROM Habitantes ORDER BY ApePat
END
--==============================================================================================--
CREATE PROCEDURE Habitantes_Crear (@Nombre NVARCHAR(50), @ApePat NVARCHAR(50), @ApeMat NVARCHAR(50), @Sexo NVARCHAR(7),
@Direccion NVARCHAR(100), @FechaNac DATETIME, @Telefono VARCHAR(14))
AS
BEGIN
INSERT INTO Habitantes (Nombre, ApePat, ApeMat, Sexo, Direccion, FechaNac, Telefono)
VALUES (@Nombre, @ApePat, @ApeMat, @Sexo, @Direccion, @FechaNac, @Telefono)
SELECT SCOPE_IDENTITY()
END
--==============================================================================================--
CREATE PROCEDURE Habitantes_Actualizar (@IdHabitante INT, @Nombre NVARCHAR(50), @ApePat NVARCHAR(50), @ApeMat NVARCHAR(50), @Sexo NVARCHAR(7),
@Direccion NVARCHAR(100), @FechaNac DATETIME, @Telefono VARCHAR(14))
AS
BEGIN
UPDATE Habitantes SET Nombre = @Nombre, ApePat = @ApePat, ApeMat= @ApeMat, Sexo= @Sexo,
Direccion = @Direccion, FechaNac = @FechaNac, Telefono = @Telefono WHERE IdHabitante = @IdHabitante
END
--==============================================================================================--
CREATE PROCEDURE Habitantes_Eliminar (@IdHabitante INT)
AS
BEGIN
DELETE FROM Habitantes WHERE IdHabitante = @IdHabitante
END
--==============================================================================================--

--PROCEDIMIENTOS ALMACENADOS Beneficios--

--==============================================================================================--
CREATE PROCEDURE Beneficios_ObtenerId (@IdBeneficio INT)
AS
BEGIN
SELECT * FROM Beneficios WHERE IdBeneficio = @IdBeneficio
END
--==============================================================================================--
CREATE PROCEDURE Beneficios_Listar
AS
BEGIN
SELECT Beneficios.IdBeneficio, h.Nombre AS Habitante, a.Nombre AS Apoyo, Beneficios.Cantidad, e.Estado AS Estado, Fecha
FROM Beneficios
INNER JOIN Habitantes h
ON h.IdHabitante = Beneficios.IdHabitante
INNER JOIN Apoyos a
ON a.IdApoyo = Beneficios.IdApoyo
INNER JOIN Estados e
ON e.IdEstado = Beneficios.IdEstado
ORDER BY a.Nombre
END
--==============================================================================================--
CREATE PROCEDURE Beneficios_Crear (@IdHabitante INT, @IdApoyo INT, @Cantidad INT, @IdEstado INT, @Fecha DATETIME)
AS
BEGIN
INSERT INTO Beneficios (IdHabitante, IdApoyo, Cantidad, IdEstado, Fecha)
VALUES (@IdHabitante, @IdApoyo, @Cantidad, @IdEstado, @Fecha)

UPDATE Apoyos SET Cantidad -= @Cantidad WHERE IdApoyo = @IdApoyo

SELECT SCOPE_IDENTITY()
END

--==============================================================================================--
CREATE PROCEDURE Beneficios_Actualizar(@IdBeneficio INT, @IdHabitante INT, @IdApoyo INT, @Cantidad INT, @IdEstado INT, @Fecha DATETIME)
AS
BEGIN
UPDATE Beneficios SET IdHabitante = @IdHabitante, IdApoyo = @IdApoyo, Cantidad = @Cantidad, IdEstado = @IdEstado, Fecha = @Fecha
WHERE IdBeneficio = @IdBeneficio
END

SET DATEFORMAT dmy

EXEC Beneficios_Actualizar 3,3,3,2,1,'17-08-2000'
--==============================================================================================--
CREATE PROCEDURE Beneficios_Eliminar (@IdBeneficio INT)
AS
BEGIN
DELETE FROM Beneficios WHERE IdBeneficio = @IdBeneficio
END
--==============================================================================================--


SELECT Beneficios.IdBeneficio, h.Nombre AS Habitante, a.Nombre AS Apoyo, Beneficios.Cantidad, e.Estado AS Estado, Fecha
FROM Beneficios
INNER JOIN Habitantes h
ON h.IdHabitante = Beneficios.IdHabitante
INNER JOIN Apoyos a
ON a.IdApoyo = Beneficios.IdApoyo
INNER JOIN Estados e
ON e.IdEstado = Beneficios.IdEstado
