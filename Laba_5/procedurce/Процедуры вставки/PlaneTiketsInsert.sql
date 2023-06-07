CREATE PROCEDURE InsertPlaneTickets(@Surname nchar(30), @Name nchar(30), @Medellname nchar(30), @PasportData nchar(40), @FlightID int, @Price money, @PlaceInPlane int)
AS
INSERT INTO PlaneTickets(Surname, [Name], Midllename, PasportData, FlightID, Price, PlaceInPlane)
VALUES (@Surname, @Name, @Medellname, @PasportData, @FlightID, @Price, @PlaceInPlane)