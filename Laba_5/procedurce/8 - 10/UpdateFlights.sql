CREATE PROCEDURE UpdateFlights(@FlightID int, @DateOfFlight date,@ArivalDispatchID int,@PlaneID int, @FlightTime float, @PostID int, @EmployesID int)
AS
UPDATE Flights 
SET DateOfFlight = @DateOfFlight, PlaneID = @PlaneID, FlightTime = @FlightTime, PostID = @PostID, EmployesID = @EmployesID, ArivalDispatchID = @ArivalDispatchID
WHERE @FlightID = FlightID