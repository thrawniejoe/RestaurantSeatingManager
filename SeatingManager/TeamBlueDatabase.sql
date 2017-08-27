USE master;
IF EXISTS(select * from sys.databases where name='SeatingManager')
DROP DATABASE SeatingManager;

CREATE DATABASE SeatingManager

USE SeatingManager;
GO
DROP TABLE IF EXISTS assigncustomer;
GO

CREATE TABLE assigncustomer (
  tableAssignID int NOT NULL IDENTITY(1,1),
  tableID int NOT NULL,
  customerID int NOT NULL,
  assignTime datetime NOT NULL,
  isAssigned tinyint NOT NULL DEFAULT 0,
  PRIMARY KEY (tableAssignID)
);

GO
DROP TABLE IF EXISTS customer;
GO

CREATE TABLE customer (
  customerID int NOT NULL IDENTITY(1,1),
  customerName nvarchar(25) NOT NULL,
  wait tinyint NOT NULL DEFAULT 0,
  reservation tinyint NOT NULL DEFAULT 0,
  timeIn datetime NOT NULL,
  timeMade datetime NOT NULL,
  PRIMARY KEY (customerID)
);
GO
DROP TABLE IF EXISTS tablemap;
GO


CREATE TABLE tablemap (
  tableID int NOT NULL IDENTITY(1,1),
  tableX int DEFAULT NULL,
  tableY int DEFAULT NULL,
  numberOfSeats int NOT NULL,
  visible tinyint NOT NULL DEFAULT 1,
  sectionID int NOT NULL,
  tableType int NOT NULL DEFAULT 1,
  PRIMARY KEY (tableID)
);


GO
DROP TABLE IF EXISTS tablemerge;
GO

CREATE TABLE tablemerge (
  tableMergeID int NOT NULL IDENTITY(1,1),
  tableID1 int NOT NULL,
  tableID2 int NOT NULL,
  tableID3 int DEFAULT NULL,
  tableID4 int DEFAULT NULL,
  visible tinyint NOT NULL DEFAULT 0,
  isMerged tinyint NOT NULL DEFAULT 0,
  PRIMARY KEY (tableMergeID)
);

GO
DROP TABLE IF EXISTS tablesection;
GO


CREATE TABLE tablesection (
  tableSectionID int NOT NULL IDENTITY(1,1),
  sectionColor nvarchar(15) NOT NULL,
  PRIMARY KEY (tableSectionID)
);



GO
DROP TABLE IF EXISTS users;
 GO

CREATE TABLE users (
  userID int NOT NULL IDENTITY(1,1),
  firstName nvarchar(25) NOT NULL,
  lastName nvarchar(25) NOT NULL,
  phone nvarchar(15) DEFAULT NULL,
  role int NOT NULL,
  isActive tinyint NOT NULL DEFAULT 0,
  isOnDuty tinyint NOT NULL DEFAULT 0,
  sectionID int,
  password nvarchar(45) NOT NULL,
  title nvarchar(15) NOT NULL,
  dateHired date NOT NULL,
  PRIMARY KEY (userID)
);
