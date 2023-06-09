USE [Karpenko]
GO
/****** Object:  StoredProcedure [dbo].[summ]    Script Date: 25.03.2023 16:01:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[summ](@money money = 2000, @faclty nchar(20) = 'AST')
AS
SELECT [Name], Surname, MiddleName, SUM(Payments.Amount) AS SUMM
FROM Student, Payments, Faculty
GROUP BY Student.StudentId, Payments.StudentId, [Name], Surname, MiddleName
HAVING Payments.StudentId = Student.StudentId AND @money > SUM(Payments.Amount)