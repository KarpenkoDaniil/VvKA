USE Karpenko

SELECT Surname, Student.[Name], MiddleName, HouseNumber,
ApartmentNumber, PhoneNumber, BirthDate, AdmissionYear, 
GroupName, Note, sum(Payments.Amount)
FROM Student, Payments, Town, Streets
WHERE Town.Names = '������' AND Town.TownsId =	Streets.TownsId AND Student.StreetId = Streets.StreetId
GROUP BY Surname, Student.[Name], MiddleName, HouseNumber, ApartmentNumber, PhoneNumber, BirthDate, AdmissionYear, GroupName, Note

SELECT Surname, Student.[Name], MiddleName, HouseNumber,
ApartmentNumber, PhoneNumber, BirthDate, AdmissionYear, 
GroupName, Note, sum(Payments.Amount)
FROM Student, Payments, Town, Streets
WHERE Student.[Name] = '������' AND Student.Surname = '��������' AND Student.MiddleName = '����������' AND Payments.StudentId = Student.StudentId
GROUP BY Surname, Student.[Name], MiddleName, HouseNumber, ApartmentNumber, PhoneNumber, BirthDate, AdmissionYear, GroupName, Note

SELECT Surname, Student.[Name], MiddleName, HouseNumber,
ApartmentNumber, PhoneNumber, BirthDate, AdmissionYear, 
GroupName, Note, sum(Payments.Amount)
FROM Student, Payments, Town, Streets
WHERE 2000 < AdmissionYear AND AdmissionYear < 2020 AND Payments.StudentId = Student.StudentId
GROUP BY Surname, Student.[Name], MiddleName, HouseNumber, ApartmentNumber, PhoneNumber, BirthDate, AdmissionYear, GroupName, Note
