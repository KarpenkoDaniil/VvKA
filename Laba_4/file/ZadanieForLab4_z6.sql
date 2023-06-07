CREATE TRIGGER trg_prevent_alter_views
ON DATABASE
FOR ALTER_VIEW
AS
BEGIN
    RAISERROR('��� �� ����������� �������� ������������� � ���� ���� ������!', 16, 1)
    ROLLBACK;
END;