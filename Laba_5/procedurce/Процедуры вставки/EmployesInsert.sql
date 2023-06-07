CREATE PROCEDURE InsertInformationEmployes(@Name nchar(20),
											@Surname nchar(20), @Midellname nchar(20),
											@Age int, @PasportData nchar(30), @PostID int)
AS
INSERT INTO Employees([Name], Surname, Midllename, Age, PassportData, PostID)
VALUES (@Name, @Surname, @Midellname, @Age, @PasportData, @PostID)