SELECT Payments.StudentId, Surname, [Name], MiddleName, PaymentDate, Amount, PurposeId
FROM Student, Payments 
where Student.StudentId = Payments.StudentId
