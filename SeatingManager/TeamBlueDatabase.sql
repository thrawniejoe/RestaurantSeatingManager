CREATE DATABASE  IF NOT EXISTS `teamblue` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `teamblue`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: teamblue
-- ------------------------------------------------------
-- Server version	5.6.28-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `assigncustomer`
--

DROP TABLE IF EXISTS `assigncustomer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `assigncustomer` (
  `tableAssignID` int(11) NOT NULL AUTO_INCREMENT,
  `tableID` int(11) NOT NULL,
  `customerID` int(11) NOT NULL,
  `assignTime` datetime NOT NULL,
  `isAssigned` tinyint(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`tableAssignID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `assigncustomer`
--

LOCK TABLES `assigncustomer` WRITE;
/*!40000 ALTER TABLE `assigncustomer` DISABLE KEYS */;
/*!40000 ALTER TABLE `assigncustomer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customer`
--

DROP TABLE IF EXISTS `customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `customer` (
  `customerID` int(11) NOT NULL AUTO_INCREMENT,
  `customerName` varchar(25) NOT NULL,
  `wait` tinyint(4) NOT NULL DEFAULT '0',
  `reservation` tinyint(4) NOT NULL DEFAULT '0',
  `timeIn` datetime NOT NULL,
  `timeMade` datetime NOT NULL,
  PRIMARY KEY (`customerID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customer`
--

LOCK TABLES `customer` WRITE;
/*!40000 ALTER TABLE `customer` DISABLE KEYS */;
/*!40000 ALTER TABLE `customer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tablemap`
--

DROP TABLE IF EXISTS `tablemap`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tablemap` (
  `tableID` int(11) NOT NULL AUTO_INCREMENT,
  `tableX` int(11) DEFAULT NULL,
  `tableY` int(11) DEFAULT NULL,
  `numberOfSeats` int(11) NOT NULL,
  `visible` tinyint(4) NOT NULL DEFAULT '1',
  `sectionID` int(11) NOT NULL,
  `tableType` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`tableID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tablemap`
--

LOCK TABLES `tablemap` WRITE;
/*!40000 ALTER TABLE `tablemap` DISABLE KEYS */;
/*!40000 ALTER TABLE `tablemap` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tablemerge`
--

DROP TABLE IF EXISTS `tablemerge`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tablemerge` (
  `tableMergeID` int(11) NOT NULL AUTO_INCREMENT,
  `tableID1` int(11) NOT NULL,
  `tableID2` int(11) NOT NULL,
  `tableID3` int(11) DEFAULT NULL,
  `tableID4` int(11) DEFAULT NULL,
  `visible` tinyint(4) NOT NULL DEFAULT '0',
  `isMerged` tinyint(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`tableMergeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tablemerge`
--

LOCK TABLES `tablemerge` WRITE;
/*!40000 ALTER TABLE `tablemerge` DISABLE KEYS */;
/*!40000 ALTER TABLE `tablemerge` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tablesection`
--

DROP TABLE IF EXISTS `tablesection`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tablesection` (
  `tableSectionID` int(11) NOT NULL AUTO_INCREMENT,
  `sectionColor` varchar(15) NOT NULL,
  PRIMARY KEY (`tableSectionID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tablesection`
--

LOCK TABLES `tablesection` WRITE;
/*!40000 ALTER TABLE `tablesection` DISABLE KEYS */;
/*!40000 ALTER TABLE `tablesection` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `userID` int(11) NOT NULL AUTO_INCREMENT,
  `firstName` varchar(25) NOT NULL,
  `lastName` varchar(25) NOT NULL,
  `phone` varchar(15) DEFAULT NULL,
  `role` int(11) NOT NULL,
  `isActive` tinyint(4) NOT NULL DEFAULT '0',
  `isOnDuty` tinyint(4) NOT NULL DEFAULT '0',
  `sectionID` int(11) NOT NULL,
  `password` varchar(45) NOT NULL,
  `title` varchar(15) NOT NULL,
  `dateHired` date NOT NULL,
  PRIMARY KEY (`userID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'teamblue'
--

--
-- Dumping routines for database 'teamblue'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-08-24 16:41:00
