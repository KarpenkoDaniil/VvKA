SELECT Surname, Student.[Name], MiddleName, Streets.[Name], Town.Names
FROM Student, Town, Streets
WHERE Student.StreetId = Streets.StreetId AND Streets.TownsId = Town.TownsId