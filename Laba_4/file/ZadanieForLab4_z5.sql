CREATE TRIGGER dmlTrigerInstead
ON Student INSTEAD OF DELETE
AS
BEGIN 
	SELECT COUNT(*) FROM Payments, Student WHERE Payments.StudentId = Student.StudentId 
	IF (@@ROWCOUNT > 0)
	BEGIN
	BEGIN TRANSACTION Payments
		ROLLBACK TRANSACTION  
		PRINT 'Отмена удаления, так как имеются связанные строки в таблице Платежи'
	END
	ELSE
		COMMIT TRANSACTION
		DELETE  Student
		FROM Student
			LEFT JOIN Payments ON Student.StudentId = Payments.StudentId
		WHERE Payments.StudentId = Student.StudentId
END