SELECT Surname	,[Name] ,MiddleName ,SUM([Amount]) AS '�����' ,Sum([Amount])*100/100000 AS '��������'
FROM Student,Payments
WHERE Student.StudentId = Payments.StudentId
GROUP BY MiddleName, [Name], Surname
