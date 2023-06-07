SELECT Surname, [Name], MiddleName, sum(Payments.Amount) AS 'ясллю'
FROM Student, Payments
WHERE Student.StudentId = Payments.StudentId
GROUP BY MiddleName, [Name], Surname