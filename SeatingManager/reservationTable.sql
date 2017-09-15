CREATE TABLE [dbo].Reservation
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ReservationDateTime] DATETIME NOT NULL, 
    [CustomerID] INT NOT NULL,
	CONSTRAINT fk_reservation_customers FOREIGN KEY (CustomerID)
	REFERENCES customers(customerID)
)