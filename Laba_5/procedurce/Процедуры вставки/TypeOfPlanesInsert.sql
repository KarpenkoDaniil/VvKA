CREATE PROCEDURE TypeOfPlanesInsert(@NameOfType nchar(30), @Appointments nchar(500), @Limitations nchar(500))
AS
INSERT INTO TypeOfPlanes(NameOfType, Appointments, Limitations)
VALUES (@NameOfType, @Appointments, @Limitations)