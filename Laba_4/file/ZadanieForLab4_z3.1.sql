USE [Karpenko]
GO
/****** Object:  UserDefinedFunction [dbo].[StudentInfo]    Script Date: 25.03.2023 13:16:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[StudentInfo](@dateFirst date, @dateLast date)
RETURNS @new TABLE (FirstName nchar(20), Surname nchar(20), Midlename nchar(20), faculty nchar(10))
AS
BEGIN 
DECLARE @table TABLE (FirstName nchar(20), Surname nchar(20), Midlename nchar(20), faculty nchar(30))
INSERT @table SELECT Student.[Name], Student.Surname, Student.MiddleName, Faculty.ShortNames
	FROM Student,Faculty
	WHERE DATEPART(YYYY, @dateFirst) - DATEPART(YYYY, Student.BirthDate) = 18 AND DATEPART(MM, @dateFirst) < DATEPART(MM, Student.BirthDate) 
		AND DATEPART(MM, @dateLast) > DATEPART(MM, Student.BirthDate) AND Student.FacultysId = Faculty.FacultysId
	INSERT @new SELECT FirstName, Surname, Midlename, faculty
	FROM @table
RETURN
END
