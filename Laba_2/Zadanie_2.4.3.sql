DECLARE @group1 VARCHAR(20), @group2  VARCHAR(20), @sum1 int, @sum2 int
SET @group1 = 'ITI'
SET @group2 = 'IP'

SELECT @sum1 = SUM(Amount)
FROM Student, Payments
WHERE Student.GroupName = @group1 AND Student.StudentId = Payments.StudentId

SELECT @sum2 = SUM(Amount)
FROM Student, Payments
WHERE Student.GroupName = @group2 AND Student.StudentId = Payments.StudentId

SELECT abs(@sum1 - @sum2) AS '–¿«Õ»÷¿'