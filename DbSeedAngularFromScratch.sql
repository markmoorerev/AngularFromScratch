SELECT * FROM Users;

SET IDENTITY_INSERT Users ON;

INSERT INTO Users (Id, UserName)
VALUES (1, 'Jim');

INSERT INTO Users (Id, UserName)
VALUES (2, 'Mark');

INSERT INTO Users (Id, UserName)
VALUES (3, 'Arely');

INSERT INTO Users (Id, UserName)
VALUES (4, 'Maya');

INSERT INTO Users (Id, UserName)
VALUES (5, 'Ethan');

INSERT INTO Users (Id, UserName)
VALUES (6, 'Hope');

SET IDENTITY_INSERT Users OFF;
