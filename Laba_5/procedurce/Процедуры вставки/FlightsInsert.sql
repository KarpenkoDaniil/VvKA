CREATE PROCEDURE FlightsInsert(@DateOfFlight date, @ArivalDispatchID int, @PlaneID int, @FlightTime float, @PostID int)
AS
INSERT INTO Flights (DateOfFlight, ArivalDispatchID,PlaneID, FlightTime, PostID)
VALUES (@DateOfFlight, @ArivalDispatchID, @PlaneID, @FlightTime, @PostID)