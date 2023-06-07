
DELETE Payments
	FROM Payments 
		LEFT JOIN Student ON Student.StudentId = Payments.StudentId
		WHERE Payments.PaymentDate < '2023-01-01' 


SELECT Student.[Name], Student.Surname, Student.GroupName, Payments.PaymentDate
FROM Student, Payments
WHERE Student.StudentId = Payments.StudentId
