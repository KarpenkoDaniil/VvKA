CREATE PROCEDURE cursr_DIMP(@TownID INT, @TownName VARCHAR(20))
AS
DECLARE @showTown nchar(20)
	DECLARE curD CURSOR DYNAMIC SCROLL FOR
		SELECT *
			FROM Town
			WHERE Town.TownsId = @TownID

OPEN curD
FETCH NEXT FROM curD
WHILE @@FETCH_STATUS=0 
BEGIN
	    IF @TownName = 'Гомель'
       DELETE Town
       WHERE CURRENT OF curD
    ELSE
 
	SELECT @showTown = 'Город: '
PRINT @showTown

FETCH NEXT FROM curD
END
CLOSE curD
DEALLOCATE curD
