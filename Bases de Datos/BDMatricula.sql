CREATE DATABASE BDMatricula
go

USE BDMatricula
go

/*
  USE MASTER
  GO

  DROP DATABASE BDMatricula
  go
*/
----Tablas----
Create TABLE tblFACultad
(
  id_FAC int Primary Key NOT NULL,
  strNombre_FAC varchar (30) NOT NULL
)
go

CREATE TABLE tblPROgrama
(
	id_PRO int Primary Key NOT NULL,
	idFAC_PRO int NOT NULL,
	strNombre_PRO varchar(30) NOT NULL
)
go

CREATE TABLE tblASIgnatura
(
	id_ASI Varchar(10) Primary Key NOT NULL,
	idPRO_ASI int NOT NULL,
	strNombre_ASI varchar(50) NOT NULL
)
go
CREATE TABLE tblPERiodo
(
	id_PER int Primary Key NOT NULL,
	strNombre_PER varchar (30) NOT NULL
)
go
CREATE TABLE tblJORnada
(
	id_JOR int Primary Key NOT NULL,
	strNombre_JOR varchar (10) NOT NULL
)
go

CREATE TABLE tblESTudiante
(
id_EST Varchar(10) Primary Key NOT NULL,
intNroDoc_EST int NOT NULL,
strNombre_EST varchar (50) NOT NULL,
idPRO_EST int NOT NULL,
blnActivo_EST bit default 1 NOT NULL,
idJOR_EST int check (idJOR_EST= 1 or idJOR_EST=2 or idJOR_EST = 3 or idJOR_EST = 4 or idJOR_EST=5)
default 1,
strObservac_EST varchar(500) NULL
)
go

CREATE TABLE tblMATricula
(
id_MAT int identity (1,1) Primary Key NOT NULL,
dtmFecha_MAT datetime default getdate(),
idPER_MAT int NOT NULL,
idEST_MAT Varchar (10) NOT NULL,
idASI_MAT Varchar(10) NOT NULL
)
go


---RELACIONAR LAS TABLAS

ALTER TABLE tblPROgrama add Foreign Key (idFAC_PRO) REFERENCES tblFACultad (id_FAC)
ALTER TABLE tblASIgnatura add Foreign Key (idPRO_ASI) REFERENCES tblPROgrama (id_PRO)
ALTER TABLE tblESTudiante add Foreign Key (idJOR_EST) REFERENCES tblJORnada (id_JOR)
ALTER TABLE tblMATricula add Foreign Key (idPER_MAT) REFERENCES tblPERiodo (id_PER)
ALTER TABLE tblMATricula add Foreign Key (idEST_MAT) REFERENCES tblESTudiante (id_EST)
ALTER TABLE tblMATricula add Foreign Key (idASI_MAT) REFERENCES tblASIgnatura (id_ASI)
ALTER TABLE tblESTudiante add Foreign Key (idPRO_EST) REFERENCES tblPROgrama (id_PRO)
GO


----INSERTAR DATOS---------

---FACULTAD---
INSERT INTO tblFACultad VALUES (1,'Tecnologías'),(2,'Ingenierías')

--PROGRAMA---

INSERT INTO tblPROgrama VALUES (10,1, 'Tecn. Desarollo de Sofware'), (20,2,'Ing. de Sistemas'),
(30,1,'Tecn. em Telecomunicaciones') , (40,2,'Ing. de Telecomunicaciones'),
(50,1, 'Tecn. Electromecánica'),(60,2,'Ing. Electromecánica'),
(70,1, 'Tecn. en Calidad');

--ASIGNATURA--

INSERT INTO tblASIgnatura VALUES
('580204008',10, 'Desarollo de Bases de Datos'),('PDI74',20,'Programación Distribuida'),
('ANT32', 40 , 'Antenas I'),('CXT34',30, 'Circuitos Digitales I'),('EST14',70,'Estadistica'),
('MAT24', 60, 'Materiales 1'), ('MIL44', 70, 'Metrología'), ('CXT44', 30, 'Circuitos Digitales II'),
('580304006', 10, 'Programación de Software'), ('ANT42', 40, 'Antenas II'),
('ACI82', 20, 'Arquitectura de Computadores'),('MQN22', 50, 'Maquinas'),
('CNT34', 50, 'Controles Digitales'), ('MAT34', 60, 'Materiales II'),
('VAI92', 20, 'Visión Artificial'), ('000506001', 10, 'Lógica De Programación Y Laboratorio');

------PERIODO-----------
INSERT INTO tblPERiodo VALUES (57,'2022-2'),(58,'2023-1'),(59,'2023-2'),(60,'2024-1'),(61,'2024-2');

--JORNADA---
INSERT INTO tblJORnada VALUES (1,'Mañana'),(2,'Tarde'),(3,'Noche'),(4,'Única'),(5,'Virtual');

---ESTUDIANTE

INSERT INTO tblESTudiante VALUES
('101010101' , 70500600 ,'Juan P. Aristizábal', 10,1,1, 'Ppto Particip'),
('10102020' , 1020500500,'Mario A. Martínez',20,1,4,'Fondo EPM'),
('10102021' , 1020200200,'Natalia Castrillon',70,0,3,''),
('90909090',1015100100,'Rubén Dario Soto',30,1,1,'Pruebas de todo'),
('40404040',1010101010 ,'Mariana Aguirre', 50,1,2,'Reserva'),
('50505050',2020202020, 'Jazmin Angarita',70,1,4,'Ppto Particip'),
('60606060', 43100100 ,'Maria José Soto Diaz' ,10,0,1,'Suspende Matric'),
('70707070',1414141414,'Antonio Suaréz Lopéz',20,1,3,'Beca Sapiencia'),
('80808080',1400400400,'Maria A.Zapata Rico' ,60,1,2,''),
('20202020',1212121212,'Ana M. Cardona A',40,1,5,'Hija Docente');
go

---PROCEDIMIENTOS ALMACENADOS---

---Facultad--

CREATE PROCEDURE USP_Facultad_BuscarGeneral 
AS
 BEGIN
  SELECT  id_FAC Codigo,
      strNombre_FAC Nombre
  FROM tblFACultad
  ORDER BY Nombre
 END
GO

----JORNADA---
CREATE PROCEDURE USP_Jornada_BuscarGeneral 
AS
 BEGIN
  SELECT  id_JOR Codigo,
      strNombre_JOR Nombre
  FROM tblJORnada
  ORDER BY Codigo
 END
GO

--PROGRAMA--

--Llena el combo de acuerdo a el id de la facultad
CREATE PROCEDURE USP_Programa_LlenarCombo 
@idFac int
AS
 BEGIN
  SELECT  id_PRO Codigo,
      strNombre_PRO Nombre
  FROM tblPROgrama
  WHERE idFAC_PRO= @idFac
  ORDER BY Nombre
 END
GO

--EXEC USP_Programa_LlenarCombo 2

--ESTUDIANTE---
CREATE PROCEDURE usp_EstudiantesXPrograma
@intProg int
AS
 BEGIN
  SELECT  id_EST Carnet,
      strNombre_EST Nombres,
	  strNombre_JOR Jornada,
	  strObservac_EST Observaciones,
	  blnActivo_EST  Activo
  FROM tblESTudiante
    INNER JOIN tblPROgrama ON idPRO_EST= id_PRO
	INNER JOIN tblJORnada ON idJOR_EST= id_JOR
  WHERE id_PRO =@intProg
  ORDER BY Nombres,Activo DESC
 END
GO

--EXEC usp_EstudiantesXPrograma 20

CREATE PROCEDURE USP_Estudiante_BuscarXCodigo
@strCodigo Varchar(10)
AS
 BEGIN
  SELECT id_EST Codigo,
    intNroDoc_EST Nro_Doc,
	strNombre_EST Nombre,
	idFAC_PRO idFacultad,
	idPRO_EST idPrograma,
	blnActivo_EST Activo,
	idJOR_EST idJornada,
	strObservac_EST Observaciones,
	strNombre_PRO Programa
  FROM tblESTudiante
    INNER JOIN tblPROgrama ON id_PRO= idPRO_EST
  WHERE id_EST =@strCodigo
 END
GO

--EXEC USP_Estudiante_BuscarXCodigo '40404040';


CREATE PROCEDURE USP_Estudiante_Grabar
@strCodigo Varchar(10),
@intNroDoc int,
@strNombre Varchar (50),
@intPrograma int,
@bitActivo bit,
@intdjor int,
@strObserv Varchar(500)
AS
  BEGIN
  IF EXISTS( SELECT * FROM tblESTudiante WHERE id_EST = @strCodigo )
  Begin --INICIO DE EL IF SI EXISTE EL CODIGO DE EL ESTUDIANT
    SELECT -1 AS Rpta
    Return
  END--FIN DE EL IF
  ELSE
	Begin--INICIO DE EL ELSE, SI EL CODIGO NO EXISTE
	BEGIN TRANSACTION tx
	INSERT INTO tblESTudiante Values (@strCodigo, @intNroDoc, @strNombre,
	@intPrograma, @bitActivo, @intdjor, @strObserv);
	IF (@@ERROR >0)
	  BEGIN
	   ROLLBACK TRANSACTION tx --Si hubo un error en el insertar 
	   SELECT 0 AS Rpta
	   RETURN
      END
     COMMIT TRANSACTION tx
	 SELECT @strCodigo AS Rpta
	 Return
    END --FIN DE EL ELSE
END
GO

--EXEC USP_Estudiante_Grabar '09201010',98100100,'Juan Díaz',70,1,2,null;


CREATE PROCEDURE USP_Estudiante_Modificar
@strCodigo Varchar(10),
@intNroDoc int,
@strNombre Varchar (50),
@intPrograma int,
@bitActivo bit,
@intdjor int,
@strObserv Varchar(500)
AS
 BEGIN
  BEGIN TRANSACTION tx
   UPDATE tblESTudiante
    SET intNroDoc_EST =@intNroDoc,
	strNombre_EST=@strNombre,
	idPRO_EST=@intPrograma,
	blnActivo_EST= @bitActivo,
	idJOR_EST= @intdjor,
	strObservac_EST =@strObserv
   WHERE id_EST= @strCodigo --Actualiza solo el estudiante que tenga el mismo codigo
  IF (@@ERROR >0)
   BEGIN
	   ROLLBACK TRANSACTION tx --Si hubo un error al actualizar 
	   SELECT 0 AS Rpta
	   RETURN
   END
  COMMIT TRANSACTION tx
  SELECT @strCodigo AS Rpta
  Return
END
GO

--EXEC USP_Estudiante_Modificar '09201010',98100100,'Andres Sierra',60,1,3,'ok'
--EXEC USP_Estudiante_BuscarXCodigo '09201010'

---Periodo----

CREATE PROCEDURE USP_Periodo_LlenarCombo
AS
 BEGIN
  SELECT  id_PER Codigo,
      strNombre_PER Nombre
  FROM tblPERiodo
  ORDER BY Codigo ASC
 END
GO

--EXEC USP_Periodo_LlenarCombo


--Asignatura--
CREATE PROCEDURE USP_Asignatura_LlenarCombo
@idPrograma int

AS
 BEGIN
  SELECT  id_ASI Codigo,
      strNombre_ASI Nombre
  FROM tblASIgnatura
  WHERE idPRO_ASI= @idPrograma
  ORDER BY Nombre
 END
GO

--EXEC USP_Asignatura_LlenarCombo 40

--MATRICULA---
CREATE PROCEDURE USP_Matricula_Buscar_Asig_Per
@strCarnet Varchar(10),
@intPeriodo int
AS
 BEGIN
  SELECT  idASI_MAT Asig
  FROM tblMATricula
  WHERE idEST_MAT= @strCarnet AND idPER_MAT= @intPeriodo
 END
GO

--EXEC USP_Matricula_Buscar_Asig_Per '20202020',57


CREATE PROCEDURE USO_Matricula_LlenarGrid
@strCarnet Varchar(10),
@strPeriodo int

AS
 BEGIN 
  SELECT id_MAT Cod,
	  strNombre_PER Periodo,
	  id_ASI Codigo_Asig,
	  strNombre_ASI Asignatura,
	  dtmFecha_MAT Fecha
  FROM tblMATricula
   INNER JOIN tblPERiodo ON idPER_MAT = id_PER
   INNER JOIN tblASIgnatura ON id_ASI = idASI_MAT
  WHERE idEST_MAT = @strCarnet AND idPER_MAT = @strPeriodo
 END
GO

--INSERT INTO tblMATricula VALUES (GETDATE(),57,'20202020','MAT24')
--EXEC USO_Matricula_LlenarGrid '20202020',57


---proceso grabar matricula--
CREATE PROCEDURE USP_Matricula_Grabar
@intPeriodo int,
@strCarnet Varchar (10),
@strAsig Varchar(10)

AS
 BEGIN
  IF EXISTS (SELECT * FROM tblMATricula WHERE idPER_MAT =@intPeriodo AND idEST_MAT =@strCarnet AND idASI_MAT =@strAsig)
    BEGIN
	   SELECT -1 AS Rpta
	   Return
    END
  ELSE
   BEGIN
    BEGIN TRANSACTION tx
	 INSERT INTO tblMATricula (dtmFecha_MAT,idPER_MAT,idEST_MAT,idASI_MAT)
	 VALUES  (GETDATE(),@intPeriodo,@strCarnet,@strAsig);

	 IF (@@ERROR >0)
	  BEGIN 
	   ROLLBACK TRANSACTION tx
	   SELECT 0 AS Rpta
	   Return
      END
     COMMIT TRANSACTION tx
	 SELECT @strAsig AS Rpta 
	 Return 
    END
  END
GO

--EXEC USP_Matricula_Grabar 57,'101010101','CNT34'

--Borrar Matricula--
Create PROCEDURE USP_Matricula_Borrar
@intldMat int
AS 

 BEGIN TRANSACTION tx
  DELETE FROM tblMATricula
   WHERE id_MAT = @intldMat;
  IF (@@ERROR >0)
   BEGIN 
    ROLLBACK TRANSACTION tx
	SELECT 0 AS Rpta
	Return
   END
  COMMIT TRANSACTION tx
  SELECT @intldMat AS Rpta
  Return
GO

--EXEC USP_Matricula_Borrar 2