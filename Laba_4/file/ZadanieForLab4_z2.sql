CREATE FUNCTION phoneNumbers(@firstDate date, @lastDate date)
RETURNS TABLE
AS 
RETURN (SELECT Payments.PaymentDate, Student.PhoneNumber
		FROM Payments, Student
		WHERE Payments.PaymentDate >= @firstDate AND Payments.PaymentDate <= @lastDate
		GROUP BY PaymentDate, Student.PhoneNumber)