CREATE TRIGGER trg_prevent_alter_views
ON DATABASE
FOR ALTER_VIEW
AS
BEGIN
    RAISERROR('Вам не разрешается изменять представления в этой базе данных!', 16, 1)
    ROLLBACK;
END;