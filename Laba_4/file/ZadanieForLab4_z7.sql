CREATE PROCEDURE cursr_D(@TownID int)
AS 
	DECLARE @showTown nchar(20)
	DECLARE curD CURSOR DYNAMIC SCROLL FOR
		SELECT *
			FROM Town
			WHERE Town.TownsId = @TownID


OPEN curD
	FETCH NEXT FROM curD
		WHILE @@FETCH_STATUS = 0
		BEGIN
			PRINT 'Employee name: '
			FETCH NEXT from curD
		END
	CLOSE curD
DEALLOCATE curD
