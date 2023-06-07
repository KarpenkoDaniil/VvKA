SELECT Surname	,[Name] ,MiddleName ,SUM([Amount]) AS 'ясллю' ,Sum([Amount])*100/100000 AS 'опнжемрш'
FROM Student,Payments
WHERE Student.StudentId = Payments.StudentId
GROUP BY MiddleName, [Name], Surname
