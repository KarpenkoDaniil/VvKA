CREATE PROCEDURE PlanesInsert(@PlaneName nchar(40), @PasangerCapacity int, @LoadCapacity float, @TypePlaneID int, @TechnicalSpecification nchar(200),
							  @DateOfCreate date, @FlightHours float, @DateOfLastReparing date)
AS
INSERT INTO Planes (PlaneName, PasangerCapacity, LoadCapacity, TypePlaneID, TechnicalSpecification, DateOfCreate, FlightHours, DateOfLastReparing)
VALUES (@PlaneName, @PasangerCapacity, @LoadCapacity, @TypePlaneID, @TechnicalSpecification, @DateOfCreate, @FlightHours, @DateOfLastReparing)