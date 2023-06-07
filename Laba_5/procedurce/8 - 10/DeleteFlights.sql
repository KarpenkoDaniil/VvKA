CREATE PROCEDURE DeleteFlights(@FlightID int)
AS
DELETE FROM Flights
WHERE FlightID = @FlightID