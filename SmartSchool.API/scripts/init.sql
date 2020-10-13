CREATE DATABASE IF NOT EXISTS SmartSchoolDB;

USE SmartSchoolDB;

CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

CREATE TABLE IF NOT EXISTS `Alunos` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Matricula` int NOT NULL,
    `Nome` text NULL,
    `Sobrenome` text NULL,
    `Telefone` text NULL,
    `DataNascimento` datetime NOT NULL,
    `DataInicioMatricula` datetime NOT NULL,
    `DataFimMatricula` datetime NULL,
    `Ativo` bit NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE IF NOT EXISTS `Cursos` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Nome` text NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE IF NOT EXISTS `Professores` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Registro` int NOT NULL,
    `Nome` text NULL,
    `Sobrenome` text NULL,
    `Telefone` text NULL,
    `DataInicioRegistro` datetime NOT NULL,
    `DataFimRegistro` datetime NULL,
    `Ativo` bit NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE IF NOT EXISTS `AlunosCursos` (
    `AlunoId` int NOT NULL,
    `CursoId` int NOT NULL,
    `DataInicio` datetime NOT NULL,
    `DataFim` datetime NULL,
    PRIMARY KEY (`AlunoId`, `CursoId`),
    CONSTRAINT `FK_AlunosCursos_Alunos_AlunoId` FOREIGN KEY (`AlunoId`) REFERENCES `Alunos` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AlunosCursos_Cursos_CursoId` FOREIGN KEY (`CursoId`) REFERENCES `Cursos` (`Id`) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS `Disciplinas` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `CargaHoraria` int NOT NULL,
    `Nome` text NULL,
    `ProfessorId` int NOT NULL,
    `PreRequisitoId` int NULL,
    `CursoId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Disciplinas_Cursos_CursoId` FOREIGN KEY (`CursoId`) REFERENCES `Cursos` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Disciplinas_Disciplinas_PreRequisitoId` FOREIGN KEY (`PreRequisitoId`) REFERENCES `Disciplinas` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Disciplinas_Professores_ProfessorId` FOREIGN KEY (`ProfessorId`) REFERENCES `Professores` (`Id`) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS `AlunosDisciplinas` (
    `AlunoId` int NOT NULL,
    `DisciplinaId` int NOT NULL,
    `Nota` int NULL,
    `DataInicio` datetime NOT NULL,
    `DataFim` datetime NULL,
    PRIMARY KEY (`AlunoId`, `DisciplinaId`),
    CONSTRAINT `FK_AlunosDisciplinas_Alunos_AlunoId` FOREIGN KEY (`AlunoId`) REFERENCES `Alunos` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AlunosDisciplinas_Disciplinas_DisciplinaId` FOREIGN KEY (`DisciplinaId`) REFERENCES `Disciplinas` (`Id`) ON DELETE CASCADE
);

INSERT INTO `Alunos` (`Id`, `Ativo`, `DataFimMatricula`, `DataInicioMatricula`, `DataNascimento`, `Matricula`, `Nome`, `Sobrenome`, `Telefone`)
VALUES (1, True, NULL, '2020-10-12 20:28:32.173479', '2000-03-14 00:00:00.000000', 1, 'Marta', 'Kent', '+551122223333');
INSERT INTO `Alunos` (`Id`, `Ativo`, `DataFimMatricula`, `DataInicioMatricula`, `DataNascimento`, `Matricula`, `Nome`, `Sobrenome`, `Telefone`)
VALUES (2, True, NULL, '2020-10-12 20:28:32.174145', '2000-09-24 00:00:00.000000', 2, 'Paula', 'Isabela', '+551133334444');
INSERT INTO `Alunos` (`Id`, `Ativo`, `DataFimMatricula`, `DataInicioMatricula`, `DataNascimento`, `Matricula`, `Nome`, `Sobrenome`, `Telefone`)
VALUES (3, True, NULL, '2020-10-12 20:28:32.174151', '2002-06-10 00:00:00.000000', 3, 'Laura', 'Antonia', '+551144445555');
INSERT INTO `Alunos` (`Id`, `Ativo`, `DataFimMatricula`, `DataInicioMatricula`, `DataNascimento`, `Matricula`, `Nome`, `Sobrenome`, `Telefone`)
VALUES (4, True, NULL, '2020-10-12 20:28:32.174151', '1998-04-30 00:00:00.000000', 4, 'Luiza', 'Maria', '+551155556666');
INSERT INTO `Alunos` (`Id`, `Ativo`, `DataFimMatricula`, `DataInicioMatricula`, `DataNascimento`, `Matricula`, `Nome`, `Sobrenome`, `Telefone`)
VALUES (5, True, NULL, '2020-10-12 20:28:32.174152', '2001-02-15 00:00:00.000000', 5, 'Lucas', 'Machado', '+551166667777');
INSERT INTO `Alunos` (`Id`, `Ativo`, `DataFimMatricula`, `DataInicioMatricula`, `DataNascimento`, `Matricula`, `Nome`, `Sobrenome`, `Telefone`)
VALUES (6, True, NULL, '2020-10-12 20:28:32.174152', '2003-10-12 00:00:00.000000', 6, 'Pedro', 'Alvares', '+551177778888');
INSERT INTO `Alunos` (`Id`, `Ativo`, `DataFimMatricula`, `DataInicioMatricula`, `DataNascimento`, `Matricula`, `Nome`, `Sobrenome`, `Telefone`)
VALUES (7, True, NULL, '2020-10-12 20:28:32.174153', '2005-12-02 00:00:00.000000', 7, 'Paulo', 'José', '+551188889999');

INSERT INTO `Cursos` (`Id`, `Nome`)
VALUES (1, 'Tecnologia da Informação');
INSERT INTO `Cursos` (`Id`, `Nome`)
VALUES (2, 'Sistemas de Informação');
INSERT INTO `Cursos` (`Id`, `Nome`)
VALUES (3, 'Ciência da Computação');

INSERT INTO `Professores` (`Id`, `Ativo`, `DataFimRegistro`, `DataInicioRegistro`, `Nome`, `Registro`, `Sobrenome`, `Telefone`)
VALUES (1, True, NULL, '2020-10-12 20:28:32.175094', 'Lauro', 1, 'Oliveira', NULL);
INSERT INTO `Professores` (`Id`, `Ativo`, `DataFimRegistro`, `DataInicioRegistro`, `Nome`, `Registro`, `Sobrenome`, `Telefone`)
VALUES (2, True, NULL, '2020-10-12 20:28:32.175199', 'Roberto', 2, 'Soares', NULL);
INSERT INTO `Professores` (`Id`, `Ativo`, `DataFimRegistro`, `DataInicioRegistro`, `Nome`, `Registro`, `Sobrenome`, `Telefone`)
VALUES (3, True, NULL, '2020-10-12 20:28:32.175202', 'Ronaldo', 3, 'Marconi', NULL);
INSERT INTO `Professores` (`Id`, `Ativo`, `DataFimRegistro`, `DataInicioRegistro`, `Nome`, `Registro`, `Sobrenome`, `Telefone`)
VALUES (4, True, NULL, '2020-10-12 20:28:32.175203', 'Rodrigo', 4, 'Carvalho', NULL);
INSERT INTO `Professores` (`Id`, `Ativo`, `DataFimRegistro`, `DataInicioRegistro`, `Nome`, `Registro`, `Sobrenome`, `Telefone`)
VALUES (5, True, NULL, '2020-10-12 20:28:32.175203', 'Alexandre', 5, 'Montanha', NULL);

INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (1, 1, NULL, '2020-10-12 20:28:32.175809');
INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (7, 3, NULL, '2020-10-12 20:28:32.175860');
INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (6, 3, NULL, '2020-10-12 20:28:32.175860');
INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (5, 3, NULL, '2020-10-12 20:28:32.175859');
INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (4, 3, NULL, '2020-10-12 20:28:32.175859');
INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (3, 3, NULL, '2020-10-12 20:28:32.175859');
INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (2, 3, NULL, '2020-10-12 20:28:32.175859');
INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (7, 2, NULL, '2020-10-12 20:28:32.175859');
INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (6, 2, NULL, '2020-10-12 20:28:32.175859');
INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (5, 2, NULL, '2020-10-12 20:28:32.175859');
INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (1, 3, NULL, '2020-10-12 20:28:32.175859');
INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (3, 2, NULL, '2020-10-12 20:28:32.175858');
INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (4, 2, NULL, '2020-10-12 20:28:32.175859');
INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (3, 1, NULL, '2020-10-12 20:28:32.175858');
INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (4, 1, NULL, '2020-10-12 20:28:32.175858');
INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (5, 1, NULL, '2020-10-12 20:28:32.175858');
INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (2, 1, NULL, '2020-10-12 20:28:32.175856');
INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (7, 1, NULL, '2020-10-12 20:28:32.175858');
INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (1, 2, NULL, '2020-10-12 20:28:32.175858');
INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (2, 2, NULL, '2020-10-12 20:28:32.175858');
INSERT INTO `AlunosCursos` (`AlunoId`, `CursoId`, `DataFim`, `DataInicio`)
VALUES (6, 1, NULL, '2020-10-12 20:28:32.175858');

INSERT INTO `Disciplinas` (`Id`, `CargaHoraria`, `CursoId`, `Nome`, `PreRequisitoId`, `ProfessorId`)
VALUES (9, 420, 2, 'Programação', NULL, 5);
INSERT INTO `Disciplinas` (`Id`, `CargaHoraria`, `CursoId`, `Nome`, `PreRequisitoId`, `ProfessorId`)
VALUES (1, 200, 1, 'Matemática', NULL, 1);
INSERT INTO `Disciplinas` (`Id`, `CargaHoraria`, `CursoId`, `Nome`, `PreRequisitoId`, `ProfessorId`)
VALUES (2, 400, 3, 'Matemática', NULL, 1);
INSERT INTO `Disciplinas` (`Id`, `CargaHoraria`, `CursoId`, `Nome`, `PreRequisitoId`, `ProfessorId`)
VALUES (4, 100, 1, 'Português', NULL, 3);
INSERT INTO `Disciplinas` (`Id`, `CargaHoraria`, `CursoId`, `Nome`, `PreRequisitoId`, `ProfessorId`)
VALUES (5, 150, 1, 'Inglês', NULL, 4);
INSERT INTO `Disciplinas` (`Id`, `CargaHoraria`, `CursoId`, `Nome`, `PreRequisitoId`, `ProfessorId`)
VALUES (6, 150, 2, 'Inglês', NULL, 4);
INSERT INTO `Disciplinas` (`Id`, `CargaHoraria`, `CursoId`, `Nome`, `PreRequisitoId`, `ProfessorId`)
VALUES (7, 250, 3, 'Inglês', NULL, 4);
INSERT INTO `Disciplinas` (`Id`, `CargaHoraria`, `CursoId`, `Nome`, `PreRequisitoId`, `ProfessorId`)
VALUES (8, 480, 1, 'Programação', NULL, 5);
INSERT INTO `Disciplinas` (`Id`, `CargaHoraria`, `CursoId`, `Nome`, `PreRequisitoId`, `ProfessorId`)
VALUES (10, 420, 3, 'Programação', NULL, 5);

INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (2, 1, NULL, '2020-10-12 20:28:32.175715', NULL);
INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (4, 5, NULL, '2020-10-12 20:28:32.175716', NULL);
INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (2, 5, NULL, '2020-10-12 20:28:32.175715', NULL);
INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (1, 5, NULL, '2020-10-12 20:28:32.175714', NULL);
INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (7, 4, NULL, '2020-10-12 20:28:32.175717', NULL);
INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (6, 4, NULL, '2020-10-12 20:28:32.175717', NULL);
INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (5, 4, NULL, '2020-10-12 20:28:32.175716', NULL);
INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (4, 4, NULL, '2020-10-12 20:28:32.175716', NULL);
INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (1, 4, NULL, '2020-10-12 20:28:32.175713', NULL);
INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (5, 5, NULL, '2020-10-12 20:28:32.175716', NULL);
INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (7, 5, NULL, '2020-10-12 20:28:32.175717', NULL);
INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (6, 2, NULL, '2020-10-12 20:28:32.175716', NULL);
INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (3, 2, NULL, '2020-10-12 20:28:32.175715', NULL);
INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (2, 2, NULL, '2020-10-12 20:28:32.175715', NULL);
INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (1, 2, NULL, '2020-10-12 20:28:32.175663', NULL);
INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (7, 1, NULL, '2020-10-12 20:28:32.175717', NULL);
INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (6, 1, NULL, '2020-10-12 20:28:32.175716', NULL);
INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (4, 1, NULL, '2020-10-12 20:28:32.175716', NULL);
INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (3, 1, NULL, '2020-10-12 20:28:32.175715', NULL);
INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (7, 2, NULL, '2020-10-12 20:28:32.175717', NULL);

INSERT INTO `Disciplinas` (`Id`, `CargaHoraria`, `CursoId`, `Nome`, `PreRequisitoId`, `ProfessorId`)
VALUES (3, 450, 3, 'Física', 2, 2);

INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (3, 3, NULL, '2020-10-12 20:28:32.175715', NULL);

INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (6, 3, NULL, '2020-10-12 20:28:32.175717', NULL);

INSERT INTO `AlunosDisciplinas` (`AlunoId`, `DisciplinaId`, `DataFim`, `DataInicio`, `Nota`)
VALUES (7, 3, NULL, '2020-10-12 20:28:32.175717', NULL);

CREATE INDEX `IX_AlunosCursos_CursoId` ON `AlunosCursos` (`CursoId`);

CREATE INDEX `IX_AlunosDisciplinas_DisciplinaId` ON `AlunosDisciplinas` (`DisciplinaId`);

CREATE INDEX `IX_Disciplinas_CursoId` ON `Disciplinas` (`CursoId`);

CREATE INDEX `IX_Disciplinas_PreRequisitoId` ON `Disciplinas` (`PreRequisitoId`);

CREATE INDEX `IX_Disciplinas_ProfessorId` ON `Disciplinas` (`ProfessorId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20201012232832_InitialConfiguration', '3.1.1');
