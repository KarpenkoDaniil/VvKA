SELECT GroupName, SUM(Amount) AS '����� ������ ������', Student.[Name], Student.Surname, Student.MiddleName
FROM Faculty, Student, Payments
GROUP BY GroupName, Student.[Name] , Student.Surname, Student.MiddleName