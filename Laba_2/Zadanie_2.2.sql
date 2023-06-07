SELECT SUM(Payments.Amount) as 'ясллю он цпсооюл', Student.GroupName
FROM Student INNER JOIN 
	Payments on Student.StudentId = Payments.StudentId
GROUP BY Student.GroupName