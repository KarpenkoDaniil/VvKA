SELECT SUM(Payments.Amount) as '����� �� �������', Student.GroupName
FROM Student INNER JOIN 
	Payments on Student.StudentId = Payments.StudentId
GROUP BY Student.GroupName