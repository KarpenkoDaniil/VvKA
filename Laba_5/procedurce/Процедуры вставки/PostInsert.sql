CREATE PROCEDURE InsertPost(@NameOfPost nchar(20), @Salary money, @Responsibilities nchar(500), @Requirements nchar(500))
AS
INSERT INTO Posts(PostName, Salary, Responsibilities, Requirements)
VALUES (@NameOfPost, @Salary, @Responsibilities, @Requirements)