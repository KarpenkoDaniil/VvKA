CREATE FUNCTION CostOfSalles(@TownId INT)
RETURNS DECIMAL(18,0)
AS
BEGIN 
DECLARE @SUM DECIMAL
SET @SUM = (
	SELECT SUM(Payments.Amount)
	FROM Student, Town, Payments, Streets
	WHERE Town.TownsId = Streets.TownsId AND Streets.StreetId = Student.StreetId AND Payments.StudentId = Student.StudentId)
	RETURN @SUM
END

