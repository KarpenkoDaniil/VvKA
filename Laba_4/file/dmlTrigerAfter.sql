USE [Karpenko]
GO
/****** Object:  Trigger [dbo].[dmlSender]    Script Date: 25.03.2023 16:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TRIGGER [dbo].[dmlSender]
ON [dbo].[Student] AFTER INSERT
AS
SELECT inserted.AdmissionYear, 'Incorrect date' as 'ALERT', inserted.[Name], inserted.Surname, inserted.MiddleName, inserted.AdmissionYear
FROM Student, deleted, inserted
WHERE inserted.AdmissionYear < 1991 OR inserted.AdmissionYear > 2023 
