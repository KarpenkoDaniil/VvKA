CREATE TRIGGER dmlTrigerAfter
ON Student AFTER INSERT
AS
SELECT 'Incorrect date' as 'ALERT', inserted.[Name], inserted.Surname, inserted.MiddleName, inserted.AdmissionYear
FROM inserted
WHERE inserted.AdmissionYear > 2022



